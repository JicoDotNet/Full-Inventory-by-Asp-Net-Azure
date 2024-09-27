using System;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Entities.Inner;

namespace JicoDotNet.Inventory.Core.Models
{
    public class StockDetail : IStockDetail, IActivity, IStatus, IHRequest
    {
        public long StockDetailId { get; set; }
        public long ProductId { get; set; }
        public long WareHouseId { get; set; }

        public decimal Stock { get; set; }

        public bool IsPerishable { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string BatchNo { get; set; }

        public string Remarks { get; set; }
        public DateTime GRNDate { get; set; }

         
        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }
         
         
        public string RequestId { get; set; }
    }
}
