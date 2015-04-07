using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    public class AreaRangeHandler:DBase
    {
        private static AreaRangeHandler _handler = null;
        public static AreaRangeHandler Handler { get { return _handler = _handler ?? new AreaRangeHandler(); } }

        private AreaRangeHandler() { }

        /// <summary>
        /// 获取指定标识的所有的范围信息
        /// <para>如果没有指定标识信息，程序将查询所有的区域范围信息</para>
        /// </summary>
        /// <param name="ids">标识组</param>
        /// <returns></returns>
        public List<Model.AreaRange> GetAllEntities(params string[] ids)
        {
            var query = SelectHandler.From<Model.AreaRange>();
            if (ids.Length == 0)
            {
                return ExecuteList<Model.AreaRange>(query.Execute().ExecuteDataReader());
            }
            var handler = query.IQueryWhere.Where<Model.AreaRange>(t => t.ID.In(ids)).Where<IDao.ISelect>(query).Execute();
            return ExecuteList<Model.AreaRange>(handler.ExecuteDataReader());
        }

        /// <summary>
        /// 查询指定区域标识的区域范围信息
        /// </summary>
        /// <param name="areaid"></param>
        /// <returns></returns>
        public List<Model.AreaRange> GetAllEntities(int areaid)
        {
            var query = SelectHandler.From<Model.AreaRange>();
            var handler = query.IQueryWhere.Where<Model.AreaRange>(t => t.AreaID == areaid).Where<IDao.ISelect>(query).Execute();
            return ExecuteList<Model.AreaRange>(handler.ExecuteDataReader());
        }

        /// <summary>
        /// 查询指定区域ID范围信息
        /// </summary>
        /// <param name="pids"></param>
        /// <returns></returns>
        public List<Model.AreaRange> GetChildEntities(params string[] pids)
        {
            if (pids.Length == 0) return new List<Model.AreaRange>();
            var query = SelectHandler.From<Model.AreaRange>();
            var handler = query.IQueryWhere.Where<Model.AreaRange>(t => t.AreaID.In(pids)).Where<IDao.ISelect>(query).Execute();
            return ExecuteList<Model.AreaRange>(handler.ExecuteDataReader());
        }

        /// <summary>
        /// 添加指定的记录信息
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int Add(Model.AreaRange e)
        {
            var query = InsertHandler.Into<Model.AreaRange>()
                .Table("AreaID", "Color", "Range", "X", "Y")
                .Values(e.AreaID, e.Color, e.Range,e.X, e.Y);
            return query.Execute().ExecuteNonQuery();
        }

        /// <summary>
        /// 更新指定的记录信息
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int Update(Model.AreaRange e)
        {
            var query = UpdateHandler.Table<Model.AreaRange>()
                .Set("AreaID").EqualTo(e.AreaID)
                .Set("Color").EqualTo(e.Color)
                .Set("Range").EqualTo(e.Range)
                .Set("X").EqualTo(e.X)
                .Set("Y").EqualTo(e.Y);
            var handler = query.IQueryWhere.Where<Model.AreaRange>(t => t.ID == e.ID).Where<IDao.IUpdate>(query).Execute();
            return handler.ExecuteNonQuery();
        }

        /// <summary>
        /// 移除指定标识符的所有区域范围信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int Delete(params string[] ids)
        {
            if (ids.Length == 0) return 0;

            var query = DeleteHandler.From<Model.AreaRange>();
            var handler = query.IQueryWhere.Where<Model.AreaRange>(t => t.ID.In(ids)).Where<IDao.IDelete>(query).Execute();
            return handler.ExecuteNonQuery();
        }

        /// <summary>
        /// 移除指定区域ID的范围信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteByAreaID(params string[] ids)
        {
            var query = DeleteHandler.From<Model.AreaRange>();
            var handler = query.IQueryWhere.Where<Model.AreaRange>(t => t.AreaID.In(ids)).Where<IDao.IDelete>(query).Execute();
            return handler.ExecuteNonQuery();
        }
    }
}
