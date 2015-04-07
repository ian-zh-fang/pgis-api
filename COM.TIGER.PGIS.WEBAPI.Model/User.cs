using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Model
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [System.Runtime.Serialization.DataContract(Name = "User", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class User : MBase, IComparable<User>
    {
        /// <summary>
        /// 主键标识
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "ID")]
        public int ID { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Code")]
        public string Code { get; set; }

        /// <summary>
        /// 账户名称
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "UserName")]
        public string UserName { get; set; }

        /// <summary>
        /// 账户密码
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// 部门机构标识
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "DepartmentID")]
        public int DepartmentID { get; set; }

        /// <summary>
        /// 性别
        /// <para>1标识男性;0标识女性</para>
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Gender")]
        public int Gender { get; set; }

        /// <summary>
        /// 用户等级
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Lvl")]
        public int Lvl { get; set; }

        /// <summary>
        /// 启用/禁用标识.
        /// <para>1标识启用;0标识禁用</para>
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Disabled")]
        public int Disabled { get; set; }

        /// <summary>
        /// 警员ID
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "OfficerID")]
        public int OfficerID { get; set; }

        /// <summary>
        /// 警员信息
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Officer")]
        public Officer Officer { get; set; }

        private Department _department = null;
        [System.Runtime.Serialization.DataMember(Name = "Department")]
        public Department Department {
            get { return _department; }
            set { _department = value; }
        }

        /// <summary>
        /// 默认排序方式
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(User other)
        {
            if (this.ID > other.ID) return 1;
            if (this.ID < other.ID) return -1;
            return 0;
        }
    }
}
