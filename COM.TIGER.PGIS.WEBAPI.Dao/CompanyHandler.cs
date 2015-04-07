using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    public class CompanyHandler:DBase
    {
        private const string COMPANYTRADES = "hylx";
        private const string COMPANYGRNERS = "dwxl";
        private const string COMPANYTYPES = "dwdl";

        private static CompanyHandler _instance = null;
        public static CompanyHandler Handler { get { return _instance = _instance ?? new CompanyHandler(); } }
        private CompanyHandler() { }

        public override int InsertEntity(object obj)
        {
            var e = obj as Model.Company;
            if (e == null) return 0;

            var addr = GetAddressInfo(e.Addr);
            if (addr == null) return 0;

            GetAddressInfoOnRoomID(e.RoomID, ref addr);

            e.AddressID = addr.ID;
            return InsertHandler.Into<Model.Company>()
                .Table("TypeName", "TypeID", "TradeName", "TradeID", "Tel", "StartTime", "Square", "RoomID", "OrganName", "OrganID", "Name", "MigrantWorks", "MainFrame", "LicenceStartTime", "LicenceNum", "LicenceEndTime", "GenreName", "GenreID", "FireRating", "Corporation", "Concurrently", "Capital", "AddressID")
                .Values(e.TypeName, e.TypeID, e.TradeName, e.TradeID, e.Tel, e.StartTimeStr, e.Square, e.RoomID, e.OrganName, e.OrganID, e.Name, e.MigrantWorks, e.MainFrame, e.LicenceStartTimeStr, e.LicenceNum, e.LicenceEndTimeStr, e.GenreName, e.GenreID, e.FireRating, e.Corporation, e.Concurrently, e.Capital, e.AddressID)
                .Execute()
                .ExecuteNonQuery();
        }

        private void GetAddressInfoOnRoomID(string roomid, ref Model.Address addr)
        {
            if (string.IsNullOrWhiteSpace(roomid)) return;

            var room = RoomHandler.Handler.GetEntity(roomid);
            if (room == null) return;

            addr.AdminID = room.BuildingInfo.AdminID;
            addr.OwnerInfoID = room.OwnerInfoID;
            addr.RoomID = room.Room_ID;
            addr.UnitID = room.UnitID;
            AddressHandler.Handler.UpdateEntity(addr);
        }

        public override int UpdateEntity(object obj)
        {
            var e = obj as Model.Company;
            if (e == null) return 0;

            var addr = GetAddressInfo(e.Addr);
            if (addr == null) return 0;

            e.AddressID = addr.ID;
            return UpdateHandler.Table<Model.Company>()
                .Set("AddressID").EqualTo(e.AddressID)
                .Set("Capital").EqualTo(e.Capital)
                .Set("Concurrently").EqualTo(e.Concurrently)
                .Set("Corporation").EqualTo(e.Corporation)
                .Set("FireRating").EqualTo(e.FireRating)
                .Set("GenreID").EqualTo(e.GenreID)
                .Set("GenreName").EqualTo(e.GenreName)
                .Set("LicenceEndTime").EqualTo(e.LicenceEndTimeStr)
                .Set("LicenceNum").EqualTo(e.LicenceNum)
                .Set("LicenceStartTime").EqualTo(e.LicenceStartTimeStr)
                .Set("MainFrame").EqualTo(e.MainFrame)
                .Set("MigrantWorks").EqualTo(e.MigrantWorks)
                .Set("Name").EqualTo(e.Name)
                .Set("OrganID").EqualTo(e.OrganID)
                .Set("OrganName").EqualTo(e.OrganName)
                .Set("RoomID").EqualTo(e.RoomID)
                .Set("Square").EqualTo(e.Square)
                .Set("StartTime").EqualTo(e.StartTimeStr)
                .Set("Tel").EqualTo(e.Tel)
                .Set("TradeID").EqualTo(e.TradeID)
                .Set("TradeName").EqualTo(e.TradeName)
                .Set("TypeID").EqualTo(e.TypeID)
                .Set("TypeName").EqualTo(e.TypeName)
                .Where<Model.Company>(t => t.ID == e.ID)
                .Execute()
                .ExecuteNonQuery();
        }

        public override int DeleteEntities(params string[] ids)
        {
            if (ids.Length == 0) return 0;

            return DeleteHandler.From<Model.Company>()
                .Where<Model.Company>(t => t.ID.In(ids))
                .Execute()
                .ExecuteNonQuery();
        }

        public int AddKind(Model.Param e)
        {
            var p = ParamHandler.Handler.GetEntityByCode(COMPANYTYPES);
            if (p == null) return 0;

            e.PID = p.ID;
            return ParamHandler.Handler.InsertNew(e);
        }

        public int AddGenre(Model.Param e)
        {
            var p = ParamHandler.Handler.GetEntityByCode(COMPANYGRNERS);
            if (p == null) return 0;

            e.PID = p.ID;
            return ParamHandler.Handler.InsertNew(e);
        }

        public int AddTrade(Model.Param e)
        {
            var p = ParamHandler.Handler.GetEntityByCode(COMPANYTRADES);
            if (p == null) return 0;

            e.PID = p.ID;
            return ParamHandler.Handler.InsertNew(e);
        }

        public int UpdateParam(Model.Param e)
        {
            return ParamHandler.Handler.UpdateNew(e.ID, e);
        }

        public int DeleteParams(params string[] ids)
        {
            return ParamHandler.Handler.DeleteEntities(ids);
        }

        public List<Model.Company> PagingCompanys(int index, int size, out int records)
        {
            var list = Paging<Model.Company>(index, size, null, OrderType.Desc, out records, string.Format("{0}.ID", GetTableName<Model.Company>()));
            GetAddresses(ref list);
            return list;
        }

        public List<Model.Company> QueryCompany(string name, string addr, int index, int size, out int records)
        {
            var ncmp = GetTableName<Model.Company>();
            var naddr = GetTableName<Model.Address>();

            var query = GetPageQuery(ncmp, "ID");
            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where<Model.Company>(t => t.Name.Like(name));
            if (!string.IsNullOrWhiteSpace(addr))
                query = query
                    .Join(JoinType.Left, naddr).On(string.Format("{0}.AddressID = {1}.ID", ncmp, naddr))
                    .Where<Model.Address>(t => t.Content.Like(addr));

            var list = Paging<Model.Company>(query, index, size, out records);
            GetAddresses(ref list);
            return list;
        }

        public List<Model.Company> PageCompanies(string query, int index, int size, out int records)
        {
            System.Linq.Expressions.Expression<Func<Model.Company, bool>> express = null;
            if (!string.IsNullOrWhiteSpace(query))
                express = t => t.Name.Like(query);

            var list = Paging<Model.Company>(index, size, express, OrderType.Desc, out records, string.Format("{0}.ID", GetTableName<Model.Company>()));
            GetAddresses(ref list);
            return list;
        }

        private void GetAddresses(ref List<Model.Company> list)
        {
            if (list == null) return;
            if (list.Count == 0) return;

            string[] ids = (from t in list select t.AddressID.ToString()).ToArray();
            var addresses = GetEntities<Model.Address>(t => t.ID.In(ids));
            list.ForEach(t => t.Address = addresses.FirstOrDefault(x => t.AddressID == x.ID));
        }

        public List<Model.Param> GetCompanyTrades()
        {
            return ParamHandler.Handler.GetParams(COMPANYTRADES, false);
        }

        public List<Model.Param> GetCompanyGenres()
        {
            return ParamHandler.Handler.GetParams(COMPANYGRNERS, false);
        }

        public List<Model.Param> GetCompanyTypes()
        {
            return ParamHandler.Handler.GetParams(COMPANYTYPES, false);
        }

        public List<Model.Company> GetCompneysOnBuilding(int id)
        {
            var query = GetQuery().Where<Model.Address>(t => t.OwnerInfoID == id);
            var list = ExecuteList<Model.Company>(query.Execute().ExecuteDataReader());
            GetAddresses(ref list);
            return list;
        }

        public List<Model.Company> GetCompneysOnUnit(int id)
        {
            var query = GetQuery().Where<Model.Address>(t => t.UnitID == id);
            var list = ExecuteList<Model.Company>(query.Execute().ExecuteDataReader());
            GetAddresses(ref list);
            return list;
        }

        public List<Model.Company> GetCompanysOnRoom(string id)
        {
            var query = SelectHandler.From<Model.Company>().Where<Model.Company>(t => t.RoomID == id);
            var list = ExecuteList<Model.Company>(query.Execute().ExecuteDataReader());
            GetAddresses(ref list);
            return list;
        }

        private IDao.ISelect GetQuery()
        {
            var ncomp = GetTableName<Model.Company>();
            var naddr = GetTableName<Model.Address>();

            return SelectHandler.From<Model.Company>()
                .Join(JoinType.Left, naddr).On(string.Format("{0}.AddressID = {1}.ID", ncomp, naddr));
        }
    }
}
