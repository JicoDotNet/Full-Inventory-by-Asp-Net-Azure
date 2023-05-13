using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class StockAdjustDetail : IStockAdjustDetail, IActivity, IStatus,   IHRequest  
    {
        public long StockAdjustDetailId { get; set; }
         

        public long StockAdjustId { get; set; }
        public string StockAdjustNumber { get; set; }
        public bool IsStockIncrease { get; set; }
         
        public long ProductId { get; set; }
        public decimal AvailableQuantity { get; set; }
        public decimal AdjustQuantity { get; set; }
         
        public bool IsPerishable { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string BatchNo { get; set; }
        public DateTime? GRNDate { get; set; }
         
        public long StockDetailId { get; set; }
         
        public string Description { get; set; }

        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }
         
         
        public string RequestId { get; set; }
    }
}
