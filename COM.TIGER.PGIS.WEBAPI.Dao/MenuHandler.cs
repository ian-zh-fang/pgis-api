using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    /// <summary>
    /// 菜单处理程序
    /// </summary>
    public class MenuHandler : DBase
    {
        private static MenuHandler _handler = null;
        /// <summary>
        /// 菜单处理程序
        /// </summary>
        public static MenuHandler Handler
        {
            get { return (_handler = _handler ?? new MenuHandler()); }
        }
        private MenuHandler() { }

        /// <summary>
        /// 获取所有的菜单信息
        /// </summary>
        /// <returns></returns>
        public List<Model.Menu> GetMenus()
        {
            var query = SelectHandler.Columns().From<Model.Menu>().Execute();
            return ExecuteList<Model.Menu>(query.ExecuteDataReader());
        }

        /// <summary>
        /// 获取树形式的菜单信息
        /// </summary>
        /// <returns></returns>
        public List<Model.Menu> GetMenusTree()
        {
            var items = GetMenus();
            var topMenus = items.Where(t => t.PID == 0 || t.PID == null).Where(t=>t.Disabled==1).ToList();
            foreach (var t in topMenus)
            {
                t.AddRange(items);
            }
            return topMenus;
        }

        /// <summary>
        /// 获取所有顶级菜单信息
        /// </summary>
        /// <returns></returns>
        public List<Model.Menu> GetTopMenus()
        {
            var query = SelectHandler.From<Model.Menu>();
            var handler = query.IQueryWhere.Where<Model.Menu>(t => 
                (t.PID == null || t.PID == 0) && t.Disabled == 1).Where<IDao.ISelect>(query).Execute();
            return ExecuteList<Model.Menu>(handler.ExecuteDataReader());
        }


        public List<Model.Menu> GetMenusOnRoles(params string[] ids)
        {
            var tname = GetTableName<Model.Menu>();
            var rname = GetTableName<Model.RoleMenu>();
            var query = SelectHandler.From<Model.Menu>()
                .Join(JoinType.Inner, rname).On(string.Format("({0}.ID = {1}.MenuID) or ({0}.PID = {1}.MenuID)", tname, rname))
                .Where<Model.RoleMenu>(t => t.RoleID.In(ids))
                .Where<Model.Menu>(t => t.Disabled == 1);

            return ExecuteList<Model.Menu>(query.Execute().ExecuteDataReader());
        }


        public List<Model.Menu> GetMenusOnUser(int id)
        {
            var tname = GetTableName<Model.Menu>();
            var uname = GetTableName<Model.UserRole>();
            var rname = GetTableName<Model.RoleMenu>();

            var query = SelectHandler.From<Model.Menu>()
                .Join(JoinType.Inner, rname).On(string.Format("({0}.ID = {1}.MenuID) or ({0}.PID = {1}.MenuID)", tname, rname))
                .Join(JoinType.Inner, uname).On(string.Format("{0}.RoleID = {1}.RoleID", rname, uname))
                .Where<Model.UserRole>(t => t.UserID == id)
                .Where<Model.Menu>(t => t.Disabled == 1 );

            return ExecuteList<Model.Menu>(query.Execute().ExecuteDataReader());
        }

        /// <summary>
        /// 分页获取顶级菜单信息
        /// </summary>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="records">当前查询总条目数</param>
        /// <returns></returns>
        public List<Model.Menu> PagingTopMenus(int index, int size, out int records)
        {
            return Paging<Model.Menu>(index, size,
                t => (t.PID == null || t.PID == 0) , 
                OrderType.Desc, out records, "pgis_menu.id");
        }
        /// <summary>
        /// 分页获取顶级菜单信息
        /// </summary>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="sortfield">DESC方式排序字段，按顺序</param>
        /// <param name="records">当前查询总条目数</param>
        /// <returns></returns>
        public List<Model.Menu> PagingTopMenus(int index, int size,string sortfield, out int records)
        {
            return Paging<Model.Menu>(index, size,
                t => (t.PID == null || t.PID == 0) ,
                OrderType.Desc, out records, sortfield);
        }

        /// <summary>
        /// 获取指定ID的菜单信息
        /// <para>该方法会根据第二个参数决定是否获取当前菜单的子菜单信息</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public Model.Menu GetEntity(int id, bool flag)
        {
            if (flag) return GetMenu(id);

            var query = SelectHandler.From<Model.Menu>();
            var handler = query.IQueryWhere.Where<Model.Menu>(t =>
                t.ID == id && t.Disabled == 1).Where<IDao.ISelect>(query).Execute();
            return ExecuteEntity<Model.Menu>(handler.ExecuteDataReader());
        }

        /// <summary>
        /// 获取指定ID的菜单信息
        /// <para>该方法会获取当前菜单的子菜单信息</para>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private Model.Menu GetMenu(int id)
        {
            var list = GetMenusByID(id);
            if (list.Count == 0) return null;
            var entity = list.First(t => t.ID == id);
            entity.AddRange(list);
            return entity;
        }

        /// <summary>
        /// 获取指定ID的菜单信息。
        /// <para>该方法总是会获取当前菜单的子菜单信息，并将会根据第二个参数确定返回值的对象：</para>
        /// <para>若为false，返回包含子菜单信息和当前菜单信息的实体集合，返回一组数据对象；</para>
        /// <para>若为true，返回当前菜单信息，子菜单信息将保存在当前菜单信息的子菜单信息字段内，返回一个数据对象。</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public object GetMenusByID(int id, bool flag)
        {
            if (flag) return GetMenu(id);

            return GetMenusByID(id);
        }

        /// <summary>
        /// 获取指定ID的菜单信息
        /// <para>该方法总是会获取当前菜单的子菜单信息</para>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Model.Menu> GetMenusByID(int id)
        {
            var query = SelectHandler.From<Model.Menu>();
            var handler = query.IQueryWhere.Where<Model.Menu>(x =>
                (x.ID == id || x.PID == id) && x.Disabled == 1).Where<IDao.ISelect>(query).Execute();
            var list = ExecuteList<Model.Menu>(handler.ExecuteDataReader());
            return list;
        }

        /// <summary>
        /// 获取指定ID的子菜单信息，返回子菜单集合
        /// <para>该方法总是会获取子菜单信息，并根据第二个参数确认是否获取当前菜单信息：</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="flag">如果需要获取当前菜单信息，该参数必须为TRUE，否者为FALSE</param>
        /// <returns></returns>
        public List<Model.Menu> GetMenus(int id, bool flag)
        {
            if (flag) return GetMenusByID(id);

            var query = SelectHandler.From<Model.Menu>();
            var handler = query.IQueryWhere.Where<Model.Menu>(t => t.PID == id ).Where<IDao.ISelect>(query).Execute();
            return ExecuteList<Model.Menu>(handler.ExecuteDataReader());
        }

        /// <summary>
        /// 添加新的菜单信息
        /// <para>返回值大于0，标识命令执行成功，数据操作成功</para>
        /// <para>返回值等于0，标识命令执行成功，数据操作失败</para>
        /// <para>返回值等于-1，返回命令执行失败</para>
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int InsertNew(Model.Menu e)
        {
            if (null != GetEntity<Model.Menu>(t => t.Code == e.Code))
                return -2;

            var query = InsertHandler.Into<Model.Menu>()
                .Table("Checked", "Code", "Disabled", "Handler", "Iconcls", "Description", "PID", "Sort", "Text")
                .Values(e.Checked, e.Code, e.Disabled, e.Handler, e.Iconcls, e.Description, e.PID, e.Sort, e.Text);
            return query.Execute().ExecuteNonQuery();
        }

        /// <summary>
        /// 添加新的菜单信息
        ///<para>执行成功，返回当前信息</para>
        ///<para>执行失败，返回NULL</para>
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public Model.Menu InsertEntity(Model.Menu e)
        {
            if (InsertNew(e) > 0)
            {
                e = GetEntity<Model.Menu>(t => (t.Text == e.Text)
                    && (t.Sort == e.Sort)
                    && (t.PID == e.PID)
                    && (t.Code == e.Code)
                    && (t.Iconcls == e.Iconcls)
                    && (t.Handler == e.Handler)
                    && (t.Disabled == e.Disabled)
                    && (t.Description == e.Description)
                    && (t.Checked == e.Checked));
                return e;
            }
            return null;
        }

        /// <summary>
        /// 更改指定ID的菜单信息
        /// <para>返回值大于0，标识命令执行成功，数据操作成功</para>
        /// <para>返回值等于0，标识命令执行成功，数据操作失败</para>
        /// <para>返回值等于-1，返回命令执行失败</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public int UpdateNew(int id, Model.Menu e)
        {
            if (null != GetEntity<Model.Menu>(t => t.Code == e.Code && t.ID != e.ID))
                return -2;

            var query = UpdateHandler.Table<Model.Menu>()
                .Set("Checked").EqualTo(e.Checked)
                .Set("Description").EqualTo(e.Description)
                .Set("Disabled").EqualTo(e.Disabled)
                .Set("Handler").EqualTo(e.Handler)
                .Set("Iconcls").EqualTo(e.Iconcls)
                .Set("Code").EqualTo(e.Code)
                .Set("PID").EqualTo(e.PID)
                .Set("Sort").EqualTo(e.Sort)
                .Set("Text").EqualTo(e.Text);
            var handler = query.IQueryWhere.Where<Model.Menu>(t => t.ID == id).Where<IDao.IUpdate>(query).Execute();
            return handler.ExecuteNonQuery();
        }

        /// <summary>
        /// 更改指定ID的菜单信息
        ///<para>执行成功，返回当前信息</para>
        ///<para>执行失败，返回NULL</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public Model.Menu UpdateEntity(int id, Model.Menu e)
        {
            if (UpdateNew(id, e) > 0)
            {
                e.ID = id;
                return e;
            }
            return null;
        }

        /// <summary>
        /// 移除指定的菜单信息
        /// <para>返回值大于0，标识命令执行成功，数据操作成功</para>
        /// <para>返回值等于0，标识命令执行成功，数据操作失败</para>
        /// <para>返回值等于-1，返回命令执行失败</para>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteEntity(int id)
        {
            return DeleteOperate<Model.Menu>(t => t.ID == id || t.PID == id);
        }

        /// <summary>
        /// 批量删除指定ID的记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public override int DeleteEntities(params string[] ids)
        {
            if (ids.Length == 0) return 0;
            return DeleteOperate<Model.Menu>(t => t.ID.In(ids) || t.PID.In(ids));
        }
    }
}
