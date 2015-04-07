using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    public class PatrolTrackHandler:DBase
    {
        private PatrolTrackHandler() { }
        private static PatrolTrackHandler _instance;
        public static PatrolTrackHandler Handler
        {
            get { return _instance = _instance ?? new PatrolTrackHandler(); }
        }

        public override int InsertEntity(object obj)
        {
            var e = obj as Model.PatrolTrack;
            if (e == null) 
                return 0;

            return InsertHandler.Into<Model.PatrolTrack>()
                .Table("Name", "SettedTime", "UpdatedTime")
                .Values(e.Name, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                .Execute()
                .ExecuteNonQuery();
        }

        public override int UpdateEntity(object obj)
        {
            var e = obj as Model.PatrolTrack;
            if (e == null) 
                return 0;

            return UpdateHandler.Table<Model.PatrolTrack>()
                .Set("UpdatedTime").EqualTo(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                .Set("Name").EqualTo(e.Name)
                .Where<Model.PatrolTrack>(t => t.ID == e.ID)
                .Execute()
                .ExecuteNonQuery();
        }

        public override int DeleteEntities(params string[] ids)
        {
            var rst = -1;
            using (var tran = new System.Transactions.TransactionScope())
            {
                try
                {
                    rst = DeleteHandler.From<Model.PatrolMonitor>()
                        .Where<Model.PatrolMonitor>(t => t.TrackID.In(ids))
                        .Execute()
                        .ExecuteNonQuery();

                    rst += DeleteHandler.From<Model.PatrolTrack>()
                        .Where<Model.PatrolTrack>(t => t.ID.In(ids))
                        .Execute()
                        .ExecuteNonQuery();

                    tran.Complete();
                }
                catch (Exception e)
                {
                    rst = -1;
                }
            }
            return rst;
            
        }

        public int AddTrackDevices(int trackid, string deviceids)
        {
            if (trackid == 0) return -2;
            if (string.IsNullOrWhiteSpace(deviceids)) return -2;

            var ret = -1;
            using (var tran = new System.Transactions.TransactionScope())
            {
                try
                {
                    if ((ret = DeleteHandler.From<Model.PatrolMonitor>().Where<Model.PatrolMonitor>(t => t.TrackID == trackid).Execute().ExecuteNonQuery()) >= 0)
                    {
                        var tickcount = 0;
                        deviceids.Split(',').FirstOrDefault(t =>
                            {
                                ret += InsertHandler.Into<Model.PatrolMonitor>()
                                    .Table("TrackID", "MonitorID", "Sort")
                                    .Values(trackid, t, ++tickcount)
                                    .Execute()
                                    .ExecuteNonQuery();
                                return false;
                            });
                        tran.Complete();
                    }
                }
                catch (Exception e) { ret = -1; }
            }
            return ret;
        }

        public List<Model.PatrolTrack> Page(int index, int size, out int records)
        {
            var list = Paging<Model.PatrolTrack>(index, size, null, OrderType.Desc, out records, string.Format("{0}.ID", GetTableName<Model.PatrolTrack>()));
            var ids = (from t in list select t.ID.ToString()).ToArray();
            var devices = GetDevicesOnTrack(ids);
            list.ForEach(t => t.Add(devices));
            return list;
        }

        public List<Model.PatrolTrack> GetEntities()
        {
            var query = SelectHandler.From<Model.PatrolTrack>();
            return ExecuteList<Model.PatrolTrack>(query.Execute().ExecuteDataReader());
        }

        public List<Model.MonitorDeviceEx> GetDevicesOnTrack(int trackid)
        {
            return GetDevicesOnTrack(trackid.ToString());
        }

        public List<Model.MonitorDeviceEx> GetDevicesOnTrack(params string[] trackids)
        {
            var devicename = GetTableName<Model.MonitorDevice>();
            var patroltrackname = GetTableName<Model.PatrolMonitor>();

            var query = SelectHandler.From<Model.MonitorDevice>()
                .Columns(string.Format("{0}.*, {1}.TrackID", devicename, patroltrackname))
                .Join(JoinType.Inner, patroltrackname).On(string.Format("{0}.ID = {1}.MonitorID", devicename, patroltrackname))
                .Where<Model.PatrolMonitor>(t => t.TrackID.In(trackids))
                .OrderBy(OrderType.Asc, string.Format("{0}.Sort", patroltrackname));
            return ExecuteList<Model.MonitorDeviceEx>(query.Execute().ExecuteDataReader());
        }

        public int InsertEntity(Model.PatrolRecord e)
        {
            return InsertHandler.Into<Model.PatrolRecord>()
                .Table("CurrentTime", "DeviceID", "PatrolID", "Patrol", "PatrolMonitorID", "Remark")
                .Values(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), e.DeviceID, e.PatrolID, e.Patrol, e.PatrolMonitorID, e.Remark)
                .Execute()
                .ExecuteNonQuery();
        }

        public List<Model.PatrolRecord> Page(string devicename, string officername, DateTime? timestart, DateTime? timeend, int index, int size, out int records)
        {
            var x_recordname = GetTableName<Model.PatrolRecord>();
            //var x_trackdevicename = GetTableName<Model.PatrolMonitor>();
            //var x_trackname = GetTableName<Model.PatrolTrack>();
            var x_devicename = GetTableName<Model.MonitorDevice>();
            var x_officername = GetTableName<Model.Officer>();

            var query = GetPageQuery(x_recordname, "CurrentTime")
                //.Join(JoinType.Inner, x_trackdevicename).On(string.Format("{0}.ID = {1}.PatrolMonitorID", x_trackdevicename, x_recordname))
                .Join(JoinType.Left, x_devicename).On(string.Format("{0}.ID = {1}.DeviceID", x_devicename, x_recordname))
                .Join(JoinType.Left, x_officername).On(string.Format("{0}.ID = {1}.PatrolID", x_officername, x_recordname));

            if (!string.IsNullOrWhiteSpace(devicename))
            {
                query = query.Where<Model.MonitorDevice>(t => t.Name.Like(devicename));
            }

            if (!string.IsNullOrWhiteSpace(officername))
            {
                query = query.Where<Model.Officer>(t => t.Name.Like(officername));
            }

            query = MatchTime(timestart, timeend, query);

            var list =  Paging<Model.PatrolRecord>(query, index, size, out records);

            if (list.Count > 0)
                GetDeviceAndTrackAndOfficerOfRecords(ref list);
            
            return list;
        }

        private void GetDeviceAndTrackAndOfficerOfRecords(ref List<Model.PatrolRecord> list)
        {
            var officerids = new string[] { };
            var deviceids = new string[] { };
            var trackids = new string[] { };
            list.ForEach(t =>
            {
                officerids = officerids.Concat(new string[] { t.PatrolID.ToString() }).ToArray();
                deviceids = deviceids.Concat(new string[] { t.DeviceID.ToString() }).ToArray();
                trackids = trackids.Concat(new string[] { t.PatrolMonitorID.ToString() }).ToArray();
            });
            //获取警员信息
            var offcers = GetEntities<Model.Officer>(t => t.ID.In(officerids));
            //获取设备信息
            var devices = Dao.MonitorDeviceHandler.Handler.GetDevices(deviceids);
            //获取路线信息
            var tracks = GetEntities<Model.PatrolTrack>(t => t.ID.In(trackids));

            list.ForEach(t =>
            {
                t.Device = devices.FirstOrDefault(x => x.ID == t.DeviceID);
                t.Track = tracks.FirstOrDefault(x => x.ID == t.PatrolMonitorID);
                t.Officer = offcers.FirstOrDefault(x => x.ID == t.PatrolID);
            });
        }
        
        private IDao.ISelect MatchTime(DateTime? timestart, DateTime? timeend, IDao.ISelect query)
        {
            if (timestart == null && timeend == null)
            {
                return query;
            }

            var x_recordname = GetTableName<Model.PatrolRecord>();
            if (timestart == null)
            {
                return query.Where(string.Format("{0}.CurrentTime <= '{1}'", x_recordname, ((DateTime)timeend).ToString("yyyy-MM-dd HH:mm")));
            }

            if (timeend == null)
            {
                return query.Where(string.Format("{0}.CurrentTime >= '{1}'", x_recordname, ((DateTime)timestart).ToString("yyyy-MM-dd HH:mm")));
            }

            var timespan = ((DateTime)timeend - (DateTime)timestart).Ticks >= 0;
            if (timespan)
            {
                return query.Where(string.Format("{0}.CurrentTime >= '{1}' and {0}.CurrentTime <= '{2}'", x_recordname, ((DateTime)timestart).ToString("yyyy-MM-dd HH:mm"), ((DateTime)timeend).ToString("yyyy-MM-dd HH:mm")));
            }

            return query.Where(string.Format("{0}.CurrentTime >= '{1}' and {0}.CurrentTime <= '{2}'", x_recordname, ((DateTime)timeend).ToString("yyyy-MM-dd HH:mm"), ((DateTime)timestart).ToString("yyyy-MM-dd HH:mm")));
        }
    }
}
