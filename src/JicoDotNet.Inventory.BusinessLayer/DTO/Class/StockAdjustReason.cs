using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
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
