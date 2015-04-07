using System;   
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using DEntLib = Microsoft.Practices.EnterpriseLibrary.Data;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    /// <summary>
    /// 数据库操作基类
    /// <para>提供基础数据哭操作程序</para>
    /// </summary>
    public class HandlerBase
    {
        protected CompositionContainer _container;

        public HandlerBase()
        {
            InitContainer();
        }

        /// <summary>
        /// 初始化容器
        /// </summary>
        /// <param name="path"></param>
        protected virtual void InitContainer()
        {
            var context = System.Web.HttpContext.Current;
            var path = string.Empty;
            if (context != null)
            {
                path = context.Server.MapPath("~/Plugins");
            }
            else
            {
                path = string.Format("{0}\\Plugins", Environment.CurrentDirectory);
            }
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog(path));
            _container = new CompositionContainer(catalog);
            try
            {
                this._container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }        
    }
}
