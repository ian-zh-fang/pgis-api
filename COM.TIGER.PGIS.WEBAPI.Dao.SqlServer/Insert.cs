/***********************************************************************
 * Module:  Insert.cs
 * Author:  fun
 * Purpose: Definition of the Class Insert
 ***********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao.SqlServer
{
    [System.ComponentModel.Composition.Export(typeof(IDao.IInsert))]
    public class Insert: QBase, IDao.IInsert
    {
        protected string[] _fields;
        protected string[] _values;


        public string OrderType
        {
            get { return _orderType; }
        }

        public Insert()
        {
            this._orderType = "Insert into";
        }

        IDao.IInsert IDao.IInsert.Into<T>()
        {
            return Table<Insert>(this, typeof(T));
        }

        public IDao.IInsert Into(string tableName)
        {
            return Table<Insert>(this, tableName);
        }

        IDao.IInsert IDao.IInsert.Table(params string[] fieldCollection)
        {
            if (fieldCollection.Length == 0) throw new ArgumentNullException();
            var list = new List<string>();
            for (var i = 0; i < fieldCollection.Length; i++)
            {
                list.AddRange(fieldCollection[i].Split(','));    
            }
            this._fields = list.ToArray();
            return this;
        }

        IDao.IInsert IDao.IInsert.Values(params object[] valueCollection)
        {
            if (valueCollection.Length == 0) throw new ArgumentNullException();
            if (this._fields.Length != valueCollection.Length) throw new ArgumentOutOfRangeException();
            _values = new string[valueCollection.Length];
            for (var i = 0; i < valueCollection.Length; i++)
            {
                var obj = valueCollection[i];
                if (obj == null)
                {
                    _values[i] = "NULL";
                    continue;
                }
                _values[i] = string.Format("'{0}'", obj);
            }
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
            if (_fields == null) throw new ArgumentNullException();
            if (_values == null) throw new ArgumentNullException();
            if (_fields.Length == 0) throw new ArgumentNullException();
            if (_values.Length != _fields.Length) throw new ArgumentOutOfRangeException();

            var result = new List<string>();
            result.Add(_orderType);
            result.Add(string.Format("{0}({1})", _tableName, string.Join(",", _fields)));
            result.Add(string.Format("values({0})", string.Join(",", _values)));

            //Array.Clear(_fields, 0, _fields.Length);
            //Array.Clear(_values, 0, _values.Length);

            return string.Join(" ", result);
        }

        public IDao.IQueryWhere IQueryWhere
        {
            get { throw new NotImplementedException(); }
        }


        public string CommandText
        {
            get { return ExecuteResult(); }
        }


        public IDao.IInsert InsertValue(string fieldName, string value)
        {
            if (string.IsNullOrWhiteSpace(fieldName))
                throw new ArgumentNullException();
            _fields = _fields ?? new string[0];
            _fields = _fields.Concat(new string[] { fieldName }).ToArray();
            _values = _values ?? new string[0];
            _values = _values.Concat(new string[] { value ?? string.Empty }).ToArray();
            return this;
        }
    }
}