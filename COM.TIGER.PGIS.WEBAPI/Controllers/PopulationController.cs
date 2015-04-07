using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class PopulationController : BaseApiController
    {
        /// <summary>
        /// 获取所有的人员信息
        /// </summary>
        /// <returns></returns>
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.PopulationBasicInfo>> GetEntities()
        {
            var data = Dao.PopulationHandler.Handler.GetEntities();
            return ResultOk<List<Model.PopulationBasicInfo>>(data);
        }

        /// <summary>
        /// 批量获取指定ID的人员信息
        /// </summary>
        /// <param name="ids">人员ID组，以“，”分隔</param>
        /// <returns></returns>
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.PopulationBasicInfo>> GetEntities(string ids)
        {
            var data = Dao.PopulationHandler.Handler.GetEntities(ids);
            return ResultOk<List<Model.PopulationBasicInfo>>(data);
        }

        /// <summary>
        /// 分页所有的人员信息，并获取当前页码的数据信息
        /// </summary>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <returns></returns>
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<PagingModel<Model.PopulationBasicInfo>> PagingPopulationBasicInfos(int index, int size)
        {
            var records = 0;
            var data = Dao.PopulationHandler.Handler.Page(index, size, out records);
            return ResultPagingEx<Model.PopulationBasicInfo>(data, records);
        }

        /// <summary>
        /// 分页指定名称的人员信息，并获取当前页码的数据信息
        /// </summary>
        /// <param name="name">人员名称</param>
        /// <param name="index">当前页码，从1开始</param>
        /// <param name="size">每页条目数</param>
        /// <returns></returns>
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<PagingModel<Model.PopulationBasicInfo>> PagingPopulationBasicInfos(string name, int index, int size)
        {
            var records = 0;
            var data = Dao.PopulationHandler.Handler.Page(name, index, size, out records);
            return ResultPagingEx<Model.PopulationBasicInfo>(data, records);
        }

        /// <summary>
        /// 分页指定名称的人员信息，并获取当前页码的数据信息
        /// </summary>
        /// <param name="name">人员名称或者证件号码</param>
        /// <param name="index">当前页码，从1开始</param>
        /// <param name="size">每页条目数</param>
        /// <returns></returns>
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<PagingModel<Model.PopulationBasicInfo>> PagePopulation(string query, int index, int size)
        {
            var records = 0;
            var data = Dao.PopulationHandler.Handler.PagePopulation(query, index, size, out records);
            return ResultPagingEx<Model.PopulationBasicInfo>(data, records);
        }

        /// <summary>
        /// 获取指定ID的人员信息
        /// </summary>
        /// <param name="id">指定人员IID</param>
        /// <returns></returns>
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<Model.PopulationBasicInfo> GetEntity(int id)
        {
            var data = Dao.PopulationHandler.Handler.GetEntity(id);
            return ResultOk<Model.PopulationBasicInfo>(data);
        }

        /// <summary>
        /// 获取指定身份证编号的人员信息
        /// </summary>
        /// <param name="cardno">指定人员身份证编号</param>
        /// <returns></returns>
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<Model.PopulationBasicInfo> GetEntity(string cardno)
        {
            var data = Dao.PopulationHandler.Handler.GetEntity(cardno);
            return ResultOk<Model.PopulationBasicInfo>(data);
        }

        /// <summary>
        /// 获取指定户号的户籍信息和当前户籍人员信息
        /// </summary>
        /// <param name="houseno">户籍编号</param>
        /// <returns></returns>
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.PopulationBasicInfo>> GetHouseOlder(string houseno)
        {
            var data = Dao.PopulationHandler.Handler.GetHouseolder(houseno);
            return ResultOk<List<Model.PopulationBasicInfo>>(data);
        }

        /// <summary>
        /// 获取指定人员的移动轨迹记录
        /// </summary>
        /// <param name="id">指定人员ID</param>
        /// <returns></returns>
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.TemporaryPopulation>> GetMoveRecords(int id)
        {
            var data = Dao.PopulationHandler.Handler.GetMoveRecords(id);
            return ResultOk<List<Model.TemporaryPopulation>>(data);
        }

        /// <summary>
        /// 获取指定人员的入境记录
        /// </summary>
        /// <param name="id">指定人员ID</param>
        /// <returns></returns>
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.AbroadPerson>> GetAbroadRecords(int id)
        {
            var data = Dao.PopulationHandler.Handler.GetAbroadRecords(id);
            return ResultOk<List<Model.AbroadPerson>>(data);
        }

        /// <summary>
        /// 分页实有人口指定姓名，住址基本信息，并获取当前页码数据
        /// <para>一般性查询</para>
        /// </summary>
        /// <param name="name">人员姓名</param>
        /// <param name="address">住址</param>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="records">当前查询总条目数</param>
        /// <returns></returns>
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<PagingModel<Model.PopulationBasicInfo>> PagingEntities(string name, string address, int index, int size)
        {
            var records = 0;
            var data = Dao.PopulationHandler.Handler.PageEntities(name, address, index, size, out records);
            return ResultPagingEx<Model.PopulationBasicInfo>(data, records);
        }

        /// <summary>
        /// 分页查询指定人员姓名，当前住址基本信息，并获取当前页码数据
        /// <para>高级查询</para>
        /// </summary>
        /// <param name="name">人员姓名</param>
        /// <param name="address">当前住址或者家庭住址</param>
        /// <param name="domicile">户籍地址</param>
        /// <param name="genderid">性别</param>
        /// <param name="educationid">学历</param>
        /// <param name="marriageid">婚姻状况</param>
        /// <param name="excuageid">兵役状况</param>
        /// <param name="politicalstatusid">政治面貌</param>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="records">当前查询总条目数</param>
        /// <returns></returns>
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<PagingModel<Model.PopulationBasicInfo>> PagingEntities(string name, string address, string domicile,
            int genderid, int educationid, int marriageid, int excuageid, int politicalstatusid, int index, int size)
        {
            var records = 0;
            var data = Dao.PopulationHandler.Handler.PageEntities(name, address, domicile, genderid, educationid, 
                marriageid, excuageid, politicalstatusid, index, size, out records);
            return ResultPagingEx<Model.PopulationBasicInfo>(data, records);
        }

        /// <summary>
        /// 分页查询常驻指定人员姓名，当前住址基本信息，并获取当前页码数据
        /// <para>一般查询</para>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <param name="records">当前查询总条目数</param>
        /// <returns></returns>
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<PagingModel<Model.PopulationBasicInfo>> PagingCZ(string name, string address, int index, int size)
        {
            var records = 0;
            var data = Dao.PopulationHandler.Handler.PageCZ(name, address, index, size, out records);
            return ResultPagingEx<Model.PopulationBasicInfo>(data, records);
        }

        /// <summary>
        /// 分页查询常驻指定人员姓名，当前住址基本信息，并获取当前页码数据
        /// <para>高级查询</para>
        /// </summary>
        /// <param name="name">人员姓名</param>
        /// <param name="address">当前住址或者家庭住址</param>
        /// <param name="domicile">户籍地址</param>
        /// <param name="genderid">性别</param>
        /// <param name="educationid">学历</param>
        /// <param name="marriageid">婚姻状况</param>
        /// <param name="excuageid">兵役状况</param>
        /// <param name="politicalstatusid">政治面貌</param>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="records">当前查询总条目数</param>
        /// <returns></returns>
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<PagingModel<Model.PopulationBasicInfo>> PagingCZ(string name, string address, string domicile,
            int genderid, int educationid, int marriageid, int excuageid, int politicalstatusid, int index, int size)
        {
            var records = 0;
            var data = Dao.PopulationHandler.Handler.PageCZ(name, address, domicile, genderid, educationid,
                marriageid, excuageid, politicalstatusid, index, size, out records);
            return ResultPagingEx<Model.PopulationBasicInfo>(data, records);
        }

        /// <summary>
        /// 分页查询暂住指定人员名称，居住证号，房东姓名，房东电话，居住地址基本信息，并获取当前页码数据记录
        /// </summary>
        /// <param name="name">人员姓名</param>
        /// <param name="cardno">居住证号</param>
        /// <param name="houseoldname">房东名称</param>
        /// <param name="houseoldtel">房东联系电话</param>
        /// <param name="address">居住地址</param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <param name="records">当前查询总条目数</param>
        /// <returns></returns>
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<PagingModel<Model.PopulationBasicInfo>> PagingZZ(string name, string cardno, string houseoldname, string houseoldtel, string address, int index, int size)
        {
            var records = 0;
            var data = Dao.PopulationHandler.Handler.PageZZ(name, cardno, houseoldname, houseoldtel, address, index, size, out records);
            return ResultPagingEx<Model.PopulationBasicInfo>(data, records);
        }

        /// <summary>
        /// 分页境外人员查询指定中文姓名，英文姓名，国籍，有效证件类型，有效证件类型编号，签证类型，签证编号，入境口岸吗，当前地址基本信息，并获取当前页码数据记录
        /// </summary>
        /// <param name="name">中文姓名</param>
        /// <param name="firstname">英文名</param>
        /// <param name="lastname">英文姓</param>
        /// <param name="countryid">国籍ID</param>
        /// <param name="cardtypeid">有效证件类型ID</param>
        /// <param name="cardtypeno">有效证件编号</param>
        /// <param name="visatypeid">签证类型ID</param>
        /// <param name="visano">签证编号</param>
        /// <param name="entryport">入境口岸</param>
        /// <param name="address">当前住址</param>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="records">当前查询总条目数</param>
        /// <returns></returns>
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<PagingModel<Model.PopulationBasicInfo>> PagingJW(string name, string firstname, string lastname, int countryid, 
            int cardtypeid, string cardtypeno, int visatypeid, string visano, string entryport, 
            string address, int index, int size)
        {
            var records = 0;
            var data = Dao.PopulationHandler.Handler.PageJW(name, firstname, lastname, countryid, cardtypeid,
                cardtypeno, visatypeid, visano, entryport, address, index, size, out records);
            return ResultPagingEx<Model.PopulationBasicInfo>(data, records);
        }

        /// <summary>
        /// 分页查询重点指定人员名称，重点类别，居住地址基本信息，并获取当前页码数据记录
        /// </summary>
        /// <param name="name">人员姓名</param>
        /// <param name="importtypeid">重点类别ID</param>
        /// <param name="address">居住地址</param>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="records">当前查询总条目数</param>
        /// <param name="records">当前查询总条目数</param>
        /// <returns></returns>
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<PagingModel<Model.PopulationBasicInfo>> PagingZD(string name, int importtypeid, string address, int index, int size)
        {
            var records = 0;
            var data = Dao.PopulationHandler.Handler.PageZD(name, importtypeid, address, index, size, out records);
            return ResultPagingEx<Model.PopulationBasicInfo>(data, records);
        }

        /// <summary>
        /// 分页框选查询数据，并获取当前页码的数据信息
        /// </summary>
        /// <param name="x1">左上角横坐标</param>
        /// <param name="y1">左上角纵坐标</param>
        /// <param name="x2">右下角横坐标</param>
        /// <param name="y2">右下角纵坐标</param>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="records">当前查询总条目数</param>
        /// <param name="czCount">当前查询常住人口数</param>
        /// <param name="zzCount">当前查询暂住人口数</param>
        /// <param name="jwCount">当前查询境外人口数</param>
        /// <param name="zdCount">当前查询重点人口数</param>
        /// <returns></returns>
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<PopulationKX> PagingKX(int x1, int y1, int x2, int y2, int index, int size)
        {
            int records, czCount, zzCount, jwCount, zdCount;
            var data = Dao.PopulationHandler.Handler.PageKX(x1, y1, x2, y2, index, size, 
                out records, out czCount, out zzCount, out jwCount, out zdCount);

            PopulationKX obj = new PopulationKX()
            {
                Data = data,
                CZCount = czCount,
                ZZCount = zzCount,
                JWCount = jwCount,
                ZDCount = zdCount,
                TotalRecords = records
            };

            return ResultPagingEx(obj);
        }

        protected ApiResult<PopulationKX> ResultPagingEx(PopulationKX t)
        {
            return ResultOk<PopulationKX>(t);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Maptheme>> GetGroupByOwnerinfo(int x1, int y1, int x2, int y2, int mod)
        {
            var data = Dao.PopulationHandler.Handler.GetGroupByOwnerinfo(x1, y1, x2, y2, mod);
            return ResultOk<List<Model.Maptheme>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<Model.ElementHot> LoactionPopu(int addrid)
        {
            var data = Dao.PopulationHandler.Handler.LoactionPopu(addrid);
            return ResultOk<Model.ElementHot>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<PagingModel<Model.PopulationBasicInfoEx>> GetKXPopulation(string coords, int index, int size)
        {
            int records = 0;
            List<Model.PopulationBasicInfoEx> data = new List<Model.PopulationBasicInfoEx>();

            if (string.IsNullOrWhiteSpace(coords))
                return ResultPagingEx<Model.PopulationBasicInfoEx>(data, records);

            var coordinates = (from t in coords.Split(',') select double.Parse(t)).ToArray();
            double x1, y1, x2, y2;
            GetXY(coordinates, out x1, out y1, out x2, out y2);

            //矩形选择
            if (coordinates.Length == 4)
            {
                data = Dao.PopulationHandler.Handler.Query(x1, y1, x2, y2, index, size, out records);
            }

            //多边形选择
            if (coordinates.Length > 4)
            {
                //首先计算当前最大矩形框内的所有设备信息
                //其次计算当前多边形内的所有设备信息
                data = Dao.PopulationHandler.Handler.Query(coordinates, x1, y1, x2, y2, index, size, out records);
            }

            return ResultPagingEx<Model.PopulationBasicInfoEx>(data, records);
        }
    }
}
