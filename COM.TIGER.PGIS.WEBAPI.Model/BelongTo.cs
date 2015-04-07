using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Model
{
    /// <summary>
    /// 数据归属地类型
    /// </summary>
    [System.Runtime.Serialization.DataContract(Name = "belongto", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class BelongTo : MBase, IComparable<BelongTo>
    {
        /// <summary>
        /// 主键标识
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "ID")]
        public int ID { get; set; }

        /// <summary>
        /// 数据归属地类型代码
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Code")]
        public string Code { get; set; }

        /// <summary>
        /// 数据归属地类型名称
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// 数据归属地类型说明
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// 排序比较
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(BelongTo other)
        {
            if (other.ID > ID) return -1;
            if (other.ID < ID) return 1;
            return 0;
        }
    }
}
