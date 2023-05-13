using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class StockTransfer : IStockTransfer, IActivity, IStatus,   IHRequest  
    {
        public long StockTransferId { get; set; }        
        public string StockTransferNumber { get; set; }
        public DateTime StockTransferDate { get; set; }
        public long FromWareHouseId { get; set; }
        public long ToWareHouseId { get; set; }
        public string Remarks { get; set; }

         
        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }
         
         
        public string RequestId { get; set; }

        public List<StockTransferDetail> StockTransferDetails { get; set; }
    }
}
