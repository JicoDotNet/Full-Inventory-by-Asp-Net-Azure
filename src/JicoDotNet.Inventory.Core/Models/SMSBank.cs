using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Entities.Inner;
using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace JicoDotNet.Inventory.Core.Models
{
    public class SMSBank : TableEntity, ISMSBank, IActivity, IStatus, IHRequest
    {
        public long Balance { get; set; }

        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }


        public string RequestId { get; set; }
    }
}
