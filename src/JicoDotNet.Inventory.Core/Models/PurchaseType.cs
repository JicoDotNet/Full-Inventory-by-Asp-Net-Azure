using JicoDotNet.Inventory.Core.Entities;
using System;

namespace JicoDotNet.Inventory.Core.Models
{
    public class PurchaseType : IPurchaseType, IDtoHeader
    {
        public long PurchaseTypeId { get; set; }
        public string PurchaseTypeName { get; set; }
        public string Description { get; set; }


        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }
        public string RequestId { get; set; }
    }
}
