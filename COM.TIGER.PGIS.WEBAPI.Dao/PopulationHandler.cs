using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    //问题：该条件查询是根据人员的家庭地址，或是当前地址，或是单位地址？
    /// <summary>
    /// 人口数据处理程序
    /// <para>查询所有人员信息</para>
    /// <para>分页查询所有人员信息</para>
    /// <para>查询指定大楼的所有人员信息</para>
    /// <para>分页查询指定大楼的所有人员信息</para>
    /// <para>获取指定房间的所有人员信息</para>
    /// <para>获取指定地址的所有人员信息</para>
    /// <para>分页获取自定地址的所有人员信息</para>
    /// <para>获取指定ID的人员信息</para>
    /// <para>获取指定身份证号码的人员信息</para>
    /// <para>获取指定人员的户籍信息（包含当前人员在内的当事人所在当前户口的所有人员信息）</para>
    /// <para></para>
    /// <para></para>
    /// </summary>
    public class PopulationHandler : DBase
    {
        private const string SORTFIELD = "Pgis_PopulationBasicInfo.id";

        //setting singleton instance
        private PopulationHandler() { }
        private static PopulationHandler _instance;
        /// <summary>
        /// singleton instance
        /// </summary>
        public static PopulationHandler Handler
        {
            get { return _instance = _instance ?? new PopulationHandler(); }
        }

        /// <summary>
        /// 获取所有的人员基本信息记录
        /// </summary>
        /// <returns></returns>
        public List<Model.PopulationBasicInfo> GetEntities()
        {
            var query = SelectHandler.From<Model.PopulationBasicInfo>();
            return ExecuteList<Model.PopulationBasicInfo>(query.Execute().ExecuteDataReader());
        }

        /// <summary>
        /// 获取指定ID的人员基本信息记录
        /// </summary>
        /// <param name="ids">指定人员ID组，以“，”分隔</param>
        /// <returns></returns>
        public List<Model.PopulationBasicInfo> GetEntities(params string[] ids)
        {
            return GetEntities<Model.PopulationBasicInfo>(t => t.ID.In(ids));
        }

        /// <summary>
        /// 分页获取所有的人员基本信息，并获取当前页码的人员基本信息
        /// </summary>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="records">总人员数</param>
        /// <returns></returns>
        public List<Model.PopulationBasicInfo> Page(int index, int size, out int records)
        {
            return Page(null, index, size, out records);
        }

        /// <summary>
        /// 分页获取指定名称的人员基本信息，并获取当前页码的人员基本信息
        /// </summary>
        /// <param name="name">人员名称</param>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="records">名称匹配人员基本信息的总记录数量</param>
        /// <returns></returns>
        public List<Model.PopulationBasicInfo> Page(string name, int index, int size, out int records)
        {
            System.Linq.Expressions.Expression<Func<Model.PopulationBasicInfo, bool>> expression = null;
            if (!string.IsNullOrWhiteSpace(name))
            {
                expression = t => t.Name == name;
            }

            return Paging<Model.PopulationBasicInfo>(index, size, expression, OrderType.Desc, out records, SORTFIELD);
        }

        /// <summary>
        /// 分页获取指定名称的人员基本信息，并获取当前页码的人员基本信息
        /// </summary>
        /// <param name="name">人员名称或者证件编号</param>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="records">名称匹配人员基本信息的总记录数量</param>
        /// <returns></returns>
        public List<Model.PopulationBasicInfo> PagePopulation(string query, int index, int size, out int records)
        {
            System.Linq.Expressions.Expression<Func<Model.PopulationBasicInfo, bool>> expression = null;
            if (!string.IsNullOrWhiteSpace(query))
                expression = t => t.Name.Like(query) || t.CardNo.Like(query);

            return Paging<Model.PopulationBasicInfo>(index, size, expression, OrderType.Desc, out records, SORTFIELD);
        }

        /// <summary>
        /// 获取指定ID的人员基本信息记录
        /// </summary>
        /// <param name="id">指定人员ID</param>
        /// <returns></returns>
        public Model.PopulationBasicInfo GetEntity(int id)
        {
            var e = GetEntity<Model.PopulationBasicInfo>(t => t.ID == id);
            SetEntityExtention(ref e);
            return e;
        }

        /// <summary>
        /// 获取指定身份证号码的人员基本信息记录
        /// </summary>
        /// <param name="cardno">指定身份证号码</param>
        /// <returns></returns>
        public Model.PopulationBasicInfo GetEntity(string cardno)
        {
            if (!string.IsNullOrWhiteSpace(cardno))
                return new Model.PopulationBasicInfo();

            var e = GetEntity<Model.PopulationBasicInfo>(t => t.CardNo == cardno);
            SetEntityExtention(ref e);
            return e;
        }

        /// <summary>
        /// 设置人员的关联项值，家庭住址和当前住址
        /// </summary>
        /// <param name="e"></param>
        private void SetEntityExtention(ref Model.PopulationBasicInfo e)
        {
            var param = ParamHandler.Handler.GetEntities(e.BloodTypeID.ToString(), //血型
                e.EducationID.ToString(), //学历
                e.HRelationID.ToString(), //与户主关系
                e.MarriageStatusID.ToString(),// 婚姻
                e.OriginCityID.ToString(),//籍贯市
                e.OriginProvinceID.ToString(),// 籍贯省
                e.PoliticalStatusID.ToString(),//政治面貌
                e.PsychosisTypeID.ToString(), //重点人员类别
                e.SexID.ToString(),//性别
                e.SoldierStatusID.ToString());//兵役
            e.SetJoins(param);
            var addresses = AddressHandler.Handler.GetEntities(e.HomeAddrID.ToString(), e.CurrentAddrID.ToString());
            e.SetAddress(addresses);
        }

        /// <summary>
        /// 获取指定大楼的信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="livetypeid">居住类型：1-常住；2-暂住；3-境外；4-重点；0-所有</param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <param name="reocords"></param>
        /// <returns></returns>
        public List<Model.PopulationBasicInfoEx> GetPopulationsOnBuilding(string id, int livetypeid, int index, int size, out int records)
        {
            var query = GetQueryJoinAddress().Where<Model.Address>(t => t.OwnerInfoID.In(id));
            if (livetypeid > 0)
                query = query.Where<Model.PopulationBasicInfo>(t => t.LiveTypeID == livetypeid);

            var list = Paging<Model.PopulationBasicInfoEx>(query, index, size, out records);
            SetEntityExtention(ref list);
            return list;
        }

        /// <summary>
        /// 获取指定单元的人员信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <param name="reocords"></param>
        /// <returns></returns>
        public List<Model.PopulationBasicInfoEx> GetPopulationsOnUnit(string id, int index, int size, out int records)
        {
            var query = GetQueryJoinAddress().Where<Model.Address>(t => t.UnitID.In(id));
            var list = Paging<Model.PopulationBasicInfoEx>(query, index, size, out records);
            SetEntityExtention(ref list);
            return list;
        }

        /// <summary>
        /// 获取指定房间的人员信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Model.PopulationBasicInfoEx> GetPopulationsOnRoom(string id)
        {
            var query = GetQueryJoinAddress().Where<Model.Address>(t => t.RoomID.In(id));
            var list = ExecuteList<Model.PopulationBasicInfoEx>(query.Execute().ExecuteDataReader());
            SetEntityExtention(ref list);
            return list;
        }


        private void SetEntityExtention(ref List<Model.PopulationBasicInfoEx> items)
        {
            List<string> ids = new List<string>();
            List<string> addrids = new List<string>();

            items.ForEach(e =>
                {
                    var arr = new string[] { 
                        e.EducationID.ToString(), //学历
                        e.HRelationID.ToString(), //与户主关系
                        e.MarriageStatusID.ToString(),// 婚姻
                        e.OriginCityID.ToString(),//籍贯市
                        e.OriginProvinceID.ToString(),// 籍贯省
                        e.PoliticalStatusID.ToString(),//政治面貌
                        e.PsychosisTypeID.ToString(), //重点人员类别
                        e.SexID.ToString(),//性别
                        e.SoldierStatusID.ToString()
                    };
                    ids.AddRange(arr);

                    var buff = new string[] { e.HomeAddrID.ToString(), e.CurrentAddrID.ToString() };
                    addrids.AddRange(buff);

                });
            var param = ParamHandler.Handler.GetEntities(ids.ToArray());
            var addresses = AddressHandler.Handler.GetEntities(addrids.ToArray());
            items.ForEach(t =>
                {
                    t.SetJoins(param);
                    t.SetAddress(addresses);
                });
        }


        private void SetEntityExtention(ref List<Model.PopulationBasicInfo> items)
        {
            List<string> ids = new List<string>();
            List<string> addrids = new List<string>();

            items.ForEach(e =>
            {
                var arr = new string[] { 
                        e.EducationID.ToString(), //学历
                        e.HRelationID.ToString(), //与户主关系
                        e.MarriageStatusID.ToString(),// 婚姻
                        e.OriginCityID.ToString(),//籍贯市
                        e.OriginProvinceID.ToString(),// 籍贯省
                        e.PoliticalStatusID.ToString(),//政治面貌
                        e.PsychosisTypeID.ToString(), //重点人员类别
                        e.SexID.ToString(),//性别
                        e.SoldierStatusID.ToString()
                    };
                ids.AddRange(arr);

                var buff = new string[] { e.HomeAddrID.ToString(), e.CurrentAddrID.ToString() };
                addrids.AddRange(buff);

            });
            var param = ParamHandler.Handler.GetEntities(ids.ToArray());
            var addresses = AddressHandler.Handler.GetEntities(addrids.ToArray());
            items.ForEach(t =>
            {
                t.SetJoins(param);
                t.SetAddress(addresses);
            });
        }

        /********************************************************************
         *  人员详情查询
         * ******************************************************************
         */

        /// <summary>
        /// 获取指定户号的户籍信息
        /// </summary>
        /// <param name="houseNo">户籍号</param>
        /// <returns></returns>
        public List<Model.PopulationBasicInfo> GetHouseolder(string houseNo)
        {
            return GetEntities<Model.PopulationBasicInfo>(t => t.HouseholdNo == houseNo);
        }

        public List<Model.PopulationBasicInfoEx> GetHouseOwner(string houseNo)
        {
            if (string.IsNullOrWhiteSpace(houseNo)) return new List<Model.PopulationBasicInfoEx>();

            var query = SelectHandler.From<Model.PopulationBasicInfo>().Where<Model.PopulationBasicInfo>(t => t.HouseholdNo == houseNo);
            var list = ExecuteList<Model.PopulationBasicInfoEx>(query.Execute().ExecuteDataReader());
            SetEntityExtention(ref list);
            return list;
        }

        /// <summary>
        /// 获取指定人员的移动轨迹
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Model.TemporaryPopulation> GetMoveRecords(int id)
        {
            return TemporaryPopulationHandler.Handler.GetEntities(id);
        }

        /// <summary>
        /// 获取指定人员入境信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Model.AbroadPerson> GetAbroadRecords(int id)
        {
            return AbroadPersonHandler.Handler.GetEntities(id);
        }

        public List<Model.AbroadPerson> GetAbroadRecords(string cardNo)
        {
            return AbroadPersonHandler.Handler.GetEntities(cardNo);
        }

        /********************************************************************
         *  按条件分页查询数据
         * ******************************************************************
         */

        //实有人口查询
        //  人员姓名，当前住址查询
        //  高级查询：性别，学历，婚姻，兵役，政治面貌，籍贯，户籍地，家庭住址

        /// <summary>
        /// 分页查询指定人员姓名，当前住址基本信息，并获取当前页码数据
        /// <para>一般性查询</para>
        /// </summary>
        /// <param name="name">人员名称</param>
        /// <param name="address"></param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <param name="records"></param>
        /// <returns></returns>
        public List<Model.PopulationBasicInfo> PageEntities(string name, string address, int index, int size, out int records)
        {
            //var query = GetQuery();
            //if (!string.IsNullOrWhiteSpace(name))
            //{
            //    GetQueryMatchName(name, ref query);
            //}

            //if (!string.IsNullOrWhiteSpace(address))
            //{
            //    query = GetQueryJoinAddress(query);
            //    GetQueryMatchAddress(address, ref query);
            //}

            //var list = Paging<Model.PopulationBasicInfo>(query, index, size, out records);
            //SetEntityExtention(ref list);
            //return list;

            return PageEntities(name, address, null, null, index, size, out records);
        }

        public List<Model.PopulationBasicInfo> PageEntities(string name, string addr, string cardno, string aliasename, int index, int size, out int records)
        {
            var query = GetQuery();
            if (!string.IsNullOrWhiteSpace(name))
            {
                GetQueryMatchName(name, ref query);
            }

            if (!string.IsNullOrWhiteSpace(addr))
            {
                query = GetQueryJoinAddress(query);
                GetQueryMatchAddress(addr, ref query);
            }

            if (!string.IsNullOrWhiteSpace(cardno))
                query = query.Where<Model.PopulationBasicInfo>(t => t.CardNo.Like(cardno));

            if (!string.IsNullOrWhiteSpace(aliasename))
                query = query.Where<Model.PopulationBasicInfo>(t => t.OtherName.Like(aliasename));

            var list = Paging<Model.PopulationBasicInfo>(query, index, size, out records);
            SetEntityExtention(ref list);
            return list;
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
        /// <param name="records">当前查询总记录数</param>
        /// <returns></returns>
        public List<Model.PopulationBasicInfo> PageEntities(string name, string address, string domicile,
            int genderid, int educationid, int marriageid, int excuageid, int politicalstatusid, int index, int size, out int records)
        {
            var query = GetQuery();
            //匹配人员姓名
            if (!string.IsNullOrWhiteSpace(name))
            {
                GetQueryMatchName(name, ref query);
            }
            //匹配户籍地址
            if (!string.IsNullOrWhiteSpace(domicile))
            {
                GetQueryMatchDomicile(domicile, ref query);
            }
            //匹配性别
            if (genderid > 0)
            {
                query = query.Where<Model.PopulationBasicInfo>(t => t.SexID == genderid);
            }
            //匹配学历
            if (educationid > 0)
            {
                query = query.Where<Model.PopulationBasicInfo>(t => t.EducationID == educationid);
            }
            //匹配婚姻状况
            if (marriageid > 0)
            {
                query = query.Where<Model.PopulationBasicInfo>(t => t.MarriageStatusID == marriageid);
            }
            //匹配并以状况
            if (excuageid > 0)
            {
                query = query.Where<Model.PopulationBasicInfo>(t => t.SoldierStatusID == excuageid);
            }
            //匹配政治面貌
            if (politicalstatusid > 0)
            {
                query = query.Where<Model.PopulationBasicInfo>(t => t.PoliticalStatusID == politicalstatusid);
            }
            //匹配当前住址或者家庭住址
            if (!string.IsNullOrWhiteSpace(address))
            {
                query = GetQueryJoinAddress(query);
                GetQueryMatchAddress(address, ref query);
            }

            var list = Paging<Model.PopulationBasicInfo>(query, index, size, out records);
            SetEntityExtention(ref list);
            return list;
        }

        //常住人口查询
        //  人员姓名，当前住址查询
        //  高级查询：性别，学历，婚姻，兵役，政治面貌，籍贯，户籍地，家庭住址

        /// <summary>
        /// 分页查询常驻指定人员姓名，当前住址基本信息，并获取当前页码数据
        /// <para>一般查询</para>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <param name="records"></param>
        /// <returns></returns>
        public List<Model.PopulationBasicInfo> PageCZ(string name, string address, int index, int size, out int records)
        {
            var typeid = (int)LiveProperty.CZ;
            var query = GetQuery().Where<Model.PopulationBasicInfo>(t => t.LiveTypeID == typeid);
            if (!string.IsNullOrWhiteSpace(name))
            {
                GetQueryMatchName(name, ref query);
            }

            if (!string.IsNullOrWhiteSpace(address))
            {
                query = GetQueryJoinAddress(query);
                GetQueryMatchAddress(address, ref query);
            }

            var list = Paging<Model.PopulationBasicInfo>(query, index, size, out records);
            SetEntityExtention(ref list);
            return list;
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
        /// <param name="records">当前查询总记录数</param>
        /// <returns></returns>
        public List<Model.PopulationBasicInfo> PageCZ(string name, string address, string domicile,
            int genderid, int educationid, int marriageid, int excuageid, int politicalstatusid, int index, int size, out int records)
        {
            var typeid = (int)LiveProperty.CZ;
            var query = GetQuery().Where<Model.PopulationBasicInfo>(t => t.LiveTypeID == typeid);
            //匹配人员姓名
            if (!string.IsNullOrWhiteSpace(name))
            {
                GetQueryMatchName(name, ref query);
            }
            //匹配户籍地址
            if (!string.IsNullOrWhiteSpace(domicile))
            {
                GetQueryMatchDomicile(domicile, ref query);
            }
            //匹配性别
            if (genderid > 0)
            {
                query = query.Where<Model.PopulationBasicInfo>(t => t.SexID == genderid);
            }
            //匹配学历
            if (educationid > 0)
            {
                query = query.Where<Model.PopulationBasicInfo>(t => t.EducationID == educationid);
            }
            //匹配婚姻状况
            if (marriageid > 0)
            {
                query = query.Where<Model.PopulationBasicInfo>(t => t.MarriageStatusID == marriageid);
            }
            //匹配并以状况
            if (excuageid > 0)
            {
                query = query.Where<Model.PopulationBasicInfo>(t => t.SoldierStatusID == excuageid);
            }
            //匹配政治面貌
            if (politicalstatusid > 0)
            {
                query = query.Where<Model.PopulationBasicInfo>(t => t.PoliticalStatusID == politicalstatusid);
            }
            //匹配当前住址或者家庭住址
            if (!string.IsNullOrWhiteSpace(address))
            {
                query = GetQueryJoinAddress(query);
                GetQueryMatchAddress(address, ref query);
            }

            var list = Paging<Model.PopulationBasicInfo>(query, index, size, out records);
            SetEntityExtention(ref list);
            return list;
        }

        //暂住人口查询
        //  人员姓名，暂住证编号，房东姓名，房东电话，当前住址

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
        /// <param name="records"></param>
        /// <returns></returns>
        public List<Model.PopulationBasicInfo> PageZZ(string name, string cardno, string houseoldname, string houseoldtel, string address, int index, int size, out int records)
        {
            var typeid = (int)LiveProperty.ZZ;
            var query = GetQuery().Where<Model.PopulationBasicInfo>(t => t.LiveTypeID == typeid);
            //  匹配人员姓名
            if (!string.IsNullOrWhiteSpace(name))
            {
                GetQueryMatchName(name, ref query);
            }
            //  匹配暂住证编号
            if (!string.IsNullOrWhiteSpace(cardno))
            {
                query = GetQueryJoinTemporary(query)
                    .Where<Model.TemporaryPopulation>(t => t.ResidenceNo == cardno);
            }
            //  匹配房东姓名
            if (!string.IsNullOrWhiteSpace(houseoldname))
            {
                query = GetQueryJoinTemporary(query)
                    .Where<Model.TemporaryPopulation>(t => t.LandlordName == houseoldname);
            }
            //  匹配房东电话
            if (!string.IsNullOrWhiteSpace(houseoldtel))
            {
                query = GetQueryJoinTemporary(query)
                    .Where<Model.TemporaryPopulation>(t => t.LandlordPhone == houseoldtel);
            }
            //  匹配地址
            if (!string.IsNullOrWhiteSpace(address))
            {
                query = GetQueryJoinAddress(query);
                GetQueryMatchAddress(address, ref query);
            }
            //执行命令
            var list = Paging<Model.PopulationBasicInfo>(query, index, size, out records);
            SetEntityExtention(ref list);
            return list;
        }

        //境外人口查询
        //  中文姓名，英文姓名，国籍，证件类型，证件编号，签证类型，签证编号，入境口岸，当前住址

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
        public List<Model.PopulationBasicInfo> PageJW(string name, string firstname, string lastname, int countryid, int cardtypeid, string cardtypeno, int visatypeid, string visano, string entryport, string address, int index, int size, out int records)
        {
            var typeid = (int)LiveProperty.JW;
            var query = GetQuery().Where<Model.PopulationBasicInfo>(t => t.LiveTypeID == typeid);
            //  匹配中文姓名
            if (!string.IsNullOrWhiteSpace(name))
            {
                GetQueryMatchName(name, ref query);
            }
            //  匹配英文名
            if (!string.IsNullOrWhiteSpace(firstname))
            {
                query = GetQueryJoinAbroadPerson(query)
                    .Where<Model.AbroadPerson>(t => t.FirstName.Like(firstname));
            }
            //  匹配英文姓
            if (!string.IsNullOrWhiteSpace(lastname))
            {
                query = GetQueryJoinAbroadPerson(query)
                    .Where<Model.AbroadPerson>(t => t.LastName.Like(lastname));
            }
            //  匹配国籍
            if (countryid > 0)
            {
                query = GetQueryJoinAbroadPerson(query)
                    .Where<Model.AbroadPerson>(t => t.CountryID == countryid);
            }
            //  匹配有效证件类别
            if (cardtypeid > 0)
            {
                query = GetQueryJoinAbroadPerson(query)
                    .Where<Model.AbroadPerson>(t => t.CardTypeID == cardtypeid);
            }
            //  匹配有效证件编号
            if (!string.IsNullOrWhiteSpace(cardtypeno))
            {
                query = GetQueryJoinAbroadPerson(query)
                    .Where<Model.AbroadPerson>(t => t.CardNo == cardtypeno);
            }
            //  匹配签证类别
            if (visatypeid > 0)
            {
                query = GetQueryJoinAbroadPerson(query)
                    .Where<Model.AbroadPerson>(t => t.VisaTypeID == visatypeid);
            }
            //  匹配签证编号
            if (!string.IsNullOrWhiteSpace(visano))
            {
                query = GetQueryJoinAbroadPerson(query)
                    .Where<Model.AbroadPerson>(t => t.VisaNoAndValidity.Like(visano));
            }
            //  匹配入境口岸
            if (!string.IsNullOrWhiteSpace(entryport))
            {
                query = GetQueryJoinAbroadPerson(query)
                    .Where<Model.AbroadPerson>(t => t.EntryPort == entryport);
            }
            //  匹配地址
            if (!string.IsNullOrWhiteSpace(address))
            {
                query = GetQueryJoinAddress(query);
                GetQueryMatchAddress(address, ref query);
            }
            //  执行命令
            var list = Paging<Model.PopulationBasicInfo>(query, index, size, out records);
            SetEntityExtention(ref list);
            return list;
        }

        //重点人口查询
        //  人员姓名，重点人口类别，当前住址

        /// <summary>
        /// 分页查询重点指定人员名称，重点类别，居住地址基本信息，并获取当前页码数据记录
        /// </summary>
        /// <param name="name">人员姓名</param>
        /// <param name="importtypeid">重点类别ID</param>
        /// <param name="address">居住地址</param>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="records">当前查询总条目数</param>
        /// <returns></returns>
        public List<Model.PopulationBasicInfo> PageZD(string name, int importtypeid, string address, int index, int size, out int records)
        {
            int typeid = (int)LiveProperty.ZD;
            var query = GetQuery().Where<Model.PopulationBasicInfo>(t => t.LiveTypeID == typeid);
            //  匹配人员姓名
            if (!string.IsNullOrWhiteSpace(name))
            {
                GetQueryMatchName(name, ref query);
            }
            //  匹配重点人口类型
            if (importtypeid > 0)
            {
                query = query.Where<Model.PopulationBasicInfo>(t => t.PsychosisTypeID == importtypeid);
            }
            //  匹配居住地址
            if (string.IsNullOrWhiteSpace(address))
            {
                query = GetQueryJoinAddress(query);
                GetQueryMatchAddress(address, ref query);
            }
            //  执行查询命令
            var list = Paging<Model.PopulationBasicInfo>(query, index, size, out records);
            SetEntityExtention(ref list);
            return list;
        }

        //框选
        //  左上角座标{x,y},右下角座标{x,y}

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
        public List<Model.PopulationBasicInfo> PageKX(int x1, int y1, int x2, int y2, int index, int size, out int records, out int czCount, out int zzCount, out int jwCount, out int zdCount)
        {
            var query = GetQueryJoinBuilding()
                .Where(string.Format("Map_ElementHot.MEH_CenterX >= {0}", x1))
                .Where(string.Format("Map_ElementHot.MEH_CenterX <= {0}", x2))
                .Where(string.Format("Map_ElementHot.MEH_CenterY >= {0}", y1))
                .Where(string.Format("Map_ElementHot.MEH_CenterY <= {0}", y2));
            czCount = GetCZ(query);
            zzCount = GetZZ(query);
            jwCount = GetJW(query);
            zdCount = GetZD(query);
            var list = Paging<Model.PopulationBasicInfo>(query, index, size, out records);
            SetEntityExtention(ref list);
            return list;
        }

        /// <summary>
        /// 获取当前查询常住人口数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private int GetCZ(IDao.ISelect query)
        {
            return GetCountWithLiveType(query, (int)LiveProperty.CZ);
        }

        /// <summary>
        /// 获取当前查询暂住人口数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private int GetZZ(IDao.ISelect query)
        {
            return GetCountWithLiveType(query, (int)LiveProperty.ZZ);
        }

        /// <summary>
        /// 获取当前查询境外人口数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private int GetJW(IDao.ISelect query)
        {
            return GetCountWithLiveType(query, (int)LiveProperty.JW);
        }

        /// <summary>
        /// 获取当前查询重点人口数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private int GetZD(IDao.ISelect query)
        {
            return GetCountWithLiveType(query, (int)LiveProperty.ZD);
        }

        /// <summary>
        /// 获取指定居住性质的数据信息
        /// </summary>
        /// <param name="livetypeid"></param>
        /// <returns></returns>
        private int GetCountWithLiveType(IDao.ISelect query, int livetypeid)
        {
            query = query.Where<Model.PopulationBasicInfo>(t => t.LiveTypeID == livetypeid);
            return GetCount(query);
        }

        /// <summary>
        /// 只查询基础信息
        /// </summary>
        /// <returns></returns>
        private IDao.ISelect GetQuery()
        {
            var tablename = "Pgis_PopulationBasicInfo";
            var sortfield = "ID";
            var query = GetPageQuery(tablename, sortfield);
            return query;
        }

        /// <summary>
        /// 联合地址查询
        /// </summary>
        /// <returns></returns>
        private IDao.ISelect GetQueryJoinAddress(IDao.ISelect query = null)
        {
            query = query ?? GetQuery();
            query = query
                .Join(JoinType.Inner, "Pgis_Address")
                .On("Pgis_PopulationBasicInfo.HomeAddrID = Pgis_Address.ID or Pgis_PopulationBasicInfo.CurrentAddrID = Pgis_Address.ID");
            return query;
        }

        /// <summary>
        /// 联合地址，行政区划查询
        /// </summary>
        /// <returns></returns>
        private IDao.ISelect GetQueryJoinAdministrative(IDao.ISelect query = null)
        {
            query = query ?? GetQueryJoinAddress(query);
            query = GetQuery(query, "Pgis_Administrative", "Pgis_Address.AdminID = Pgis_Administrative.ID");
            return query;
        }

        /// <summary>
        /// 联合地址，大楼查询
        /// </summary>
        /// <returns></returns>
        private IDao.ISelect GetQueryJoinBuilding(IDao.ISelect query = null)
        {
            query = query ?? GetQueryJoinElement(query);
            query = GetQuery(query, "Map_OwnerInfo", "Map_OwnerInfo.MOI_ID = Pgis_Address.OwnerInfoID");
            return query;
        }

        private IDao.ISelect GetQueryJoinElement(IDao.ISelect query = null)
        {
            query = query ?? GetQueryJoinAddress(query);
            query = GetQuery(query, "Map_ElementHot", "Map_ElementHot.MEH_MOI_ID = Pgis_Address.OwnerInfoID");
            return query;
        }

        /// <summary>
        /// 联合暂住信息查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private IDao.ISelect GetQueryJoinTemporary(IDao.ISelect query = null)
        {
            query = query ?? GetQuery();
            query = GetQuery(query, "Pgis_TemporaryPopulation", "Pgis_PopulationBasicInfo.id = Pgis_TemporaryPopulation.PoID");
            return query;
        }

        /// <summary>
        /// 联合境外信息查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private IDao.ISelect GetQueryJoinAbroadPerson(IDao.ISelect query = null)
        {
            query = query ?? GetQuery();
            query = GetQuery(query, "Pgis_AbroadPerson", "Pgis_PopulationBasicInfo.id = Pgis_AbroadPerson.PoID");
            return query;
        }

        private IDao.ISelect GetQuery(IDao.ISelect query, string targetTableName, params string[] conditions)
        {
            return query.Join(JoinType.Inner, targetTableName).On(conditions);
        }

        /********************************************************************
         *  匹配查询
         * ******************************************************************
         */

        /// <summary>
        /// 获取匹配用户姓名的查询表达式
        /// </summary>
        /// <param name="name"></param>
        /// <param name="query"></param>
        private void GetQueryMatchName(string name, ref IDao.ISelect query)
        {
            query = query.Where<Model.PopulationBasicInfo>(t => t.Name.Like(name));
        }

        /// <summary>
        /// 获取匹配地址的查询表达式
        /// </summary>
        /// <param name="address"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        private void GetQueryMatchAddress(string address, ref IDao.ISelect query)
        {
            var arr = string.IsNullOrWhiteSpace(address) ? new string[0] : address.Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < arr.Length; i++)
            {
                var str = arr[i];
                query = query.Where<Model.Address>(t => t.Content.Like(str));
            }
        }

        /// <summary>
        /// 获取匹配户籍地的查询表达式
        /// </summary>
        /// <param name="domicile">户籍地址</param>
        /// <param name="query"></param>
        private void GetQueryMatchDomicile(string domicile, ref IDao.ISelect query)
        {
            var arr = string.IsNullOrWhiteSpace(domicile) ? new string[0] : domicile.Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries); for (var i = 0; i < arr.Length; i++)
            {
                var str = arr[i];
                query = query.Where<Model.PopulationBasicInfo>(t => t.Domicile.Like(str));
            }
        }

        public override int InsertEntity(object obj)
        {
            var e = obj as Model.PopulationBasicInfo;
            if (e == null) return -1;

            return InsertHandler.Into<Model.PopulationBasicInfo>()
                .Table("BloodType", "BloodTypeID", "CardNo", "CurrentAddrID", "Domicile", "Education", "EducationID", "HomeAddrID", "HouseholdNo", "HRelation", "HRelationID", "IsPsychosis", "LivePhone", "LiveTypeID", "LiveType", "MarriageStatus", "MarriageStatusID", "Name", "Nation", "OriginCity", "OriginCityID", "OriginProvince", "OriginProvinceID", "OtherName", "PhotoPath", "PoliticalStatus", "PoliticalStatusID", "PsychosisType", "PsychosisTypeID", "Religion", "Sex", "SexID", "SoldierStatus", "SoldierStatusID", "Stature", "Telephone1")
                .Values(e.BloodType, e.BloodTypeID, e.CardNo, e.CurrentAddrID, e.Domicile, e.Education, e.EducationID, e.HomeAddrID, e.HouseholdNo, e.HRelation, e.HRelationID, e.IsPsychosis, e.LivePhone, e.LiveTypeID, e.LiveType, e.MarriageStatus, e.MarriageStatusID, e.Name, e.Nation, e.OriginCity, e.OriginCityID, e.OriginProvince, e.OriginProvinceID, e.OtherName, e.PhotoPath, e.PoliticalStatus, e.PoliticalStatusID, e.PsychosisType, e.PsychosisTypeID, e.Religion, e.Sex, e.SexID, e.SoldierStatus, e.SoldierStatusID, e.Stature, e.Telephone1)
                .Execute().ExecuteNonQuery();
        }

        public override int UpdateEntity(object obj)
        {
            var e = obj as Model.PopulationBasicInfo;
            if (e == null) return -1;

            return UpdateHandler.Table<Model.PopulationBasicInfo>()
                .Set("Telephone1").EqualTo(e.Telephone1)
                .Set("Stature").EqualTo(e.Stature)
                .Set("SoldierStatusID").EqualTo(e.SoldierStatusID)
                .Set("SoldierStatus").EqualTo(e.SoldierStatus)
                .Set("SexID").EqualTo(e.SexID)
                .Set("Sex").EqualTo(e.Sex)
                .Set("Religion").EqualTo(e.Religion)
                .Set("PsychosisTypeID").EqualTo(e.PsychosisTypeID)
                .Set("PsychosisType").EqualTo(e.PsychosisType)
                .Set("PoliticalStatusID").EqualTo(e.PoliticalStatusID)
                .Set("PoliticalStatus").EqualTo(e.PoliticalStatus)
                .Set("PhotoPath").EqualTo(e.PhotoPath)
                .Set("OtherName").EqualTo(e.OtherName)
                .Set("OriginProvinceID").EqualTo(e.OriginProvinceID)
                .Set("OriginProvince").EqualTo(e.OriginProvince)
                .Set("OriginCityID").EqualTo(e.OriginCityID)
                .Set("OriginCity").EqualTo(e.OriginCity)
                .Set("Nation").EqualTo(e.Nation)
                .Set("Name").EqualTo(e.Name)
                .Set("MarriageStatusID").EqualTo(e.MarriageStatusID)
                .Set("MarriageStatus").EqualTo(e.MarriageStatus)
                .Set("LiveTypeID").EqualTo(e.LiveTypeID)
                .Set("LiveType").EqualTo(e.LiveType)
                .Set("LivePhone").EqualTo(e.LivePhone)
                .Set("IsPsychosis").EqualTo(e.IsPsychosis)
                .Set("HRelationID").EqualTo(e.HRelationID)
                .Set("HRelation").EqualTo(e.HRelation)
                .Set("HouseholdNo").EqualTo(e.HouseholdNo)
                .Set("HomeAddrID").EqualTo(e.HomeAddrID)
                .Set("EducationID").EqualTo(e.EducationID)
                .Set("Education").EqualTo(e.Education)
                .Set("Domicile").EqualTo(e.Domicile)
                .Set("CurrentAddrID").EqualTo(e.CurrentAddrID)
                .Set("CardNo").EqualTo(e.CardNo)
                .Set("BloodTypeID").EqualTo(e.BloodTypeID)
                .Set("BloodType").EqualTo(e.BloodType)
                .Where<Model.PopulationBasicInfo>(t => t.ID == e.ID)
                .Execute().ExecuteNonQuery();
        }

        public override int DeleteEntities(params string[] ids)
        {
            return DeleteHandler.From<Model.PopulationBasicInfo>()
                .Where<Model.PopulationBasicInfo>(t => t.ID.In(ids))
                .Execute().ExecuteNonQuery();
        }

        public List<Model.Pop> TotalPop(string id)
        {
            var query = SelectHandler.Columns("count(0) as Count, LiveTypeID as Type, LiveType as Name").From<Model.PopulationBasicInfo>().GroupBy("LiveTypeID, LiveType");
            query = GetQueryJoinAddress(query).Where<Model.Address>(t => t.OwnerInfoID.In(id));
            return ExecuteList<Model.Pop>(query.Execute().ExecuteDataReader());
        }

        /**************************************************************************************
         *@ 主题地图模式
         **************************************************************************************
        */

        /// <summary>
        /// 根据大楼分组，并根据居住性质获取每一幢大楼的人口数据
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="mod"></param>
        /// <returns></returns>
        public List<Model.Maptheme> GetGroupByOwnerinfo(int x1, int y1, int x2, int y2, int mod)
        {
            var pname = GetTableName<Model.PopulationBasicInfo>();
            var aname = GetTableName<Model.Address>();
            var ename = GetTableName<Model.ElementHot>();
            var query = SelectHandler.Columns(string.Format("{0}.MEH_CenterX, {0}.MEH_CenterY, {0}.MEH_MOI_ID", ename), "Count(0) as Record").From<Model.PopulationBasicInfo>()
                .Join(JoinType.Inner, aname).On(string.Format("{0}.ID = {1}.CurrentAddrID", aname, pname))
                .Join(JoinType.Inner, ename).On(string.Format("{0}.MEH_MOI_ID = {1}.OwnerInfoID", ename, aname))
                .Where<Model.PopulationBasicInfo>(t => t.LiveTypeID == mod)
                .Where(string.Format("(CAST({0}.MEH_CenterX as float) >= CAST({1} as float))", ename, x1))
                .Where(string.Format("(CAST({0}.MEH_CenterX as float) <= CAST({1} as float))", ename, x2))
                .Where(string.Format("(CAST({0}.MEH_CenterY as float) >= CAST({1} as float))", ename, y1))
                .Where(string.Format("(CAST({0}.MEH_CenterY as float) <= CAST({1} as float))", ename, y2))
                .GroupBy(string.Format("{0}.MEH_CenterX, {0}.MEH_CenterY, {0}.MEH_MOI_ID", ename));

            return ExecuteList<Model.Maptheme>(query.Execute().ExecuteDataReader());
        }

        public Model.ElementHot LoactionPopu(int addrid)
        {
            return BuildingHandler.Handler.GetEleByAddress(addrid);
        }

        public List<Model.PopulationBasicInfoEx> Query(double x1, double y1, double x2, double y2, int index, int size, out int records)
        {
            var query = GetQueryJoinElement()
                .Where(string.Format("Map_ElementHot.MEH_CenterX >= {0}", x1))
                .Where(string.Format("Map_ElementHot.MEH_CenterX <= {0}", x2))
                .Where(string.Format("Map_ElementHot.MEH_CenterY >= {0}", y1))
                .Where(string.Format("Map_ElementHot.MEH_CenterY <= {0}", y2));

            var list = Paging<Model.PopulationBasicInfoEx>(query, index, size, out records);
            SetEntityExtention(ref list);
            return list;
        }

        public List<Model.PopulationBasicInfoEx> Query(double[] coords, double x1, double y1, double x2, double y2, int index, int size, out int records)
        {
            var list = GetRangePopu(x1, y1, x2, y2);
            var poly = GetPoints(coords);
            list = list.Where(t =>
                WEBAPI.Common.GDI.GDIHelper.PointInPoly(poly, new Common.GDI.Point() { 
                    X = string.IsNullOrWhiteSpace(t.MEH_CenterX) ? 0 : double.Parse(t.MEH_CenterX), 
                    Y = string.IsNullOrWhiteSpace(t.MEH_CenterY) ? 0 : double.Parse(t.MEH_CenterY) 
                })).Take(index * size).Skip((index - 1) * size).ToList();
            records = list.Count;
            SetEntityExtention(ref list);
            return list;
        }

        private List<Model.PopulationBasicInfoEx> GetRangePopu(double x1, double y1, double x2, double y2)
        {
            var query = GetQueryJoinElement()
                .AddColumn("Map_ElementHot.MEH_CenterX,Map_ElementHot.MEH_CenterY")
                .Where(string.Format("Map_ElementHot.MEH_CenterX >= {0}", x1))
                .Where(string.Format("Map_ElementHot.MEH_CenterX <= {0}", x2))
                .Where(string.Format("Map_ElementHot.MEH_CenterY >= {0}", y1))
                .Where(string.Format("Map_ElementHot.MEH_CenterY <= {0}", y2));

            return ExecuteList<Model.PopulationBasicInfoEx>(query.Execute().ExecuteDataReader());
        }
    }
}
