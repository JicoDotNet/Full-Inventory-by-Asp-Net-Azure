using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Entities.Inner;
using JicoDotNet.Inventory.Core.Enumeration;
using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace JicoDotNet.Inventory.Core.Models
{
    public class Draft : TableEntity, IDraft, IActivity, IStatus, IHttpRequest
    {
        public long UserId { get; set; }
        public string DraftData { get; set; }
        public string DraftType { get; set; }

        public EDraft _DraftType { get; set; }

        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }


        public string RequestId { get; set; }
    }
}
