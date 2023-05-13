using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class StockDetail : IStockDetail, IActivity, IStatus,   IHRequest
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
