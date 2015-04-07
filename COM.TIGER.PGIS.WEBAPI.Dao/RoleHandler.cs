using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    /// <summary>
    /// 角色信息处理程序
    /// </summary>
    public class RoleHandler:DBase
    {
        private static RoleHandler _handler = null;
        /// <summary>
        /// 角色信息处理程序唯一实例
        /// </summary>
        public static RoleHandler Handler
        {
            get { return (_handler = _handler ?? new RoleHandler()); }
        }
        private RoleHandler() { }

        /// <summary>
        /// 获取所有角色信息
        /// </summary>
        /// <returns></returns>
        public List<Model.Role> GetRoles()
        {
            var handler = SelectHandler.From<Model.Role>().Execute();
            return ExecuteList<Model.Role>(handler.ExecuteDataReader());
        }

        /// <summary>
        /// 分页获取所有的角色嘻嘻你
        /// </summary>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="records">角色数量</param>
        /// <returns></returns>
        public List<Model.Role> PagingRoles(int index, int size, out int records)
        {
            return Paging<Model.Role>(index, size, null, OrderType.Desc, out records, "pgis_role.id");
        }

        /// <summary>
        /// 获取指定用户的所有角色信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<Model.Role> GetUserRole(int userid)
        {
            var uquery = SelectHandler.Columns("pgis_userrole.roleid").From("pgis_userrole");
            var uhandler = uquery.IQueryWhere.Where(string.Format("pgis_userrole.userid = {0}", userid)).Where<IDao.ISelect>(uquery).Execute();
            var query = SelectHandler.From<Model.Role>();
            var handler = query.IQueryWhere.Where(string.Format("pgis_role.id in({0})", uhandler.CommandText)).Where<IDao.ISelect>(query).Execute();
            return ExecuteList<Model.Role>(handler.ExecuteDataReader());
        }

        /// <summary>
        /// 获取指定ID的角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.Role GetEntity(int id)
        {
            var query = SelectHandler.From<Model.Role>();
            var handler = query.IQueryWhere.Where<Model.Role>(t => t.ID == id).Where<IDao.ISelect>(query).Execute();
            return ExecuteEntity<Model.Role>(handler.ExecuteDataReader());
        }

        /// <summary>
        /// 添加新的角色信息
        /// <para>返回值大于0，标识命令执行成功，数据操作成功</para>
        /// <para>返回值等于0，标识命令执行成功，数据操作失败</para>
        /// <para>返回值等于-1，返回命令执行失败</para>
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int InsertNew(Model.Role e)
        {
            var query = InsertHandler.Into<Model.Role>()
                .Table("Remarks", "Name")
                .Values(e.Remarks, e.Name);
            return query.Execute().ExecuteNonQuery();
        }

        /// <summary>
        /// 添加新的角色信息
        ///<para>执行成功，返回当前信息</para>
        ///<para>执行失败，返回NULL</para>
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public Model.Role InsertEntity(Model.Role e)
        {
            if (InsertNew(e) > 0)
            {
                e = GetEntity<Model.Role>(t => (t.Name == e.Name)
                    && (t.Remarks == e.Remarks));
                return e;
            }
            return null;
        }

        /// <summary>
        /// 更改指定ID的角色信息
        /// <para>返回值大于0，标识命令执行成功，数据操作成功</para>
        /// <para>返回值等于0，标识命令执行成功，数据操作失败</para>
        /// <para>返回值等于-1，返回命令执行失败</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public int UpdateNew(int id, Model.Role e)
        {
            var query = UpdateHandler.Table<Model.Role>()
                .Set("Remarks").EqualTo(e.Remarks)
                .Set("Name").EqualTo(e.Name);
            var handler = query.IQueryWhere.Where<Model.Role>(t => t.ID == id).Where<IDao.IUpdate>(query).Execute();
            return handler.ExecuteNonQuery();
        }

        /// <summary>
        /// 更改指定ID的角色信息
        ///<para>执行成功，返回当前信息</para>
        ///<para>执行失败，返回NULL</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public Model.Role UpdateEntity(int id, Model.Role e)
        {
            if (UpdateNew(id, e) > 0)
            {
                e.ID = id;
                return e;
            }
            return null;
        }

        /// <summary>
        /// 移除指定的角色信息
        /// <para>返回值大于0，标识命令执行成功，数据操作成功</para>
        /// <para>返回值等于0，标识命令执行成功，数据操作失败</para>
        /// <para>返回值等于-1，返回命令执行失败</para>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteEntity(int id)
        {
            return DeleteOperate<Model.Role>(t => t.ID == id);
        }

        /// <summary>
        /// 批量删除指定ID的记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public override int DeleteEntities(params string[] ids)
        {
            if (ids.Length == 0) return 0;
            return DeleteOperate<Model.Role>(t => t.ID.In(ids));
        }

        /// <summary>
        /// 获取指定角色的菜单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Model.RoleMenu> GetRoleMenus(int id)
        {
            var query = SelectHandler.From<Model.RoleMenu>();
            var handler = query.IQueryWhere.Where<Model.RoleMenu>(t => t.RoleID == id).Where<IDao.ISelect>(query).Execute();
            return ExecuteList<Model.RoleMenu>(handler.ExecuteDataReader());
        }

        /// <summary>
        /// 保存角色的新材电脑信息
        /// <para>该方法采用事务处理方式，事务前置条件达成，事务提交，并返回新增行数；否者，事务回滚，并返回-1。</para>
        /// <para>前置条件一：删除当前角色的以前的菜单信息，删除失败，事务回滚，并返回-1；成功，执行前置条件二。</para>
        /// <para>前置条件二：保存新的角色菜单信息，保存失败，事务回滚，并返回-1；成功，提交事务，并返回添加的行数。</para>
        /// </summary>
        /// <param name="roleid">操作指定的角色ID</param>
        /// <param name="menuids">菜单ID组</param>
        /// <returns></returns>
        public int SaveRoleMenus(int roleid, params string[] menuids)
        {
            try
            {
                using (var tran = new System.Transactions.TransactionScope())
                {
                    //首先移除以前的角色菜单信息，
                    if (DeleteRoleMenus(roleid) < 0) return -1;
                    //其次添加新的角色菜单信息
                    var r = SaveMenus(roleid, menuids);
                    //前置条件都成功，执行事物提交，否者事务回滚，并返回-1
                    if (r >= 0)
                    {
                        tran.Complete();
                        return r;
                    }
                    return -1;
                }
            }
            catch { return -1; }
        }

        /// <summary>
        /// 移除指定角色ID的所有菜单信息
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        private int DeleteRoleMenus(int roleid)
        {
            var query = DeleteHandler.From<Model.RoleMenu>();
            var handler = query.IQueryWhere.Where<Model.RoleMenu>(t => t.RoleID == roleid).Where<IDao.IDelete>(query).Execute();
            return handler.ExecuteNonQuery();
        }

        /// <summary>
        /// 保存指定角色ID的菜单信息组
        /// </summary>
        /// <param name="roleid"></param>
        /// <param name="menuids"></param>
        /// <returns></returns>
        private int SaveMenus(int roleid, string[] menuids)
        {
            var query = InsertHandler.Into<Model.RoleMenu>();
            var r = 0;
            for (var i = 0; i < menuids.Length; i++)
            {
                var handler = query.Table("roleid", "menuid").Values(roleid, menuids[i]).Execute();
                if (handler.ExecuteNonQuery() < 0)
                    return -1;
                r++;
            }
            return r;
        }
    }
}
