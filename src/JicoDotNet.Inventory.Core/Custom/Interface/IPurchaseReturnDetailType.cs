using System;

namespace JicoDotNet.Inventory.Core.Custom.Interface
{
    public interface IPurchaseReturnDetailType
    {
        int Id { get; set; }
        long GRNDetailId { get; set; }
        long PurchaseOrderDetailId { get; set; }
        long ProductId { get; set; }
        decimal ReturnedQuantity { get; set; }
        string Description { get; set; }
        bool IsPerishable { get; set; }
        string BatchNo { get; set; }
        DateTime? ExpiryDate { get; set; }
    }
}
