using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    /// <summary>
    /// 组织机构处理程序
    /// </summary>
    public class DepartmentHandler:DBase
    {
        private static DepartmentHandler _handler = null;
        /// <summary>
        /// 组织机构处理程序唯一实例
        /// </summary>
        public static DepartmentHandler Handler
        {
            get { return (_handler = _handler ?? new DepartmentHandler()); }
        }
        private DepartmentHandler() { }

        /// <summary>
        /// 获取所有组织机构信息
        /// </summary>
        /// <returns></returns>
        public List<Model.Department> GetDepartments()
        {
            var handler = SelectHandler.From<Model.Department>().Execute();
            return ExecuteList<Model.Department>(handler.ExecuteDataReader());
        }

        /// <summary>
        /// 批量获取部门
        /// </summary>
        /// <param name="ids">ID组</param>
        /// <returns></returns>
        public List<Model.Department> GetDepartments(string[] ids)
        {
            if(ids == null) return new List<Model.Department>();
            if(ids.Length == 0) return new List<Model.Department>();
            var query = SelectHandler.From<Model.Department>();
            var handler = query.IQueryWhere.Where<Model.Department>(t => t.ID.In(ids)).Where<IDao.ISelect>(query).Execute();
            return ExecuteList<Model.Department>(handler.ExecuteDataReader());
        }

        /// <summary>
        /// 获取所有部门信息的树形式数据
        /// </summary>
        /// <returns></returns>
        public List<Model.Department> GetDepartmentsTree()
        {
            var items = GetDepartments();
            var arr = items.Where(t => t.PID == 0 || t.PID == null).ToList();
            foreach (var t in arr)
            {
                t.AddRange(items);
            }
            return arr;
        }

        /// <summary>
        /// 获取所有顶级组织结构信息
        /// </summary>
        /// <returns></returns>
        public List<Model.Department> GetTopDepartments()
        {
            var query = SelectHandler.From<Model.Department>();
            var handler = query.IQueryWhere.Where<Model.Department>(t => t.PID == null || t.PID == 0).Where<IDao.ISelect>(query).Execute();
            return ExecuteList<Model.Department>(handler.ExecuteDataReader());
        }

        /// <summary>
        /// 分页获取组织机构信息
        /// </summary>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="records">组织机构数量</param>
        /// <returns></returns>
        public List<Model.Department> PagingDepartment(int index, int size, out int records)
        {
            return Paging<Model.Department>(index, size, null, OrderType.Desc, out records, "pgis_department.id");
        }

        /// <summary>
        /// 分页获取顶级组织机构信息
        /// </summary>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="records">顶级组织机构数量</param>
        /// <returns></returns>
        public List<Model.Department> PagingTopDepartment(int index, int size, out int records)
        {
            return Paging<Model.Department>(index, size,
                t => t.PID == null || t.PID == 0, OrderType.Desc, out records, "pgis_department.id");
        }
        
        /// <summary>
        /// 获取指定ID的组织机构信息
        /// <para>该方法会依据第二个参数决定是否获取子机构信息，</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public Model.Department GetEntity(int id, bool flag)
        {
            if (flag) return GetDepartment(id);

            var query = SelectHandler.From<Model.Department>();
            var handler = query.IQueryWhere.Where<Model.Department>(t => t.ID == id).Where<IDao.ISelect>(query).Execute();
            return ExecuteEntity<Model.Department>(handler.ExecuteDataReader());
        }

        /// <summary>
        /// 获取指定ID的组织机构信息
        /// <para>该方法会自动获取当前组织机构下的子机构信息</para>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private Model.Department GetDepartment(int id)
        {
            var list = GetDepartmentsByID(id);
            if (list.Count == 0) return null;

            var entity = list.First(t => t.ID == id);
            entity.AddRange(list);
            return entity;
        }

        /// <summary>
        /// 获取指定ID的组织机构信息。
        /// <para>该方法总是会获取当前组织机构的子机构信息，并将会根据第二个参数确定返回值的对象：</para>
        /// <para>若为false，返回包含子机构信息和当前组织机构信息的实体集合，返回一组数据对象；</para>
        /// <para>若为true，返回当前组织机构信息，子机构信息将保存在当前组织机构信息的子机构信息字段内，返回一个数据对象。</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="flag">确定返回值的对象</param>
        /// <returns></returns>
        public object GetDepartmentsByID(int id, bool flag)
        {
            if (flag) return GetDepartment(id);

            return GetDepartmentsByID(id);
        }

        /// <summary>
        /// 获取指定ID的组织机构信息
        /// <para>该方法总是会获取当前组织机构的子机构部门</para>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Model.Department> GetDepartmentsByID(int id)
        {
            var query = SelectHandler.From<Model.Department>();
            var handler = query.IQueryWhere.Where<Model.Department>(t => t.ID == id || t.PID == id).Where<IDao.ISelect>(query).Execute();
            var list = ExecuteList<Model.Department>(handler.ExecuteDataReader());
            return list;
        }

        /// <summary>
        /// 获取指定ID的子机构信息，返回子机构集合
        /// <para>该方法总是会获取子机构信息，并根据第二个参数确认是否获取当前机构信息：</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="flag">如果需要获取当前机构信息，该参数必须为TRUE，否者为FALSE</param>
        /// <returns></returns>
        public List<Model.Department> GetDepartments(int id, bool flag)
        {
            if (flag) return GetDepartmentsByID(id);

            var query = SelectHandler.From<Model.Department>();
            var handler = query.IQueryWhere.Where<Model.Department>(t => (t.PID == id)).Where<IDao.ISelect>(query).Execute();
            return ExecuteList<Model.Department>(handler.ExecuteDataReader());
        }

        /// <summary>
        /// 添加新的机构信息
        /// <para>返回值大于0，标识命令执行成功，数据更新成功</para>
        /// <para>返回值等于0，标识命令执行成功，数据更新失败</para>
        /// <para>返回值等于-1，返回命令执行失败</para>
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int InsertNew(Model.Department e)
        {
            if (null != GetEntity<Model.Department>(t => t.Code == e.Code))
                return -2;

            var query = InsertHandler.Into<Model.Department>();
            var handler = query.Table("Code", "Name", "PID", "Remarks")
                .Values(e.Code, e.Name, e.PID, e.Remarks);
            return handler.Execute().ExecuteNonQuery();
        }

        /// <summary>
        /// 添加新的机构信息
        ///<para>执行成功，返回新增信息</para>
        ///<para>执行失败，返回NULL</para>
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public Model.Department InsertEntity(Model.Department e)
        {
            if (InsertNew(e) > 0)
            {
                var o = GetEntity<Model.Department>(t => (t.Code == e.Code)
                    && (t.Name == e.Name)
                    && (t.PID == e.PID)
                    && (t.Remarks == e.Remarks));
                return o;
            }
            return null;
        }

        /// <summary>
        /// 更新指定ID的机构信息
        /// <para>返回值大于0，标识命令执行成功，数据更新成功</para>
        /// <para>返回值等于0，标识命令执行成功，数据更新失败</para>
        /// <para>返回值等于-1，返回命令执行失败</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="e">当前机构的新信息</param>
        /// <returns></returns>
        public int UpdateNew(int id, Model.Department e)
        {
            if (null != GetEntity<Model.Department>(t => t.Code == e.Code && t.ID != e.ID))
                return -2;

            var query = UpdateHandler.Table<Model.Department>()
                .Set("Code").EqualTo(e.Code)
                .Set("Name").EqualTo(e.Name)
                .Set("PID").EqualTo(e.PID)
                .Set("Remarks").EqualTo(e.Remarks);

            var handler = query.IQueryWhere.Where<Model.Department>(t => t.ID == id).Where<IDao.IUpdate>(query).Execute();
            return handler.ExecuteNonQuery();
        }

        /// <summary>
        ///更新指定ID的机构信息
        ///<para>执行成功，返回当前机构的信息</para>
        ///<para>执行失败，返回NULL</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="e">当前机构的新信息</param>
        /// <returns></returns>
        public Model.Department UpdateEntity(int id, Model.Department e)
        {
            if (UpdateNew(id, e) > 0)
            {
                e.ID = id;
                return e;
            }

            return null;
        }

        /// <summary>
        /// 移除指定ID的机构信息
        /// <para>返回值大于0，标识命令执行成功，数据操作成功</para>
        /// <para>返回值等于0，标识命令执行成功，数据操作失败</para>
        /// <para>返回值等于-1，返回命令执行失败</para>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteEntity(int id)
        {
            return DeleteOperate<Model.Department>(t => t.ID == id);
        }

        /// <summary>
        /// 批量删除指定ID的记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public override int DeleteEntities(params string[] ids)
        {
            if (ids.Length == 0) return 0;
            return DeleteOperate<Model.Department>(t => t.ID.In(ids));
        }
    }
}
