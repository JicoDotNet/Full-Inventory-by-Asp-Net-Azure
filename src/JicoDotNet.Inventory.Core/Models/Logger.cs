using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Entities.Inner;
using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace JicoDotNet.Inventory.Core.Models
{
    public class Logger : TableEntity, ILogger, IHRequest, IActivity
    {
        public string IPAddress { get; set; }
        public string DNS { get; set; }
        public string HttpVerbs { get; set; }
        public string Browser { get; set; }
        public string BrowserType { get; set; }
        public string BrowserVersion { get; set; }
        public string AbsoluteUri { get; set; }
        public string MacAddress { get; set; }
        public bool IsMobileDevice { get; set; }
        public string OSType { get; set; }

        public string Controller { get; set; }
        public string Action { get; set; }
        public string Id { get; set; }
        public string Id2 { get; set; }

        public string RequestId { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
