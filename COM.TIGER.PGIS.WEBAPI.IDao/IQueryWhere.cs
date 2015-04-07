using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.IDao
{
    public interface IQueryWhere
    {
        /// <summary>
        /// 查询条件
        /// </summary>
        string Result { get; }
        /// <summary>
        /// 查询条件
        /// </summary>
        T Where<T>(T t, params string[] condition);

        /// <summary>
        /// 查询条件
        /// </summary>
        T Where<T,T1>(T t, System.Linq.Expressions.Expression<Func<T1, bool>> expression);

        IQueryWhere Where(params string[] condition);

        IQueryWhere Where<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression);
    }
}
