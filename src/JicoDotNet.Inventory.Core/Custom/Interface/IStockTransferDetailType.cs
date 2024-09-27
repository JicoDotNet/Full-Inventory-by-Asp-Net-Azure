using System;

namespace JicoDotNet.Inventory.Core.Custom.Interface
{
    public interface IStockTransferDetailType
    {
        int Id { get; set; }
        long ProductId { get; set; }
        decimal AvailableQuantity { get; set; }
        decimal TransferQuantity { get; set; }
        string Description { get; set; }

        long StockDetailId { get; set; }
        bool IsPerishable { get; set; }
        string BatchNo { get; set; }
        DateTime? ExpiryDate { get; set; }
    }
}
