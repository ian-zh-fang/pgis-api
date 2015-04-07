using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Model
{
    /// <summary>
    /// 单位标注信息
    /// </summary>
    [System.Runtime.Serialization.DataContract(Name = "companymark", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class CompanyMark :MBase, IComparable<CompanyMark>
    {
        private double _x = 0.0f;
        private double _y = 0.0f;

        [System.Runtime.Serialization.DataMember(Name = "ID")]
        public int ID { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "Name")]
        public string Name { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "Telephone")]
        public string Telephone { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "Description")]
        public string Description { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "Type")]
        public int Type { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "X")]
        public double X { get { return _x; } set { _x = value; } }

        [System.Runtime.Serialization.DataMember(Name = "Y")]
        public double Y { get { return _y; } set { _y = value; } }

        public int CompareTo(CompanyMark other)
        {
            if (ID > other.ID) return 1;
            if (ID < other.ID) return -1;
            return 0;
        }
    }
}
