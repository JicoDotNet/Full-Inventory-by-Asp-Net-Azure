namespace System.Web.Mvc
{
    using Configuration;

    public class WebConfigDbConnection
    {
        public static object SqlServer
        {
            get
            {
                if (WebConfigurationManager.ConnectionStrings["SqlServerConnection"] != null
                    && !string.IsNullOrEmpty(WebConfigurationManager.ConnectionStrings["SqlServerConnection"].ToString())
                    && WebConfigurationManager.ConnectionStrings["SqlServerConnection"].ToString().Contains("Data Source"))
                {
                    return WebConfigurationManager.ConnectionStrings["SqlServerConnection"];
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
                if (WebConfigurationManager.ConnectionStrings["AzureStorageConnection"] != null
                    && !string.IsNullOrEmpty(WebConfigurationManager.ConnectionStrings["AzureStorageConnection"].ToString())
                    && WebConfigurationManager.ConnectionStrings["AzureStorageConnection"].ToString().Contains("AccountName"))
                {
                    return WebConfigurationManager.ConnectionStrings["AzureStorageConnection"];
                }
                else
                {
                    return Environment.GetEnvironmentVariable("AzureStorageConnection");
                }
            }
        }
    }
}