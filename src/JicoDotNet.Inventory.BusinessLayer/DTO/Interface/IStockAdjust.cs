using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface IStockAdjust
    {
        long StockAdjustId { get; set; }
        long WareHouseId { get; set; }

        string StockAdjustNumber { get; set; }
        DateTime StockAdjustDate { get; set; }

        long AdjustReasonId { get; set; }        
        string AdjustReason { get; set; }      
        
        bool IsStockIncrease { get; set; }
        string Remarks { get; set; }
    }
}
