using System;

namespace JicoDotNet.Inventory.Core.Entities
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
