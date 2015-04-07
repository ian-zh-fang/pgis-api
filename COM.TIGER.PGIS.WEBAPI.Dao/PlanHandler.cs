using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    /// <summary>
    /// 预案数据处理程序
    /// </summary>
    public class PlanHandler:DBase
    {
        private static PlanHandler _handler = null;
        /// <summary>
        /// 预案数据处理程序的唯一实例
        /// </summary>
        public static PlanHandler Handler { get { return _handler = _handler ?? new PlanHandler(); } }
        private PlanHandler() { }

        /// <summary>
        /// 获取指定ID的预案信息
        /// <para>没有指定ID，程序获取所有的预案信息</para>
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<Model.Plan> GetPlans(params string[] ids)
        {
            var query = SelectHandler.From<Model.Plan>();
            List<Model.Plan> list = null;
            if (ids.Length == 0)
            {
                list = ExecuteList<Model.Plan>(query.Execute().ExecuteDataReader());
            }
            else
            {
                var handler = query.Where<Model.Plan>(t => t.ID.In(ids)).Execute();
                list = ExecuteList<Model.Plan>(handler.ExecuteDataReader());
            }
            GetInfo(ref list);
            return list;
        }

        /// <summary>
        /// 分页所有的预案信息，并获取指定页码的预案数据
        /// </summary>
        /// <param name="index">指定页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="records">总条目数</param>
        /// <returns></returns>
        public List<Model.Plan> Paging(int index, int size, out int records)
        {
            var list = Paging<Model.Plan>(index, size, null, OrderType.Desc, out records, "pgis_plan.id");
            GetInfo(ref list);
            return list;
        }

        /// <summary>
        /// 获取指定预案ID的文档数据
        /// <para>没有指定ID，返回一个零长度的数据集合</para>
        /// </summary>
        /// <param name="ids">需要指定的预案ID值</param>
        /// <returns></returns>
        public List<Model.File> GetPlanFiles(params string[] ids)
        {
            if (ids.Length == 0) return new List<Model.File>();

            var query = SelectHandler.Columns("Pgis_File.*").From<Model.File>().Join(JoinType.Inner, "Pgis_PlanFile").On("Pgis_PlanFile.FileID = Pgis_File.ID");
            var handler = query.Where<Model.PlanFile>(t => t.PlanID.In(ids));
            return ExecuteList<Model.File>(handler.Execute().ExecuteDataReader());
        }

        /// <summary>
        /// 获取指定预案ID的地图标注数据
        /// <para>没有指定ID，返回一个零长度的数据集合</para>
        /// </summary>
        /// <param name="ids">需要指定的预案ID值</param>
        /// <returns></returns>
        public List<Model.Tag> GetPlanTags(params string[] ids)
        {
            if (ids.Length == 0) return new List<Model.Tag>();

            var query = SelectHandler.Columns("Pgis_Tag.*").From<Model.Tag>().Join(JoinType.Inner, "Pgis_PlanTag").On("Pgis_PlanTag.TagID = Pgis_Tag.ID");
            var handler = query.Where<Model.PlanTag>(t => t.PlanID.In(ids));
            return ExecuteList<Model.Tag>(handler.Execute().ExecuteDataReader());
        }

        /// <summary>
        /// 移除指定预案的指定的标注信息
        ///  <para>该方法移除标注和预案的关系数据，但是不会标注信息</para>
        /// </summary>
        /// <param name="planid">指定的预案ID</param>
        /// <param name="ids">指定的标注ID</param>
        /// <returns></returns>
        public int DeletePlanTags(int planid, params string[] ids)
        {
            var query = DeleteHandler.From<Model.PlanTag>().Where<Model.PlanTag>(t => t.PlanID == planid && t.TagID.In(ids));
            return query.Execute().ExecuteNonQuery();
        }

        /// <summary>
        /// 移除指定预案的指定的文档信息
        /// <para>该方法移除预案和文档的关系数据，并移除文档数据</para>
        /// </summary>
        /// <param name="planid">指定的预案ID</param>
        /// <param name="ids">指定的文档ID</param>
        /// <returns></returns>
        public int DeletePlanFiles(int planid, params string[] ids)
        {
            using (var tran = new System.Transactions.TransactionScope())
            {
                //移除文档数据记录
                var result = FileHandler.Handler.DeleteEntities(ids);
                if (result < 0) return -1;
                //移除文档数据与预案关联数据记录
                var query = DeleteHandler.From<Model.PlanFile>().Where<Model.PlanFile>(t => t.PlanID == planid && t.FileID.In(ids));
                result += query.Execute().ExecuteNonQuery();
                tran.Complete();
                return result;
            }
        }

        /// <summary>
        /// 保存预案的标注信息
        /// </summary>
        /// <param name="t">标注信息</param>
        /// <param name="planid">指定的预案信息</param>
        /// <returns></returns>
        public int InsertEntity(Model.Tag t, int planid)
        {
            if (planid <= 0) return -1;
            if (t == null) return -1;
            if (string.IsNullOrEmpty(t.Name) || string.IsNullOrEmpty(t.Coordinates)) return -1;
            var result = -1;
            using (var tran = new System.Transactions.TransactionScope())
            {
                result = Dao.TagHandler.Handler.InsertEntity(t);
                if (result <= 0) return result;
                t = GetEntity<Model.Tag>(x => x.Color == t.Color
                    && x.Coordinates == t.Coordinates
                    && x.Description == t.Description
                    && x.IconCls == t.IconCls
                    && x.Name == t.Name
                    && x.Type == t.Type
                    && x.X == t.X
                    && x.Y == t.Y);
                var query = InsertHandler.Into<Model.PlanTag>()
                    .Table("TagID", "PlanID")
                    .Values(t.ID, planid);
                result = query.Execute().ExecuteNonQuery();
                tran.Complete();
            }
            return result;
        }

        /// <summary>
        /// 保存预案的文档信息
        /// </summary>
        /// <param name="f">文档信息</param>
        /// <param name="planid">指定的预案信息</param>
        /// <returns></returns>
        public int InsertEntity(Model.File f, int planid)
        {
            if (planid <= 0) return -1;
            if (f == null) return -1;
            if (string.IsNullOrEmpty(f.Alias) || string.IsNullOrEmpty(f.Name) || string.IsNullOrEmpty(f.Suffix)) return -1;
            var result = -1;
            using (var tran = new System.Transactions.TransactionScope())
            {
                if (Dao.FileHandler.Handler.InsertEntity(f) <= 0) return -1;
                f = GetEntity<Model.File>(t => t.Alias == f.Alias
                    && t.Name == f.Name
                    && t.Path == f.Path
                    && t.Suffix == f.Suffix);
                result = InsertHandler.Into<Model.PlanFile>()
                    .Table("FileID", "PlanID")
                    .Values(f.ID, planid)
                    .Execute().ExecuteNonQuery();
                tran.Complete();
            }
            return result;
        }

        /// <summary>
        /// 批量添加预案和预案文件信息
        /// </summary>
        /// <param name="files"></param>
        /// <param name="plan"></param>
        /// <returns></returns>
        public int InsertEntity(Model.Plan plan, params Model.File[] files)
        {
            var result = -1;
            if (plan == null) return result;
            using (var tran = new System.Transactions.TransactionScope())
            {
                result = InsertEntity(plan);
                if (result <= 0 || files.Length == 0)
                {
                    tran.Complete();
                    return result;
                }
                plan = GetEntity<Model.Plan>(t => t.Description == plan.Description && t.Name == plan.Name);
                for (var i = 0; i < files.Length; i++)
                {
                    var file = files[i];
                    if (file != null && FileHandler.Handler.InsertEntity(file) > 0)
                    {
                        file = GetEntity<Model.File>(t => t.Alias == file.Alias && t.Name == file.Name && t.Path == file.Path && t.Suffix == file.Suffix);
                        result += InsertEntity(new Model.PlanFile() { PlanID = plan.ID, FileID = file.ID });
                    }
                }
                tran.Complete();
                return result;
            }
        }

        /// <summary>
        /// 批量添加文件到指定预案
        /// </summary>
        /// <param name="files"></param>
        /// <param name="planid"></param>
        /// <returns></returns>
        public int InsertEntity(Model.File[] files, int planid)
        {
            using (var tran = new System.Transactions.TransactionScope())
            {
                var result = 0;
                var flag = true;
                for (var i = 0; files != null && i < files.Length; i++)
                {
                    var file = files[i];
                    if (file == null) continue;
                    if (FileHandler.Handler.InsertEntity(file) <= 0)
                    {
                        flag = false;
                        break;
                    }
                    file = GetEntity<Model.File>(t => t.Alias == file.Alias && t.Name == file.Name && t.Path == file.Path && t.Suffix == file.Suffix);
                    if (InsertEntity(new Model.PlanFile() { FileID = file.ID, PlanID = planid }) <= 0)
                    {
                        flag = false;
                        break;
                    }
                    result++;
                }
                //所有的数据提交成功，返回受影响的行数，否者返回-1
                if (flag)
                {
                    tran.Complete();
                    return result;
                }
                return -1;
            }
        }

        /// <summary>
        /// 添加预案文件记录
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private int InsertEntity(Model.PlanFile e)
        {
            return InsertHandler.Into<Model.PlanFile>()
                .Table("FileID", "PlanID")
                .Values(e.FileID, e.PlanID)
                .Execute().ExecuteNonQuery();
        }

        /// <summary>
        /// 新增预案标注记录
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private int InsertEntity(Model.PlanTag e)
        {
            return InsertHandler.Into<Model.PlanTag>()
                .Table("PlanID", "TagID")
                .Values(e.PlanID, e.TagID)
                .Execute()
                .ExecuteNonQuery();
        }

        /// <summary>
        /// 添加新的预案记录
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int InsertEntity(Model.Plan e)
        {
            var query = InsertHandler.Into<Model.Plan>()
                .Table("Description", "Name")
                .Values(e.Description, e.Name);
            return query.Execute().ExecuteNonQuery();
        }

        /// <summary>
        /// 更新指定的预案记录
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int UpdateEntity(Model.Plan e)
        {
            var query = UpdateHandler.Table<Model.Plan>()
                .Set("Description").EqualTo(e.Description)
                .Set("Name").EqualTo(e.Name);
            var handler = query.Where<Model.Plan>(t => t.ID == e.ID).Execute();
            return handler.ExecuteNonQuery();
        }

        /// <summary>
        /// 移除指定ID的预案记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public override int DeleteEntities(params string[] ids)
        {
            var result = 0;
            using (var tran = new System.Transactions.TransactionScope())
            {
                try
                {
                    //移除文件信息
                    result += DeleteHandler.From<Model.File>().Where(string.Format("Pgis_File.ID in({0})",
                        SelectHandler.Columns("Pgis_PlanFile.FileID").From<Model.PlanFile>().Where<Model.PlanFile>(x => x.PlanID.In(ids)).Execute().CommandText))
                        .Execute().ExecuteNonQuery();
                    //移除预案文件关联信息
                    result += DeleteHandler.From<Model.PlanFile>().Where<Model.PlanFile>(t => t.PlanID.In(ids)).Execute().ExecuteNonQuery();
                    //移除预案标注关联信息，此处不删除标注信息，提供标注重用
                    result += DeleteHandler.From<Model.PlanTag>().Where<Model.PlanTag>(t => t.PlanID.In(ids)).Execute().ExecuteNonQuery();
                    //移除预案数据记录
                    result += DeleteHandler.From<Model.Plan>().Where<Model.Plan>(t => t.ID.In(ids)).Execute().ExecuteNonQuery();
                    //事务提交
                    tran.Complete();
                }
                catch { result = -1; }
            }
            return result;
        }

        /// <summary>
        /// 移除指定的文档信息
        /// </summary>
        /// <param name="files">指定的文档</param>
        /// <returns></returns>
        public int DeleteEntities(params Model.File[] files)
        {
            if (files.Length == 0) return 0;
            var result = -1;
            using (var tran = new System.Transactions.TransactionScope())
            {
                try
                {
                    var ids = (from t in files select t.ID.ToString()).ToArray();
                    //移除预案关联数据
                    result += DeleteHandler.From<Model.PlanFile>().Where<Model.PlanFile>(t => t.FileID.In(ids)).Execute().ExecuteNonQuery();
                    //移除文件数据
                    result += FileHandler.Handler.DeleteEntities(ids);
                    //事务提交
                    tran.Complete();
                }
                catch { result = -1; }
            }
            return result;
        }

        /// <summary>
        /// 获取预案内部其他数据
        /// <para>包含但不限于预案文档数据，预案地图标注数据...</para>
        /// </summary>
        /// <param name="items"></param>
        private void GetInfo(ref List<Model.Plan> items)
        {
            var ids = (from t in items select t.ID.ToString()).Distinct().ToArray();
            var tags = GetPlanTags(ids);
            var files = GetPlanFiles(ids);

            items.ForEach(t =>
            {
                t.AddRange(tags);
                t.AddRange(files);
            });
        }
    }
}
