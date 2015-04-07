using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using COM.TIGER.PGIS.WEBAPI.Dao;

namespace COM.TIGER.PGIS.WEBAPI.Tests.DAL
{
    [TestClass]
    public class GPSHandlerUnitTest
    {
        [TestMethod]
        public void GetDevicesCurrentPosition()
        {
            var data = GPSHandler.Handler.GetDevicesCurrentPosition();

            Assert.IsNotNull(data);
            Assert.IsTrue(data.Count > 0);
            Assert.IsTrue(data.Count == 11);
        }
    }
}
