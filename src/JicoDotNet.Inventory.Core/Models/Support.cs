using System;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Entities.Inner;
using Microsoft.WindowsAzure.Storage.Table;

namespace JicoDotNet.Inventory.Core.Models
{
    public class Support : TableEntity, ISupport, IActivity, IStatus, IHRequest
    {
        public string ScreenshotImageUrl { get; set; }
        public string QueryStatement { get; set; }
        public string TicketNumber { get; set; }
        public long UserId { get; set; }
        public string QueriesUrl { get; set; }
        public string IP { get; set; }

        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }
         
         
        public string RequestId { get; set; }
    }
}
