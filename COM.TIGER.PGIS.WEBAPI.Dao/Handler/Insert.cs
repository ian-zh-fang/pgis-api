using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    public class Insert : HandlerBase 
    {
        [System.ComponentModel.Composition.Import(typeof(IDao.IInsert))]
        protected IDao.IInsert _insertHandler;

        public COM.TIGER.PGIS.WEBAPI.IDao.IInsert InsertHandler
        {
            get { return _insertHandler; }
        }
    }
}
