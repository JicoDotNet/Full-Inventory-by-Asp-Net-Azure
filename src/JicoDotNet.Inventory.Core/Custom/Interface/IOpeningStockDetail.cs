using System;

namespace JicoDotNet.Inventory.Core.Custom.Interface
{
    public interface IOpeningStockDetail
    {
        int Id { get; set; }
        long WareHouseId { get; set; }
        long ProductId { get; set; }
        decimal Quantity { get; set; }
        DateTime? GRNDate { get; set; }
        bool IsPerishable { get; set; }
        string BatchNo { get; set; }
        DateTime? ExpiryDate { get; set; }
        string Description { get; set; }
    }
}
