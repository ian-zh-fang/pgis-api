using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace COM.TIGER.PGIS.WEBAPI.Tests.DAL
{
    [TestClass]
    public class StreetNum
    {
        [TestMethod]
        public void GetEntities()
        {
            var e = new Model.StreetNum()
            {
                EndTime = DateTime.Now,
                Name = "501",
                StreetID = 1,
                StreetName = "ttt",
                StreetTypeName = ""
            };

            var items = Dao.StreetNumHandler.Handler.GetEntities();
            var rst = Dao.StreetNumHandler.Handler.DeleteEntities((from t in items select t.ID.ToString()).ToArray());
            Assert.IsTrue(rst >= 0);
            Assert.IsTrue(Dao.StreetNumHandler.Handler.InsertEntity(e) > 0);
            Assert.IsTrue(Dao.StreetNumHandler.Handler.InsertEntity(e) < 0);
            e.Name = "502";
            Assert.IsTrue(Dao.StreetNumHandler.Handler.InsertEntity(e) > 0);
            items = Dao.StreetNumHandler.Handler.GetEntities();
            Assert.IsTrue(items.Count == 2);
            Assert.IsTrue(Dao.StreetNumHandler.Handler.GetEntities(1).Count == 2);
            e = Dao.StreetNumHandler.Handler.GetEntity(items[0].ID);
            Assert.IsNotNull(e);
            e.Name = "502";
            Assert.IsTrue(Dao.StreetNumHandler.Handler.UpdateEntity(e) < 0);
            e.Name = "503";
            rst = Dao.StreetNumHandler.Handler.UpdateEntity(e);
            Assert.IsTrue(rst == 1);
            Assert.IsTrue(Dao.StreetNumHandler.Handler.GetEntity(e.ID).Name == "503");
            Assert.IsTrue(Dao.StreetNumHandler.Handler.DeleteEntities((from t in items select t.ID.ToString()).ToArray()) == 2);
        }
    }
}
