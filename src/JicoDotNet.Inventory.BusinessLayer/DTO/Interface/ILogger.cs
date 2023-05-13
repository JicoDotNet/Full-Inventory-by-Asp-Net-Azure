using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
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
        bool IsMobile { get; set; }

        string Subdomain { get; set; }
        string Controller { get; set; }
        string Action { get; set; }
        string Id { get; set; }
        string Id2 { get; set; }        
    }
}
