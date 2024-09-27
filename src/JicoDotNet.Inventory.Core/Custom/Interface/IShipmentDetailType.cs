using System;

namespace JicoDotNet.Inventory.Core.Custom.Interface
{
    public interface IShipmentDetailType
    {
        int Id { get; set; }
        long SalesOrderDetailId { get; set; }
        long ProductId { get; set; }
        decimal ShippedQuantity { get; set; }
        long StockDetailId { get; set; }
        bool IsPerishable { get; set; }
        DateTime? ExpiryDate { get; set; }
        string BatchNo { get; set; }
        string Description { get; set; }
    }
}
