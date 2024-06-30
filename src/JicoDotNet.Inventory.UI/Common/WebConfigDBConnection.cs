// ReSharper disable once CheckNamespace
namespace System.Web.Mvc
{
    using Configuration;
    
    public class WebConfigDbConnection
    {
        public static object SqlServer => WebConfigurationManager.ConnectionStrings["SqlServerConnection"];
        public static object AzureStorage => WebConfigurationManager.ConnectionStrings["AzureStorageConnection"];
    }
}