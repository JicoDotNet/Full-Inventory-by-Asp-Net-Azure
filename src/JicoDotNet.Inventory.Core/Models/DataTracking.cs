using System;
using JicoDotNet.Inventory.Core.Entities;
using Microsoft.WindowsAzure.Storage.Table;

namespace JicoDotNet.Inventory.Core.Models
{
    public class DataTracking : TableEntity, IDataTracking
    {
        public string Data { get; set; }
        public string RequestId { get; set; }

        public long UserId { get; set; }
        public string UserName { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}
