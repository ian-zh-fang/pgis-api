using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace COM.TIGER.PGIS.WEBAPI.Tests.DAL
{
    [TestClass]
    public class User
    {
        [TestMethod]
        public void userTestMethod1()
        {
            var e = new Model.User() { UserName = "account", Password = "pwd", Name = "name", Disabled = 1, DepartmentID = 0, Code = "code" };
            var r = Dao.UserHandler.Handler.InsertNew(e);
            Assert.IsTrue(r > 0);
            var o = Dao.UserHandler.Handler.InsertEntity(e);
            Assert.IsNotNull(o);

            e.DepartmentID = 1;
            r = Dao.UserHandler.Handler.UpdateNew(o.ID, e);
            Assert.IsTrue(r > 0);
            o = Dao.UserHandler.Handler.UpdateEntity(o.ID, e);
            Assert.IsNotNull(o);

            r = Dao.UserHandler.Handler.DeleteEntity(o.ID);
            Assert.IsTrue(r > 0);
        }
    }
}
