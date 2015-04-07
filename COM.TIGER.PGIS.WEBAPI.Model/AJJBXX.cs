using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Model
{
    [System.Runtime.Serialization.DataContract(Name = "AJJBXX", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class AJJBXX : MBase
    {
        /// <summary>
        /// 
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "ID")]
        public int ID { get; set; }
        
        /// <summary>
        /// 公民身份证编号
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "CardNo")]
        public string CardNo { get; set; }
        
        /// <summary>
        /// 案件编号
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Ajbh")]
        public string Ajbh { get; set; }
        
        /// <summary>
        /// 姓名
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Xm")]
        public string Xm { get; set; }

        /// <summary>
        /// 吸毒标识，1标识吸毒
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "IsDrup")]
        public int IsDrup { get; set; }
        
        /// <summary>
        /// 网上追逃标识，1标识网上追逃
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "IsPursuit")]
        public int IsPursuit { get; set; }
        
        /// <summary>
        /// 绰号
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Alias")]
        public string Alias { get; set; }
        
        /// <summary>
        /// 刑拘标识，1标识刑拘
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "IsArrest")]
        public int IsArrest { get; set; }

        /// <summary>
        /// 当前住址
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "CurrentAddr")]
        public string CurrentAddr { get; set; }

        /// <summary>
        /// 案件说明
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Proof")]
        public string Proof { get; set; }
    }
}
