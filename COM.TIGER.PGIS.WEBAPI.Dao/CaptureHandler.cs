using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    public class CaptureHandler:DBase
    {
        private static CaptureHandler _instance = null;
        public static CaptureHandler Handler
        {
            get { return _instance = _instance ?? new CaptureHandler(); }
        }
        private CaptureHandler() { }

        public List<Model.Capture> Pagging(int index, int size, out int records)
        {
            return Paging<Model.Capture>(index, size, null, OrderType.Desc, out records, "Pgis_Capture.ID");
        }

        public override int InsertEntity(object obj)
        {
            var e = obj as Model.Capture;
            var query = InsertHandler.Into<Model.Capture>()
                .Table("Y", "X", "Type", "Remark", "Name", "Coordinates")
                .Values(e.Y, e.X, e.Type, e.Remark, e.Name, e.Coordinates);
            return query.Execute().ExecuteNonQuery();
        }

        public override int UpdateEntity(object obj)
        {
            var e = obj as Model.Capture;
            var query = UpdateHandler.Table<Model.Capture>()
                .Set("Coordinates").EqualTo(e.Coordinates)
                .Set("Name").EqualTo(e.Name)
                .Set("Remark").EqualTo(e.Remark)
                .Set("Type").EqualTo(e.Type)
                .Set("X").EqualTo(e.X)
                .Set("Y").EqualTo(e.Y)
                .Where<Model.Capture>(t => t.ID == e.ID);
            return query.Execute().ExecuteNonQuery();
        }

        public override int DeleteEntities(params string[] ids)
        {
            var query = DeleteHandler.From<Model.Capture>()
                .Where<Model.Capture>(t => t.ID.In(ids));
            return query.Execute().ExecuteNonQuery();
        }

        public List<Model.Capture> GetEntities()
        {
            var query = SelectHandler.From<Model.Capture>();
            return ExecuteList<Model.Capture>(query.Execute().ExecuteDataReader());
        }
    }
}
