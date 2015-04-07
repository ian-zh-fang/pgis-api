using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class BuildingController : BaseApiController
    {
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> PagingBuildings(string name, string address, int index, int size)
        {
            var records = 0;
            var buildings = Dao.BuildingHandler.Handler.Page(address, name, index, size, out records);
            return ResultPaging(buildings, records);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<Model.OwnerInfoEx> GetEntity(int id)
        {
            var e = Dao.BuildingHandler.Handler.Entity(id);
            return ResultOk<Model.OwnerInfoEx>(e);
        }
        
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertNewForJson(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.OwnerInfoEx>(v);
                return ResultOk<int>(Dao.BuildingHandler.Handler.InsertEntity(e));
            }
            catch (Exception e)
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> UpdateNewJson(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Building>(v);
                return ResultOk<int>(Dao.BuildingHandler.Handler.UpdateEntity(e));
            }
            catch (Exception e)
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteEntities(string ids)
        {
            var rst = Dao.BuildingHandler.Handler.DeleteEntities(ids);
            return ResultOk<int>(rst);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.OwnerInfoEx>> GetBuildings()
        {
            var data = Dao.BuildingHandler.Handler.GetEntities();
            return ResultOk<List<Model.OwnerInfoEx>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> PagingBuildings(string name, int index, int size)
        {
            return PagingBuildings(name, string.Empty, index, size);
        }

        /***************************************************************************
         *  
         * *************************************************************************
         */

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> AddOwnerInfo(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.OwnerInfoEx>(v);
                var data = Dao.BuildingHandler.Handler.AddOwnerInfo(e);
                return ResultOk<int>(data);
            }
            catch (Exception e) { return ResultException<int>(e.Message); }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> UpdateOwnerInfo(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.OwnerInfoEx>(v);
                var data = Dao.BuildingHandler.Handler.UpdateOwnerInfo(e);
                return ResultOk<int>(data);
            }
            catch (Exception e) { return ResultException<int>(e.Message); }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteOwnerInfos(string ids)
        {
            var data = Dao.BuildingHandler.Handler.DeleteOwnerInfos(ids);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> AddStruct(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Param>(v);

                var data = Dao.BuildingHandler.Handler.AddStruct(e);
                return ResultOk<int>(data);
            }
            catch (Exception e) { return ResultException<int>(e.Message); }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> UpdateStruct(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Param>(v);
                var data = Dao.BuildingHandler.Handler.UpdateStruct(e);
                return ResultOk<int>(data);
            }
            catch (Exception e) { return ResultException<int>(e.Message); }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteStructs(string ids)
        {
            var data = Dao.BuildingHandler.Handler.DeleteStructs(ids);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> AddUseType(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Param>(v);

                var data = Dao.BuildingHandler.Handler.AddUseType(e);
                return ResultOk<int>(data);
            }
            catch (Exception e) { return ResultException<int>(e.Message); }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> UpdateUseType(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Param>(v);
                var data = Dao.BuildingHandler.Handler.UpdateUseType(e);
                return ResultOk<int>(data);
            }
            catch (Exception e) { return ResultException<int>(e.Message); }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteUseTypes(string ids)
        {
            var data = Dao.BuildingHandler.Handler.DeleteUseTypes(ids);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> AddProperty(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Param>(v);

                var data = Dao.BuildingHandler.Handler.AddProperty(e);
                return ResultOk<int>(data);
            }
            catch (Exception e) { return ResultException<int>(e.Message); }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> UpdateProperty(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Param>(v);
                var data = Dao.BuildingHandler.Handler.UpdateProperty(e);
                return ResultOk<int>(data);
            }
            catch (Exception e) { return ResultException<int>(e.Message); }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteProperties(string ids)
        {
            var data = Dao.BuildingHandler.Handler.DeleteProperties(ids);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> AddUnit(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.UnitEx>(v);
                var data = Dao.BuildingHandler.Handler.AddUnit(e);
                return ResultOk<int>(data);
            }
            catch (Exception e) { return ResultException<int>(e.Message); }

        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> UpdateUnit(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.UnitEx>(v);
                var data = Dao.BuildingHandler.Handler.UpdateUnit(e);
                return ResultOk<int>(data);
            }
            catch (Exception e) { return ResultException<int>(e.Message); }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteUnits(string ids)
        {
            var data = Dao.BuildingHandler.Handler.DeleteUnits(ids);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Param>> GetStructs()
        {
            var data = Dao.BuildingHandler.Handler.GetStructs();
            return ResultOk<List<Model.Param>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Param>> GetUseTypes()
        {
            var data = Dao.BuildingHandler.Handler.GetUseTypes();
            return ResultOk<List<Model.Param>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Param>> GetProperties()
        {
            var data = Dao.BuildingHandler.Handler.GetProperties();
            return ResultOk<List<Model.Param>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Unit>> GetUnitsOnBuilding(string ids)
        {
            var data = Dao.BuildingHandler.Handler.GetUnitsOnBuilding(ids);
            return ResultOk<List<Model.Unit>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> AddRoom(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Rooms>(v);
                var data = Dao.BuildingHandler.Handler.AddRoom(e);
                return ResultOk<int>(data);
            }
            catch (Exception e) { return ResultException<int>(e.Message); }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> UpdateRoom(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Rooms>(v);
                var data = Dao.BuildingHandler.Handler.UpdateRoom(e);
                return ResultOk<int>(data);
            }
            catch (Exception e) { return ResultException<int>(e.Message); }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteRooms(string ids)
        {
            var data = Dao.BuildingHandler.Handler.DeleteRooms(ids);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Rooms>> GetRoomsOnUnit(string id)
        {
            var data = Dao.BuildingHandler.Handler.GetRoomsOnUnit(id);
            return ResultOk<List<Model.Rooms>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> AddPic(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.OwnerPic>(v);
                var data = Dao.BuildingHandler.Handler.AddPic(e);
                return ResultOk<int>(data);
            }
            catch (Exception e) { return ResultException<int>(e.Message); }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> UpdatePic(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.OwnerPic>(v);
                var data = Dao.BuildingHandler.Handler.UpdatePic(e);
                return ResultOk<int>(data);
            }
            catch (Exception e) { return ResultException<int>(e.Message); }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeletePics(string ids)
        {
            var data = Dao.BuildingHandler.Handler.DeletePics(ids);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.OwnerPic>> GetPics(string id)
        {
            var data = Dao.BuildingHandler.Handler.GetPics(id);
            return ResultOk<List<Model.OwnerPic>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> AddPopulation(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.PopulationBasicInfoEx>(v);
                var data = Dao.BuildingHandler.Handler.AddPopulation(e);
                return ResultOk<int>(data);
            }
            catch (Exception e) { return ResultException<int>(e.Message); }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> UpdatePopulation(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.PopulationBasicInfoEx>(v);
                var data = Dao.BuildingHandler.Handler.UpdatePopulation(e);
                return ResultOk<int>(data);
            }
            catch (Exception e) { return ResultException<int>(e.Message); }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeletePopulations(string ids)
        {
            var data = Dao.BuildingHandler.Handler.DeletePopulations(ids);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> GetPopulationsOnBuilding(string id, int index, int size)
        {
            var record = 0;
            var data = Dao.BuildingHandler.Handler.GetPopulationsOnBuilding(id, index, size, out record);
            return ResultPaging(data, record);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> GetPopulationsOnUnit(string id, int index, int size)
        {
            var record = 0;
            var data = Dao.BuildingHandler.Handler.GetPopulationsOnUnit(id, index, size, out record);
            return ResultPaging(data, record);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.PopulationBasicInfoEx>> GetPopulationsOnRoom(string id)
        {
            var data = Dao.BuildingHandler.Handler.GetPopulationsOnRoom(id);
            return ResultOk<List<Model.PopulationBasicInfoEx>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.PopulationBasicInfoEx>> GetHouseOwner(string n)
        {
            var data = Dao.PopulationHandler.Handler.GetHouseOwner(n);
            return ResultOk<List<Model.PopulationBasicInfoEx>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Rooms>> GetRoomsOnBuilding(string id)
        {
            var data = Dao.RoomHandler.Handler.GetEntitiesWithBuilding(id);
            return ResultOk<List<Model.Rooms>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> GetPopulationsOnBuildings(string id, int index, int size)
        {
            var records = 0;
            var data = new List<Model.PopulationBasicInfo>();
            return ResultPaging(data, records);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Company>> GetCompneysOnBuilding(int id)
        {
            var data = Dao.CompanyHandler.Handler.GetCompneysOnBuilding(id);
            return ResultOk<List<Model.Company>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Company>> GetCompneysOnUnit(int id)
        {
            var data = Dao.CompanyHandler.Handler.GetCompneysOnUnit(id);
            return ResultOk<List<Model.Company>>(data);
        }
        
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Company>> GetCompanysOnRoom(string id)
        {
            var data = Dao.CompanyHandler.Handler.GetCompanysOnRoom(id);
            return ResultOk<List<Model.Company>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Pop>> TotalPop(string id)
        {
            var data = Dao.PopulationHandler.Handler.TotalPop(id);
            return ResultOk<List<Model.Pop>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> CompanyAdd(string addr, string ids)
        {
            var data = Dao.BuildingHandler.Handler.CompanyAdd(addr, ids);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> PopuAdd(string addr, string ids)
        {
            var data = Dao.BuildingHandler.Handler.PopuAdd(addr, ids);
            return ResultOk<int>(data);
        }
    }
}
