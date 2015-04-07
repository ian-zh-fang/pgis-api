using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    /// <summary>
    /// 辖区信息处理程序
    /// </summary>
    public class AreaHandler : DBase
    {
        private static AreaHandler _handler = null;
        /// <summary>
        /// 辖区信息处理程序
        /// </summary>
        public static AreaHandler Handler
        {
            get { return (_handler = _handler ?? new AreaHandler()); }
        }
        private AreaHandler() { }

        /// <summary>
        /// 获取所有的区域信息
        /// </summary>
        /// <returns></returns>
        public List<Model.Area> GetAreas()
        {
            var handler = SelectHandler.From<Model.Area>().Execute();
            var list = ExecuteList<Model.Area>(handler.ExecuteDataReader());
            var ranges = AreaRangeHandler.Handler.GetChildEntities((from t in list select t.ID.ToString()).ToArray());
            list.ForEach(t => t.AddRanges(ranges.Where(x => x.AreaID == t.ID).ToArray()));
            return SetBelongTo(list);
        }

        /// <summary>
        /// 获取所有的区域信息
        /// <para>如果传入的参数是true，方法将返回一棵树的集合</para>
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public List<Model.Area> GetAreas(bool flag)
        { 
            var list = GetAreas();

            if (flag) {
                var items = list.Where(t => t.PID == 0 || t.PID == null).ToList();
                items.ForEach(t => t.AddRange(list));
                return items;
            }
            return list;
        }

        /// <summary>
        /// 获取区域指定数据归属单位类型区域集合
        /// </summary>
        /// <param name="belongtoID">数据归属单位类型ID</param>
        /// <returns></returns>
        public List<Model.Area> GetAreas(int belongtoID)
        {
            var query = SelectHandler.From<Model.Area>();
            var handler = query.IQueryWhere.Where<Model.Area>(t => t.BelongToID == belongtoID)
                .Where<IDao.ISelect>(query).Execute();
            var list = ExecuteList<Model.Area>(handler.ExecuteDataReader());
            var ranges = AreaRangeHandler.Handler.GetChildEntities((from t in list select t.ID.ToString()).ToArray());
            list.ForEach(t => t.AddRanges(ranges.Where(x => x.AreaID == t.ID).ToArray()));
            return SetBelongTo(list);
        }

        /// <summary>
        /// 分页获取所有区域集合
        /// </summary>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="records">区域数量</param>
        /// <returns></returns>
        public List<Model.Area> PagingArea(int index, int size, out int records)
        {
            var list = Paging<Model.Area>(index, size, null, OrderType.Desc, out records, "pgis_area.id");
            var ranges = AreaRangeHandler.Handler.GetChildEntities((from t in list select t.ID.ToString()).ToArray());
            list.ForEach(t => t.AddRanges(ranges.Where(x => x.AreaID == t.ID).ToArray()));
            return SetBelongTo(list);
        }

        /// <summary>
        /// 分页获取顶级区域集合
        /// </summary>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="records">顶级区域数量</param>
        /// <returns></returns>
        public List<Model.Area> PagingTopArea(int index, int size, out int records)
        {
            var list = Paging<Model.Area>(index, size, t => t.PID == null || t.PID == 0,
                OrderType.Desc, out records, "pgis_area.id");
            var ranges = AreaRangeHandler.Handler.GetChildEntities((from t in list select t.ID.ToString()).ToArray());
            list.ForEach(t => t.AddRanges(ranges.Where(x => x.AreaID == t.ID).ToArray()));
            return SetBelongTo(list);
        }

        /// <summary>
        /// 分页获取指定PID的辖区信息
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <param name="records"></param>
        /// <returns></returns>
        public List<Model.Area> PagingAreas(int pid, int index, int size, out int records)
        {
            var list = Paging<Model.Area>(index, size, t => t.PID == pid,
                OrderType.Desc, out records, "pgis_area.id");
            var ranges = AreaRangeHandler.Handler.GetChildEntities((from t in list select t.ID.ToString()).ToArray());
            list.ForEach(t => t.AddRanges(ranges.Where(x => x.AreaID == t.ID).ToArray()));
            return SetBelongTo(list);
        }

        /// <summary>
        /// 获取顶级区域信息集合
        /// </summary>
        /// <returns></returns>
        public List<Model.Area> GetTopAreas()
        {
            var query = SelectHandler.From<Model.Area>();
            var handler = query.IQueryWhere.Where<Model.Area>(t => t.PID == null || t.PID == 0).Where<IDao.ISelect>(query).Execute();
            var list = ExecuteList<Model.Area>(handler.ExecuteDataReader());
            var ranges = AreaRangeHandler.Handler.GetChildEntities((from t in list select t.ID.ToString()).ToArray());
            list.ForEach(t => t.AddRanges(ranges.Where(x => x.AreaID == t.ID).ToArray()));
            return SetBelongTo(list);
        }

        /// <summary>
        /// 获取指定ID的区域信息
        /// <para>该方法将会依据第二个参数决定是否查询当前区域信息的子区域信息</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="flag">如果需要查询当前区域的子区域信息，该值应设为true</param>
        /// <returns></returns>
        public Model.Area GetEntity(int id, bool flag)
        {
            if (flag) return GetArea(id);

            var query = SelectHandler.From<Model.Area>();
            var handler = query.IQueryWhere.Where<Model.Area>(t => t.ID == id).Where<IDao.ISelect>(query).Execute();
            var entity = ExecuteEntity<Model.Area>(handler.ExecuteDataReader());
            var ranges = AreaRangeHandler.Handler.GetAllEntities(id);
            entity.AddRanges(ranges.ToArray());
            return entity;
        }

        /// <summary>
        /// 获取指定ID的区域信息
        /// <para>该方法总是会获取当前查询区域的子区域信息</para>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private Model.Area GetArea(int id)
        {
            var list = GetAreasByID(id);
            if (list.Count == 0) return null;
            var entity = list.First(t => t.ID == id);
            entity.AddRange(list);
            var ranges = AreaRangeHandler.Handler.GetAllEntities(id);
            entity.AddRanges(ranges.ToArray());
            return entity;
        }

        /// <summary>
        /// 获取指定ID的区域信息。
        /// <para>该方法总是会获取当前区域的子区域信息，并将会根据第二个参数确定返回值的对象：</para>
        /// <para>若为false，返回包含子区域信息和当前区域信息的实体集合，返回一组数据对象；</para>
        /// <para>若为true，返回当前区域信息，子区域信息将保存在当前区域信息的子区域信息字段内，返回一个数据对象。</para>
        /// </summary>
        /// <param name="id">获取的当前区域ID</param>
        /// <param name="flag">确定返回值的对象</param>
        /// <returns></returns>
        public object GetAreasByID(int id, bool flag)
        {
            if (flag) return GetArea(id);

            return GetAreasByID(id);
        }

        /// <summary>
        /// 获取指定ID的区域信息。
        /// <para>该方法总是会获取当前区域的子区域信息，并返回一个数据对象集合。</para>
        /// </summary>
        /// <param name="id">获取的当前区域ID</param>
        /// <returns></returns>
        public List<Model.Area> GetAreasByID(int id)
        {
            var query = SelectHandler.From<Model.Area>();
            var handler = query.IQueryWhere.Where<Model.Area>(t => t.ID == id || t.PID == id).Where<IDao.ISelect>(query).Execute();
            var list = ExecuteList<Model.Area>(handler.ExecuteDataReader());
            var ranges = AreaRangeHandler.Handler.GetChildEntities((from t in list select t.ID.ToString()).ToArray());
            list.ForEach(t => t.AddRanges(ranges.Where(x => x.AreaID == t.ID).ToArray()));
            return SetBelongTo(list);
        }
        
        /// <summary>
        /// 获取指定ID的子区域信息，返回子区域集合
        /// <para>该方法总是会获取子区域信息，并根据第二个参数确认是否获取当前机构信息：</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="flag">如果需要获取当前区域信息，该参数必须为TRUE，否者为FALSE</param>
        /// <returns></returns>
        public List<Model.Area> GetAreas(int id, bool flag)
        {
            if (flag) return GetAreasByID(id);

            var query = SelectHandler.From<Model.Area>();
            var handler = query.IQueryWhere.Where<Model.Area>(t => (t.PID == id)).Where<IDao.ISelect>(query).Execute();
            var list = ExecuteList<Model.Area>(handler.ExecuteDataReader());
            var ranges = AreaRangeHandler.Handler.GetChildEntities((from t in list select t.ID.ToString()).ToArray());
            list.ForEach(t => t.AddRanges(ranges.Where(x => x.AreaID == t.ID).ToArray()));
            return SetBelongTo(list);
        }

        /// <summary>
        /// 更新指定ID的区域信息
        /// <para>返回值大于0，标识命令执行成功，数据更新成功</para>
        /// <para>返回值等于0，标识命令执行成功，数据更新失败</para>
        /// <para>返回值等于-1，返回命令执行失败</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="e">类型的新实例</param>
        /// <returns></returns>
        public int UpdateNew(int id, Model.Area e)
        {
            var query = UpdateHandler.Table<Model.Area>();
            query = query
                .Set("BelongToID").EqualTo(e.BelongToID)
                .Set("Name").EqualTo(e.Name)
                .Set("NewCode").EqualTo(e.NewCode)
                .Set("OldCode").EqualTo(e.OldCode)
                .Set("PID").EqualTo(e.PID)
                .Set("Description").EqualTo(e.Description);
            var handler = query.IQueryWhere.Where<Model.Area>(t => t.ID == id).Where<IDao.IUpdate>(query).Execute();
            return handler.ExecuteNonQuery();
        }

        /// <summary>
        /// 更新指定ID的区域信息
        /// <para>更新成功，返回当前的实例</para>
        /// <para>更新失败，返回NULL</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="e">类型的新实例</param>
        /// <returns></returns>
        public Model.Area UpdateEntity(int id, Model.Area e)
        {
            var r = UpdateNew(id, e);
            if (r > 0) 
            {
                e.ID = id;                
                return e;
            }
            return null;
        }

        /// <summary>
        /// 保存新的区域实例
        /// <para>返回值大于0，标识命令执行成功，数据更新成功</para>
        /// <para>返回值等于0，标识命令执行成功，数据更新失败</para>
        /// <para>返回值等于-1，返回命令执行失败</para> 
        /// </summary>
        /// <param name="e">新实例</param>
        /// <returns></returns>
        public int InsertNew(Model.Area e)
        {
            var query = InsertHandler.Into<Model.Area>()
                .Table("BelongToID", "Name", "NewCode", "OldCode", "PID", "Description")
                .Values(e.BelongToID, e.Name, e.NewCode, e.OldCode, e.PID, e.Description);
            return query.Execute().ExecuteNonQuery();
        }

        /// <summary>
        /// 保存新的区域实例
        /// <para>执行成功，返回当前实例</para>
        /// <para>执行失败，返回NULL</para>
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public Model.Area InsertEntity(Model.Area e)
        {
            if (InsertNew(e) > 0)
            {
                var o = GetEntity<Model.Area>(t => (t.PID == e.PID)
                    && (t.OldCode == e.OldCode)
                    && (t.NewCode == e.NewCode)
                    && (t.Name == e.Name)
                    && (t.BelongToID == e.BelongToID));
                return o;
            }
            return null;
        }

        /// <summary>
        /// 删除指定ID的区域信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteEntity(int id)
        {
            return DeleteOperate<Model.Area>(t => t.ID == id);
        }

        /// <summary>
        /// 批量删除指定ID的记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public override int DeleteEntities(params string[] ids)
        {
            if (ids.Length == 0) return 0;
            try
            {
                using (var tran = new System.Transactions.TransactionScope())
                {
                    var r = DeleteOperate<Model.Area>(t => t.ID.In(ids));
                    r += AreaRangeHandler.Handler.DeleteByAreaID(ids);
                    tran.Complete();
                    return r;
                }
            }
            catch { return -1; }
        }

        /// <summary>
        /// 设置区域数据归属单位类型
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        private List<Model.Area> SetBelongTo(List<Model.Area> items)
        {
            var bids = (from t in items select t.BelongToID.ToString()).Distinct().ToArray();
            var belong = BelongToHandler.Handler.GetEntities(bids);
            items.ForEach(t => t.CompanyType = belong.FirstOrDefault(x => x.ID == t.BelongToID));
            return items;
        }
    }
}
