using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace COM.TIGER.PGIS.WEBAPI.Tests.Controllers
{
    [TestClass]
    public class Select:Dao.DBase
    {
        [TestMethod]
        public void WhereInstanceTest()
        {
            var arr = GetParams();
            var p = GetParam(1);
            Assert.IsTrue(false);
        }

        /// <summary>
        /// 获取所有的参数项信息
        /// </summary>
        /// <returns></returns>
        public List<Model.Param> GetParams()
        {
            var query = SelectHandler.Columns().From<Model.Param>().Execute();
            //return ExecuteList<Model.Param>(query.ExecuteDataReader());
            return null;
        }

        /// <summary>
        /// 获取指定ID的参数项信息
        /// <para>该方法将根据第二个参数来决定是否获取子参数项</para>
        /// </summary>
        /// <param name="id">参数项标识符</param>
        /// <param name="flag">获取子参数项标识.TRUE标识需要获取子参数项.默认为TRUE</param>
        /// <returns></returns>
        public Model.Param GetParam(int id, bool flag = true)
        {
            if (flag) return GetParams(id);

            var query = SelectHandler.Columns().From<Model.Param>();
            var handler = query.IQueryWhere.Where<Model.Param>(t => t.ID == id).Where<IDao.ISelect>(query).Execute();
            //return ExecuteEntity<Model.Param>(handler.ExecuteDataReader());
            return null;
        }

        /// <summary>
        /// 获取指定ID的参数项信息
        /// <para>此方法会返回当前参数项的首层自参数项信息</para>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private Model.Param GetParams(int id)
        {
            var query = SelectHandler.Columns().From<Model.Param>();
            var handler = query.IQueryWhere
                .Where<Model.Param>(t => t.ID == id || t.PID == id).Where<IDao.ISelect>(query).Execute();
            //var list = ExecuteList<Model.Param>(handler.ExecuteDataReader());
            //var p = list.First(t => t.ID == id);
            //p.AddRange(list);
            //return p;
            return null;
        }
    }
}
