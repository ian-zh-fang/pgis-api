using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DEntLib = Microsoft.Practices.EnterpriseLibrary.Data;

namespace COM.TIGER.PGIS.WEBAPI.IDao
{
    public class Dbase : IDbase
    {
        /// <summary>
        /// T-SQL查询语句
        /// </summary>
        private string _commandText;

        /// <summary>
        /// T-SQL查询语句
        /// </summary>
        public string CommandText
        {
            get { return _commandText; }
        }

        /// <summary>
        /// 获取当前数据库
        /// </summary>
        /// <returns></returns>
        protected DEntLib.Database DB
        {
            get
            {
                try
                {
                    var db = DEntLib.DatabaseFactory.CreateDatabase();
                    return db;
                }
                catch
                {
                    throw;
                }
            }
        }

        public Dbase(string commandText = null)
        {
            _commandText = commandText;
        }

        public int ExecuteNonQuery(string commandText)
        {
            return DB.ExecuteNonQuery(System.Data.CommandType.Text, commandText);
        }

        public int ExecuteNonQuery(System.Data.CommandType commandType, string commandText, params System.Data.Common.DbParameter[] paramCollection)
        {
            var cmd = ExecuteCommand(commandType, commandText, paramCollection);
            return DB.ExecuteNonQuery(cmd);
        }

        public object ExecuteSaclar(string commandText)
        {
            return ExecuteSaclar(System.Data.CommandType.Text, commandText);
        }

        public object ExecuteSaclar(System.Data.CommandType commandType, string commandText, params System.Data.Common.DbParameter[] paramCollection)
        {
            var cmd = ExecuteCommand(commandType, commandText, paramCollection);
            return DB.ExecuteScalar(cmd);
        }

        public System.Data.DataSet ExecuteDataSet(string commandText)
        {
            return ExecuteDataSet(System.Data.CommandType.Text, commandText);
        }

        public System.Data.DataSet ExecuteDataSet(System.Data.CommandType commandType, string commandText, params System.Data.Common.DbParameter[] paramCollection)
        {
            var cmd = ExecuteCommand(commandType, commandText, paramCollection);
            return DB.ExecuteDataSet(cmd);
        }

        public System.Data.IDataReader ExecuteDataReader(string commandText)
        {
            return ExecuteDataReader(System.Data.CommandType.Text, commandText);
        }

        public System.Data.IDataReader ExecuteDataReader(System.Data.CommandType commandType, string commandText, params System.Data.Common.DbParameter[] paraCollection)
        {
            var cmd = ExecuteCommand(commandType, commandText, paraCollection);
            return DB.ExecuteReader(cmd);
        }
        
        public int ExecuteNonQuery()
        {
            return ExecuteNonQuery(_commandText);
        }

        public object ExecuteSaclar()
        {
            return ExecuteSaclar(_commandText);
        }

        public System.Data.DataSet ExecuteDataSet()
        {
            return ExecuteDataSet(_commandText);
        }

        public System.Data.IDataReader ExecuteDataReader()
        {
            return ExecuteDataReader(_commandText);
        }

        protected System.Data.Common.DbCommand ExecuteCommand(System.Data.CommandType commandType, string commandText, params System.Data.Common.DbParameter[] paramCollection)
        {
            if (string.IsNullOrWhiteSpace(commandText)) throw new ArgumentNullException();
            System.Data.Common.DbCommand cmd = null;
            switch (commandType)
            {
                case System.Data.CommandType.Text:
                    cmd = DB.GetSqlStringCommand(commandText);
                    break;
                case System.Data.CommandType.StoredProcedure:
                    cmd = DB.GetStoredProcCommand(commandText);
                    break;
                default:
                    break;
            }
            if (cmd == null) throw new ArgumentNullException();
            if (paramCollection.Length > 0)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(paramCollection);
            }
            return cmd;
        }        
    }
}
