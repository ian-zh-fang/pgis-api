using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace COM.TIGER.PGIS.WEBAPI.Tests.DAL
{
    /// <summary>
    /// Street 的摘要说明
    /// </summary>
    [TestClass]
    public class Street
    {
        public Street()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性:
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void GetEntities()
        {
            //
            // TODO: 在此处添加测试逻辑
            //

            var e = new Model.Street()
            {
                AdminID = 1,
                AdminName = "admin",
                EndTime = DateTime.Now,
                Alias = "alias",
                FirstLetter = "t",
                LeftNumTypeName = "l",
                Name = "ttt",
                Position = "addr"
            };

            var items = Dao.StreetHandler.Handler.GetEntities();
            var rst = Dao.StreetHandler.Handler.DeleteEntities((from t in items select t.ID.ToString()).ToArray());
            Assert.IsTrue(rst >= 0);
            rst = Dao.StreetHandler.Handler.InsertEntity(e);
            Assert.IsTrue(rst > 0);
            rst = Dao.StreetHandler.Handler.InsertEntity(e);
            Assert.IsTrue(rst < 0);
            e.AdminID = 2;
            rst = Dao.StreetHandler.Handler.InsertEntity(e);
            Assert.IsTrue(rst > 0);
            items = Dao.StreetHandler.Handler.GetEntities();
            Assert.IsTrue(items.Count > 0);
            e = items[0];
            Assert.IsTrue(e.ID > 0);
            e = Dao.StreetHandler.Handler.GetEntity(e.ID);
            Assert.IsNotNull(e);
            Assert.IsNull(Dao.StreetHandler.Handler.GetEntity(0));
            rst = Dao.StreetHandler.Handler.DeleteEntities((from t in items select t.ID.ToString()).ToArray());
            Assert.IsTrue(rst > 0);
        }
    }
}
