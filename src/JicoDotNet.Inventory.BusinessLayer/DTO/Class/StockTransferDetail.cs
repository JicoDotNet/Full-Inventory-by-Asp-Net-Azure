using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class StockTransferDetail : IStockTransferDetail, IProductAttribute, IActivity, IStatus, IHRequest
    {
        public long StockTransferDetailId { get; set; }        
        public long StockTransferId { get; set; }
        public string StockTransferNumber { get; set; }
        public long ProductId { get; set; }
        public decimal AvailableQuantity { get; set; }
        public decimal TransferQuantity { get; set; }
        public string Description { get; set; }

        public long StockDetailId { get; set; }

        public bool IsPerishable { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string BatchNo { get; set; }

         
        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }
         
         
        public string RequestId { get; set; }
    }
}
