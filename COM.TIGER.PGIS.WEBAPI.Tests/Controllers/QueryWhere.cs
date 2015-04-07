using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace COM.TIGER.PGIS.WEBAPI.Tests.Controllers
{
    using COM.TIGER.PGIS.WEBAPI.IDao;

    [TestClass]
    public class QueryWhere
    {
        [TestMethod]
        public void querywhereTestMethod1()
        {
            var qw = new IDao.QueryWhere();
            qw = (IDao.QueryWhere)qw.Where<Model.User>(t => t.Code == Guid.NewGuid().ToString() && (t.DepartmentID == 1 || t.Gender == 1));
            qw = (IDao.QueryWhere)qw.Where<Model.Role>(t => t.ID > GeneralID() && t.Remarks == DateTime.Now.ToString());
            Assert.IsFalse(string.IsNullOrWhiteSpace(qw.Result));
        }

        private int GeneralID()
        {
            return new Random().Next(100, 99999999);
        }
    }
}
