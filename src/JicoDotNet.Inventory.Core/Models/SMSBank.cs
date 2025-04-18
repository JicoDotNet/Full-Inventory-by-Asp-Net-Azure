using DataAccess.AzureStorage.Table;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Entities.Inner;
using System;

namespace JicoDotNet.Inventory.Core.Models
{
    public class SMSBank : TableEntity, ISMSBank, IActivity, IStatus, IHttpRequest
    {
        public long Balance { get; set; }

        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }


        public string RequestId { get; set; }
    }
}
