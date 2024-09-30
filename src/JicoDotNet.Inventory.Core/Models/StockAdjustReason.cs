using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Entities.Inner;
using System;

namespace JicoDotNet.Inventory.Core.Models
{
    public class StockAdjustReason : IStockAdjustReason, IActivity, IStatus
    {
        public long AdjustReasonId { get; set; }
        public string AdjustReason { get; set; }
        public bool IsDefault { get; set; }

        public bool IsStockIncreasing { get; set; }

        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }



        public string RequestId { get; set; }
    }
}
