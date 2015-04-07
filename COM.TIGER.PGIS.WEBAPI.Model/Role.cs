using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Model
{
    /// <summary>
    /// 系统角色
    /// </summary>
    [System.Runtime.Serialization.DataContract(Name = "Role", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class Role : MBase, IComparable<Role>
    {
        /// <summary>
        /// 主键标识
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "ID")]
        public int ID { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Remarks")]
        public string Remarks { get; set; }

        /// <summary>
        /// 默认排序比较
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Role other)
        {
            if (this.ID > other.ID) return 1;
            if (this.ID < other.ID) return -1;
            return 0;
        }
    }
}
