using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace COM.TIGER.PGIS.WEBAPI.Tests.Controllers
{
    [TestClass]
    public class delete
    {
        [TestMethod]
        public void DeleteTest()
        {
            var r = Dao.AreaHandler.Handler.DeleteEntity(1);
            Assert.IsTrue(r > 0);
            r = Dao.AreaHandler.Handler.DeleteEntity(2);
            Assert.IsTrue(r > 0);
            r = Dao.AreaHandler.Handler.DeleteEntity(3);
            Assert.IsTrue(r > 0);
            r = Dao.AreaHandler.Handler.DeleteEntity(4);
            Assert.IsTrue(r > 0);
            r = Dao.AreaHandler.Handler.DeleteEntity(5);
            Assert.IsTrue(r > 0);
        }
    }
}
