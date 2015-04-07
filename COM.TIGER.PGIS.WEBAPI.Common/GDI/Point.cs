using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Common.GDI
{
    /// <summary>
    /// 二维平面直角座标系座标
    /// </summary>
    public class Point
    {
        /// <summary>
        /// 横坐标
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// 纵坐标
        /// </summary>
        public double Y { get; set; }
        
        public override bool Equals(object obj)
        {
            var p = obj as Point;
            if (p == null)
                return false;

            return X == p.X && Y == p.Y;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() & X.GetHashCode() & Y.GetHashCode(); 
        }
    }
}
