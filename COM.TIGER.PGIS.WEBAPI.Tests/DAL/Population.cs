using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace COM.TIGER.PGIS.WEBAPI.Tests.DAL
{
    [TestClass]
    public class Population
    {
        [TestMethod]
        public void Test()
        {

            /*************************************
             *  分页条件
             * ***********************************
             */
            var index = 1;
            var size = 10;
            var records = 0;

            /*************************************
             *  查询条件
             * ***********************************
             */

            var name = "ttt";
            var address = "西湖";
            var domicile = "";
            var gender = 0;
            var education = 0;
            var marriage = 0;
            var excuage = 0;
            var politicalstatus = 0;

            var list = Dao.PopulationHandler.Handler.PageEntities(name, address, index, size, out records);
            Assert.IsTrue(list.Count == 0);
            Assert.IsTrue(records == 0);

            list = Dao.PopulationHandler.Handler.PageEntities(name, address, domicile, gender, education, marriage, excuage, politicalstatus, index, size, out records);
            Assert.IsTrue(list.Count == 0);
            Assert.IsTrue(records == 0);

            /*************************************
             *  重点人口查询条件
             * ***********************************
             */

            var importid = 10;
            list = Dao.PopulationHandler.Handler.PageZD(name, importid, address, index, size, out records);
            Assert.IsTrue(list.Count == 0);
            Assert.IsTrue(records == 0);

            /*************************************
             *  框选人口查询条件
             * ***********************************
             */
            int x1 = 1000, y1 = 1000, x2 = 10000, y2 = 20000, 
                cz = 0, zz = 0, jw = 0, zd = 0;
            list = Dao.PopulationHandler.Handler.PageKX(x1, y1, x2, y2, index, size, out records,
                out cz, out zz, out jw, out zd);
            Assert.IsTrue(list.Count == 0);
            Assert.IsTrue(records == 0);
            Assert.IsTrue(cz == 0);
            Assert.IsTrue(zz == 0);
            Assert.IsTrue(jw == 0);
            Assert.IsTrue(zd == 0);

            /*************************************************
             *  入境人员信息查询
             * ***********************************************
             */

            string firstname = "ian", lastname = "fun",
                cardno = "000000000000000000", visano = "0000000000", entryport = "广州";
            int countryid = 1, cardid = 1, visaid = 1;
            list = Dao.PopulationHandler.Handler.PageJW(name, firstname, lastname, countryid, cardid, cardno, visaid, visano, entryport, address, index, size, out records);
            Assert.IsTrue(list.Count == 0);
            Assert.IsTrue(records == 0);
        }
    }
}
