namespace System.Web.Mvc
{
    using Configuration;
    using System.Configuration;

    public class WebConfigDbConnection
    {
        public static string SqlServer
        {
            get
            {
                ConnectionStringSettings sqlConnectionString = WebConfigurationManager.ConnectionStrings["SqlServerConnection"];

                if (sqlConnectionString != null
                    && !string.IsNullOrEmpty(sqlConnectionString.ToString())
                    && sqlConnectionString.ToString().Contains("Data Source"))
                    {
                    return sqlConnectionString.ToString();
                }
                else
                {
                    return Environment.GetEnvironmentVariable("SqlServerConnection");
                }
            }
        }
        public static object AzureStorage
        {
            get
            {
                ConnectionStringSettings azureConnectionString = WebConfigurationManager.ConnectionStrings["AzureStorageConnection"];

                if (azureConnectionString != null
                    && !string.IsNullOrEmpty(azureConnectionString.ToString())
                    && azureConnectionString.ToString().Contains("AccountName"))
                {
                    return azureConnectionString;
                }
                else
                {
                    return Environment.GetEnvironmentVariable("AzureStorageConnection");
                }
            }
        }
    }
}