using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GISAPI = COM.TIGER.PGIS.WEBAPI;

namespace COM.TIGER.PGIS.WEBAPI.Dao.SqlServer
{
    public abstract class QBase
    {
        /// 功能类型，此处为Select模式
        protected string _orderType;
        /// 数据表名称
        protected string _tableName;

        protected string _commandText;

        protected IDao.IQueryWhere _where;

        /// <summary>
        /// 设置T-SQL语句数据表名称，并返回原型实例
        /// </summary>
        /// <typeparam name="T">原型</typeparam>
        /// <param name="t">原型实例</param>
        /// <param name="type">和数据表映射的类型</param>
        /// <returns></returns>
        protected virtual T Table<T>(T t, Type type) where T:QBase
        {
            t._tableName = string.Format("{0}{1}{2}", 
                GISAPI.Common.Attributes.AttributeHandler.GetPrefixName(type), 
                type.Name, 
                GISAPI.Common.Attributes.AttributeHandler.GetSuffixName(type));
            return t;
        }

        /// <summary>
        /// 设置T-SQL语句数据表名称，并返回原型实例
        /// </summary>
        /// <typeparam name="T">原型</typeparam>
        /// <param name="t">原型实例</param>
        /// <param name="tableName">数据表名称</param>
        /// <returns></returns>
        protected virtual T Table<T>(T t, string tableName) where T : QBase
        {
            if (string.IsNullOrWhiteSpace(tableName)) throw new ArgumentNullException();
            t._tableName = tableName;
            return t;
        }

        // 以下可以考虑通过模块化设计，提高系统的扩展性
        //=======================================================================

        /// <summary>
        /// 此方法提供数据库低层访问程序
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        protected virtual IDao.IDbase QExecute(string commandText)
        {
            var db = new IDao.Dbase(commandText);
            return db;
        }

        /// <summary>
        /// 此方法提供T-SQL语句WHERE子句条件
        /// </summary>
        /// <returns></returns>
        protected virtual IDao.IQueryWhere QWhere()
        {
            _where = _where ?? new IDao.QueryWhere();
            return _where;
        }

        // 扩展点结束
        //=======================================================================

        /// <summary>
        /// 获取T-SQL语句脚本
        /// </summary>
        protected abstract string ExecuteResult();
    }
}
