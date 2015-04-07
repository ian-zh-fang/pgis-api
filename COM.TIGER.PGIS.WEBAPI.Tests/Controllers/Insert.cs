using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace COM.TIGER.PGIS.WEBAPI.Tests.Controllers
{
    [TestClass]
    public class Insert
    {
        [TestMethod]
        public void insertTestMethod1()
        {
            IDao.IInsert insert = new Dao.SqlServer.Insert();
            insert = insert.Into<Model.Area>();
            insert = insert.Table("id, pid, name");
            insert = insert.Values(1, 0, "aaa");
            Assert.IsFalse(string.IsNullOrWhiteSpace(insert.CommandText));
            insert = insert.Values(1, null, null);
            Assert.IsFalse(string.IsNullOrWhiteSpace(insert.CommandText));            
        }


        [TestMethod]
        public void InsertTest()
        {
            var e = new Model.Area() { BelongToID=0, Name="tt", NewCode="code_new", OldCode="code_old" };
            Assert.IsNotNull(e);
            //var r = Dao.AreaHandler.Handler.InsertNew(e);
            //Assert.IsTrue(r >= 0);
            var o = Dao.AreaHandler.Handler.InsertEntity(e);
            Assert.IsNotNull(o);
        }
    }
}
