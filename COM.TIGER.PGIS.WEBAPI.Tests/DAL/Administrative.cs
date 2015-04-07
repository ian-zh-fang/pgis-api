using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace COM.TIGER.PGIS.WEBAPI.Tests.DAL
{
    [TestClass]
    public class Administrative
    {
        [TestMethod]
        public void GetEntities()
        {
            var e = new Model.Administrative()
            {
                AreaID = 1,
                AreaName = "111",
                FirstLetter = "a",
                Name = "test",
                PID = 0
            };

            var items = Dao.AdministrativeHandler.Handler.GetEntities();
            var rst = Dao.AdministrativeHandler.Handler.DeleteEntities((from t in items select t.ID.ToString()).ToArray());
            Assert.IsTrue(rst >= 0);
            rst = Dao.AdministrativeHandler.Handler.InsertEntity(e);
            Assert.IsTrue(rst > 0);
            rst = Dao.AdministrativeHandler.Handler.InsertEntity(e);
            Assert.IsTrue(rst < 0);
            items = Dao.AdministrativeHandler.Handler.GetEntitiesTree();
            Assert.IsTrue(items.Count > 0);
            Assert.IsTrue(items[0].Items.Length == 0);
            var p = items[0];
            e.PID = p.ID;
            rst = Dao.AdministrativeHandler.Handler.InsertEntity(e);
            Assert.IsTrue(rst < 0);
            e.Name = "test2";
            rst = Dao.AdministrativeHandler.Handler.InsertEntity(e);
            Assert.IsTrue(rst > 0);
            items = Dao.AdministrativeHandler.Handler.GetEntitiesTree();
            Assert.IsTrue(items[0].Items.Length > 0);
            items = Dao.AdministrativeHandler.Handler.GetEntities();
            rst = Dao.AdministrativeHandler.Handler.DeleteEntities((from t in items select t.ID.ToString()).ToArray());
            Assert.IsTrue(rst > 0);
        }
    }
}
