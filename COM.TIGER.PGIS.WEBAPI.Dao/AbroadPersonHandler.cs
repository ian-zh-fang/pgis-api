using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    public class AbroadPersonHandler:DBase
    {
        //setting singleton instance
        private AbroadPersonHandler() { }
        private static AbroadPersonHandler _instance;
        /// <summary>
        /// Singleton Instance
        /// </summary>
        public static AbroadPersonHandler Handler
        {
            get { return _instance = _instance ?? new AbroadPersonHandler(); }
        }

        /// <summary>
        /// 获取所有的入境人员信息
        /// </summary>
        /// <returns></returns>
        public List<Model.AbroadPerson> GetEntities()
        {
            var query = SelectHandler.From<Model.AbroadPerson>();
            return ExecuteList<Model.AbroadPerson>(query.Execute().ExecuteDataReader());
        }

        /// <summary>
        /// 获取指定人员的入境信息
        /// </summary>
        /// <param name="popid"></param>
        /// <returns></returns>
        public List<Model.AbroadPerson> GetEntities(int popid)
        {
            return GetEntities<Model.AbroadPerson>(t => t.PoID == popid);
        }

        public List<Model.AbroadPerson> GetEntities(string cardNo)
        {
            return GetEntities<Model.AbroadPerson>(t => t.CardNo == cardNo);
        }

        /// <summary>
        /// 获取指定人员的最新的入境信息
        /// </summary>
        /// <param name="popid"></param>
        /// <returns></returns>
        public Model.AbroadPerson GetLastEntity(int popid)
        {
            var query = SelectHandler.From<Model.AbroadPerson>()
                .Where<Model.AbroadPerson>(t => t.PoID == popid)
                .OrderBy(OrderType.Desc, "Pgis_AbroadPerson.ArrivalDate");
            return ExecuteEntity<Model.AbroadPerson>(query.Execute().ExecuteDataReader());
        }

        /// <summary>
        /// 分页查询所有入境人员信息，并获取当前页码的数据记录
        /// </summary>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="records">当前查询总条目数</param>
        /// <returns></returns>
        public List<Model.AbroadPerson> Page(int index, int size, out int records)
        {
            return Paging<Model.AbroadPerson>(index, size, null, OrderType.Desc, out records, "Pgis_AbroadPerson.AP_ID");
        }
    }
}
