using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    /// <summary>
    /// 参数项处理程序
    /// </summary>
    public class ParamHandler : DBase
    {
        //性别
        private const string GENDERCODE = "xb";
        //居住性质
        private const string LIVECODE = "jzxz";
        //文化程度
        private const string EDUCATIONCODE = "whcd";
        //籍贯省
        private const string PROVINCECODE = "province";
        //籍贯市
        private const string CITYCODE = "province";
        //政治面貌
        private const string POLITICALCODE = "zzmm";
        //血型
        private const string BLOODCODE = "xx";
        //兵役状况
        private const string SOLDIERCODE = "byzk";
        //婚姻状况
        private const string MARRYCODE = "hyzk";
        //重点人口类别
        private const string PSYCHOISISCODE = "zdrklb";
        //与户主关系
        private const string HRATIONCODE = "yhzgx";

        private static ParamHandler _handler = null;
        /// <summary>
        /// 参数项处理程序
        /// </summary>
        public static ParamHandler Handler
        {
            get { return (_handler = _handler ?? new ParamHandler()); }
        }
        private ParamHandler() { }

        /// <summary>
        /// 获取所有的参数项信息
        /// </summary>
        /// <returns></returns>
        public List<Model.Param> GetParams()
        {
            var query = SelectHandler.Columns().From<Model.Param>().Execute();
            return ExecuteList<Model.Param>(query.ExecuteDataReader());
        }

        /// <summary>
        /// 分页获取参数项信息
        /// </summary>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="expression">当前查询条件</param>
        /// <param name="orderType">当前查询排序方式</param>
        /// <param name="records">当前查询总条目数</param>
        /// <param name="sortFields">当前查询排序字段组</param>
        /// <returns></returns>
        public List<Model.Param> PagingParams(int index, int size,
            System.Linq.Expressions.Expression<Func<Model.Param, bool>> expression,
            OrderType orderType, out int records, params string[] sortFields)
        {
            return Paging<Model.Param>(index, size, expression, orderType, out records, sortFields);
        }

        /// <summary>
        /// 分页获取顶级参数项信息
        /// </summary>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="records">当前查询条件下总条目数</param>
        /// <returns></returns>
        public List<Model.Param> PagingTopParams(int index, int size, out int records)
        {
            return PagingParams(index, size,
                x =>  x.PID == null || x.PID == 0,
                OrderType.Desc, out records, "Pgis_param.id");
        }

        /// <summary>
        /// 获取指定ID的参数项信息
        /// <para>该方法将根据第二个参数来决定是否获取子参数项</para>
        /// </summary>
        /// <param name="id">参数项标识符</param>
        /// <param name="flag">获取子参数项标识.TRUE标识需要获取子参数项.</param>
        /// <returns></returns>
        public Model.Param GetEntity(int id, bool flag)
        {
            if (flag) return GetParam(id);

            var query = SelectHandler.Columns().From<Model.Param>();
            var handler = query.IQueryWhere.Where<Model.Param>(x => x.ID == id).Where<IDao.ISelect>(query).Execute();
            return ExecuteEntity<Model.Param>(handler.ExecuteDataReader());
        }

        /// <summary>
        /// 获取指定唯一编码的参数项值
        /// <para>该方法将根据第二个参数来决定是否获取子参数项</para>
        /// </summary>
        /// <param name="code">参数项唯一标识符</param>
        /// <param name="flag">获取子参数项标识.TRUE标识需要获取子参数项.</param>
        /// <returns></returns>
        public Model.Param GetEntity(string code, bool flag)
        {
            if (flag) return GetParam(code);

            var query = SelectHandler.Columns().From<Model.Param>();
            var handler = query.IQueryWhere.Where<Model.Param>(x => x.Code == code).Where<IDao.ISelect>(query).Execute();
            return ExecuteEntity<Model.Param>(handler.ExecuteDataReader());
        }

        /// <summary>
        /// 查询指定代码的参数信息，返回一个Model.Param的新实例
        /// </summary>
        /// <param name="code">参数代码</param>
        /// <returns></returns>
        public Model.Param GetEntityByCode(string code)
        {
            return GetEntity<Model.Param>(t => t.Code == code);
        }

        /// <summary>
        /// 查询指定代码的参数信息，返回一个Model.Param的列表
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        public List<Model.Param> GetEntityByCodes(params string[] codes)
        {
            return GetEntities<Model.Param>(t => t.Code.In(codes));
        }

        /// <summary>
        /// 获取指定唯一编码的参数项值
        /// <para>此方法会返回当前参数项的首层自参数项信息</para>
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private Model.Param GetParam(string code)
        {
            //T-SQL语句：
            //---------------------------------------------------------------------------------
            //select * from pgis_param 
            //where (pid = (select id from pgis_param where code = 'aaa')) or (code = 'aaa');

            var list = GetParamsByCode(code);

            if (list.Count == 0) return null;
            var entity = list.First(t => t.Code == code);
            entity.AddRange(list);
            return entity;
        }

        /// <summary>
        /// 获取指定ID的参数项信息
        /// <para>此方法会返回当前参数项的首层自参数项信息</para>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private Model.Param GetParam(int id)
        {
            var list = GetParamsByID(id);
            if (list.Count == 0) return null;
            var p = list.First(t => t.ID == id);
            p.AddRange(list);
            return p;
        }

        /// <summary>
        /// 获取指定ID的参数项信息。
        /// <para>该方法总是会获取当前参数项的子参数项信息，并将会根据第二个参数确定返回值的对象：</para>
        /// <para>若为false，返回包含子参数项信息和当前参数项信息的实体集合，返回一组数据对象；</para>
        /// <para>若为true，返回当前参数项信息，子参数项信息将保存在当前参数项信息的子参数项信息字段内，返回一个数据对象。</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public object GetParamsByID(int id, bool flag)
        {
            if (flag) return GetParam(id);

            return GetParamsByID(id);
        }

        /// <summary>
        /// 获取指定ID的参数项信息
        /// <para>该方法总是会获取当前参数项信息的子参数项信息</para>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Model.Param> GetParamsByID(int id)
        {
            var query = SelectHandler.Columns().From<Model.Param>();
            var handler = query.IQueryWhere
                .Where<Model.Param>(x => x.ID == id || x.PID == id).Where<IDao.ISelect>(query).Execute();
            var list = ExecuteList<Model.Param>(handler.ExecuteDataReader());
            return list;
        }

        /// <summary>
        /// 获取指定CODE的参数项信息。
        /// <para>该方法总是会获取当前参数项的子参数项信息，并将会根据第二个参数确定返回值的对象：</para>
        /// <para>若为false，返回包含子参数项信息和当前参数项信息的实体集合，返回一组数据对象；</para>
        /// <para>若为true，返回当前参数项信息，子参数项信息将保存在当前参数项信息的子参数项信息字段内，返回一个数据对象。</para>
        /// </summary>
        /// <param name="code"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public object GetParamsByCode(string code, bool flag)
        {
            if (flag) return GetParam(code);

            return GetParamsByCode(code);
        }

        /// <summary>
        /// 获取指定CODE的参数项信息
        /// <para>该方法总是会获取当前参数项信息的子参数项信息</para>
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public List<Model.Param> GetParamsByCode(string code)
        {
            var query = SelectHandler.Columns().From<Model.Param>();
            var squery = SelectHandler.Columns("id").From<Model.Param>();
            var shandler = squery.IQueryWhere.Where<Model.Param>(x => x.Code == code ).Where<IDao.ISelect>(squery);
            var handler = query.IQueryWhere.Where<Model.Param>(x => x.Code == code )
                .Where(string.Format("or (pid = ({0}))", shandler.CommandText))
                .Where<IDao.ISelect>(query).Execute();
            var list = ExecuteList<Model.Param>(handler.ExecuteDataReader());
            return list;
        }

        /// <summary>
        /// 获取指定ID的子参数项信息，返回子参数集合
        /// <para>该方法总是会获取子参数信息，并根据第二个参数确认是否获取当前参数信息：</para>
        /// <para></para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="flag">如果需要获取当前参数信息，该参数必须为TRUE，否者为FALSE</param>
        /// <returns></returns>
        public List<Model.Param> GetParams(int id, bool flag)
        {
            if (flag) return GetParamsByID(id);

            var query = SelectHandler.From<Model.Param>();
            var handler = query.IQueryWhere.Where<Model.Param>(t => (t.PID == id)).Where<IDao.ISelect>(query).Execute();
            return ExecuteList<Model.Param>(handler.ExecuteDataReader());
        }

        /// <summary>
        /// 获取指定CODE的子参数项信息，返回子参数集合
        /// <para>该方法总是会获取子参数信息，并根据第二个参数确认是否获取当前参数信息：</para>
        /// </summary>
        /// <param name="code"></param>
        /// <param name="flag">如果需要获取当前参数信息，该参数必须为TRUE，否者为FALSE</param>
        /// <returns></returns>
        public List<Model.Param> GetParams(string code, bool flag)
        {
            if (flag) return GetParamsByCode(code);
            //获取select 子句
            //select id from param where code = '' and disabled = true
            var squery = SelectHandler.Columns("id").From<Model.Param>();
            var shandler = squery.IQueryWhere.Where<Model.Param>(x => x.Code == code).Where<IDao.ISelect>(squery);
            //获取主要sql语句
            //select * from param where (pid = (select id from param where code = '' and disabled = true )) and (disabled = true)
            var query = SelectHandler.Columns().From<Model.Param>();
            var handler = query.IQueryWhere
                .Where(string.Format("(pid = ({0}))", shandler.CommandText))
                .Where<IDao.ISelect>(query).Execute();
            var list = ExecuteList<Model.Param>(handler.ExecuteDataReader());
            return list;
        }

        /// <summary>
        /// 添加新的参数信息
        /// <para>返回值大于0，标识命令执行成功，数据操作成功</para>
        /// <para>返回值等于0，标识命令执行成功，数据操作失败</para>
        /// <para>返回值等于-1，返回命令执行失败</para>
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int InsertNew(Model.Param e)
        {
            if (GetEntityByCode(e.Code) != null)
                return -2;

            var query = InsertHandler.Into<Model.Param>()
                .Table("Code", "Disabled", "Name", "PID", "Sort")
                .Values(e.Code, e.Disabled, e.Name, e.PID, e.Sort);
            return query.Execute().ExecuteNonQuery();
        }

        /// <summary>
        /// 添加新的参数信息
        ///<para>执行成功，返回当前信息</para>
        ///<para>执行失败，返回NULL</para>
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public Model.Param InsertEntity(Model.Param e)
        {
            if (InsertNew(e) > 0)
            {
                e = GetEntity<Model.Param>(t => (t.Sort == e.Sort)
                    && (t.Name == e.Name)
                    && (t.PID == e.PID)
                    && (t.Disabled == e.Disabled)
                    && (t.Code == e.Code));
                return e;
            }
            return null;
        }

        /// <summary>
        /// 更改指定ID的参数信息
        /// <para>返回值大于0，标识命令执行成功，数据操作成功</para>
        /// <para>返回值等于0，标识命令执行成功，数据操作失败</para>
        /// <para>返回值等于-1，返回命令执行失败</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public int UpdateNew(int id, Model.Param e)
        {
            if (null != GetEntity<Model.Param>(t => t.ID != e.ID && t.Code == e.Code))
                return -2;

            var query = UpdateHandler.Table<Model.Param>()
                .Set("Code").EqualTo(e.Code)
                .Set("Name").EqualTo(e.Name)
                .Set("Disabled").EqualTo(e.Disabled)
                .Set("PID").EqualTo(e.PID)
                .Set("Sort").EqualTo(e.Sort);
            var handler = query.IQueryWhere.Where<Model.Param>(t => t.ID == id).Where<IDao.IUpdate>(query).Execute();
            return handler.ExecuteNonQuery();
        }

        /// <summary>
        /// 更改指定ID的参数信息
        ///<para>执行成功，返回当前信息</para>
        ///<para>执行失败，返回NULL</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public Model.Param UpdateEntity(int id, Model.Param e)
        {
            if (UpdateNew(id, e) > 0)
            {
                e.ID = id;
                return e;
            }
            return null;
        }

        /// <summary>
        /// 移除指定的参数信息
        /// <para>返回值大于0，标识命令执行成功，数据操作成功</para>
        /// <para>返回值等于0，标识命令执行成功，数据操作失败</para>
        /// <para>返回值等于-1，返回命令执行失败</para>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteEntity(int id)
        {
            return DeleteOperate<Model.Param >(t => t.ID == id);
        }

        /// <summary>
        /// 批量删除指定ID的记录
        /// </summary>
        /// <param name="ids">ID组</param>
        /// <returns></returns>
        public override int DeleteEntities(params string[] ids)
        {
            return DeleteOperate<Model.Param>(t => t.ID.In(ids));            
        }

        /// <summary>
        /// 获取指定ID的参数信息
        /// </summary>
        /// <param name="ids">配置参数ID组，以“，”分隔</param>
        /// <returns></returns>
        public List<Model.Param> GetEntities(params string[] ids)
        {
            if (ids.Length == 0) return new List<Model.Param>();

            return GetEntities<Model.Param>(t => t.ID.In(ids));
        }

        /// <summary>
        /// 性别
        /// </summary>
        /// <returns></returns>
        public List<Model.Param> GetGenders()
        {
            return GetParams(GENDERCODE, false);
        }

        /// <summary>
        /// 居住性质
        /// </summary>
        /// <returns></returns>
        public List<Model.Param> GetLiveTypes()
        {
            return GetParams(LIVECODE, false);
        }

        /// <summary>
        /// 文化程度
        /// </summary>
        /// <returns></returns>
        public List<Model.Param> GetEducations()
        {
            return GetParams(EDUCATIONCODE, false);
        }

        /// <summary>
        /// 籍贯省
        /// </summary>
        /// <returns></returns>
        public List<Model.Param> GetProvinces()
        {
            return GetParams(PROVINCECODE, false);
        }

        /// <summary>
        /// 籍贯市
        /// </summary>
        /// <returns></returns>
        public List<Model.Param> GetCities()
        {
            return GetParams(CITYCODE, false);
        }

        /// <summary>
        /// 政治面貌
        /// </summary>
        /// <returns></returns>
        public List<Model.Param> GetPoliticalStatus()
        {
            return GetParams(POLITICALCODE, false);
        }

        /// <summary>
        /// 血型
        /// </summary>
        /// <returns></returns>
        public List<Model.Param> GetBloodTypes()
        {
            return GetParams(BLOODCODE, false);
        }

        /// <summary>
        /// 兵役状况
        /// </summary>
        /// <returns></returns>
        public List<Model.Param> GetSoldierStatus()
        {
            return GetParams(SOLDIERCODE, false);
        }

        /// <summary>
        /// 婚姻状况
        /// </summary>
        /// <returns></returns>
        public List<Model.Param> GetMarriageStatus()
        {
            return GetParams(MARRYCODE, false);
        }

        /// <summary>
        /// 重点人口类别
        /// </summary>
        /// <returns></returns>
        public List<Model.Param> GetPsychosisTypes()
        {
            return GetParams(PSYCHOISISCODE, false);
        }

        /// <summary>
        /// 与户主关系
        /// </summary>
        /// <returns></returns>
        public List<Model.Param> GetHRelation()
        {
            return GetParams(HRATIONCODE, false);
        }
    }
}
