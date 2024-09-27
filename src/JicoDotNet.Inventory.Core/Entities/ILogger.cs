namespace JicoDotNet.Inventory.Core.Entities
{
    public interface ILogger
    {
        string IPAddress { get; set; }
        string DNS { get; set; }
        string HttpVerbs { get; set; }
        string Browser { get; set; }
        string BrowserType { get; set; }
        string BrowserVersion { get; set; }
        string AbsoluteUri { get; set; }
        string MacAddress { get; set; }
        bool IsMobileDevice { get; set; }
        string OSType { get; set; }

        string Controller { get; set; }
        string Action { get; set; }
        string Id { get; set; }
        string Id2 { get; set; }        
    }
}
