using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Sql
{
    public abstract class SQLManager
    {
        internal protected string ConnectionString { get; private set; }

        public SQLManager(object _ConnectionString)
        {
            try
            {
                if (string.IsNullOrEmpty(_ConnectionString.ToString()))
                {
                    throw new ArgumentNullException("_ConnectionString", "Connection String value can't be empty");
                }
                ConnectionString = _ConnectionString.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ~SQLManager()
        {
            GC.Collect();
        }
    }
}
