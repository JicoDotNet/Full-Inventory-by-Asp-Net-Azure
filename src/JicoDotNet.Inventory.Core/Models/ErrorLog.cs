using JicoDotNet.Inventory.Core.Entities;
using System;

namespace JicoDotNet.Inventory.Core.Models
{
    public class ErrorLog : IErrorLog
    {
        public string RequestId { get; set; }
        public Exception Exception { get; set; }
        public string FolderPath { get; set; }
        public string HttpMethod { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string UrlParameterId { get; set; }
        public string UrlParameterId2 { get; set; }
    }
}
