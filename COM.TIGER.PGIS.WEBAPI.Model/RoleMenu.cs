using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Model
{
    /// <summary>
    /// 角色菜单
    /// </summary>
    [System.Runtime.Serialization.DataContract(Name = "rolemenu", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class RoleMenu : MBase
    {
        /// <summary>
        /// 主键标识
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "ID")]
        public int ID { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "RoleID")]
        public int RoleID { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "MenuID")]
        public int MenuID { get; set; }
    }
}
