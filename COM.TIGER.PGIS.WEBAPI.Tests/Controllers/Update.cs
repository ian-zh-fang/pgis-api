using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace COM.TIGER.PGIS.WEBAPI.Tests.Controllers
{
    [TestClass]
    public class Update
    {
        [TestMethod]
        public void UpdateTest()
        {
            var e = new Model.Area() { 
                BelongToID = 0, 
                Name = "tt1", 
                NewCode = "code_new", 
                OldCode = "code_old"
            };

            var r = Dao.AreaHandler.Handler.UpdateNew(1, e);
            Assert.IsTrue(r > 0);
            var o = Dao.AreaHandler.Handler.UpdateEntity(2, e);
            Assert.IsTrue(o.ID == 2);
        }
    }
}
