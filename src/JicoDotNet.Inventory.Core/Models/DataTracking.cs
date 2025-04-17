using DataAccess.AzureStorage.Table;
using JicoDotNet.Inventory.Core.Entities;
using System;

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
