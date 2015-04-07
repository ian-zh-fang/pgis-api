using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    /// <summary>
    /// 地图标注处理程序
    /// </summary>
    public class MarkHandler:DBase
    {
        private static MarkHandler _handler = null;

        public static MarkHandler Handler
        {
            get { return _handler = _handler ?? new MarkHandler(); }
        }
        private MarkHandler() { }

        /// <summary>
        /// 获取指定ID的标注数据集合
        /// <para>如果没有指定ID，程序返回所有的标注数据信息</para>
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<Model.Mark> GetMarks(params string[] ids)
        {
            var query = SelectHandler.From<Model.Mark>();
            if (ids.Length == 0)
                return ExecuteList<Model.Mark>(query.Execute().ExecuteDataReader());

            var handler = query.Where<Model.Mark>(t => t.ID.In(ids)).Execute();
            return ExecuteList<Model.Mark>(handler.ExecuteDataReader());
        }

        /// <summary>
        /// 返回一个 Model.Mark 类型的对象列表，它仅包含有标注类型的标注
        /// </summary>
        /// <returns></returns>
        public List<Model.Mark> GetMarksHasType()
        {
            var query = SelectHandler.From<Model.Mark>().Join(JoinType.Inner, "Pgis_MarkType").On("Pgis_Mark.MarkTypeID = Pgis_MarkType.ID")
                .Where<Model.Mark>(t => t.MarkTypeID > 0);
            return ExecuteList<Model.Mark>(query.Execute().ExecuteDataReader());
        }

        /// <summary>
        /// 分页获取所有的标注数据，斌获取指定页码的数据
        /// </summary>
        /// <param name="index">页码</param>
        /// <param name="size">每页记录条目数</param>
        /// <param name="records">记录条目总数</param>
        /// <returns></returns>
        public List<Model.Mark> Paging(int index, int size, out int records)
        {
            var list = Paging<Model.Mark>(index, size, null, OrderType.Desc, out records, "pgis_mark.id");
            GetMarkType(ref list);
            return list;
        }

        /// <summary>
        /// 分页获取所有的标注数据，斌获取指定页码的数据
        /// </summary>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="name">标注名称</param>
        /// <param name="typeid">标注类型</param>
        /// <param name="records">当前查询总条目数</param>
        /// <returns></returns>
        public List<Model.Mark> Paging(int index, int size, string name, int typeid, out int records)
        {
            var list = QueryByNameAndType(index, size, name, typeid, out records);
            GetMarkType(ref list);
            return list;
        }

        private List<Model.Mark> QueryByNameAndType(int index, int size, string name, int typeid, out int records)
        {
            if (string.IsNullOrWhiteSpace(name) && typeid <= 0)
            {
                return Paging<Model.Mark>(index, size, null, OrderType.Desc, out records, "pgis_mark.id");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                return Paging<Model.Mark>(index, size, t => t.MarkTypeID == typeid, OrderType.Desc, out records, "pgis_mark.id");
            }

            if (typeid <= 0)
            {
                return Paging<Model.Mark>(index, size, t => t.Name.Like(name), OrderType.Desc, out records, "pgis_mark.id");
            }

            return Paging<Model.Mark>(index, size, t => t.Name.Like(name) && t.MarkTypeID == typeid, OrderType.Desc, out records, "pgis_mark.id");
        }

        private void GetMarkType(ref List<Model.Mark> list)
        {
            var ids = (from t in list select t.MarkTypeID.ToString()).ToArray();
            var types = MarkTypeHandler.Handler.GetEntities(ids);
            list.ForEach(t => t.MarkType = types.FirstOrDefault(x => t.MarkTypeID == x.ID));
        }
        
        /// <summary>
        /// 获取指定ID的标注信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.Mark GetEntity(int id)
        {
            return GetEntity<Model.Mark>(t => t.ID == id);
        }

        /// <summary>
        /// 添加新的标注数据记录
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int InsertEntity(Model.Mark e)
        {
            var query = InsertHandler.Into<Model.Mark>()
                .Table("Color", "Coordinates", "Description", "IconCls", "Name", "X", "Y", "MarkTypeID")
                .Values(e.Color, e.Coordinates, e.Description, e.IconCls, e.Name, e.X, e.Y, e.MarkTypeID);
            return query.Execute().ExecuteNonQuery();
        }

        /// <summary>
        /// 更新指定的标注数据记录
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int UpdateEntity(Model.Mark e)
        {
            var query = UpdateHandler.Table<Model.Mark>()
                .Set("Y").EqualTo(e.Y)
                .Set("X").EqualTo(e.X)
                .Set("MarkTypeID").EqualTo(e.MarkTypeID)
                .Set("Name").EqualTo(e.Name)
                .Set("IconCls").EqualTo(e.IconCls)
                .Set("Description").EqualTo(e.Description)
                .Set("Coordinates").EqualTo(e.Coordinates)
                .Set("Color").EqualTo(e.Color);
            var handler = query.IQueryWhere.Where<Model.Mark>(t => t.ID == e.ID).Where<IDao.IUpdate>(query).Execute();
            return handler.ExecuteNonQuery();
        }

        /// <summary>
        /// 移除指定ID的标注数据记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public override int DeleteEntities(params string[] ids)
        {
            var query = DeleteHandler.From<Model.Mark>();
            var handler = query.IQueryWhere.Where<Model.Mark>(t => t.ID.In(ids)).Where<IDao.IDelete>(query).Execute();
            return handler.ExecuteNonQuery();
        }
    }
}
