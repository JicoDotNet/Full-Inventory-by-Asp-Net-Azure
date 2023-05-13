using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class StockAdjust : IStockAdjust, IActivity, IStatus,   IHRequest  
    {
        public long StockAdjustId { get; set; }
         

        public long WareHouseId { get; set; }

        public string StockAdjustNumber { get; set; }
        public DateTime StockAdjustDate { get; set; }

        public long AdjustReasonId { get; set; }
        public string AdjustReason { get; set; }

        public bool IsStockIncrease { get; set; }
        public string Remarks { get; set; }

        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }
         
         
        public string RequestId { get; set; }

        public List<StockAdjustDetail> StockAdjustDetails { get; set; }
    }
}
