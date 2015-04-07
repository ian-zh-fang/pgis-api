using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace COM.TIGER.PGIS.WEBAPI.Tests.DAL
{
    [TestClass]
    public class Param
    {
        [TestMethod]
        public void paramTestMethod1()
        {
            var e = new Model.Param() { Code="tc", Name="tt", Disabled=1, Sort=0};
            var r = Dao.ParamHandler.Handler.InsertNew(e);
            Assert.IsTrue(r > 0);
            var o = Dao.ParamHandler.Handler.InsertEntity(e);
            Assert.IsNotNull(o);

            e.Sort = 1;
            r = Dao.ParamHandler.Handler.UpdateNew(o.ID, e);
            Assert.IsTrue(r > 0);
            o = Dao.ParamHandler.Handler.UpdateEntity(o.ID, e);
            Assert.IsNotNull(o);

            r = Dao.ParamHandler.Handler.DeleteEntity(o.ID);
            Assert.IsTrue(r > 0);
        }
    }
}
