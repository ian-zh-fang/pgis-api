using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    public class TemporaryPopulationHandler : DBase
    {
        //setting singleton instance
        private TemporaryPopulationHandler() { }
        private static TemporaryPopulationHandler _instance;
        /// <summary>
        /// Singleton Instance
        /// </summary>
        public static TemporaryPopulationHandler Handler
        {
            get { return _instance = _instance ?? new TemporaryPopulationHandler(); }
        }

        /// <summary>
        /// 获取所有的暂住人员信息
        /// </summary>
        /// <returns></returns>
        public List<Model.TemporaryPopulation> GetEntities()
        {
            var query = SelectHandler.From<Model.TemporaryPopulation>();
            return ExecuteList<Model.TemporaryPopulation>(query.Execute().ExecuteDataReader());
        }

        /// <summary>
        /// 获取指定人员的所有暂住信息
        /// </summary>
        /// <param name="popid">指定人员ID</param>
        /// <returns></returns>
        public List<Model.TemporaryPopulation> GetEntities(int popid)
        {
            return GetEntities<Model.TemporaryPopulation>(t => t.PoID == popid);
        }

        /// <summary>
        /// 获取指定人最新的居住信息
        /// </summary>
        /// <param name="popid"></param>
        /// <returns></returns>
        public Model.TemporaryPopulation GetEntity(int popid)
        {
            var query = SelectHandler.From<Model.TemporaryPopulation>()
                .Where<Model.TemporaryPopulation>(t => t.PoID == popid)
                .OrderBy(OrderType.Desc, "Pgis_TemporaryPopulation.TP_Date");
            return ExecuteEntity<Model.TemporaryPopulation>(query.Execute().ExecuteDataReader());
        }

        /// <summary>
        /// 分页所有的暂住人员信息，并获取当前页码的人员信息
        /// </summary>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="records">当前查询的总人员数量</param>
        /// <returns></returns>
        public List<Model.TemporaryPopulation> Page(int index, int size, out int records)
        {
            return Paging<Model.TemporaryPopulation>(index, size, null, OrderType.Desc, out records, "Pgis_TemporaryPopulation.TP_ID");
        }
    }
}
