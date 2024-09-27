using System;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IStockTransfer
    {
        long StockTransferId { get; set; }
        string StockTransferNumber { get; set; }
        DateTime StockTransferDate { get; set; }
        long FromWareHouseId { get; set; }
        long ToWareHouseId { get; set; }
        string Remarks { get; set; }
    }
}
