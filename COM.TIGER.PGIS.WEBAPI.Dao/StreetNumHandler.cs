using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    /// <summary>
    /// 门牌号处理程序
    /// </summary>
    public class StreetNumHandler:DBase
    {
        private static StreetNumHandler _instance = null;
        /// <summary>
        /// gingleton instance
        /// </summary>
        public static StreetNumHandler Handler
        {
            get { return _instance = _instance ?? new StreetNumHandler(); }
        }
        ///set gingleton instance
        private StreetNumHandler() { }

        /// <summary>
        /// 获取所有的门牌号
        /// </summary>
        /// <returns></returns>
        public List<Model.StreetNum> GetEntities()
        {
            var query = SelectHandler.From<Model.StreetNum>();
            return ExecuteList<Model.StreetNum>(query.Execute().ExecuteDataReader());
        }

        /// <summary>
        /// 获取指定街巷的所有门牌号记录
        /// </summary>
        /// <param name="streetid"></param>
        /// <returns></returns>
        public List<Model.StreetNum> GetEntities(int streetid)
        {
            return GetEntities<Model.StreetNum>(t => t.StreetID == streetid);
        }

        /// <summary>
        /// 获取指定街巷的所有门牌号记录
        /// </summary>
        /// <param name="streetids">指定的街巷ID组，以“，”分隔</param>
        /// <returns></returns>
        public List<Model.StreetNum> GetEntities(params string[] streetids)
        {
            if (streetids.Length == 0) return new List<Model.StreetNum>();

            return GetEntities<Model.StreetNum>(t => t.StreetID.In(streetids));
        }

        /// <summary>
        /// 获取指定ID的门牌号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.StreetNum GetEntity(int id)
        {
            return GetEntity<Model.StreetNum>(t => t.ID == id);
        }

        /// <summary>
        /// 添加新的门牌号记录
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int InsertEntity(object obj)
        {
            var e = obj as Model.StreetNum;
            if (CheckReNumber(e)) return -2;

            var query = InsertHandler.Into<Model.StreetNum>()
                .Table("CX", "CY", "EndTime", "LivingState", "Name", "SourceFrom", "StreetID", "StreetName", "StreetTypeID", "StreetTypeName", "X", "Y")
                .Values(e.CX, e.CY, e.EndTime == DateTime.MinValue ? null : e.EndTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), e.LivingState, e.Name, e.SourceFrom, e.StreetID, e.StreetName, e.StreetTypeID, e.StreetTypeName, e.X, e.Y);
            return query.Execute().ExecuteNonQuery();
        }

        /// <summary>
        /// 更新指定的门牌号记录
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int UpdateEntity(object obj)
        {
            var e = obj as Model.StreetNum;
            if (CheckReNumber(e)) return -2;

            var query = UpdateHandler.Table<Model.StreetNum>()
                .Set("CX").EqualTo(e.CX)
                .Set("CY").EqualTo(e.CY)
                .Set("EndTime").EqualTo(e.EndTime == DateTime.MinValue ? null : e.EndTime.ToString("yyyy-MM-dd HH:mm:ss.fff"))
                .Set("LivingState").EqualTo(e.LivingState)
                .Set("Name").EqualTo(e.Name)
                .Set("SourceFrom").EqualTo(e.SourceFrom)
                .Set("StreetID").EqualTo(e.StreetID)
                .Set("StreetName").EqualTo(e.StreetName)
                .Set("StreetTypeID").EqualTo(e.StreetTypeID)
                .Set("StreetTypeName").EqualTo(e.StreetTypeName)
                .Set("X").EqualTo(e.X)
                .Set("Y").EqualTo(e.Y)
                .Where<Model.StreetNum>(t => t.ID == e.ID);
            return query.Execute().ExecuteNonQuery();
        }

        /// <summary>
        /// 移除指定ID的门牌号
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public override int DeleteEntities(params string[] ids)
        {
            if (ids.Length == 0) return 0;
            return DeleteHandler.From<Model.StreetNum>().Where<Model.StreetNum>(t => t.ID.In(ids)).Execute().ExecuteNonQuery();
        }

        /// <summary>
        /// 校验重复的门牌号
        /// <para>每一个街巷的门牌号唯一</para>
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool CheckReNumber(Model.StreetNum e)
        {
            var o = GetEntity<Model.StreetNum>(t => t.Name == e.Name && t.StreetID == e.StreetID);
            return o != null;
        }
    }
}
