using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    public class Select : HandlerBase
    {
        [System.ComponentModel.Composition.Import(typeof(IDao.ISelect))]
        private IDao.ISelect _selectHandler;

        public COM.TIGER.PGIS.WEBAPI.IDao.ISelect SelectHandler
        {
            get { return _selectHandler; }
        }
    }
}
