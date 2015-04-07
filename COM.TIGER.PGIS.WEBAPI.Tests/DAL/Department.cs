using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace COM.TIGER.PGIS.WEBAPI.Tests.DAL
{
    [TestClass]
    public class Department
    {
        [TestMethod]
        public void departmentTestMethod1()
        {
            var e = new Model.Department() { Code = "code", Name = "department", PID = 0, Remarks = string.Empty };
            var r = Dao.DepartmentHandler.Handler.InsertNew(e);
            Assert.IsTrue(r > 0);
            var o = Dao.DepartmentHandler.Handler.InsertEntity(e);
            Assert.IsNotNull(o);

            e.Remarks = "Remarks";
            r = Dao.DepartmentHandler.Handler.UpdateNew(o.ID, e);
            Assert.IsTrue(r > 0);
            o = Dao.DepartmentHandler.Handler.UpdateEntity(o.ID, e);
            Assert.IsNotNull(o);

            r = Dao.DepartmentHandler.Handler.DeleteEntity(o.ID);
            Assert.IsTrue(r > 0);
        }
    }
}
