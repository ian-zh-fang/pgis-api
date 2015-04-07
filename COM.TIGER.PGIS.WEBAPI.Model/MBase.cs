using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Model
{
    /// <summary>
    /// 统一设定实体前缀
    /// </summary>
    [COM.TIGER.PGIS.WEBAPI.Common.Attributes.Prefix(Name = "PGis_")]
    [System.Runtime.Serialization.DataContract(Name = "MBase", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public abstract class MBase { }

    /// <summary>
    /// 地图数据表前缀
    /// </summary>
    [COM.TIGER.PGIS.WEBAPI.Common.Attributes.Prefix(Name = "Map_")]
    [System.Runtime.Serialization.DataContract(Name = "MBaseEx", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public abstract class MBase_Map { }
}
