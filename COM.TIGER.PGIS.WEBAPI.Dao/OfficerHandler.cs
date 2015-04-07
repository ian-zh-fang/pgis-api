using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    public class OfficerHandler:DBase
    {
        private OfficerHandler() { }
        private static OfficerHandler _instance;
        public static OfficerHandler Handler { get { return _instance = _instance ?? new OfficerHandler(); } }

        public override int InsertEntity(object obj)
        {
            var e = obj as Model.Officer;
            if (e == null) return 0;
            //验证警号重复
            if (null != GetEntity<Model.Officer>(t => t.Num == e.Num))
                return -2;

            return InsertHandler.Into<Model.Officer>()
                .Table("IdentityID", "Name", "Num", "Tel", "DeptID", "Gender")
                .Values(e.IdentityID, e.Name, e.Num, e.Tel, e.DeptID, e.Gender)
                .Execute()
                .ExecuteNonQuery();
        }

        public override int UpdateEntity(object obj)
        {
            var e = obj as Model.Officer;
            if (e == null) return 0;
            //验证警号重复
            if (null != GetEntity<Model.Officer>(t => t.Num == e.Num && t.ID != e.ID))
                return -2;

            return UpdateHandler.Table<Model.Officer>()
                .Set("Tel").EqualTo(e.Tel)
                .Set("Num").EqualTo(e.Num)
                .Set("Name").EqualTo(e.Name)
                .Set("IdentityID").EqualTo(e.IdentityID)
                .Set("DeptID").EqualTo(e.DeptID)
                .Set("Gender").EqualTo(e.Gender)
                .Where<Model.Officer>(t => t.ID == e.ID)
                .Execute()
                .ExecuteNonQuery();
        }

        public override int DeleteEntities(params string[] ids)
        {
            return DeleteHandler.From<Model.Officer>()
                .Where<Model.Officer>(t => t.ID.In(ids))
                .Execute()
                .ExecuteNonQuery();
        }

        public List<Model.Officer> Page(int index, int size, out int records)
        {
            var list = Paging<Model.Officer>(index, size, null, OrderType.Desc, out records, string.Format("{0}.ID", GetTableName<Model.Officer>()));
            GetDepartments(ref list);
            return list;
        }

        public List<Model.Officer> GetEntities()
        {
            var query = SelectHandler.From<Model.Officer>();
            var list = ExecuteList<Model.Officer>(query.Execute().ExecuteDataReader());
            GetDepartments(ref list);
            return list;
        }

        private void GetDepartments(ref List<Model.Officer> list)
        {
            if (list.Count == 0)
                return;

            var ids = (from t in list select t.DeptID.ToString()).ToArray();
            var depts = DepartmentHandler.Handler.GetDepartments(ids);
            list.ForEach(x =>
            {
                x.Department = depts.FirstOrDefault(t => t.ID == x.DeptID);
            });
        }
    }
}
