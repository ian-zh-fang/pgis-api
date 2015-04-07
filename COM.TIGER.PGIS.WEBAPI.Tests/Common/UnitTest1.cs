using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace COM.TIGER.PGIS.WEBAPI.Tests.Common
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test()
        {
            var i = -1;
            i *= i;
            Assert.IsTrue(i > 0);

            WEBAPI.Common.GDI.Point p1 = new WEBAPI.Common.GDI.Point() { X=10d, Y=10d };
            WEBAPI.Common.GDI.Point p2 = new WEBAPI.Common.GDI.Point() { X = 10d, Y = 11d };
            Assert.IsTrue(p1.Equals(p2));
        }

        [TestMethod]
        public void Test2()
        {
            var now = DateTime.Now;
            var other = now.Add(new TimeSpan(40000000000));

            var timespan = now - other;
            var timespananother = other - now;

            Assert.IsTrue(true);
        }
    }
}
