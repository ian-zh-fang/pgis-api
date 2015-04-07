using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    /// <summary>
    /// 用户信息处理程序
    /// </summary>
    public class UserHandler:DBase
    {
        private static UserHandler _handler = null;
        /// <summary>
        /// 用户信息处理程序唯一实例
        /// </summary>
        public static UserHandler Handler
        {
            get { return (_handler = _handler ?? new UserHandler()); }
        }
        private UserHandler() { }

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        public List<Model.User> GetUsers()
        {
            var handler = SelectHandler.From<Model.User>().Execute();
            var data = ExecuteList<Model.User>(handler.ExecuteDataReader());
            return GetUserDepartment(data);
        }

        /// <summary>
        /// 分页获取所有用户信息
        /// </summary>
        /// <param name="index">当前野马</param>
        /// <param name="size">每页条目数</param>
        /// <param name="records">所有用户条目数</param>
        /// <returns></returns>
        public List<Model.User> PagingUsers(int index, int size, out int records)
        {
            var data = Paging<Model.User>(index, size, null, OrderType.Desc, out records, "pgis_user.id");
            return GetUserDepartment(data);
        }

        /// <summary>
        /// 获取指定组织机构的所有用户信息
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        public List<Model.User> GetDepartmentUsers(int departmentID)
        {
            return GetUsers(t => t.DepartmentID == departmentID);
        }

        /// <summary>
        /// 获取当前查询条件下的所有用户信息
        /// </summary>
        /// <param name="expression">查询条件</param>
        /// <returns></returns>
        public  List<Model.User> GetUsers(System.Linq.Expressions.Expression<Func<Model.User, bool>> expression)
        {
            var query = SelectHandler.From<Model.User>();
            var handler = query.IQueryWhere.Where<Model.User>(expression).Where<IDao.ISelect>(query).Execute();
            var data = ExecuteList<Model.User>(handler.ExecuteDataReader());
            return GetUserDepartment(data);
        }

        /// <summary>
        /// 获取拥有指定角色的所有用户信息
        /// </summary>
        /// <param name="roleid">角色标识</param>
        /// <returns></returns>
        public List<Model.User> GetRoleUser(int roleid)
        {
            var rquery = SelectHandler.Columns("pgis_userrole.userid").From("pgis_userrole");
            var rhandler = rquery.IQueryWhere.Where(string.Format("pgis_userrole.roleid = {0}", roleid)).Where<IDao.ISelect>(rquery).Execute();
            var query = SelectHandler.From<Model.User>();
            var handler = query.IQueryWhere.Where(string.Format("pgis_user.id in({0})", rhandler.CommandText)).Where<IDao.ISelect>(query).Execute();
            var data = ExecuteList<Model.User>(handler.ExecuteDataReader());
            return GetUserDepartment(data);
        }

        /// <summary>
        /// 获取指定ID的用户信息
        /// </summary>
        /// <param name="id">用户标识</param>
        /// <returns></returns>
        public Model.User GetEntity(int id)
        {
            var user = GetEntity(t => t.ID == id);
            GetUserExtention(ref user);
            return user;
        }

        /// <summary>
        /// 获取指定账户名称的用户信息
        /// </summary>
        /// <param name="username">登陆账户</param>
        /// <returns></returns>
        public Model.User GetEntity(string username)
        {
            return GetEntity(t => t.UserName == username);
        }

        /// <summary>
        /// 获取指定查询条件下的用户信息。
        /// <para>如果当前查询条件下存在多个匹配值，自动返回首条匹配值</para>
        /// </summary>
        /// <param name="expression">查询条件</param>
        /// <returns></returns>
        public Model.User GetEntity(System.Linq.Expressions.Expression<Func<Model.User, bool>> expression)
        {
            var query = SelectHandler.From<Model.User>();
            var handler = query.IQueryWhere.Where<Model.User>(expression).Where<IDao.ISelect>(query).Execute();
            var data = ExecuteEntity<Model.User>(handler.ExecuteDataReader());
            data.Department = DepartmentHandler.Handler.GetEntity(data.DepartmentID, false);
            return data;
        }

        /// <summary>
        /// 添加新的用户信息
        /// <para>返回值大于0，标识命令执行成功，数据操作成功</para>
        /// <para>返回值等于0，标识命令执行成功，数据操作失败</para>
        /// <para>返回值等于-1，返回命令执行失败</para>
        /// <para>返回值等于-2，返回账户重名</para>
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int InsertNew(Model.User e)
        {
            if (GetEntity<Model.User>(t => t.UserName == e.UserName) != null)
                return -2;

            var query = InsertHandler.Into<Model.User>()
                .Table("Code", "DepartmentID", "Disabled", "Gender", "Lvl", "Name", "Password", "UserName", "OfficerID")
                .Values(e.Code, e.DepartmentID, e.Disabled, e.Gender, e.Lvl, e.Name, e.Password, e.UserName, e.OfficerID);
            return query.Execute().ExecuteNonQuery();
        }

        /// <summary>
        /// 添加新的用户信息
        ///<para>执行成功，返回当前信息</para>
        ///<para>执行失败，返回NULL</para>
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public Model.User InsertEntity(Model.User e)
        {
            if (InsertNew(e) > 0)
            {
                e = GetEntity<Model.User>(t => (t.UserName == e.UserName)
                    && (t.Password == e.Password)
                    && (t.Name == e.Name)
                    && (t.Lvl == e.Lvl)
                    && (t.Gender == e.Gender)
                    && (t.Disabled == e.Disabled)
                    && (t.DepartmentID == e.DepartmentID)
                    && (t.Code == e.Code));
                return e;
            }
            return null;
        }

        /// <summary>
        /// 更改指定ID的用户信息
        /// <para>返回值大于0，标识命令执行成功，数据操作成功</para>
        /// <para>返回值等于0，标识命令执行成功，数据操作失败</para>
        /// <para>返回值等于-1，返回命令执行失败</para>
        /// <para>返回值等于-2，返回账户重名</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public int UpdateNew(int id, Model.User e)
        {
            //当账户存在，并且ID不一致时，标识账户名称重复
            if (GetEntity<Model.User>(t => t.UserName == e.UserName && t.ID != e.ID) != null)
                return -2;

            var query = UpdateHandler.Table<Model.User>()
                .Set("Code").EqualTo(e.Code)
                .Set("Name").EqualTo(e.Name)
                .Set("Disabled").EqualTo(e.Disabled)
                .Set("DepartmentID").EqualTo(e.DepartmentID)
                .Set("Gender").EqualTo(e.Gender)
                .Set("Lvl").EqualTo(e.Lvl)
                .Set("Password").EqualTo(e.Password)
                .Set("OfficerID").EqualTo(e.OfficerID)
                .Set("UserName").EqualTo(e.UserName);
            var handler = query.IQueryWhere.Where<Model.User>(t => t.ID == id).Where<IDao.IUpdate>(query).Execute();
            return handler.ExecuteNonQuery();
        }

        /// <summary>
        /// 更改指定ID的用户信息
        ///<para>执行成功，返回当前信息</para>
        ///<para>执行失败，返回NULL</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public Model.User UpdateEntity(int id, Model.User e)
        {
            if (UpdateNew(id, e) > 0)
            {
                e.ID = id;
                return e;
            }
            return null;
        }

        /// <summary>
        /// 移除指定的用户信息
        /// <para>返回值大于0，标识命令执行成功，数据操作成功</para>
        /// <para>返回值等于0，标识命令执行成功，数据操作失败</para>
        /// <para>返回值等于-1，返回命令执行失败</para>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteEntity(int id)
        {
            return DeleteOperate<Model.User>(t => t.ID == id);
        }

        /// <summary>
        /// 批量删除指定ID的记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public override int DeleteEntities(params string[] ids)
        {
            if (ids.Length == 0) return 0;
            return DeleteOperate<Model.User>(t => t.ID.In(ids));
        }

        /// <summary>
        /// 获取指定角色的菜单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Model.UserRole> GetUserRoles(int id)
        {
            var query = SelectHandler.From<Model.UserRole>();
            var handler = query.IQueryWhere.Where<Model.UserRole>(t => t.UserID == id).Where<IDao.ISelect>(query).Execute();
            return ExecuteList<Model.UserRole>(handler.ExecuteDataReader());
        }

        /// <summary>
        /// 保存角色的新材电脑信息
        /// <para>该方法采用事务处理方式，事务前置条件达成，事务提交，并返回新增行数；否者，事务回滚，并返回-1。</para>
        /// <para>前置条件一：删除当前角色的以前的菜单信息，删除失败，事务回滚，并返回-1；成功，执行前置条件二。</para>
        /// <para>前置条件二：保存新的角色菜单信息，保存失败，事务回滚，并返回-1；成功，提交事务，并返回添加的行数。</para>
        /// </summary>
        /// <param name="id">操作指定的角色ID</param>
        /// <param name="menuids">菜单ID组</param>
        /// <returns></returns>
        public int SaveUserRoles(int id, params string[] menuids)
        {
            try
            {
                using (var tran = new System.Transactions.TransactionScope())
                {
                    //首先移除以前的角色菜单信息，
                    if (DeleteRoleMenus(id) < 0) return -1;
                    //其次添加新的角色菜单信息
                    var r = SaveMenus(id, menuids);
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
        /// 修改用户资料
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="identityid"></param>
        /// <param name="tel"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        public int ChangeInfo(int id, string name, string identityid, string tel, int gender)
        {
            System.Transactions.TransactionScope tran = null;
            try
            {
                tran = new System.Transactions.TransactionScope();

                //@ 更新用户信息
                var rst = UpdateHandler.Table<Model.User>()
                    .Set("Name").EqualTo(name)
                    .Set("Gender").EqualTo(gender)
                    .Where<Model.User>(t => t.ID == id)
                    .Execute().ExecuteNonQuery();

                //@ 更新警员信息
                var officerid = SelectHandler.Columns(string.Format("{0}.officerid", GetTableName<Model.User>())).From<Model.User>().Where<Model.User>(t => t.ID == id).CommandText;
                var quey = UpdateHandler.Table<Model.Officer>()
                        .Set("Name").EqualTo(name)
                        .Set("IdentityID").EqualTo(identityid)
                        .Set("Tel").EqualTo(tel)
                        .Where(string.Format("{0}.ID in ({1})", GetTableName<Model.Officer>(), officerid));
                rst += quey.Execute().ExecuteNonQuery();

                tran.Complete();
                return rst;
            }
            catch (Exception) { return -1; }
            finally 
            {
                if (tran != null)
                    tran.Dispose();
            }
        }

        /// <summary>
        /// 移除指定角色ID的所有菜单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private int DeleteRoleMenus(int id)
        {
            var query = DeleteHandler.From<Model.UserRole>();
            var handler = query.IQueryWhere.Where<Model.UserRole>(t => t.UserID == id).Where<IDao.IDelete>(query).Execute();
            return handler.ExecuteNonQuery();
        }

        /// <summary>
        /// 保存指定角色ID的菜单信息组
        /// </summary>
        /// <param name="id"></param>
        /// <param name="menuids"></param>
        /// <returns></returns>
        private int SaveMenus(int id, string[] menuids)
        {
            var query = InsertHandler.Into<Model.UserRole>();
            var r = 0;
            for (var i = 0; i < menuids.Length; i++)
            {
                var handler = query.Table("userid", "roleid").Values(id, menuids[i]).Execute();
                if (handler.ExecuteNonQuery() < 0)
                    return -1;
                r++;
            }
            return r;
        }

        /// <summary>
        /// 设置用户部门信息
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        private List<Model.User> GetUserDepartment(List<Model.User> items)
        {
            var ids = (from t in items select t.DepartmentID.ToString()).ToArray();
            var departments = DepartmentHandler.Handler.GetDepartments(ids);
            items.ForEach(t => t.Department = departments.FirstOrDefault(x => x.ID == t.DepartmentID));
            return items;
        }

        /// <summary>
        /// 根据账户和密码，或者警员编号和密码，或者警员身份证和密码查询用户
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Model.User GetUsersOnUserNameOrOfficerNum(string username, string password)
        {
            var x_user = GetTableName<Model.User>();
            var x_officer = GetTableName<Model.Officer>();
            var query = SelectHandler.From<Model.User>()
                .Join(JoinType.Left, x_officer).On(string.Format("{0}.officerid = {1}.id", x_user, x_officer))
                .Where<Model.User>(t => t.Password == password)
                .Where(string.Format("({0}.username = '{1}') OR ({2}.Num = '{1}') OR ({2}.IdentityID = '{1}')", x_user, username, x_officer));
            
            var user = ExecuteEntity<Model.User>(query.Execute().ExecuteDataReader());
            GetUserExtention(ref user);
            return user;
        }

        /// <summary>
        /// 修改指定用户的密码
        /// </summary>
        /// <param name="password"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int ChangePassword(string password, int id)
        {
            if (string.IsNullOrWhiteSpace(password)) return -1;
            if (id == 0) return -1;

            return UpdateHandler.Table<Model.User>()
                .Set("password").EqualTo(password)
                .Where<Model.User>(t => t.ID == id)
                .Execute().ExecuteNonQuery() ;
        }

        /// <summary>
        /// 获取用户的部门，警员信息等等
        /// </summary>
        /// <param name="user"></param>
        private void GetUserExtention(ref Model.User user)
        {
            if (user != null)
            {
                var officeid = user.OfficerID;
                user.Officer = GetEntity<Model.Officer>(t => t.ID == officeid);
                var deptid = user.DepartmentID;
                user.Department = GetEntity<Model.Department>(t => t.ID == deptid);
            }
        }
    }
}
