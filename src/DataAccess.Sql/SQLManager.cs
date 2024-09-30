using System;

namespace DataAccess.Sql
{
    public abstract class SQLManager
    {
        private protected string SqlConnectionString { get; private set; }

        private protected SQLManager(string connectionString)
        {
            try
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new ArgumentNullException(nameof(connectionString), "Connection String value can't be empty");
                }
                SqlConnectionString = connectionString;
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
