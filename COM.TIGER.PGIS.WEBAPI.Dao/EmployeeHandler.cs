using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    public class EmployeeHandler:DBase
    {
        private static EmployeeHandler _instance = null;
        public static EmployeeHandler Handler { get { return _instance = _instance ?? new EmployeeHandler(); } }
        private EmployeeHandler() { }

        public List<Model.Employee> QueryEmployees(string name, string code, string addr, int index, int size, out int records)
        {
            var query = QueryEmployees(name, code, addr);
            return Paging<Model.Employee>(query, index, size, out records);
        }

        public List<Model.Employee> QueryEmployeesOnCompany(string name, string code, string addr, int index, int size, out int records)
        {
            var query = QueryEmployees(name, code, addr).Where<Model.Employee>(t => t.OrganTypeID == OrganType.COMPANY);
            return Paging<Model.Employee>(query, index, size, out records);
        }

        public List<Model.Employee> QueryEmployeesOnHotel(string name, string code, string addr, int index, int size, out int records)
        {
            var query = QueryEmployees(name, code, addr).Where<Model.Employee>(t => t.OrganTypeID == OrganType.HOTEL);
            return Paging<Model.Employee>(query, index, size, out records);
        }

        public List<Model.Employee> GetEmployeesOnCompany(int id, int index, int size, out int records)
        {
            var query = GetPageQuery().Where<Model.Employee>(t => t.OrganID == id && t.OrganTypeID == OrganType.COMPANY);
            return Paging<Model.Employee>(query, index, size, out records);
        }

        public List<Model.Employee> GetQuitEmployeesOnCompany(int id, int index, int size, out int records)
        {
            var query = GetPageQuery().Where<Model.Employee>(t => t.OrganID == id && t.IsInService == 0 && t.OrganTypeID == OrganType.COMPANY);
            return Paging<Model.Employee>(query, index, size, out records);
        }

        public List<Model.Employee> GetEmployeesOnHotel(int id, int index, int size, out int records)
        {
            var query = GetPageQuery().Where<Model.Employee>(t => t.OrganID == id && t.OrganTypeID == OrganType.HOTEL);
            return Paging<Model.Employee>(query, index, size, out records);
        }

        public List<Model.Employee> GetQuitEmployeesOnHotel(int id, int index, int size, out int records)
        {
            var query = GetPageQuery().Where<Model.Employee>(t => t.OrganID == id && t.IsInService == 0 && t.OrganTypeID == OrganType.HOTEL);
            return Paging<Model.Employee>(query, index, size, out records);
        }

        public List<Model.CompanyEmployee> QueryEmployees(string cardNo)
        {
            if (string.IsNullOrWhiteSpace(cardNo))
                return new List<Model.CompanyEmployee>();

            List<Model.Employee> query = GetEntities<Model.Employee>(t => t.IdentityCardNum == cardNo);
            List<Model.CompanyEmployee> list = new List<Model.CompanyEmployee>();
            List<string> ids = new List<string>();
            query.ForEach(t =>
            {
                Model.CompanyEmployee e = new Model.CompanyEmployee()
                {
                    Address = t.Address,
                    CardTypeID = t.CardTypeID,
                    CardTypeName = t.CardTypeName,
                    CityID = t.CityID,
                    CityName = t.CityName,
                    EntryTime = t.EntryTime,
                    Func = t.Func,
                    GenderDesc = t.GenderDesc,
                    GenderID = t.GenderID,
                    ID = t.ID,
                    IdentityCardNum = t.IdentityCardNum,
                    IsInService = t.IsInService,
                    JobTypeID = t.JobTypeID,
                    JobTypeName = t.JobTypeName,
                    Name = t.Name,
                    OrganID = t.OrganID,
                    OrganTypeID = t.OrganTypeID,
                    OrganTypeName = t.OrganTypeName,
                    ProvinceID = t.ProvinceID,
                    ProvinceName = t.ProvinceName,
                    QuitTime = t.QuitTime,
                    Seniority = t.Seniority,
                    Tel = t.Tel
                };
                list.Add(e);
                ids.Add(e.OrganID.ToString());
            });

            QueryEmployeesCompany(ref list, ids.ToArray());
            return list;
        }

        private void QueryEmployeesCompany(ref List<Model.CompanyEmployee> list, params string[] ids)
        {
            if (ids.Length == 0) return;

            ids = ids.Distinct().ToArray();
            List<Model.Company> comps = CompanyHandler.Handler.GetCompanies(ids);
            list.ForEach(t => t.Company = comps.FirstOrDefault(x => x.ID == t.OrganID));
        }
        
        private IDao.ISelect GetPageQuery()
        {
            var tablename = GetTableName<Model.Employee>();
            var sortfield = "ID";
            return GetPageQuery(tablename, sortfield);
        }

        private IDao.ISelect QueryEmployees(string name, string code, string addr)
        {
            var query = GetPageQuery();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where<Model.Employee>(t => t.Name.Like(name));

            if (!string.IsNullOrWhiteSpace(code))
                query = query.Where<Model.Employee>(t => t.IdentityCardNum.Like(code));

            if (!string.IsNullOrWhiteSpace(addr))
                query = query.Where<Model.Employee>(t => t.Address.Like(addr));

            return query;
        }
    }

    /// <summary>
    /// 单位类型
    /// </summary>
    public struct OrganType
    {
        /// <summary>
        /// 单位
        /// </summary>
        public const int COMPANY = 1;

        /// <summary>
        /// 旅馆
        /// </summary>
        public const int HOTEL = 2;
    }
}
