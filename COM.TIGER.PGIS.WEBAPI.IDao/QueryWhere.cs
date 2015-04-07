using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.IDao
{
    public class QueryWhere : IQueryWhere
    {
        protected delegate object ExpressionMethodCallResult();
        protected delegate T ExpressionMethodCallResult<T>();

        /// <summary>
        /// T-SQL查询条件组
        /// </summary>
        private List<string> _where;

        /// <summary>
        /// 查询结果
        /// </summary>
        public string Result
        {
            get 
            {
                if(_where.Count == 0) return string.Empty;
                var str = string.Format("({0})", _where[0]);
                for(var i=1; i<_where.Count; i++)
                {
                    var s = _where[i].Trim();
                    str = string.Format("{0} {2} {1}", str, s, s.StartsWith("or ") ? "" : "and");
                }
                return str;
            }
        }

        public QueryWhere()
        {
            _where = new List<string>();
        }
    
        public T Where<T>(T t, params string[] condition)
        {
            if (condition.Length > 0)
                this._where.AddRange(condition);
            return t;
        }

        public T Where<T, T1>(T t, Expression<Func<T1, bool>> expression)
        {
            var str = ExpressionRouter(expression.Body);
            _where.Add(str);
            return t;
        }


        public IQueryWhere Where(params string[] condition)
        {
            if (condition.Length > 0)
                this._where.AddRange(condition);
            return this;
        }

        public IQueryWhere Where<T>(Expression<Func<T, bool>> expression)
        {
            var str = ExpressionRouter(expression.Body);
            _where.Add(str);
            return this;
        }

        /// <summary>
        /// 计算表达式，并返回T-SQL的Where子句内容，
        /// </summary>
        /// <param name="left">表达式左值</param>
        /// <param name="right">表达式右值</param>
        /// <param name="type">运算方式</param>
        /// <returns></returns>
        protected virtual string BinaryExpressionProider(Expression left, Expression right, ExpressionType type)
        {
            var lstr = ExpressionRouter(left);
            var ostr = ExpressionTypeCast(type).Trim();

            var rstr = ExpressionRouter(right, true);
            if (string.IsNullOrWhiteSpace(rstr))
            {
                switch (ostr)
                {
                    case "=":
                        ostr = "is";
                        break;
                    case "<>":
                        ostr = "is not";
                        break;
                    default: break;
                }
                rstr = "null";
            }
            var str = string.Format("({0} {1} {2})", lstr, ostr, rstr);
            return str;
        }

        /// <summary>
        /// （递归）计算表达式，并返回T-SQL的Where子句内容
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        protected virtual string ExpressionRouter(Expression expression, bool flag = false, bool isFlase = false)
        {
            if (expression is BinaryExpression)
            {
                var be = (BinaryExpression)expression;
                return BinaryExpressionProider(be.Left, be.Right, be.NodeType);
            }
            
            if (expression is MemberExpression)
            {
                var me = (MemberExpression)expression;
                return ExpressionMember(me, flag);
            }

            if (expression is NewArrayExpression)
            {
                var ae = (NewArrayExpression)expression;
                return ExpressionArray(ae);
            }

            if (expression is MethodCallExpression)
            {
                var ce = (MethodCallExpression)expression;
                return ExpressionMethodCall(ce);
            }

            if (expression is ConstantExpression)
            {
                var cte = (ConstantExpression)expression;
                return ExpressionConstant(cte);
            }

            if (expression is UnaryExpression)
            {
                var ue = (UnaryExpression)expression;
                if (isFlase) return ExpressionRouter(ue.Operand, false);

                return ExpressionRouter(ue.Operand, true);
            }

            return string.Empty;
        }

        /// <summary>
        /// 计算常量表达式，并返回常量值
        /// </summary>
        /// <param name="cte"></param>
        /// <returns></returns>
        protected virtual string ExpressionConstant(ConstantExpression cte)
        {
            if (cte.Value == null) return string.Empty;
            return string.Format("'{0}'", cte.Value); 
        }

        /// <summary>
        /// 计算参数成员变量，并返回成员变量名称
        /// </summary>
        /// <param name="me"></param>
        /// <returns></returns>
        protected virtual string ExpressionMember(MemberExpression me, bool flag = false)
        {
            if (flag)
            {
                return MethodCallResult(me);
            }
            return string.Format("{2}{0}{3}.{1}", me.Member.DeclaringType.Name, me.Member.Name,
                COM.TIGER.PGIS.WEBAPI.Common.Attributes.AttributeHandler.GetPrefixName(me.Member.DeclaringType)/*前缀*/,
                COM.TIGER.PGIS.WEBAPI.Common.Attributes.AttributeHandler.GetSuffixName(me.Member.DeclaringType)/*后缀*/);
        }

        /// <summary>
        /// 计算数组表达式的值，返回以逗号分隔的包含数组所有元素的字符串
        /// </summary>
        /// <param name="ae"></param>
        /// <returns></returns>
        protected virtual string ExpressionArray(NewArrayExpression ae)
        {
            var arrstr = new List<string>();
            foreach (var ex in ae.Expressions)
            {
                arrstr.Add(ExpressionRouter(ex, true));
            }
            return string.Join(",", arrstr);
        }

        /// <summary>
        /// 获取调用方法的值
        /// </summary>
        /// <param name="ce"></param>
        /// <returns></returns>
        protected virtual string ExpressionMethodCall(MethodCallExpression ce)
        {
            var str = string.Empty;
            var left = ExpressionRouter(ce.Arguments[0], false, true);
            var right = ExpressionRouter(ce.Arguments[1], true);
            switch (ce.Method.Name)
            {
                case "Like":
                    str = string.Format("({0} like '%{1}%')", left, right.Trim(new char[] { '\'', '"', '\0', ' ' }));
                    break;
                case "NotLike":
                    str = string.Format("({0} not like '%{1}%')", left, right.Trim(new char[] { '\'', '"', '\0', ' ' }));
                    break;
                case "In":
                    str = string.Format("({0} in ({1}))", left, right);
                    break;
                case "NotIn":
                    str = string.Format("({0} not in ({1}))", left, right);
                    break;
                default:
                    str = MethodCallResult(ce);
                    break;
            }
            return str;
        }

        /// <summary>
        /// 通过委托的方式，计算并得到调用方法的最终结果，并返回结果
        /// </summary>
        /// <param name="ce"></param>
        /// <returns></returns>
        private string MethodCallResult(Expression ce)
        {
            if (ce.Type.IsValueType)
                return MethodCallValueType(ce);

            return MethodCallobject(ce);
        }

        /// <summary>
        /// 通过委托的方式，计算并得到调用方法的最终结果，并返回结果
        /// <para>适用从Object类型继承的类型</para>
        /// </summary>
        /// <param name="ce"></param>
        /// <returns></returns>
        private string MethodCallobject(Expression ce)
        {
            var lamb = Expression.Lambda<ExpressionMethodCallResult>(ce);
            var obj = lamb.Compile()();
            if (obj == null) return null;

            Type type = obj.GetType();
            if (type == typeof(string[]))
            {
                string[] buffer = obj as string[];
                char[] splitchar = new char[] { ',' };
                List<string> list = new List<string>();
                for (int i = 0; i < buffer.Length; i++) 
                {
                    string s = buffer[i];
                    if (!string.IsNullOrWhiteSpace(s)) 
                    {
                        list.AddRange(s.Split(splitchar, StringSplitOptions.RemoveEmptyEntries));
                    }
                }
                    
                return string.Format("'{0}'", string.Join("','", list.ToArray()));
            }

            var str = string.Format("'{0}'", obj);
            return str;
        }

        /// <summary>
        /// 通过委托的方式，计算并得到调用方法的最终结果，并返回结果
        /// <para>适用从ValueType类型继承的类型</para>
        /// </summary>
        /// <param name="ce"></param>
        /// <returns></returns>
        private string MethodCallValueType(Expression ce)
        {
            if (ce.Type == typeof(int))
                return MethodCallValueType<int>(ce);

            if (ce.Type == typeof(int?))
                return MethodCallValueType<int?>(ce);

            if (ce.Type == typeof(bool))
                return MethodCallValueType<bool>(ce);

            if (ce.Type == typeof(byte))
                return MethodCallValueType<byte>(ce);

            if (ce.Type == typeof(char))
                return MethodCallValueType<char>(ce);

            if (ce.Type == typeof(decimal))
                return MethodCallValueType<decimal>(ce);

            if (ce.Type == typeof(decimal?))
                return MethodCallValueType<decimal?>(ce);

            if (ce.Type == typeof(double))
                return MethodCallValueType<double>(ce);

            if (ce.Type == typeof(double?))
                return MethodCallValueType<double?>(ce);

            if (ce.Type == typeof(float))
                return MethodCallValueType<float>(ce);

            if (ce.Type == typeof(float?))
                return MethodCallValueType<float?>(ce);

            if (ce.Type == typeof(long))
                return MethodCallValueType<long>(ce);

            if (ce.Type == typeof(long?))
                return MethodCallValueType<long?>(ce);

            if (ce.Type == typeof(sbyte))
                return MethodCallValueType<sbyte>(ce);

            if (ce.Type == typeof(short))
                return MethodCallValueType<short>(ce);

            if (ce.Type == typeof(short?))
                return MethodCallValueType<short?>(ce);

            if (ce.Type == typeof(uint))
                return MethodCallValueType<uint>(ce);

            if (ce.Type == typeof(uint?))
                return MethodCallValueType<uint?>(ce);

            if (ce.Type == typeof(ulong))
                return MethodCallValueType<ulong>(ce);

            if (ce.Type == typeof(ulong?))
                return MethodCallValueType<ulong?>(ce);

            if (ce.Type == typeof(ushort))
                return MethodCallValueType<ushort>(ce);

            if (ce.Type == typeof(ushort?))
                return MethodCallValueType<ushort?>(ce);

            if (ce.Type == typeof(DateTime))
                return MethodCallValueType<DateTime>(ce);

            if (ce.Type == typeof(DateTime?))
                return MethodCallValueType<DateTime?>(ce);

            return string.Empty;
        }

        /// <summary>
        /// 通过委托的方式，计算并得到调用方法的最终结果，并返回结果
        /// <para>根据不同的类型调用不同的委托方式，返回不同类型的结果值</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ce"></param>
        /// <returns></returns>
        private string MethodCallValueType<T>(Expression ce)
        {
            var lamb = Expression.Lambda<ExpressionMethodCallResult<T>>(ce);
            var obj = lamb.Compile()();
            if (typeof(T) == typeof(DateTime)) return string.Format("'{0}'", obj);

            return string.Format("{0}", obj);
        }

        /// <summary>
        /// 获取表达式运算方式
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected virtual string ExpressionTypeCast(ExpressionType type)
        {
            switch (type)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    return " and ";
                case ExpressionType.Equal:
                    return " =";
                case ExpressionType.GreaterThan:
                    return " >";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.NotEqual:
                    return "<>";
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return " or ";
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                    return "+";
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                    return "-";
                case ExpressionType.Divide:
                    return "/";
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                    return "*";
                default:
                    return null;
            }
        }
    }
}
