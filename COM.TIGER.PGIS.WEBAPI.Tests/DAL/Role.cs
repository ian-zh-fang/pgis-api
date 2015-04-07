using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace COM.TIGER.PGIS.WEBAPI.Tests.DAL
{
    [TestClass]
    public class Role
    {
        [TestMethod]
        public void roleTestMethod1()
        {
            var e = new Model.Role() { Name = "tt" };
            var r = Dao.RoleHandler.Handler.InsertNew(e);
            Assert.IsTrue(r > 0);
            var o = Dao.RoleHandler.Handler.InsertEntity(e);
            Assert.IsNotNull(o);

            e.Remarks = "tr";
            r = Dao.RoleHandler.Handler.UpdateNew(o.ID, e);
            Assert.IsTrue(r > 0);
            o = Dao.RoleHandler.Handler.UpdateEntity(o.ID, e);
            Assert.IsNotNull(o);

            r = Dao.RoleHandler.Handler.DeleteEntity(o.ID);
            Assert.IsTrue(r > 0);
        }
    }
}
