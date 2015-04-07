/***********************************************************************
 * Module:  Update.cs
 * Author:  fun
 * Purpose: Definition of the Class Update
 ***********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao.SqlServer
{
    [System.ComponentModel.Composition.Export(typeof(IDao.IUpdate))]
    public class Update : QBase, IDao.IUpdate
    {
       protected List<string> _settings = new List<string>();
       protected string _tempField;


       public string OrderType
       {
           get { return _orderType; }
       }

       public Update()
       {
           this._orderType = "Update";
       }

       public IDao.IUpdate Table<T>()
       {
           return Table<Update>(this, typeof(T));
       }

       public IDao.IUpdate Table(string tableName)
       {
           return Table<Update>(this, tableName);
       }

       public IDao.IUpdate Set(string field)
       {
           if (string.IsNullOrWhiteSpace(field)) throw new ArgumentNullException();
           this._tempField = field;
           return this;
       }

       public IDao.IUpdate EqualTo(object value, bool spacial = false)
       {
           if (string.IsNullOrWhiteSpace(_tempField)) throw new ArgumentNullException();
           var str = string.Empty;
           if (value == null)
           {
               str = string.Format("{0} = NULL", this._tempField);
           }
           else
           {
               if (spacial)
               {
                   str = string.Format("{0} = {1}", this._tempField, value);
               }
               else {
                   str = string.Format("{0} = '{1}'", this._tempField, value);
               }
               
           }
           _settings.Add(str);

           return this;
       }

       public IDao.IDbase Execute()
       {
           var cmdstr = ExecuteResult();
           return QExecute(cmdstr);
       }

       protected override string ExecuteResult()
       {
           if (string.IsNullOrWhiteSpace(_tableName)) throw new ArgumentNullException();
           if (_settings.Count == 0) throw new ArgumentOutOfRangeException();
           var result = new List<string>();
           //update
           result.Add(this._orderType);
           //tablename
           result.Add(_tableName);
           //set
           result.Add("set");
           //settingss
           result.Add(string.Join(", ", _settings));
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

       public IDao.IUpdate Where<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression)
       {
           IQueryWhere.Where<T>(expression);
           return this;
       }

       public IDao.IUpdate Where(string whereContext)
       {
           IQueryWhere.Where(whereContext);
           return this;
       }
    }
}