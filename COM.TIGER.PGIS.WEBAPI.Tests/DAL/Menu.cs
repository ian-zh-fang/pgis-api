using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace COM.TIGER.PGIS.WEBAPI.Tests.DAL
{
    [TestClass]
    public class Menu
    {
        [TestMethod]
        public void menuTestMethod1()
        {
            var e = new Model.Menu() { Text = "tt" };
            var r = Dao.MenuHandler.Handler.InsertNew(e);
            Assert.IsTrue(r > 0);
            var o = Dao.MenuHandler.Handler.InsertEntity(e);
            Assert.IsNotNull(o);

            e.Code = "tk";
            r = Dao.MenuHandler.Handler.UpdateNew(o.ID, e);
            Assert.IsTrue(r > 0);
            o = Dao.MenuHandler.Handler.UpdateEntity(o.ID, e);
            Assert.IsNotNull(o);

            r = Dao.MenuHandler.Handler.DeleteEntity(o.ID);
            Assert.IsTrue(r > 0);
        }
    }
}
