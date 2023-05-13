using System.Web;

namespace JicoDotNet.Inventory.ProjectConfig
{
    /// <summary>
    /// Project Configuration Settings
    /// </summary>
    public class AppSettings
    {
        public static object AzureStorageConnection
        {
            get
            {
                return (object)"DefaultEndpointsProtocol=https;AccountName=itemblob;AccountKey=WpfSWwStXl+f+cvwQ9lLyKMZhQQiNTsaDE8UTlD4eYo9N6BemiKyIcs0qwQCV/iZhDGeAbPAzrcLusV1pdVDaQ==;EndpointSuffix=core.windows.net";
            }
        }
        public static object SqlServerConnection
        {
            get
            {
                //- Azure Prod Server
                return (object)"Data Source=itemblobserver.database.windows.net;Initial Catalog=ItemBlobSingle;Integrated Security=False;User ID=IBPAdmin;Password=Y8@7qqj3dA0#k3y2;MultipleActiveResultSets=True;Connect Timeout=30";
            }
        }
        public static string UserNoImage { get { return "/Content/images/NoUser.png"; } }
    }
}