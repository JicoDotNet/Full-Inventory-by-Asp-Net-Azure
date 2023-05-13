using Microsoft.WindowsAzure.Storage.Table;
using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class Support : TableEntity, ISupport, IActivity, IStatus,   IHRequest
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
