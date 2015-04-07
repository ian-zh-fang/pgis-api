using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace COM.TIGER.PGIS.WEBAPI.Tests.DAL
{
    [TestClass]
    public class OwnerPic:Dao.DBase
    {
        [TestMethod]
        public void Test()
        {
            var e = new Model.OwnerPic()
            {
                MOP_MOI_ID = 10000000,
                MOP_ImgName = "ttt",
                MOP_ImgPath = "/ttt",
                MOP_ImgTitle = "ttt"
            };
            DeleteHandler.From<Model.OwnerPic>().Where<Model.OwnerPic>(t => t.MOP_MOI_ID.In("10000000")).Execute().ExecuteNonQuery();
            Assert.IsTrue(Dao.OwnerPicHandler.Handler.InsertEntity(e) > 0);
            e = GetEntity<Model.OwnerPic>(t => t.JID == e.JID &&
                t.MOP_Createdate == e.MOP_Createdate &&
                t.MOP_ImgDefault == e.MOP_ImgDefault &&
                t.MOP_ImgName == e.MOP_ImgName &&
                t.MOP_ImgPath == e.MOP_ImgPath &&
                t.MOP_ImgRemark == e.MOP_ImgRemark &&
                t.MOP_ImgTitle == e.MOP_ImgTitle &&
                t.MOP_MOI_ID == e.MOP_MOI_ID &&
                t.MOP_Sort == e.MOP_Sort &&
                t.MOP_Updatedate == e.MOP_Updatedate);
            Assert.IsNotNull(e);
            e.MOP_Sort = 10;
            Assert.IsTrue(Dao.OwnerPicHandler.Handler.UpdateEntity(e) > 0);
            var items = Dao.OwnerPicHandler.Handler.GetEntities();
            Assert.IsTrue(items.Count > 0);
            items = Dao.OwnerPicHandler.Handler.GetEntities("1");
            Assert.IsTrue(items.Count > 0);
            var records = 0;
            items = Dao.OwnerPicHandler.Handler.Page(1, 10, out records);
            Assert.IsTrue(records > 0);
            items = Dao.OwnerPicHandler.Handler.Page(1, 10, out records, "10000000");
            Assert.IsTrue(records > 0);
            Assert.IsTrue(Dao.OwnerPicHandler.Handler.DeleteEntities(e.MOP_ID.ToString()) > 0);
        }
    }

    public static class T_SQL_Helper
    {
        public static bool Like(this string str, string val)
        {
            return true;
        }

        public static bool In(this object obj, params string[] items)
        {
            return true;
        }

        public static bool NotLike(this string str, string val)
        {
            return true;
        }

        public static bool NotIn(this object obj, params string[] items)
        {
            return true;
        }
    }
}
