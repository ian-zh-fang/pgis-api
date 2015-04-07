/***********************************************************************
 * Module:  Delete.cs
 * Author:  fun
 * Purpose: Definition of the Class Delete
 ***********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao.SqlServer
{
    [System.ComponentModel.Composition.Export(typeof(IDao.IDelete))]
    public class Delete : QBase, IDao.IDelete
    {
        public string OrderType
        {
            get { return _orderType; }
        }

        public Delete()
        {
            this._orderType = "Delete";
        }

        IDao.IDelete IDao.IDelete.From<T>()
        {
            return Table<Delete>(this, typeof(T));
        }

        IDao.IDelete IDao.IDelete.From(string tableName)
        {
            return Table<Delete>(this, tableName);
        }

        public IDao.IDbase Execute()
        {
            var cmdstr = ExecuteResult();
            return QExecute(cmdstr);
        }

        protected override string ExecuteResult()
        {
            var result = new List<string>();
            //delete from
            result.Add(this._orderType);
            result.Add("From");
            //tablename
            if (string.IsNullOrWhiteSpace(_tableName)) throw new ArgumentNullException();
            result.Add(_tableName);
            //where
            if (!string.IsNullOrWhiteSpace(IQueryWhere.Result))
                result.Add(string.Format("where {0}", IQueryWhere.Result));

            return string.Join(" ", result);
        }

        public IDao.IQueryWhere IQueryWhere
        {
            get { return QWhere(); }
        }


        public string CommandText
        {
            get { return ExecuteResult(); }
        }

        public IDao.IDelete Where<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            IQueryWhere.Where<T>(expression);
            return this;
        }

        public IDao.IDelete Where(string whereContext)
        {
            IQueryWhere.Where(whereContext);
            return this;
        }
    }
}