using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    /// <summary>
    /// 文件数据处理程序
    /// </summary>
    public class FileHandler:DBase
    {
        private static FileHandler _handler = null;
        /// <summary>
        /// 处理程序的唯一实例
        /// </summary>
        public static FileHandler Handler
        {
            get { return _handler = _handler ?? new FileHandler(); }
        }
        private FileHandler() { }

        /// <summary>
        /// 获取指定ID的所有文件集合
        /// <para>如果没有指定ID，程序将获取所有的文件数据</para>
        /// </summary>
        /// <param name="ids">指定的ID</param>
        /// <returns></returns>
        public List<Model.File> GetFiles(params string[] ids)
        {
            var query = SelectHandler.From<Model.File>();
            if (ids.Length == 0)
                return ExecuteList<Model.File>(query.Execute().ExecuteDataReader());

            var handler = query.IQueryWhere.Where<Model.File>(t => t.ID.In(ids)).Where<IDao.ISelect>(query).Execute();
            return ExecuteList<Model.File>(handler.ExecuteDataReader());
        }

        /// <summary>
        /// 分页所有的文件数据，并获取指定页码的数据
        /// </summary>
        /// <param name="index">页码</param>
        /// <param name="size">每页记录条目</param>
        /// <param name="records">总条目数</param>
        /// <returns></returns>
        public List<Model.File> Paging(int index, int size, out int records)
        {
            return Paging<Model.File>(index, size, null, OrderType.Desc, out records, "pgis_file.id");
        }

        /// <summary>
        /// 获取指定ID的文件数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.File GetFile(int id)
        {
            var query = SelectHandler.From<Model.File>();
            var handler = query.IQueryWhere.Where<Model.File>(t => t.ID == id).Where<IDao.ISelect>(query).Execute();
            return ExecuteEntity<Model.File>(handler.ExecuteDataReader());
        }

        /// <summary>
        /// 添加新的文件记录
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int InsertEntity(Model.File e)
        {
            var query = InsertHandler.Into<Model.File>()
                .Table("Alias", "Name", "Path", "Suffix")
                .Values(e.Alias, e.Name, e.Path, e.Suffix);
            return query.Execute().ExecuteNonQuery();
        }

        /// <summary>
        /// 更新指定的文件数据
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int UpdateEntity(Model.File e)
        {
            var query = UpdateHandler.Table<Model.File>()
                .Set("Suffix").EqualTo(e.Suffix)
                .Set("Path").EqualTo(e.Path)
                .Set("Name").EqualTo(e.Name)
                .Set("Alias").EqualTo(e.Alias);
            var handler = query.IQueryWhere.Where<Model.File>(t => t.ID == e.ID).Where<IDao.IUpdate>(query).Execute();
            return handler.ExecuteNonQuery();
        }

        /// <summary>
        /// 批量删除指定ID的文件数据
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public override int DeleteEntities(params string[] ids)
        {
            var query = DeleteHandler.From<Model.File>();
            var handler = query.IQueryWhere.Where<Model.File>(t => t.ID.In(ids)).Where<IDao.IDelete>(query).Execute();
            return handler.ExecuteNonQuery();
        }
    }
}
