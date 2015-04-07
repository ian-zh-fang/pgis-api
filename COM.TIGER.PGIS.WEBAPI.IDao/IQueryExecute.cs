using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.IDao
{
    public interface IQueryExecute
    {
        IQueryWhere IQueryWhere { get; }

        string CommandText { get; }
        
        IDbase Execute();
    }
}
