namespace System.Web.Mvc
{
    using System.Text;
    using System.Web.Configuration;
    
    public class WebConfigDBConnection
    {
        public static object SqlServer { get { return (object)WebConfigurationManager.ConnectionStrings["SqlServerConnection"]?.ToString(); } }
        public static object AzureStorage { get { return (object)WebConfigurationManager.ConnectionStrings["AzureStorageConnection"]?.ToString(); } }
    }
}