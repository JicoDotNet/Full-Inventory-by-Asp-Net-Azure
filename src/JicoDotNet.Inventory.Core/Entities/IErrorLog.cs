using System;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IErrorLog
    {
        string RequestId { get; set; }
        Exception Exception { get; set; }
        string FolderPath { get; set; }
        string HttpMethod { get; set; }
        string ControllerName { get; set; }
        string ActionName { get; set; }
        string UrlParameterId { get; set; }
        string UrlParameterId2 { get; set; }
    }
}
