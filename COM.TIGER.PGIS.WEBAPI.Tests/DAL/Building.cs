using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace COM.TIGER.PGIS.WEBAPI.Tests.DAL
{
    [TestClass]
    public class Building:Dao.DBase
    {
        [TestMethod]
        public void Page()
        {
            var index = 1;
            var size = 10;
            var records = 0;
            var name = "";
            var address = "国道";

            var rst = Dao.BuildingHandler.Handler.Page(address, name, index, size, out records);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Entity()
        {
            var e = new Model.OwnerInfoEx()
            {
                Address = "adresstt",
                AdminID = 1000000,
                AdminName = "attt",
                MOI_LabelName = "ttt",
                MOI_OwnerName = "ttt"
            };

            DeleteHandler.From<Model.Building>().Where<Model.Building>(t => t.AdminID.In("1000000")).Execute().ExecuteNonQuery();
            Assert.IsTrue(Dao.BuildingHandler.Handler.InsertEntity(e) > 0);
            var items = Dao.BuildingHandler.Handler.GetEntities("1000000");
            Assert.IsTrue(items.Count > 0);
            e = items[0];

            var obj = ParseEntity<Model.Building>(e);
            obj.StreetID = 100000;
            Assert.IsTrue(Dao.BuildingHandler.Handler.UpdateEntity(obj) > 0);
            obj = GetEntity<Model.Building>(t => t.Building_ID == obj.Building_ID);
            Assert.IsNotNull(obj);
            var records = 0;
            items = Dao.BuildingHandler.Handler.Page("", "", 1, 10, out records);
            Assert.IsTrue(records > 0);
        }
        
        /// <summary>
        /// 将类型Model.OwnerInfoEx转换为目标类型
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="e">Model.OwnerInfoEx类型实例</param>
        /// <returns></returns>
        private T ParseEntity<T>(Model.OwnerInfoEx e) where T : new()
        {
            var t = new T();
            var targtp = e.GetType();
            var properties = t.GetType().GetProperties();
            object val = null;
            for (var i = 0; i < properties.Length; i++)
            {
                var property = properties[i];
                var targproperty = targtp.GetProperty(property.Name);
                if (targproperty != null)
                {
                    val = targproperty.GetValue(e, null);
                    property.SetValue(t, val, null);
                }
            }
            return t;
        }
    }
}
