using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace COM.TIGER.PGIS.WEBAPI.Tests.Sql
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test()
        {
            var arr = new string[0];
            var buffer = new string[] { "1", "2", "3"};
            arr = arr.Concat(buffer).ToArray();
            Assert.IsTrue(arr.Length == 3);
            arr = arr.Concat(buffer).ToArray();
            Assert.IsTrue(arr.Length == 6);
        }
    }
}
