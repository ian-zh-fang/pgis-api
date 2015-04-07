using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Model
{
    /// <summary>
    /// 文件信息
    /// </summary>
    [System.Runtime.Serialization.DataContract(Name = "file", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class File:MBase, IComparable<File>
    {
        /// <summary>
        /// 标识符
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "ID")]
        public int ID { get; set; }
        
        /// <summary>
        /// 文件名称（算法加密后的文件名称）
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Name")]
        public string Name { get; set; }
        
        /// <summary>
        /// 文件别名（文件原名）
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Alias")]
        public string Alias { get; set; }
        
        /// <summary>
        /// 文件后缀
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Suffix")]
        public string Suffix { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Path")]
        public string Path { get; set; }

        public int CompareTo(File other)
        {
            if (ID > other.ID) return 1;
            if (ID == other.ID) return 0;
            return -1;
        }

        public override bool Equals(object obj)
        {
            var other = obj as File;
            return other != null
                && ID == other.ID
                && Name == other.Name
                && Alias == other.Alias;
        }
    }
}
