using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace COM.TIGER.PGIS.WEBAPI.Tests.DAL
{
    [TestClass]
    public class Address
    {
        [TestMethod]
        public void Match()
        {
            var pattern = "浙江杭州西湖文三259";
            var matches = Dao.AddressHandler.Handler.Match(pattern);
            pattern = "浙江";
            matches = Dao.AddressHandler.Handler.Match(pattern);
            pattern = "浙江杭州";
            matches = Dao.AddressHandler.Handler.Match(pattern);
            pattern = "杭州西湖";
            matches = Dao.AddressHandler.Handler.Match(pattern);
            pattern = "浙江文三";
            matches = Dao.AddressHandler.Handler.Match(pattern);
            pattern = "文三";
            matches = Dao.AddressHandler.Handler.Match(pattern);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Add()
        {
            var e = new Model.Address() 
            {
                AdminID = 0,
                UnitID = 1,
                StreetID = 1,
                RoomID = 1,
                OwnerInfoID = 1,
                NumID = 1,
                Content = "浙江"
            };
            var rst = Dao.AddressHandler.Handler.Add(e);
            Assert.IsTrue(rst > 0);

            e.Content = "浙江杭州";
            rst = Dao.AddressHandler.Handler.Add(e);
            Assert.IsTrue(rst > 0);

            e.Content = "浙江杭州西湖";
            rst = Dao.AddressHandler.Handler.Add(e);
            Assert.IsTrue(rst > 0);

            e.Content = "杭州西湖";
            rst = Dao.AddressHandler.Handler.Add(e);
            Assert.IsTrue(rst > 0);

            e.Content = "浙江杭州西湖文三";
            rst = Dao.AddressHandler.Handler.Add(e);
            Assert.IsTrue(rst > 0);
        }
    }
}
