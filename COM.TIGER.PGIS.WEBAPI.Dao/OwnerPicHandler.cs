using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    /// <summary>
    /// 大楼图片处理程序
    /// </summary>
    public class OwnerPicHandler:DBase
    {
        //setting singleton instance
        private OwnerPicHandler() { }
        private static OwnerPicHandler _instance;
        /// <summary>
        /// singleton instance
        /// </summary>
        public static OwnerPicHandler Handler
        {
            get { return _instance = _instance ?? new OwnerPicHandler(); }
        }

        /// <summary>
        /// 获取所有大楼的图片记录
        /// </summary>
        /// <returns></returns>
        public List<Model.OwnerPic> GetEntities()
        {
            return ExecuteList<Model.OwnerPic>(SelectHandler.From<Model.OwnerPic>().Execute().ExecuteDataReader());
        }

        /// <summary>
        /// 获取指定大楼的所有图片记录
        /// </summary>
        /// <param name="ids">大楼ID组，以“，”分隔</param>
        /// <returns></returns>
        public List<Model.OwnerPic> GetEntities(params string[] ids)
        {
            if (ids.Length == 0) return new List<Model.OwnerPic>();

            return GetEntities<Model.OwnerPic>(t => t.MOP_MOI_ID.In(ids));
        }

        /// <summary>
        /// 分页所有大楼的图片记录，并获取当前页码的记录
        /// </summary>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="records">所有大楼的图片记录总数</param>
        /// <returns></returns>
        public List<Model.OwnerPic> Page(int index, int size, out int records)
        {
            return Paging<Model.OwnerPic>(index, size, null, OrderType.Desc, out records, "Map_OwnerPic.MOP_ID");
        }

        /// <summary>
        /// 分页指定大楼的图片信息，并获取当前页码的记录
        /// </summary>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="records">纵图片记录数</param>
        /// <param name="ids">大楼ID组，以“，”分隔</param>
        /// <returns></returns>
        public List<Model.OwnerPic> Page(int index, int size, out int records, params string[] ids)
        {
            if (ids.Length == 0)
            {
                records = 0;
                return new List<Model.OwnerPic>();
            }

            return Paging<Model.OwnerPic>(index, size, t => t.MOP_MOI_ID.In(ids), OrderType.Desc, out records, "Map_OwnerPic.MOP_ID");
        }

        /// <summary>
        /// 添加新的记录
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int InsertEntity(object obj)
        {
            var e = obj as Model.OwnerPic;
            var query = InsertHandler.Into<Model.OwnerPic>()
                .Table("JID", "MOP_Createdate", "MOP_ImgDefault", "MOP_ImgName", "MOP_ImgPath", "MOP_ImgRemark", "MOP_ImgTitle", "MOP_MOI_ID", "MOP_Sort", "MOP_Updatedate")
                .Values(e.JID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), e.MOP_ImgDefault, e.MOP_ImgName, e.MOP_ImgPath, e.MOP_ImgRemark, e.MOP_ImgTitle, e.MOP_MOI_ID, e.MOP_Sort, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            return query.Execute().ExecuteNonQuery();
        }

        /// <summary>
        /// 更新指定的记录
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int UpdateEntity(object obj)
        {
            var e = obj as Model.OwnerPic;
            var query = UpdateHandler.Table<Model.OwnerPic>()
                .Set("JID").EqualTo(e.JID)
                .Set("MOP_ImgDefault").EqualTo(e.MOP_ImgDefault)
                .Set("MOP_ImgName").EqualTo(e.MOP_ImgName)
                .Set("MOP_ImgPath").EqualTo(e.MOP_ImgPath)
                .Set("MOP_ImgRemark").EqualTo(e.MOP_ImgRemark)
                .Set("MOP_ImgTitle").EqualTo(e.MOP_ImgTitle)
                .Set("MOP_MOI_ID").EqualTo(e.MOP_MOI_ID)
                .Set("MOP_Sort").EqualTo(e.MOP_Sort)
                .Set("MOP_Updatedate").EqualTo(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))
                .Where<Model.OwnerPic>(t => t.MOP_ID == e.MOP_ID);
            return query.Execute().ExecuteNonQuery();
        }

        /// <summary>
        /// 移除指定的记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public override int DeleteEntities(params string[] ids)
        {
            if (ids.Length == 0) return 0;

            return DeleteHandler.From<Model.OwnerPic>().Where<Model.OwnerPic>(t => t.MOP_ID.In(ids)).Execute().ExecuteNonQuery();
        }
    }
}
