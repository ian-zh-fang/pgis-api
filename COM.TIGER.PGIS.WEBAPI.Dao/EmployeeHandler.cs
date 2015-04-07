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
