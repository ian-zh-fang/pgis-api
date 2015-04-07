using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace COM.TIGER.PGIS.WEBAPI.Tests.DAL
{
    [TestClass]
    public class Case
    {
        /// <summary>
        /// 三台合一报警记录查询测试
        /// </summary>
        [TestMethod]
        public void JCJ_JJDB()
        {
            int index = 1, size = 10, records = 0;
            string name = "ian", tel = "", addr = "", num="";
            DateTime? start = null, end = null;

            var list = Dao.JCJ_JJDBHandler.Handler.GetEntities();
            Assert.IsTrue(list.Count > 0);

            list = Dao.JCJ_JJDBHandler.Handler.Page(index, size, out records);
            Assert.IsTrue(list.Count > 0);
            Assert.IsTrue(records > 0);

            list = Dao.JCJ_JJDBHandler.Handler.Page(num, name, tel, addr, start, end, index, size, out records);
            Assert.IsTrue(list.Count > 0);
            Assert.IsTrue(records > 0);

            var e = Dao.JCJ_JJDBHandler.Handler.GetEntity(1);
            Assert.IsNotNull(e);

            var arr = Dao.JCJ_JJDBHandler.Handler.TotalByKinds();
            Assert.IsTrue(arr.Count > 0);

            arr = Dao.JCJ_JJDBHandler.Handler.TotalByKinds(1);
            Assert.IsTrue(arr.Count > 0);
        }

        /// <summary>
        /// 一键报警记录查询测试
        /// </summary>
        [TestMethod]
        public void YJBJ()
        {
            int index = 1, size = 10, records = 0;
            string name = "", tel = "", addr = "", num="";
            DateTime? start = null, end = null;

            var list = Dao.YJBJHandler.Handler.GetEntities();
            Assert.IsTrue(list.Count == 10);

            list = Dao.YJBJHandler.Handler.Page(index, size, out records);
            Assert.IsTrue(list.Count == 10);
            Assert.IsTrue(records == 10);

            list = Dao.YJBJHandler.Handler.Page(num, name, tel, addr, start, end, index, size, out records);
            Assert.IsTrue(list.Count == 10);
            Assert.IsTrue(records == 10);

            var e = Dao.YJBJHandler.Handler.GetEntity(13);
            Assert.IsNotNull(e);

            var arr = Dao.YJBJHandler.Handler.TotalByKinds();
            Assert.IsTrue(arr.Count > 0);

            arr = Dao.YJBJHandler.Handler.TotalByKinds(1);
            Assert.IsTrue(arr.Count == 1);
        }
    }
}
