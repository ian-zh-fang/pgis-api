using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    public class Delete : HandlerBase
    {
        [System.ComponentModel.Composition.Import(typeof(IDao.IDelete))]
        protected IDao.IDelete _deleteHandler;

        public COM.TIGER.PGIS.WEBAPI.IDao.IDelete DeleteHandler
        {
            get { return _deleteHandler; }
        }
    }
}
