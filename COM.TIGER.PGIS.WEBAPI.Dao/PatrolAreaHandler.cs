using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    public class PatrolAreaHandler : DBase
    {
        private static PatrolAreaHandler _handler = null;
        /// <summary>
        /// 辖区信息处理程序
        /// </summary>
        public static PatrolAreaHandler Handler
        {
            get { return (_handler = _handler ?? new PatrolAreaHandler()); }
        }
        private PatrolAreaHandler() { }

        /// <summary>
        /// 获取所有的区域信息
        /// </summary>
        /// <returns></returns>
        public List<Model.PatrolArea> GetPatrolArea()
        {
            var handler = SelectHandler.From<Model.PatrolArea>().Execute();
            var list = ExecuteList<Model.PatrolArea>(handler.ExecuteDataReader());
            return list;
        }


        public List<Model.PatrolArea> Page(int index, int size, out int records)
        {
            var data = Paging<Model.PatrolArea>(index, size, null, OrderType.Desc, out records, "Pgis_PatrolArea.ID");
            return data;
        }

        public int DeleteEntity(int id)
        {
            return DeleteOperate<Model.PatrolArea>(t => t.Id == id);
        }

        private int UpdateNew(int id, Model.PatrolArea e)
        {
            var query = UpdateHandler.Table<Model.PatrolArea>();
            query = query
                .Set("Manager").EqualTo(e.Manager)
                .Set("Name").EqualTo(e.Name)
                .Set("Phone").EqualTo(e.Phone)
                .Set("Remark").EqualTo(e.Remark)
                .Set("Color").EqualTo(e.Color)
                .Set("Centerx").EqualTo(e.Centerx)
                .Set("Centery").EqualTo(e.Centery)
                .Set("Coordinates").EqualTo(e.Coordinates);
            var handler = query.IQueryWhere.Where<Model.PatrolArea>(t => t.Id == id).Where<IDao.IUpdate>(query).Execute();
            return handler.ExecuteNonQuery();
        }

        public int InsertNew(Model.PatrolArea e)
        {
            var query = InsertHandler.Into<Model.PatrolArea>()
                .Table("Centerx", "Centery", "Color", "Coordinates", "Manager", "Name", "Phone", "Remark")
                .Values(e.Centerx, e.Centery, e.Color, e.Coordinates, e.Manager,e.Name,e.Phone,e.Remark);
            return query.Execute().ExecuteNonQuery();
        }

        public override int InsertEntity(object obj)
        {
            var e = obj as Model.PatrolArea;
            var query = InsertHandler.Into<Model.PatrolArea>()
                .Table("Centerx", "Centery", "Color", "Coordinates", "Manager", "Name", "Phone", "Remark")
                .Values(e.Centerx, e.Centery, e.Color, e.Coordinates, e.Manager, e.Name, e.Phone, e.Remark);
            return query.Execute().ExecuteNonQuery();
        }

        public override int UpdateEntity(object obj)
        {
            var e = obj as Model.PatrolArea;
            var query = UpdateHandler.Table<Model.PatrolArea>();
            query = query
                .Set("Manager").EqualTo(e.Manager)
                .Set("Name").EqualTo(e.Name)
                .Set("Phone").EqualTo(e.Phone)
                .Set("Remark").EqualTo(e.Remark)
                .Set("Color").EqualTo(e.Color)
                .Set("Centerx").EqualTo(e.Centerx)
                .Set("Centery").EqualTo(e.Centery)
                .Set("Coordinates").EqualTo(e.Coordinates);
            var handler = query.IQueryWhere.Where<Model.PatrolArea>(t => t.Id == e.Id ).Where<IDao.IUpdate>(query).Execute();
            return handler.ExecuteNonQuery();
        }

        public override int DeleteEntities(params string[] ids)
        {
            if (ids.Length == 0) return 0;

            var query = DeleteHandler.From<Model.PatrolArea>().Where<Model.PatrolArea>(t => t.Id.In(ids));
            return query.Execute().ExecuteNonQuery();
        }
    }
}
