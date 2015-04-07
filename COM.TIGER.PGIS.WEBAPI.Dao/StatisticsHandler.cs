using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    public class StatisticsHandler : DBase
    {
        private static StatisticsHandler _instance = null;
        public static StatisticsHandler Handler { get { return _instance = _instance ?? new StatisticsHandler(); } }
        private StatisticsHandler() { }

        public List<Model.CountCase> CountCase()
        {
            //var data = new List<Model.CountCase>() 
            //{
            //    new Model.CountCase(){ ID=0, Name="111", Mod=1, Records=1},
            //    new Model.CountCase(){ ID=0, Name="111", Mod=2, Records=2},
            //    new Model.CountCase(){ ID=1, Name="122", Mod=1, Records=4},
            //    new Model.CountCase(){ ID=1, Name="122", Mod=2, Records=2},
            //    new Model.CountCase(){ ID=2, Name="333", Mod=1, Records=1},
            //    new Model.CountCase(){ ID=2, Name="333", Mod=2, Records=4},
            //    new Model.CountCase(){ ID=3, Name="444", Mod=1, Records=8}
            //};

            var tname1 = GetTableName<Model.YJBJ>();
            var query1 = SelectHandler.Columns(string.Format("{0}.AdminID", tname1), string.Format("{0}.AdminName", tname1), "count(0) as Records", "1 as Mod")
                .From<Model.YJBJ>().GroupBy(string.Format("{0}.AdminID", tname1), string.Format("{0}.AdminName", tname1));

            var tname2 = GetTableName<Model.JCJ_JJDB>();
            var query2 = SelectHandler.Columns(string.Format("{0}.AdminID", tname2), string.Format("{0}.AdminName", tname2), "count(0) as Records", "2 as Mod")
                .From<Model.JCJ_JJDB>().GroupBy(string.Format("{0}.AdminID", tname2), string.Format("{0}.AdminName", tname2));

            var cmdstr = string.Format("{0} union all {1}", query1.CommandText, query2.CommandText);
            var data = ExecuteList<Model.CountCase>(query1.Execute().ExecuteDataReader(cmdstr));

            var admins = GetAdministratives();
            if (admins.Count == 0)
                return data;

            var list = new List<Model.CountCase>();
            admins.ForEach(x =>
            {
                for (var i = 1; i < 3; i++)
                {
                    var d = data.FirstOrDefault(t => t.ID == x.ID && t.Mod == i);
                    if (d != null)
                    {
                        list.Add(d);
                        continue;
                    }

                    Model.CountCase e = new Model.CountCase() { ID = x.ID, Mod = i, Name = x.Name, Records = 0 };
                    list.Add(e);
                }
            });

            list.AddRange(data.Where(t => !admins.Exists(x => x.ID == t.ID)));
            return list;
        }

        public List<Model.CountCompany> CountCompany()
        {
            //var data = new List<Model.CountCompany>() 
            //{
            //    new Model.CountCompany(){ID=0, Name="111", Records=1},
            //    new Model.CountCompany(){ID=1, Name="222", Records=2},
            //    new Model.CountCompany(){ID=2, Name="333", Records=1},
            //    new Model.CountCompany(){ID=3, Name="444", Records=5},
            //    new Model.CountCompany(){ID=4, Name="555", Records=18},
            //    new Model.CountCompany(){ID=5, Name="666", Records=0}
            //};

            var tname = GetTableName<Model.Company>();
            var aname = GetTableName<Model.Address>();
            var adname = GetTableName<Model.Administrative>();
            var query = SelectHandler.Columns(string.Format("{0}.ID, {0}.Name", adname), "count(0) as Records")
                .From<Model.Company>()
                .Join(JoinType.Left, aname).On(string.Format("{0}.AddressID = {1}.ID", tname, aname))
                .Join(JoinType.Left, adname).On(string.Format("{0}.AdminID = {1}.ID", aname, adname))
                .GroupBy(string.Format("{0}.ID, {0}.Name", adname));

            var data = ExecuteList<Model.CountCompany>(query.Execute().ExecuteDataReader());

            var admins = GetAdministratives();
            if (admins.Count == 0)
                return data;

            List<Model.CountCompany> list = new List<Model.CountCompany>();
            admins.ForEach(x =>
            {
                var d = data.FirstOrDefault(t => t.ID == x.ID);
                if (d != null)
                {
                    list.Add(d);
                    return;
                }

                Model.CountCompany e = new Model.CountCompany() { ID = x.ID, Name = x.Name, Records = 0 };
                list.Add(e);
            });

            list.AddRange(data.Where(t => !admins.Exists(x => x.ID == t.ID)));
            return list;
        }

        public List<Model.CountHotel> CountHotel()
        {
            //var data = new List<Model.CountHotel>()
            //{
            //    new Model.CountHotel(){ID=0, Name="111", Records=1},
            //    new Model.CountHotel(){ID=1, Name="222", Records=2},
            //    new Model.CountHotel(){ID=2, Name="333", Records=1},
            //    new Model.CountHotel(){ID=3, Name="444", Records=5},
            //    new Model.CountHotel(){ID=4, Name="555", Records=18},
            //    new Model.CountHotel(){ID=5, Name="666", Records=0}
            //};

            var tname = GetTableName<Model.Hotel>();
            var aname = GetTableName<Model.Address>();
            var adname = GetTableName<Model.Administrative>();
            var query = SelectHandler.Columns(string.Format("{0}.ID, {0}.Name", adname), "count(0) as Records")
                .From<Model.Hotel>()
                .Join(JoinType.Left, aname).On(string.Format("{0}.AddressID = {1}.ID", tname, aname))
                .Join(JoinType.Left, adname).On(string.Format("{0}.AdminID = {1}.ID", aname, adname))
                .GroupBy(string.Format("{0}.ID, {0}.Name", adname));

            var data = ExecuteList<Model.CountHotel>(query.Execute().ExecuteDataReader());

            var admins = GetAdministratives();
            if (admins.Count == 0)
                return data;

            List<Model.CountHotel> list = new List<Model.CountHotel>();
            admins.ForEach(x =>
            {
                var d = data.FirstOrDefault(t => t.ID == x.ID);
                if (d != null)
                {
                    list.Add(d);
                    return;
                }

                Model.CountHotel e = new Model.CountHotel() { ID = x.ID, Name = x.Name, Records = 0 };
                list.Add(e);
            });

            list.AddRange(data.Where(t => !admins.Exists(x => x.ID == t.ID)));
            return list;
        }

        public List<Model.CountMonitor> CountMonitor()
        {
            //var data = new List<Model.CountMonitor>()
            //{
            //    new Model.CountMonitor(){ID=0, Name="111", Records=1},
            //    new Model.CountMonitor(){ID=1, Name="222", Records=2},
            //    new Model.CountMonitor(){ID=2, Name="333", Records=1},
            //    new Model.CountMonitor(){ID=3, Name="444", Records=5},
            //    new Model.CountMonitor(){ID=4, Name="555", Records=18},
            //    new Model.CountMonitor(){ID=5, Name="666", Records=0}
            //};

            var tname = GetTableName<Model.MonitorDevice>();
            var aname = GetTableName<Model.Address>();
            var adname = GetTableName<Model.Administrative>();
            var query = SelectHandler.Columns(string.Format("{0}.ID, {0}.Name", adname), "count(0) as Records")
                .From<Model.MonitorDevice>()
                .Join(JoinType.Left, aname).On(string.Format("{0}.AddressID = {1}.ID", tname, aname))
                .Join(JoinType.Left, adname).On(string.Format("{0}.AdminID = {1}.ID", aname, adname))
                .GroupBy(string.Format("{0}.ID, {0}.Name", adname));

            var data = ExecuteList<Model.CountMonitor>(query.Execute().ExecuteDataReader());

            var admins = GetAdministratives();
            if (admins.Count == 0)
                return data;

            List<Model.CountMonitor> list = new List<Model.CountMonitor>();
            admins.ForEach(x =>
            {
                var d = data.FirstOrDefault(t => t.ID == x.ID);
                if (d != null)
                {
                    list.Add(d);
                    return;
                }

                Model.CountMonitor e = new Model.CountMonitor() { ID = x.ID, Name = x.Name, Records = 0 };
                list.Add(e);
            });

            list.AddRange(data.Where(t => !admins.Exists(x => x.ID == t.ID)));
            return list;
        }

        public List<Model.CountPopulation> CountPopulation()
        {
            //var data = new List<Model.CountPopulation>()
            //{ 
            //    new Model.CountPopulation(){ ID = 1, Name = "1", Records = 2, LiveTypeID = 1, LiveType = "111" },
            //    new Model.CountPopulation() { ID = 1, Name = "1", Records = 8, LiveTypeID = 2, LiveType = "222" },
            //    new Model.CountPopulation() { ID = 2, Name = "2", Records = 20, LiveTypeID = 1, LiveType = "111" },
            //    new Model.CountPopulation() { ID = 2, Name = "2", Records = 2, LiveTypeID = 2, LiveType = "222" },
            //    new Model.CountPopulation() { ID = 3, Name = "3", Records = 2, LiveTypeID = 1, LiveType = "111" }
            //};

            var tname = GetTableName<Model.PopulationBasicInfo>();
            var aname = GetTableName<Model.Address>();
            var adname = GetTableName<Model.Administrative>();
            var query = SelectHandler.Columns(string.Format("{0}.ID, {0}.Name", adname), string.Format("{0}.LiveTypeID, {0}.LiveType", tname), "count(0) as Records")
                .From<Model.PopulationBasicInfo>()
                .Join(JoinType.Left, aname).On(string.Format("{0}.CurrentAddrID = {1}.ID", tname, aname))
                .Join(JoinType.Left, adname).On(string.Format("{0}.AdminID = {1}.ID", aname, adname))
                .GroupBy(string.Format("{0}.ID, {0}.Name", adname), string.Format("{0}.LiveTypeID, {0}.LiveType", tname));

            var data = ExecuteList<Model.CountPopulation>(query.Execute().ExecuteDataReader());

            var admins = GetAdministratives();
            if (admins.Count == 0)
                return data;

            var list = new List<Model.CountPopulation>();
            admins.ForEach(x =>
            {
                for (var i = 1; i < 5; i++)
                {
                    var d = data.FirstOrDefault(t => t.ID == x.ID && t.LiveTypeID == i);
                    if (d != null)
                    {
                        list.Add(d);
                        continue;
                    }

                    Model.CountPopulation e = new Model.CountPopulation() { ID = x.ID, Name = x.Name, Records = 0, LiveTypeID = i };
                    switch (i)
                    {
                        case 1:
                            e.LiveType = "常住人口";
                            break;
                        case 2:
                            e.LiveType = "暂住人口";
                            break;
                        case 3:
                            e.LiveType = "重点人口";
                            break;
                        case 4:
                            e.LiveType = "境外人口";
                            break;
                        default:
                            e.LiveType = "其他";
                            break;
                    }
                    list.Add(e);
                }

            });

            list.AddRange(data.Where(t => !admins.Exists(x => x.ID == t.ID)));

            return list;
        }

        protected List<Model.Administrative> GetAdministratives()
        {
            return AdministrativeHandler.Handler.GetEntities();
        }
    }
}
