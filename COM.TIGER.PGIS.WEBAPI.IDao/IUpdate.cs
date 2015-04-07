using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.IDao
{
    public interface IUpdate : IQueryExecute
    {
        /// <summary>
        /// 功能类型，此处应为Update模式
        /// </summary>
        string OrderType { get; }

        /// <summary>
        /// 传入更新信息目标数据表，表名称为类型T名称的复数形式
        /// </summary>
        IUpdate Table<T>();

        /// <summary>
        /// 传入更新信息目标数据表名称
        /// </summary>
        IUpdate Table(string tableName);

        /// <summary>
        /// 传入需要更新信息的字段
        /// </summary>
        IUpdate Set(string field);

        /// <summary>
        /// 传入需要更新信息字段的值
        /// </summary>
        /// <param name="value">字段值</param>
        /// <param name="spacial">可选参数，标识当前值是特殊的值，不会在值上加上 “‘” 。默认为False</param>
        /// <returns></returns>
        IUpdate EqualTo(object value, bool spacial = false);

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">查询表达式</param>
        /// <returns></returns>
        IUpdate Where<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression);

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="whereContext"></param>
        /// <returns></returns>
        IUpdate Where(string whereContext);
    }
}
