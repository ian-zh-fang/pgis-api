using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    public class Update : HandlerBase
    {
        [System.ComponentModel.Composition.Import(typeof(IDao.IUpdate))]
        protected IDao.IUpdate _updateHandler;

        public COM.TIGER.PGIS.WEBAPI.IDao.IUpdate UpdateHandler
        {
            get { return _updateHandler; }
        }
    }
}
