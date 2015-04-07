using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace COM.TIGER.PGIS.WEBAPI.Tests.DAL
{
    [TestClass]
    public class Area
    {
        [TestMethod]
        public void AreaTestMethod1()
        {
            var e = new Model.Area() { BelongToID = 0, Name = "tt", NewCode = "nc", OldCode = "oc" };
            var r = Dao.AreaHandler.Handler.InsertNew(e);
            Assert.IsTrue(r > 0);
            var o = Dao.AreaHandler.Handler.InsertEntity(e);
            Assert.IsNotNull(o);

            e.PID = 1;
            r = Dao.AreaHandler.Handler.UpdateNew(o.ID, e);
            Assert.IsTrue(r > 0);
            o = Dao.AreaHandler.Handler.UpdateEntity(o.ID, e);
            Assert.IsNotNull(o);

            var items = Dao.AreaHandler.Handler.GetAreas();
            Assert.IsTrue(items.Count >= 0);
            if (items.Count > 0)
            {
                var ids = (from c in items select c.ID.ToString()).ToArray();
                r = Dao.AreaHandler.Handler.DeleteEntities(ids);
                Assert.IsTrue(r > 0);
            }
        }
    }
}
