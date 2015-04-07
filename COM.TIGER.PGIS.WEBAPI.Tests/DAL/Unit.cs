using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace COM.TIGER.PGIS.WEBAPI.Tests.DAL
{
    [TestClass]
    public class Unit
    {
        [TestMethod]
        public void Test()
        {
            var e = new Model.Unit()
            {
                OwnerInfoID = 1,
                UnitName = "ttt"
            };
            var items = Dao.UnitHandler.Handler.GetEntities();
            var rst = Dao.UnitHandler.Handler.DeleteEntities((from t in items select t.Unit_ID.ToString()).ToArray());
            Assert.IsTrue(rst >= 0);
            Assert.IsTrue(Dao.UnitHandler.Handler.InsertEntity(e) > 0);
            Assert.IsTrue(Dao.UnitHandler.Handler.InsertEntity(e) < 0);
            e.UnitName = "ttt2";
            Assert.IsTrue(Dao.UnitHandler.Handler.InsertEntity(e) > 0);
            items = Dao.UnitHandler.Handler.GetEntities("1", "2");
            Assert.IsTrue(items.Count > 0);
            var records = 0;
            Dao.UnitHandler.Handler.Page(1, 10, out records);
            Assert.IsTrue(records == 2);
            e = items[0];
            e.OwnerInfoID = 2;
            Assert.IsTrue(Dao.UnitHandler.Handler.UpdateEntity(e) > 0);
            Assert.IsTrue(Dao.UnitHandler.Handler.DeleteEntities((from t in items select t.Unit_ID.ToString()).ToArray()) > 0);
        }
    }
}
