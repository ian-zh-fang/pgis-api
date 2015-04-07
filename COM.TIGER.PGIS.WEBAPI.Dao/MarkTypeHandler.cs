using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    public class MarkTypeHandler:DBase
    {
        private MarkTypeHandler() { }
        private static MarkTypeHandler _instance;
        public static MarkTypeHandler Handler
        {
            get { return _instance = _instance ?? new MarkTypeHandler(); }
        }

        public override int InsertEntity(object obj)
        {
            var e = obj as Model.MarkType;
            if (e == null)
                return -2;

            var query = InsertHandler.Into<Model.MarkType>()
                .Table("Color", "IconCls", "Name", "Remark", "Type", "Sort")
                .Values(e.Color, e.IconCls, e.Name, e.Remark, e.Type, e.Sort);
            return query.Execute().ExecuteNonQuery();
        }

        public override int UpdateEntity(object obj)
        {
            var e = obj as Model.MarkType;
            if (e == null)
                return -2;

            var query = UpdateHandler.Table<Model.MarkType>()
                .Set("Type").EqualTo(e.Type)
                .Set("Remark").EqualTo(e.Remark)
                .Set("Name").EqualTo(e.Name)                
                .Set("Color").EqualTo(e.Color)
                .Set("Sort").EqualTo(e.Sort);
            if (e.IconCls != null)
                query = query.Set("IconCls").EqualTo(e.IconCls);
            query = query.Where<Model.MarkType>(t => t.ID == e.ID);

            return query.Execute().ExecuteNonQuery();
        }

        public override int DeleteEntities(params string[] ids)
        {
            if (ids.Length == 0)
                return 0;

            var r = 0;
            using (var tran = new System.Transactions.TransactionScope())
            {
                try
                {
                    var query = DeleteHandler.From<Model.MarkType>().Where<Model.MarkType>(t => t.ID.In(ids));
                    r += query.Execute().ExecuteNonQuery();
                    r += DeleteHandler.From<Model.Mark>().Where<Model.Mark>(t => t.MarkTypeID.In(ids)).Execute().ExecuteNonQuery();
                    tran.Complete();
                }
                catch { r = -2; }
            }
            return r;
        }

        /// <summary>
        /// 返回一个 COM.TIGER.PGIS.WEBAPI.Model.MarkType 类型的对象列表。
        /// </summary>
        /// <param name="ids">指定的记录 ID 组值，如果不指定，返回所有的记录</param>
        /// <returns></returns>
        public List<Model.MarkType> GetEntities(params string[] ids)
        {
            var query = SelectHandler.From<Model.MarkType>();
            if (ids.Length > 0)
                query = query.Where<Model.MarkType>(t => t.ID.In(ids));

            var list = ExecuteList<Model.MarkType>(query.Execute().ExecuteDataReader());
            return list;
        }

        /// <summary>
        /// 返回一个 Model.MarkType 类型的对象列表，每一个对象都包含对应的标注信息
        /// </summary>
        /// <returns></returns>
        public List<Model.MarkType> GetEntitiesForTree() 
        {
            var marks = MarkHandler.Handler.GetMarksHasType();
            var ids = (from t in marks select t.MarkTypeID.ToString()).ToArray();
            var types = GetEntities(ids);
            types.ForEach(t => t.AddRange(marks));
            return types;
        }
    }
}
