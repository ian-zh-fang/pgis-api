using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    /// <summary>
    /// 地图标注数据处理程序
    /// </summary>
    public class TagHandler:DBase
    {
        private static TagHandler _handler = null;
        /// <summary>
        /// 地图标注处理程序唯一实例
        /// </summary>
        public static TagHandler Handler { get { return _handler = _handler ?? new TagHandler(); } }
        private TagHandler() { }

        /// <summary>
        /// 获取指定ID的标注数据集合
        /// <para>如果没有指定ID，程序返回所有的标注数据信息</para>
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<Model.Tag> GetTags(params string[] ids)
        {
            var query = SelectHandler.From<Model.Tag>();
            if (ids.Length == 0)
                return ExecuteList<Model.Tag>(query.Execute().ExecuteDataReader());

            var handler = query.IQueryWhere.Where<Model.Tag>(t => t.ID.In(ids)).Where<IDao.ISelect>(query).Execute();
            return ExecuteList<Model.Tag>(handler.ExecuteDataReader());
        }

        /// <summary>
        /// 分页获取所有的标注数据，斌获取指定页码的数据
        /// </summary>
        /// <param name="index">页码</param>
        /// <param name="size">每页记录条目数</param>
        /// <param name="records">记录条目总数</param>
        /// <returns></returns>
        public List<Model.Tag> Paging(int index, int size, out int records)
        {
            return Paging<Model.Tag>(index, size, null, OrderType.Desc, out records, "pgis_tag.id");
        }

        /// <summary>
        /// 获取指定ID的标注信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.Tag GetEntity(int id)
        {
            return GetEntity<Model.Tag>(t => t.ID == id);
        }

        /// <summary>
        /// 添加新的标注数据记录
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int InsertEntity(Model.Tag e)
        {
            var query = InsertHandler.Into<Model.Tag>()
                .Table("Color", "Coordinates", "Description", "IconCls", "Name", "Type", "X", "Y")
                .Values(e.Color, e.Coordinates, e.Description, e.IconCls, e.Name, e.Type, e.X, e.Y);
            return query.Execute().ExecuteNonQuery();
        }

        /// <summary>
        /// 更新指定的标注数据记录
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int UpdateEntity(Model.Tag e)
        {
            var query = UpdateHandler.Table<Model.Tag>()
                .Set("Y").EqualTo(e.Y)
                .Set("X").EqualTo(e.X)
                .Set("Type").EqualTo(e.Type)
                .Set("Name").EqualTo(e.Name)
                .Set("IconCls").EqualTo(e.IconCls)
                .Set("Description").EqualTo(e.Description)
                .Set("Coordinates").EqualTo(e.Coordinates)
                .Set("Color").EqualTo(e.Color);
            var handler = query.IQueryWhere.Where<Model.Tag>(t => t.ID == e.ID).Where<IDao.IUpdate>(query).Execute();
            return handler.ExecuteNonQuery();
        }

        /// <summary>
        /// 移除指定ID的标注数据记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public override int DeleteEntities(params string[] ids)
        {
            var query = DeleteHandler.From<Model.Tag>();
            var handler = query.IQueryWhere.Where<Model.Tag>(t => t.ID.In(ids)).Where<IDao.IDelete>(query).Execute();
            return handler.ExecuteNonQuery();
        }
    }
}
