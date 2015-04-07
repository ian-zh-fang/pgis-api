/***********************************************************************
 * Module:  Select.cs
 * Author:  fun
 * Purpose: Definition of the Class Select
 ***********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao.SqlServer
{
    /// 查询数据
    [System.ComponentModel.Composition.Export(typeof(IDao.ISelect))]
    public class Select : QBase, IDao.ISelect
    {
        /// 需要查询的字段名称组合
        protected string[] _fields;
        /// 分页查询标识。True标识需要分页查询
        protected bool _page = false;
        /// 需要查询的页码
        protected int _index = 0;
        /// 信息条目数
        protected int _size = 10;
        /// Distinct标识。True标识支持Distinct模式
        protected bool _distincted = false;
        /// Having子句项目组
        protected string[] _havings;
        /// 支持分组字段组
        protected string[] _groupFields;
        /// 倒序排序字段组
        protected List<string> _descFields = new List<string>();
        /// 正序排序字段组
        protected List<string> _ascFields = new List<string>();
        /// 连接查询表和条件组
        protected List<string> _joinSettings = new List<string>();
        //  联合查询数据表名称
        protected List<string> _joinTables = new List<string>();
        /// 连接查询表名称，与On方法结合使用
        protected string _tempJoinTable;
        /// 连接查询类型
        protected JoinType _joinType = JoinType.Inner;

        public string OrderType
        {
            get { return _orderType; }
        }

        public Select()
        {
            this._orderType = "Select";
        }

        public IDao.ISelect Columns(params string[] colunm)
        {
            if (colunm.Length == 0)
            {
                _fields = new string[] { "*" };
            }
            else
            {
                _fields = colunm;
            }
            return this;
        }

        public IDao.ISelect From<T>()
        {
            var tp = typeof(T);
            return Table<Select>(this, typeof(T));
        }

        public IDao.ISelect From(string tableName)
        {
            return Table<Select>(this, tableName);
        }

        public IDao.ISelect Join(JoinType joinType, string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName)) throw new ArgumentNullException();
            this._joinType = joinType;
            this._tempJoinTable = tableName;

            return this;
        }

        public IDao.ISelect On(params string[] joinWhere)
        {
            if (joinWhere.Length == 0) throw new ArgumentNullException();
            if (!_joinTables.Exists(t => t == _tempJoinTable))
            {
                var onstr = string.Join(" and ", joinWhere);
                var jointype = GetJoinType();
                var str = string.Format("{0} {2} On {1}", jointype, onstr, _tempJoinTable);
                this._joinSettings.Add(str);
                this._joinTables.Add(_tempJoinTable);
            }

            return this;
        }

        public IDao.ISelect Distincted()
        {
            this._distincted = true;
            return this;
        }

        public IDao.ISelect OrderBy(OrderType orderType, params string[] fields)
        {
            if (fields.Length == 0) throw new ArgumentNullException();
            switch (orderType)
            {
                case WEBAPI.OrderType.Asc:
                    this._ascFields.AddRange(fields);
                    break;
                case WEBAPI.OrderType.Desc:
                    this._descFields.AddRange(fields);
                    break;
                default: break;
            }

            return this;
        }

        public IDao.ISelect GroupBy(params string[] fields)
        {
            this._groupFields = fields;
            return this;
        }

        public IDao.ISelect Page(int index, int size)
        {
            if (index < 1 || size < 1) throw new ArgumentOutOfRangeException();
            this._page = true;
            this._index = index;
            this._size = size;

            return this;
        }

        public IDao.IDbase Execute()
        {
            var cmdstr = ExecuteResult();
            return QExecute(cmdstr);
        }

        /// <summary>
        /// 获取连接查询类型关键字
        /// </summary>
        /// <returns></returns>
        private string GetJoinType()
        {
            var str = "Inner";
            switch (this._joinType)
            {
                case JoinType.Left:
                    str = "Left";
                    break;
                case JoinType.Right:
                    str = "Right";
                    break;
                case JoinType.Inner:
                default: break;
            }
            return string.Format("{0} join", str);
        }

        protected override string ExecuteResult()
        {
            if (_page) return CorePaging();

            return CoreCommandText();
        }

        /// <summary>
        /// 获得未分页T-SQL语句
        /// </summary>
        /// <returns></returns>
        private string CoreCommandText()
        {
            var result = new List<string>();
            //select
            result.Add(_orderType);
            //distinct
            if (_distincted) result.Add("distinct");
            //columns
            result.Add(GetField());
            //from table
            if (string.IsNullOrWhiteSpace(_tableName)) throw new ArgumentNullException();
            result.Add(string.Format("from {0}", _tableName));
            //join on
            if (_joinSettings.Count > 0)
                result.Add(string.Join(" ", _joinSettings));
            //where
            if (!string.IsNullOrWhiteSpace(IQueryWhere.Result))
                result.Add(string.Format("where {0}", IQueryWhere.Result));
            //order by
            var orderstr = GetSortContext();
            if (!string.IsNullOrWhiteSpace(orderstr))
                result.Add(string.Format("order by {0}", orderstr));
            //group by
            if (_groupFields != null && _groupFields.Length > 0)
            {
                //var ck = _groupFields.Where(t => _fields.FirstOrDefault(x => t == x) == default(string)).Count() > 0;
                //if (ck) throw new ArgumentException("group by 子句列不存在查询列中", "t-sql");
                result.Add(string.Format("Group By {0}", string.Join(",", _groupFields)));
            }

            return _commandText = string.Join(" ", result);
        }

        /// <summary>
        /// 获得查询的字段
        /// <para>如果不存在，那么查询所有的字段</para>
        /// </summary>
        /// <returns></returns>
        private string GetField()
        {
            if (string.IsNullOrWhiteSpace(_tableName)) throw new ArgumentNullException();
            if (_fields == null) return string.Format("{0}.*", _tableName);
            if (_fields.Length == 0) return string.Format("{0}.*", _tableName);
            //var pattern = string.Format("^([a-zA-Z0-9-_]+\\.[a-zA-Z0-9-_]+)?$");
            //var fields = _fields.Where(t => !System.Text.RegularExpressions.Regex.IsMatch(t, pattern));
            //var nfields = _fields.Where(t => System.Text.RegularExpressions.Regex.IsMatch(t, pattern));
            //var nstr = string.Join(",", nfields);
            //var str = string.Join(string.Format(",{0}.", _tableName), fields);
            //str = string.Format("{0}.{1}", _tableName, str);
            //return string.Join(",", str, nstr);
            return string.Join(",", _fields);
        }

        /// <summary>
        /// 获取查询排序方式
        /// </summary>
        /// <returns></returns>
        private string GetSortContext()
        {
            if (_descFields.Count == 0 && _ascFields.Count == 0) return string.Empty;
            if (_descFields.Count == 0) return string.Format("{0} asc", string.Join(",", _ascFields));
            if (_ascFields.Count == 0) return string.Format("{0} desc", string.Join(",", _descFields));
            return string.Format("{0} desc, {1} asc", string.Join(",", _descFields), string.Join(",", _ascFields));
        }

        /// <summary>
        /// 获得分页T-SQL语句
        /// </summary>
        /// <returns></returns>
        private string CorePaging()
        {
            //T-SQL
            //-----------------------------------------------------------------------------------------------
            //SELECT *
            //FROM (
            //    SELECT ROW_NUMBER() OVER ( order by Pgis_param.id desc, Pgis_param.id asc ) AS rownum ,
            //        Pgis_param.*
            //    FROM PGis_Param   
            //    where ((Pgis_param.Disabled  = 'True'))
            //     ) AS temp
            //WHERE   temp.rownum >= 1 and temp.rownum < 2

            var result = new List<string>();
            var sortstr = GetSortContext();
            if (string.IsNullOrWhiteSpace(sortstr)) throw new ArgumentNullException();
            var fields = string.Format("ROW_NUMBER() OVER ( order by {0} ) AS rownum ,{1}", sortstr, GetField());
            //select
            result.Add(_orderType);
            //columns
            result.Add(fields);
            //from
            result.Add(string.Format("from {0}", _tableName));
            //join on
            if (_joinSettings.Count > 0)
                result.Add(string.Join(" ", _joinSettings));
            //where
            if (!string.IsNullOrWhiteSpace(IQueryWhere.Result))
                result.Add(string.Format("where {0}", IQueryWhere.Result));
            //group by
            if (_groupFields != null && _groupFields.Length > 0)
            {
                var ck = _groupFields.Where(t => _fields.FirstOrDefault(x => t == x) == default(string)).Count() > 0;
                if (ck) throw new ArgumentException("group by 子句列不存在查询列中", "t-sql");
                result.Add(string.Format("Group By {0}", string.Join(",", _groupFields)));
            }

            var substr = string.Join(" ", result);
            return string.Format("select temp.* from ({0}) as temp where temp.rownum > {1} and temp.rownum <= {2}", substr, (_index - 1) * _size, _index * _size);
        }

        public IDao.IQueryWhere IQueryWhere
        {
            get { return QWhere(); }
        }
        
        /// <summary>
        /// 查询表达式
        /// </summary>
        public string CommandText
        {
            get { return ExecuteResult(); }
        }


        public IDao.ISelect Where<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            IQueryWhere.Where<T>(expression);
            return this;
        }

        public IDao.ISelect Where(string whereContext)
        {
            IQueryWhere.Where(whereContext);
            return this;
        }


        public IDao.ISelect AddColumn(params string[] column)
        {
            if (column.Length > 0)
            { 
                var list = new List<string>(_fields);
                var col = column.Where(t => !list.Exists(x => t == x));
                list.AddRange(col);
                _fields = list.ToArray();
            }
            return this;
        }
    }
}