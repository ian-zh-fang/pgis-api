using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    /// <summary>
    /// 建筑物处理程序
    /// </summary>
    public class BuildingHandler:DBase
    {
        private const string STRUCTCODE = "buildingtype";
        private const string USETYPECODE = "fwyt";
        private const string PROPERTYCODE = "fwsx";

        private static BuildingHandler _instance = null;
        /// <summary>
        /// get singleton instance
        /// </summary>
        public static BuildingHandler Handler { get { return _instance = _instance ?? new BuildingHandler(); } }
        private BuildingHandler() { }

        /// <summary>
        /// 分页获取指定地址和名称的大楼信息
        /// </summary>
        /// <param name="address">详细地址</param>
        /// <param name="name">大楼名称</param>
        /// <param name="index">当前野马</param>
        /// <param name="size">每页条目数</param>
        /// <param name="records">总条目数</param>
        /// <returns></returns>
        public List<Model.OwnerInfoEx> Page(string address, string name, int index, int size, out int records)
        {
            var query = GetQuery();
            if (!string.IsNullOrWhiteSpace(name)) 
            {
                query = query.Where<Model.OwnerInfo>(t => t.MOI_OwnerName.Like(name));
            }
            var expressions = GetQueryExpression(address);
            if (expressions.Count > 0)
            {
                for (var i = 0; i < expressions.Count; i++)
                {
                    query = query.Where<Model.Building>(expressions[i]);
                }
            }
            records = Count(query);
            query = SelectHandler.Columns("temp.*").From(string.Format("({0}) as temp", query.CommandText))
                .Where(string.Format("(temp.rownum > {0}) and temp.rownum <= {1} ",
                (index - 1) * size, index * size));
            return ExecuteList<Model.OwnerInfoEx>(query.Execute().ExecuteDataReader());
        }
        
        /// <summary>
        /// 获取所有的大楼信息
        /// </summary>
        /// <returns></returns>
        public List<Model.OwnerInfoEx> GetEntities()
        {
            return ExecuteList<Model.OwnerInfoEx>(GetQuery().Execute().ExecuteDataReader());
        }

        public List<Model.OwnerInfoEx> GetEntitiesAt(params string[] ids)
        {
            if (ids.Length == 0)
                return ExecuteList<Model.OwnerInfoEx>(GetQuery().Execute().ExecuteDataReader());

            return ExecuteList<Model.OwnerInfoEx>(
                GetQuery()
                .Where(string.Format("Map_OwnerInfo.MOI_ID in ({0})", string.Join(",", ids)))
                .Execute()
                .ExecuteDataReader());
        }

        /// <summary>
        /// 批量获取指定ID的大楼信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<Model.OwnerInfoEx> GetEntities(params string[] ids)
        {
            if (ids.Length == 0) return new List<Model.OwnerInfoEx>();

            var query = GetQuery().Where<Model.Building>(t => t.AdminID.In(ids));
            return ExecuteList<Model.OwnerInfoEx>(query.Execute().ExecuteDataReader());
        }

        /// <summary>
        /// 获取指定ID的大楼信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.OwnerInfoEx GetEntity(int id)
        {
            var query = GetQuery().Where<Model.OwnerInfo>(t => t.MOI_ID == id);
            return ExecuteEntity<Model.OwnerInfoEx>(query.Execute().ExecuteDataReader());
        }

        /// <summary>
        /// 获取指定ID的大楼信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.OwnerInfoEx Entity(int id)
        {
            var query = GetQuery().Where<Model.OwnerInfo>(t => t.MOI_ID == id);
            return ExecuteEntity<Model.OwnerInfoEx>(query.Execute().ExecuteDataReader());
        }

        /// <summary>
        /// 添加新的实体信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int InsertEntity(object obj)
        {
            var e = obj as Model.OwnerInfoEx;
            var rst = -1;
            //首先添加elementhot信息
            //其次添加ownerinfo信息
            //其次更新elementhot记录MEH_MOI_ID字段信息
            //其次添加building信息
            //其次添加ownerpic信息
            //其次添加unit信息
            try
            {
                //添加ElementHot
                var hot = InsertElementHot(e);
                if (hot == null) return -1;
                rst++;
                //添加OwnerInfo
                e.MOI_MEH_ID = hot.MEH_ID;
                var info = InsertOwnerInfo(e);
                if (info == null) return -1;
                //更新elementhot记录MEH_MOI_ID字段信息
                if (UpdateHandler.Table<Model.ElementHot>().Set("MEH_MOI_ID").EqualTo(info.MOI_ID).Where<Model.ElementHot>(t => t.MEH_ID == hot.MEH_ID).Execute().ExecuteNonQuery() < 0) return -1;
                rst++;
                //添加Building
                e.OwnerInfoID = info.MOI_ID;
                var build = InsertBuilding(e);
                if (build == null) return -1;
                rst++;
                //添加ownerpic信息
                var flag = false;
                Model.OwnerPic pic = null;
                for (var i = 0; i < e.Pictures.Length; i++)
                {
                    pic = e.Pictures[i];
                    pic.MOP_MOI_ID = info.MOI_ID;
                    if (OwnerPicHandler.Handler.InsertEntity(pic) < 0)
                    {
                        flag = true;
                        break;
                    }
                    rst++;
                }
                //添加unit信息
                Model.Unit ut = null;
                for (var i = 0; i < e.Units.Length; i++)
                {
                    ut = e.Units[i];
                    ut.OwnerInfoID = info.MOI_ID;
                    if (UnitHandler.Handler.InsertEntity(ut) < 0)
                    {
                        flag = true;
                        break;
                    }
                    rst++;
                }
                //标识添加单元信息，或者OwnerPic记录出错
                if (flag) return -1;
            }
            catch { rst = -3; }

            return rst;
        }

        /// <summary>
        /// 更新指定的大楼信息数据
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int UpdateEntity(object obj)
        {
            var e = obj as Model.Building;
            if (e == null) return -1;

            var query = UpdateHandler.Table<Model.Building>()
                .Set("Address").EqualTo(e.Address)
                .Set("AdminID").EqualTo(e.AdminID)
                .Set("AdminName").EqualTo(e.AdminName)
                .Set("FloorsCount").EqualTo(e.FloorsCount)
                .Set("OwnerInfoID").EqualTo(e.OwnerInfoID)
                .Set("RoomsCount").EqualTo(e.RoomsCount)
                .Set("RoomStructure").EqualTo(e.RoomStructure)
                .Set("RoomStructureID").EqualTo(e.RoomStructureID)
                .Set("StreetID").EqualTo(e.StreetID)
                .Set("StreetNum").EqualTo(e.StreetNum)
                .Where<Model.Building>(t => t.Building_ID == e.Building_ID);
            return query.Execute().ExecuteNonQuery();
        }

        /// <summary>
        /// 移除指定的大楼信息
        /// </summary>
        /// <param name="ids">指定大楼的ID组，以“，”分隔</param>
        /// <returns></returns>
        public override int DeleteEntities(params string[] ids)
        {
            //var rst = 0;
            //using (var tran = new System.Transactions.TransactionScope())
            //{
            //    try
            //    {


            //        tran.Complete();
            //    }
            //    catch { rst = -3; }
            //}
            //return rst;

            return base.DeleteEntities(ids);
        }

        /// <summary>
        /// 添加热区
        /// </summary>
        /// <returns></returns>
        private Model.ElementHot InsertElementHot(Model.OwnerInfoEx obj)
        {
            var e = ParseEntity<Model.ElementHot>(obj);

            var query = InsertHandler.Into<Model.ElementHot>()
                .Table("Area", "flag", "MEH_CenterX", "MEH_CenterY", "MEH_CreateDate", "MEH_Croods", "MEH_HotFlag", "MEH_HotLevel", "MEH_IsLabel", "MEH_IsLock", "MEH_PID", "MEH_UpdateDate")
                .Values(e.Area, e.flag, e.MEH_CenterX, e.MEH_CenterY, e.MEH_CreateDate, e.MEH_Croods, e.MEH_HotFlag, e.MEH_HotLevel, e.MEH_IsLabel, e.MEH_IsLock, e.MEH_PID, e.MEH_UpdateDate);
            
            if (query.Execute().ExecuteNonQuery() < 0) return null;

            return GetEntity<Model.ElementHot>(t => t.Area == e.Area &&
                t.flag == e.flag &&
                t.MEH_CenterX == e.MEH_CenterX &&
                t.MEH_CenterY == e.MEH_CenterY &&
                t.MEH_CreateDate == e.MEH_CreateDate &&
                t.MEH_Croods == e.MEH_Croods &&
                t.MEH_HotFlag == e.MEH_HotFlag &&
                t.MEH_HotLevel == e.MEH_HotLevel &&
                t.MEH_IsLabel == e.MEH_IsLabel &&
                t.MEH_IsLock == e.MEH_IsLock &&
                t.MEH_PID == e.MEH_PID &&
                t.MEH_UpdateDate == e.MEH_UpdateDate);
        }

        /// <summary>
        /// 添加大楼信息
        /// </summary>
        /// <returns></returns>
        private Model.OwnerInfo InsertOwnerInfo(Model.OwnerInfoEx obj)
        {
            var e = ParseEntity<Model.OwnerInfo>(obj);
            e.MOI_OwnerAddress = obj.Address;

            var query = InsertHandler.Into<Model.OwnerInfo>()
                .Table("JID", "MOI_CreateDate", "MOI_Flag", "MOI_IsActive", "MOI_LabelName", "MOI_MEH_ID", "MOI_MOA_ID", "MOI_OwnerAddress", "MOI_OwnerDes", "MOI_OwnerName", "MOI_OwnerTel", "MOI_sFlag", "MOI_UpdateDate", "PY", "REQU")
                .Values(e.JID, e.MOI_CreateDate, e.MOI_Flag, e.MOI_IsActive, e.MOI_LabelName, e.MOI_MEH_ID, e.MOI_MOA_ID, e.MOI_OwnerAddress, e.MOI_OwnerDes, e.MOI_OwnerName, e.MOI_OwnerTel, e.MOI_sFlag, e.MOI_UpdateDate, e.PY, e.REQU);

            if (query.Execute().ExecuteNonQuery() < 0) return null;

            return GetEntity<Model.OwnerInfo>(t => t.REQU == e.REQU &&
                t.MOI_UpdateDate == e.MOI_UpdateDate &&
                t.MOI_sFlag == e.MOI_sFlag &&
                t.MOI_OwnerTel == e.MOI_OwnerTel &&
                t.MOI_OwnerName == e.MOI_OwnerName &&
                t.MOI_OwnerDes == e.MOI_OwnerDes &&
                t.MOI_OwnerAddress == e.MOI_OwnerAddress &&
                t.MOI_MOA_ID == e.MOI_MOA_ID &&
                t.MOI_MEH_ID == e.MOI_MEH_ID &&
                t.MOI_LabelName == e.MOI_LabelName &&
                t.MOI_IsActive == e.MOI_IsActive &&
                t.MOI_Flag == e.MOI_Flag &&
                t.MOI_CreateDate == e.MOI_CreateDate &&
                t.JID == e.JID);
        }

        /// <summary>
        /// 添加大楼辅助信息
        /// </summary>
        /// <returns></returns>
        private Model.Building InsertBuilding(Model.OwnerInfoEx obj)
        {
            var e = ParseEntity<Model.Building>(obj);

            var query = InsertHandler.Into<Model.Building>()
                .Table("Address", "AdminID", "AdminName", "FloorsCount", "OwnerInfoID", "RoomsCount", "RoomStructure", "RoomStructureID", "StreetID", "StreetNum")
                .Values(e.Address, e.AdminID, e.AdminName, e.FloorsCount, e.OwnerInfoID, e.RoomsCount, e.RoomStructure, e.RoomStructureID, e.StreetID, e.StreetNum);

            if (query.Execute().ExecuteNonQuery() < 0) return null;

            return GetEntity<Model.Building>(t => t.StreetNum == e.StreetNum &&
                t.StreetID == e.StreetID &&
                t.RoomStructureID == e.RoomStructureID &&
                t.RoomStructure == e.RoomStructure &&
                t.RoomsCount == e.RoomsCount &&
                t.OwnerInfoID == e.OwnerInfoID &&
                t.FloorsCount == e.FloorsCount &&
                t.AdminName == e.AdminName &&
                t.AdminID == e.AdminID &&
                t.Address == e.Address);
        }
        
        /// <summary>
        /// 获取当前查询的总记录数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private int Count(IDao.ISelect query)
        {
            query = SelectHandler.Columns("count(0) as count").From(string.Format("({0}) as temp", query.CommandText));
            return Convert.ToInt32(query.Execute().ExecuteSaclar());
        }

        /// <summary>
        /// 获取大楼地址查询表达式
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns></returns>
        private List<System.Linq.Expressions.Expression<Func<Model.Building, bool>>> GetQueryExpression(string address)
        {
            var expressions = new List<System.Linq.Expressions.Expression<Func<Model.Building, bool>>>();
            if (string.IsNullOrWhiteSpace(address)) return expressions;
            var chs = address.ToCharArray();
            for (var i = 0; i < chs.Length; i++)
            {
                var s = chs[i].ToString();
                expressions.Add(t => t.Address.Like(s));
            }            
            return expressions;
        }

        /// <summary>
        /// 获取实体查询模块
        /// </summary>
        /// <returns></returns>
        private IDao.ISelect GetQuery()
        {
            var query = SelectHandler.Columns("Map_OwnerInfo.*",
                "[Map_ElementHot].[MEH_ID] ,[Map_ElementHot].[MEH_PID] ,[Map_ElementHot].[MEH_MOI_ID] ,[Map_ElementHot].[MEH_CenterX] ,[Map_ElementHot].[MEH_CenterY] ,[Map_ElementHot].[MEH_Croods] ,[Map_ElementHot].[MEH_HotLevel] ,[Map_ElementHot].[MEH_IsLabel] ,[Map_ElementHot].[MEH_HotFlag] ,[Map_ElementHot].[MEH_CreateDate] ,[Map_ElementHot].[MEH_UpdateDate] ,[Map_ElementHot].[MEH_IsLock] ,[Map_ElementHot].[Area] ,[Map_ElementHot].[flag]",
                "Pgis_Building.*",
                string.Format("ROW_NUMBER() OVER ( order by {0} desc ) AS rownum", "Map_OwnerInfo.MOI_UpdateDate"))
                .From<Model.OwnerInfo>()
                .Join(JoinType.Inner, "Map_ElementHot").On("Map_OwnerInfo.MOI_ID = Map_ElementHot.MEH_MOI_ID")
                .Join(JoinType.Left, "Pgis_Building").On("Map_OwnerInfo.MOI_ID = Pgis_Building.OwnerInfoID");
            return query;
        }

        /// <summary>
        /// 将类型Model.OwnerInfoEx转换为目标类型
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="e">Model.OwnerInfoEx类型实例</param>
        /// <returns></returns>
        private T ParseEntity<T>(Model.OwnerInfoEx e) where T : new()
        {
            var t = new T();
            var targtp = e.GetType();
            var properties = t.GetType().GetProperties();
            object val = null;
            for (var i = 0; i < properties.Length; i++)
            {
                var property = properties[i];
                var targproperty = targtp.GetProperty(property.Name);
                if (targproperty != null)
                {
                    val = targproperty.GetValue(e, null);
                    property.SetValue(t, val, null);
                }
            }                
            return t;
        }

        public int AddOwnerInfo(Model.OwnerInfoEx e)
        {
            System.Transactions.TransactionScope tran = null;
            int rst = 0;
            try
            {
                //tran = new System.Transactions.TransactionScope();

                rst = InsertHandler.Into<Model.OwnerInfo>()
                    .Table("MOI_MOA_ID", "MOI_MEH_ID", "MOI_LabelName", "MOI_OwnerName", "MOI_OwnerAddress", "MOI_OwnerTel", "MOI_OwnerDes", "MOI_Flag", "MOI_sFlag", "MOI_CreateDate", "MOI_UpdateDate", "MOI_IsActive")
                    .Values(e.MOI_MOA_ID, e.MOI_MEH_ID, e.MOI_LabelName, e.MOI_OwnerName, e.MOI_OwnerAddress, e.MOI_OwnerTel, e.MOI_OwnerDes, e.MOI_Flag, e.MOI_sFlag,
                    e.MOI_CreateDate.ToString("yyyy-MM-dd HH:mm:ss.fff"), e.MOI_UpdateDate.ToString("yyyy-MM-dd HH:mm:ss.fff"), e.MOI_IsActive)
                    .Execute().ExecuteNonQuery();
                if (rst < 0)
                    throw new Exception();
                var info = GetEntity<Model.OwnerInfo>(t => t.MOI_LabelName == e.MOI_LabelName &&
                    t.MOI_OwnerAddress == e.MOI_OwnerAddress &&
                    //t.MOI_OwnerDes == e.MOI_OwnerDes &&
                    t.MOI_OwnerName == e.MOI_OwnerName &&
                    t.MOI_OwnerTel == e.MOI_OwnerTel);

                e.MOI_ID = info.MOI_ID;
                e.MEH_MOI_ID = info.MOI_ID;
                e.OwnerInfoID = info.MOI_ID;

                if (0 > InsertHandler.Into<Model.ElementHot>()
                    .Table("MEH_PID", "MEH_MOI_ID", "MEH_CenterX", "MEH_CenterY", "MEH_Croods", "MEH_HotLevel", "MEH_IsLabel", "MEH_HotFlag", "MEH_CreateDate", "MEH_UpdateDate", "MEH_IsLock")
                    .Values(e.MEH_PID, e.MEH_MOI_ID, e.MEH_CenterX, e.MEH_CenterY, e.MEH_Croods, e.MEH_HotLevel, e.MEH_IsLabel, e.MEH_HotFlag, 
                    e.MEH_CreateDate.ToString("yyyy-MM-dd HH:mm:ss.fff"), e.MEH_UpdateDate.ToString("yyyy-MM-dd HH:mm:ss.fff"), e.MEH_IsLock)
                    .Execute().ExecuteNonQuery())
                    throw new Exception();
                rst += 1;

                var hot = GetEntity<Model.ElementHot>(t => t.MEH_Croods == e.MEH_Croods &&
                    t.MEH_CenterX == e.MEH_CenterX &&
                    t.MEH_CenterY == e.MEH_CenterY &&
                    t.MEH_MOI_ID == e.MEH_MOI_ID);
                UpdateHandler.Table<Model.OwnerInfo>().Set("MOI_MEH_ID").EqualTo(hot.MEH_ID).Execute().ExecuteNonQuery();

                //校验行政区划信息
                Model.Street street = null;
                Model.StreetNum streetnum = null;
                CheckStreet(e.MOI_ID, e.AdminID, e.StreetName, e.StreetNumber, out street, out streetnum);

                if (0 > AddBuilding(new Model.Building() { OwnerInfoID = e.MOI_ID, FloorsCount = e.FloorsCount, RoomsCount = e.RoomsCount, Address = e.MOI_OwnerAddress, AdminID = e.AdminID, AdminName = e.AdminName, RoomStructure = e.RoomStructure, RoomStructureID = e.RoomStructureID, StreetID = street.ID, StreetNum = streetnum.StreetID }))
                    throw new Exception();
                rst += 1;

                //tran.Complete();
            }
            catch { rst = -1; }
            finally {
                if (tran != null) tran.Dispose();
            }
            return rst;
        }

        private int AddBuilding(Model.Building e)
        {
            return InsertHandler.Into<Model.Building>()
                    .Table("OwnerInfoID", "FloorsCount", "RoomsCount", "AdminID", "AdminName", "StreetID", "StreetNum", "Address", "RoomStructureID", "RoomStructure")
                    .Values(e.OwnerInfoID, e.FloorsCount, e.RoomsCount, e.AdminID, e.AdminName, e.StreetID, e.StreetNum, e.Address, e.RoomStructureID, e.RoomStructure)
                    .Execute().ExecuteNonQuery();
        }
        
        //校验街道，街道号码存在
        private void CheckStreet(int ownerinfoid, int adminID, string name, string number, out Model.Street street, out Model.StreetNum streetnum)
        {
            street = GetEntity<Model.Street>(t => t.AdminID == adminID && t.Name == name);
            if (street == null)
            {
                street = new Model.Street() { AdminID = adminID, Name = name, Alias = name };

                if (0 > StreetHandler.Handler.InsertEntity(street))
                    throw new Exception();

                street = GetEntity<Model.Street>(t => t.AdminID == adminID && t.Name == name && t.Alias == name);
                //街道地址
                CheckAddress(name, adminID, ownerinfoid, street.ID);
            }

            var streetid = street.ID;
            streetnum = GetEntity<Model.StreetNum>(t => t.StreetID == streetid && t.Name == number);
            if (streetnum == null)
            {
                streetnum = new Model.StreetNum() { Name = number, StreetID = streetid, StreetName = name };
                if (0 > StreetNumHandler.Handler.InsertEntity(streetnum))
                    throw new Exception();

                streetnum = GetEntity<Model.StreetNum>(t => t.Name == number && t.StreetName == name && t.StreetID == streetid);
                //街道门牌地址
                CheckAddress(string.Format("{0},{1}", name, number), adminID, ownerinfoid, street.ID, streetnum.ID);
            }

            //在此添加新的地址信息
        }

        //校验地址是否存在
        private void CheckAddress(string address, int adminid = 0, int ownerinfoid = 0, int streetid = 0, int numid = 0, int unitid = 0, int roomid = 0)
        {
            if (string.IsNullOrWhiteSpace(address)) return;
            if (GetEntity<Model.Address>(t => t.Content == address) != null) return;

            AddressHandler.Handler.InsertEntity(new Model.Address() { 
                AdminID = adminid, UnitID = unitid, StreetID = streetid, RoomID = roomid, OwnerInfoID = ownerinfoid, NumID = numid, Content = address 
            });
        }

        public int UpdateOwnerInfo(Model.OwnerInfoEx e)
        {
            System.Transactions.TransactionScope tran = null;
            var x = GetEntity<Model.OwnerInfo>(t => t.MOI_ID == e.MOI_ID);
            var rst = 0;
            try
            {
                //tran = new System.Transactions.TransactionScope();

                if (0 > UpdateHandler.Table<Model.OwnerInfo>()
                    .Set("MOI_LabelName").EqualTo(e.MOI_LabelName)
                    .Set("MOI_OwnerAddress").EqualTo(e.MOI_OwnerAddress)
                    .Set("MOI_OwnerDes").EqualTo(e.MOI_OwnerDes)
                    .Set("MOI_OwnerName").EqualTo(e.MOI_OwnerName)
                    .Set("MOI_OwnerTel").EqualTo(e.MOI_OwnerTel)
                    .Set("MOI_UpdateDate").EqualTo(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))
                    .Where<Model.OwnerInfo>(t => t.MOI_ID == e.MOI_ID)
                    .Execute().ExecuteNonQuery())
                    throw new Exception();
                rst += 1;

                if (0 > UpdateHandler.Table<Model.ElementHot>()
                    .Set("MEH_UpdateDate").EqualTo(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))
                    .Set("MEH_Croods").EqualTo(e.MEH_Croods)
                    .Set("MEH_CenterX").EqualTo(e.MEH_CenterX)
                    .Set("MEH_CenterY").EqualTo(e.MEH_CenterY)
                    .Where<Model.ElementHot>(t => t.MEH_ID == e.MOI_MEH_ID)
                    .Execute().ExecuteNonQuery())
                    throw new Exception();
                rst += 1;
                Model.Street street = null;
                Model.StreetNum streetnum = null;
                CheckStreet(e.MOI_ID, e.AdminID, e.StreetName, e.StreetNumber, out street, out streetnum);
                CorrectAddress(x.MOI_OwnerAddress, e.MOI_OwnerAddress, e.MOI_ID, e.AdminID, street.ID, streetnum.ID);
                if (null == GetEntity<Model.Building>(t => t.OwnerInfoID == e.MOI_ID))
                {
                    if (0 > AddBuilding(new Model.Building() { OwnerInfoID = e.MOI_ID, FloorsCount = e.FloorsCount, RoomsCount = e.RoomsCount, Address = e.MOI_OwnerAddress, AdminID = e.AdminID, AdminName = e.AdminName, RoomStructure = e.RoomStructure, RoomStructureID = e.RoomStructureID, StreetID = street.ID, StreetNum = streetnum.ID }))
                        throw new Exception();
                    rst += 1;
                }
                else
                {
                    if (0 > UpdateBuilding(new Model.Building() { Address = e.MOI_OwnerAddress, AdminID = e.AdminID, StreetNum = streetnum.ID, StreetID = street.ID, RoomStructureID = e.RoomStructureID, RoomStructure = e.RoomStructure, AdminName = e.AdminName, FloorsCount = e.FloorsCount, Building_ID = e.Building_ID, RoomsCount = e.RoomsCount, OwnerInfoID = e.OwnerInfoID }))
                        throw new Exception();
                    rst += 1;
                }
                    
                //tran.Complete();
            }
            catch { rst = -1; }
            finally {
                if (tran != null)
                    tran.Dispose();
            }
            return rst;
        }

        public List<string> QueryBuildingAddress(string patternStr)
        {
            string sqlstr = "select top 10 tttemp.* from (select distinct Pgis_Building.address from Pgis_Building) as tttemp";
            if (!string.IsNullOrWhiteSpace(patternStr))
            {
                sqlstr = string.Format("{0} where tttemp.Address like '%{1}%'", sqlstr, patternStr.Trim());
            }

            List<string> addresses = new List<string>();
            System.Data.IDataReader reader = null;
            try
            {
                reader = new IDao.Dbase().ExecuteDataReader(sqlstr);
                while (reader.Read())
                {
                    addresses.Add(reader.GetValue(0).ToString());
                }
            }
            catch (Exception) { }
            finally {
                if (null != reader)
                {
                    reader.Close();
                    reader.Dispose();
                }
            }
            return addresses;
        }

        //修正地址信息
        private void CorrectAddress(string oldaddress, string newaddress, int id, int adminid = 0, int streetid = 0, int numid = 0)
        {
            var addresses = GetEntities<Model.Address>(t => t.OwnerInfoID == id);
            System.Transactions.TransactionScope tran = null;
            try
            {
                tran = new System.Transactions.TransactionScope();

                addresses.ForEach(t =>
                {
                    t.Content = t.Content.Replace(oldaddress, newaddress);
                    t.AdminID = adminid;
                    t.NumID = numid;
                    t.StreetID = streetid;

                    AddressHandler.Handler.UpdateEntity(t);
                });

                tran.Complete();
            }
            finally 
            {
                if (tran != null)
                    tran.Dispose();
            }
        }

        private int UpdateBuilding(Model.Building e)
        {
            return UpdateHandler.Table<Model.Building>()
                .Set("RoomsCount").EqualTo(e.RoomsCount)
                .Set("Address").EqualTo(e.Address)
                .Set("AdminID").EqualTo(e.AdminID)
                .Set("AdminName").EqualTo(e.AdminName)
                .Set("FloorsCount").EqualTo(e.FloorsCount)
                .Set("RoomStructure").EqualTo(e.RoomStructure)
                .Set("RoomStructureID").EqualTo(e.RoomStructureID)
                .Set("StreetID").EqualTo(e.StreetID)
                .Set("StreetNum").EqualTo(e.StreetNum)
                .Where<Model.Building>(t => t.Building_ID == e.Building_ID)
                .Execute().ExecuteNonQuery();
        }

        public int DeleteOwnerInfos(params string[] ids)
        {
            System.Transactions.TransactionScope tran = null;
            var rst = 0;
            try
            {
                tran = new System.Transactions.TransactionScope();
                rst += DeleteHandler.From<Model.OwnerInfo>().Where<Model.OwnerInfo>(t => t.MOI_ID.In(ids)).Execute().ExecuteNonQuery();
                if (rst < 0)
                    throw new Exception("大楼信息删除失败");

                rst += DeleteHandler.From<Model.ElementHot>().Where<Model.ElementHot>(t => t.MEH_MOI_ID.In(ids)).Execute().ExecuteNonQuery();
                rst += DeleteHandler.From<Model.OwnerPic>().Where<Model.OwnerPic>(t => t.MOP_MOI_ID.In(ids)).Execute().ExecuteNonQuery();
                rst += DeleteHandler.From<Model.Rooms>().Where<Model.Rooms>(t => t.OwnerInfoID.In(ids)).Execute().ExecuteNonQuery();
                rst += DeleteHandler.From<Model.Unit>().Where<Model.Unit>(t => t.OwnerInfoID.In(ids)).Execute().ExecuteNonQuery();
                rst += DeleteHandler.From<Model.Building>().Where<Model.Building>(t => t.OwnerInfoID.In(ids)).Execute().ExecuteNonQuery();

                tran.Complete();
            }
            catch { rst = -1; }
            finally
            {
                if (tran != null)
                    tran.Dispose();
            }
            return rst;
        }

        public int AddStruct(Model.Param e)
        {
            var parent = ParamHandler.Handler.GetEntityByCode(STRUCTCODE);
            if (parent == null) return -1;

            e.PID = parent.ID;
            return ParamHandler.Handler.InsertNew(e);
        }

        public int UpdateStruct(Model.Param e)
        {
            return ParamHandler.Handler.UpdateNew(e.ID, e);
        }

        public int DeleteStructs(params string[] ids)
        {
            return ParamHandler.Handler.DeleteEntities(ids);
        }

        public int AddUseType(Model.Param e)
        {
            var parent = ParamHandler.Handler.GetEntityByCode(USETYPECODE);
            if (parent == null) return -1;

            e.PID = parent.ID;
            return ParamHandler.Handler.InsertNew(e);
        }

        public int UpdateUseType(Model.Param e)
        {
            return ParamHandler.Handler.UpdateNew(e.ID, e);
        }

        public int DeleteUseTypes(params string[] ids)
        {
            return ParamHandler.Handler.DeleteEntities(ids);
        }

        public int AddProperty(Model.Param e)
        {
            var parent = ParamHandler.Handler.GetEntityByCode(PROPERTYCODE);
            if (parent == null) return -1;

            e.PID = parent.ID;
            return ParamHandler.Handler.InsertNew(e);
        }

        public int UpdateProperty(Model.Param e)
        {
            return ParamHandler.Handler.UpdateNew(e.ID, e);
        }

        public int DeleteProperties(params string[] ids)
        {
            return ParamHandler.Handler.DeleteEntities(ids);
        }

        public List<Model.Param> GetStructs()
        {
            return ParamHandler.Handler.GetParams(STRUCTCODE, false);
        }

        public List<Model.Param> GetUseTypes()
        {
            return ParamHandler.Handler.GetParams(USETYPECODE, false);
        }

        public List<Model.Param> GetProperties()
        {
            return ParamHandler.Handler.GetParams(PROPERTYCODE, false);
        }

        public int AddUnit(Model.UnitEx e)
        {
            var ret = UnitHandler.Handler.InsertEntity(e);
            if (ret <= 0)
                return ret;


            var x = GetEntity<Model.Unit>(t => t.OwnerInfoID == e.OwnerInfoID && t.Sort == e.Sort && t.UnitName == e.UnitName);
            e.Unit_ID = x.Unit_ID;
            //在此分析地址信息
            CheckAddress(e);

            return ret;
        }

        public int UpdateUnit(Model.UnitEx e)
        {
            //在此分析地址信息
            CheckAddress(e);

            return UnitHandler.Handler.UpdateEntity(e);
        }

        private void CheckAddress(Model.UnitEx e)
        {
            var building = GetEntity<Model.Building>(t => t.OwnerInfoID == e.OwnerInfoID);
            var adminid = 0;
            var streetid = 0;
            var numid = 0;
            if (building != null)
            {
                adminid = building.AdminID;
                streetid = building.StreetID;
                numid = building.StreetNum;
            }

            CheckAddress(string.Format("{0},{1}", e.Address, e.UnitName), adminid, e.OwnerInfoID, streetid, numid, e.Unit_ID, 0);
        }

        public int DeleteUnits(params string[] ids)
        {
            return UnitHandler.Handler.DeleteEntities(ids);
        }

        public List<Model.Unit> GetUnitsOnBuilding(params string[] ids)
        {
            if (ids.Length == 0) return new List<Model.Unit>();

            return GetEntities<Model.Unit>(t => t.OwnerInfoID.In(ids));
        }

        public int AddRoom(Model.Rooms e)
        {
            System.Transactions.TransactionScope tran = null;
            int rst = 0;
            try
            {
                //tran = new System.Transactions.TransactionScope();
                var building = GetEntity<Model.Building>(t => t.OwnerInfoID == e.OwnerInfoID);
                var ownerinfo = GetEntity<Model.OwnerInfo>(t => t.MOI_ID == e.OwnerInfoID);
                var adminid = 0;
                var streetid = 0;
                var numid = 0;
                if (building != null)
                {
                    adminid = building.AdminID;
                    streetid = building.StreetID;
                    numid = building.StreetNum;
                }
                //保存地址信息
                string address = string.Format("{0},{1},{2}", ownerinfo.MOI_OwnerAddress, e.UnitName, e.RoomName);

                
                //保存房间信息
                rst = RoomHandler.Handler.InsertEntity(e);

                //CheckAddress(address, adminid, e.OwnerInfoID, streetid, numid, e.UnitID);
                //address = string.Format("{0},{1}", address, e.RoomName);
                var room = GetEntity<Model.Rooms>(t =>
                    t.OwnerInfoID == e.OwnerInfoID &&
                    t.RoomArea == e.RoomArea &&
                    t.RoomAttributeID == e.RoomAttributeID &&
                    t.RoomName == e.RoomName &&
                    t.RoomName2 == e.RoomName2 &&
                    t.RoomUseID == e.RoomUseID &&
                    t.UnitID == e.UnitID);
                CheckAddress(address, adminid, e.OwnerInfoID, streetid, numid, e.UnitID, room.Room_ID);

                //tran.Complete();
            }
            catch (NullReferenceException) { rst = -2; }
            catch (Exception) { rst = -1; }
            finally 
            {
                if (tran != null)
                    tran.Dispose();
            }
            return rst;
        }

        public int UpdateRoom(Model.Rooms e)
        {
            var ret = RoomHandler.Handler.UpdateEntity(e);

            if (ret > 0)
            {
                var ownerinfo = GetEntity<Model.OwnerInfo>(t => t.MOI_ID == e.OwnerInfoID);
                string address = string.Format("{0},{1},{2}", ownerinfo.MOI_OwnerAddress, e.UnitName, e.RoomName);
                UpdateHandler.Table<Model.Address>()
                    .Set("Content").EqualTo(address)
                    .Where<Model.Address>(t => t.OwnerInfoID == e.OwnerInfoID && t.RoomID == e.Room_ID)
                    .Execute()
                    .ExecuteNonQuery();
            }

            return ret;
        }

        public int DeleteRooms(params string[] ids)
        {
            return RoomHandler.Handler.DeleteEntities(ids);
        }

        public List<Model.Rooms> GetRoomsOnUnit(string id)
        {
            return RoomHandler.Handler.GetEntitiesWithUnit(id);
        }

        public int AddPic(Model.OwnerPic e)
        {
            return OwnerPicHandler.Handler.InsertEntity(e);
        }

        public int UpdatePic(Model.OwnerPic e)
        {
            return OwnerPicHandler.Handler.UpdateEntity(e);
        }

        public int DeletePics(params string[] ids)
        {
            return OwnerPicHandler.Handler.DeleteEntities(ids);
        }

        public List<Model.OwnerPic> GetPics(string id)
        {
            return OwnerPicHandler.Handler.GetEntities(id);
        }

        public int AddPopulation(Model.PopulationBasicInfoEx e)
        {
            System.Transactions.TransactionScope tran = null;
            int rst = 0;
            try
            {
                //tran = new System.Transactions.TransactionScope();

                //校验地址
                Model.Address currentaddr = null;
                CheckAddress(e.CurrentAddress, out currentaddr);
                if (currentaddr == null)
                    throw new NullReferenceException();

                Model.Address homeaddr = null;
                CheckAddress(e.HomeAddress, out homeaddr);
                if (homeaddr == null)
                    throw new NullReferenceException();

                e.HomeAddrID = homeaddr.ID;
                e.CurrentAddrID = currentaddr.ID;

                rst = PopulationHandler.Handler.InsertEntity(e);

                //tran.Complete();
            }
            catch (NullReferenceException) { rst = -2; }
            catch (Exception) { rst = -1; }
            finally {
                if (tran != null)
                    tran.Dispose();
            }
            return rst;
        }

        private void CheckAddress(string address, out Model.Address addr)
        {
            if (string.IsNullOrWhiteSpace(address)) 
            {
                addr = null;
                return;
            }

            addr = GetEntity<Model.Address>(x => x.Content == address);
            if (addr != null) return;

            CheckAddress(address);
            addr = GetEntity<Model.Address>(x => x.Content == address);
        }

        public int UpdatePopulation(Model.PopulationBasicInfoEx e)
        {
            System.Transactions.TransactionScope tran = null;
            int rst = 0;
            try
            {
                //tran = new System.Transactions.TransactionScope();

                //校验地址
                Model.Address currentaddr = null;
                CheckAddress(e.CurrentAddress, out currentaddr);
                if (currentaddr == null)
                    throw new NullReferenceException();

                Model.Address homeaddr = null;
                CheckAddress(e.HomeAddress, out homeaddr);
                if (homeaddr == null)
                    throw new NullReferenceException();

                e.HomeAddrID = homeaddr.ID;
                e.CurrentAddrID = currentaddr.ID;

                rst = PopulationHandler.Handler.UpdateEntity(e);

                //tran.Complete();
            }
            catch (NullReferenceException) { rst = -2; }
            catch (Exception) { rst = -1; }
            finally
            {
                if (tran != null)
                    tran.Dispose();
            }
            return rst;
        }

        public int DeletePopulations(params string[] ids)
        {
            return PopulationHandler.Handler.DeleteEntities(ids);
        }

        public List<Model.PopulationBasicInfoEx> GetPopulationsOnBuilding(string id, int index, int size, out int records)
        {
            return PopulationHandler.Handler.GetPopulationsOnBuilding(id, 0, index, size, out records);
        }

        public List<Model.PopulationBasicInfoEx> GetPopulationsOnUnit(string id, int index, int size, out int records)
        {
            return PopulationHandler.Handler.GetPopulationsOnUnit(id, index, size, out records);
        }

        public List<Model.PopulationBasicInfoEx> GetPopulationsOnRoom(string id)
        {
            return PopulationHandler.Handler.GetPopulationsOnRoom(id);
        }

        public int CompanyAdd(string addr, string ids)
        {
            if (string.IsNullOrWhiteSpace(ids)) return 0;

            Model.Address address = null;
            CheckAddress(addr, out address);

            var addrid = address == null ? 0 : address.ID;
            var roomid = address == null ? 0 : address.RoomID;

            return UpdateHandler.Table<Model.Company>()
                .Set("AddressID").EqualTo(addrid)
                .Set("RoomID").EqualTo(roomid)
                .Where(string.Format("{0}.ID in ({1})", GetTableName<Model.Company>(), ids))
                .Execute()
                .ExecuteNonQuery();
        }

        public int PopuAdd(string addr, string ids)
        {
            if (string.IsNullOrWhiteSpace(ids)) return 0;

            Model.Address address = null;
            CheckAddress(addr, out address);

            var addrid = address == null ? 0 : address.ID;
            var roomid = address == null ? 0 : address.RoomID;

            return UpdateHandler.Table<Model.PopulationBasicInfo>()
                .Set("CurrentAddrID").EqualTo(addrid)
                .Where(string.Format("{0}.ID in ({1})", GetTableName<Model.PopulationBasicInfo>(), ids))
                .Execute()
                .ExecuteNonQuery();
        }

        public Model.ElementHot GetEleByAddress(int addrid)
        {
            if (addrid == 0)
                return null;

            var tname = GetTableName<Model.ElementHot>();
            var aname = GetTableName<Model.Address>();
            var query = SelectHandler.From<Model.ElementHot>()
                .Join(JoinType.Left, aname).On(string.Format("{0}.OwnerInfoID = {1}.MEH_MOI_ID", aname, tname))
                .Where<Model.Address>(t => t.ID == addrid);

            return ExecuteEntity<Model.ElementHot>(query.Execute().ExecuteDataReader());
        }
    }
}
