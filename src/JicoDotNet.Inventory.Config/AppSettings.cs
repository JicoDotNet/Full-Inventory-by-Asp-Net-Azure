using System.Web;

namespace JicoDotNet.Inventory.Config
{
    /// <summary>
    /// Project Configuration Settings
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        ///  * 0 = Prod Server 
        ///  * 1 = Dev Server 
        ///  * 2 = Test Server
        /// </summary>
        public static EEnvironmentType EnvironmentType
        {
            get
            {
                return EEnvironmentType.Prod;
            }
        }

        public static object AzureStorageConnection
        {
            get
            {
                if (EnvironmentType == EEnvironmentType.Prod)
                    return (object)"DefaultEndpointsProtocol=https;AccountName=itemblob;AccountKey=WpfSWwStXl+f+cvwQ9lLyKMZhQQiNTsaDE8UTlD4eYo9N6BemiKyIcs0qwQCV/iZhDGeAbPAzrcLusV1pdVDaQ==;EndpointSuffix=core.windows.net";
                else
                    return (object)"DefaultEndpointsProtocol=https;AccountName=invent;AccountKey=rNtj4DGOrI5W7aGxTKcWDxxNWFFgpYH1OPfcyHs//NVaR6zk++FzalZCH1WJhA4i0Laoz2lgDFWqNyIajsIDJA==;EndpointSuffix=core.windows.net";
            }
        }
        public static object SqlServerConnection
        {
            get
            {
                if (EnvironmentType == EEnvironmentType.Prod)
                {
                    //- Azure Prod Server
                    return (object)"Data Source=itemblobserver.database.windows.net;Initial Catalog=ItemBlob;Integrated Security=False;User ID=IBPAdmin;Password=Y8@7qqj3dA0#k3y2;MultipleActiveResultSets=True;Connect Timeout=30";
                }
                else
                {
                    //- GoDaddy Dev Server
                    //Server=43.255.152.25;Initial Catalog=inventDev;Persist Security Info=False;User ID=inventadmin;Password=ItemBlobDev#9051;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;
                    return (object)"POG/788kT0WgplaEK75nWdrujWhjftX6J+Y8b34AcersaFWaz39uzQCPXlWrZudpDgagyPxIe4VM0ntrz+IIvN9Xvy2buvvNdDn87TwzCDGmwLICFUtNvv+QjPcFx6olb5FE+ctidQ60PCcNQccGSZ2mgpizkoX7WFf0SgaIOpOsDwjQx79EwNl9x7D/MlfXcQI89GAXr123TpjmQq2Dnf6i4+g4atpaZF5Fw2TizQG2AwJ1H6xHBkSM0J7ooJwTNjq85aFkVRkHNoKmRtR9pRC0bmKkY16R";
                }
            }
        }

        /// <summary>
        /// https://domain.com
        /// "/" will not added at last position
        /// </summary>
        public static string CashfreeEndPoint
        {
            get
            {
                if (EnvironmentType == EEnvironmentType.Prod)
                    return "https://api.cashfree.com";
                else                    
                    return "https://test.cashfree.com";
            }
        }

        public static string CashfreeAppId
        {
            get
            {
                if (EnvironmentType == EEnvironmentType.Prod)
                    return "113096e13e245acb1aeb6e464a690311";
                else
                    return "670297af11728c000b9f0e30392076";
            }
        }

        public static string CashfreeSecretKey
        {
            get
            {
                if (EnvironmentType == EEnvironmentType.Prod)
                    return "400f5ca8ded1e721cf5d8443d5ecf8e331db7f11";
                else
                    return "c54a9f87cd1cfd21ba17aa903fc806ab272a1763";
            }
        }

        public static string UserNoImage { get { return "/Content/images/NoUser.png"; } }
    }

    public enum EEnvironmentType
    {
        Prod = 0,
        Dev = 1,
        Test = 2
    }
}