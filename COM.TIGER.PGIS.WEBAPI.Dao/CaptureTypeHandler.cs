using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    public class CaptureTypeHandler:DBase
    {
        private static CaptureTypeHandler _instance;
        public static CaptureTypeHandler Handler
        {
            get { return _instance = _instance ?? new CaptureTypeHandler(); }
        }
        private CaptureTypeHandler() { }

        public List<Model.CaptureType> GetEntities(params string[] ids)
        {
            var query = SelectHandler.From<Model.CaptureType>();
            if (ids.Length > 0)
                query = query.Where<Model.CaptureType>(t => t.ID.In(ids));

            return ExecuteList<Model.CaptureType>(query.Execute().ExecuteDataReader());
        }

        public override int InsertEntity(object obj)
        {
            var e = obj as Model.CaptureType;
            var query = InsertHandler.Into<Model.CaptureType>()
                .Table("Type", "Sort", "Remark", "Name", "IconCls", "Color")
                .Values(e.Type, e.Sort, e.Remark, e.Name, e.IconCls, e.Color);
            return query.Execute().ExecuteNonQuery();
        }

        public override int UpdateEntity(object obj)
        {
            var e = obj as Model.CaptureType;
            var query = UpdateHandler.Table<Model.CaptureType>()
                .Set("Color").EqualTo(e.Color)
                .Set("Name").EqualTo(e.Name)
                .Set("Remark").EqualTo(e.Remark)
                .Set("Sort").EqualTo(e.Sort)
                .Set("Type").EqualTo(e.Type);                
            if (e.IconCls != null)
                query = query.Set("IconCls").EqualTo(e.IconCls);
            query = query.Where<Model.CaptureType>(t => t.ID == e.ID);

            return query.Execute().ExecuteNonQuery();
        }

        public override int DeleteEntities(params string[] ids)
        {
            var r = 0;
            if (ids.Length == 0)
                return 0;
            using (var tran = new System.Transactions.TransactionScope())
            {
                try
                {
                    var query = DeleteHandler.From<Model.CaptureType>().Where<Model.CaptureType>(t => t.ID.In(ids));
                    r += query.Execute().ExecuteNonQuery();
                    r += DeleteHandler.From<Model.Capture>().Where<Model.Capture>(t => t.Type.In(ids)).Execute().ExecuteNonQuery();
                    tran.Complete();
                }
                catch { r = -1; }
            }
            return r;
        }
    }
}
