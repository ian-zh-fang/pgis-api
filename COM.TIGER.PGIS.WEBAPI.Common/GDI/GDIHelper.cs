using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Common.GDI
{
    public class GDIHelper
    {
        /// <summary>
        /// 采用分割法，将多边形分割成多个三角形，并验证平面直角座标是否在这些三角形的内部，返回一个 Boolean 类型的实例
        /// <para>True 标识当前座标在多边形内不</para>
        /// <para>False 标识当前座标不在多边形内部</para>
        /// </summary>
        /// <param name="poly">多边形各顶点座标</param>
        /// <param name="point">当前座标</param>
        /// <returns></returns>
        public static bool PointInPoly(Point[] poly, Point point)
        {
            if (poly == null) return false;
            if (point == null) return false;
            if (poly.Length < 3) return false;

            //验证端点在端点，或者边界线上
            if (PointInLine(poly, point)) return true;
            
            //验证端点在各三角形内部
            var flag = false;
            for (var i = 0; i < poly.Length - 2; i++)
            {
                //以位置 0 处座标为起始端点，和其他任意两个连续的端点构成三角形，并判断指定的端点是否在当前的三角行内部
                if (PointInTriangle(point, poly[0], poly[i + 1], poly[i + 2]))
                {
                    flag = true;
                    break;
                }
            }
                
            return flag;
        }

        /// <summary>
        /// 验证指定的端点指定的3个顶点构成的三角形内部
        /// </summary>
        /// <param name="a">指定的端点</param>
        /// <param name="x">三角形的端点</param>
        /// <param name="y">三角形的端点</param>
        /// <param name="z">三角形的端点</param>
        /// <returns></returns>
        private static bool PointInTriangle(Point a, Point x, Point y, Point z)
        {
            var pa = Math.Sqrt( GetLength(a, x));
            var pb = Math.Sqrt( GetLength(a, y));
            var pc = Math.Sqrt( GetLength(a, z));
            var ab = Math.Sqrt( GetLength(x, y));
            var ac = Math.Sqrt( GetLength(x, z));
            var bc = Math.Sqrt( GetLength(y, z));

            return ab + bc + ac > pa + pb + pc;
        }

        /// <summary>
        /// 验证平面直角座标是否在指定的两点之间的线段内，返回一个 Boolean 类型的实例
        /// <para>True 标识当前座标在多边形内不</para>
        /// <para>False 标识当前座标不在多边形内部</para>
        /// </summary>
        /// <param name="line">指定的线段各个顶点信息（可以同时指定多个线段，但是必须是连续的线段）</param>
        /// <param name="point">当前座标</param>
        /// <returns></returns>
        public static bool PointInLine(Point[] line, Point point)
        {
            if (line == null) return false;
            if (point == null) return false;

            var flag = false;
            for (var i = 0; i < line.Length - 1; i++)
            {
                if (PointInline(point, line[i], line[i + 1]))
                {
                    flag = true;
                    break;
                }
            }
            
            return flag;
        }

        /// <summary>
        /// 验证端点 a 在端点 b 和端点 c 之间，包括端点 b 和端点 c。
        /// </summary>
        /// <param name="a">需要验证的端点</param>
        /// <param name="b">被指定的端点</param>
        /// <param name="c">被指定的端点</param>
        /// <returns></returns>
        private static bool PointInline(Point a, Point b, Point c)
        { 
            //根据信任编程原理，此时，端点 a, b, c 一定存在。并且各不相同
            return GetLength(b, c) == GetLength(a, b) + GetLength(a, c);
        }

        /// <summary>
        /// 计算两点之间的长度
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static double GetLength(Point a, Point b)
        {
            var val = (a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y);
            return val;
        }
    }
}
