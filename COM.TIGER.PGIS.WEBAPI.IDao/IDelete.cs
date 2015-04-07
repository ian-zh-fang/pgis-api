using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.IDao
{
    public interface IDelete : IQueryExecute
    {
        /// <summary>
        /// 功能类型，此处应为Delete模式
        /// </summary>
        string OrderType { get; }

        /// <summary>
        /// 传入移除信息目标数据表，表名称为类型T名称的复数形式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IDelete From<T>();

        /// <summary>
        /// 传入移除信息目标数据表名称
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        IDelete From(string tableName);

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">查询表达式</param>
        /// <returns></returns>
        IDelete Where<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression);

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="whereContext"></param>
        /// <returns></returns>
        IDelete Where(string whereContext);
    }
}
