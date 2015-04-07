using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.IDao
{
    public interface IDbase
    {
        /// <summary>
        /// T-SQL查询语句
        /// </summary>
        string CommandText { get; }

        int ExecuteNonQuery();
    
        int ExecuteNonQuery(string commandText);

        int ExecuteNonQuery(System.Data.CommandType commandType, string commandText, params System.Data.Common.DbParameter[] paramCollection);

        object ExecuteSaclar();

        object ExecuteSaclar(string commandText);

        object ExecuteSaclar(System.Data.CommandType commandType, string commandText, params System.Data.Common.DbParameter[] paramCollection);

        System.Data.DataSet ExecuteDataSet();

        System.Data.DataSet ExecuteDataSet(string commandText);

        System.Data.DataSet ExecuteDataSet(System.Data.CommandType commandType, string commandText, params System.Data.Common.DbParameter[] paramCollection);

        System.Data.IDataReader ExecuteDataReader();

        System.Data.IDataReader ExecuteDataReader(string commandText);

        System.Data.IDataReader ExecuteDataReader(System.Data.CommandType commandType, string commandText, params System.Data.Common.DbParameter[] paraCollection);
    }
}
