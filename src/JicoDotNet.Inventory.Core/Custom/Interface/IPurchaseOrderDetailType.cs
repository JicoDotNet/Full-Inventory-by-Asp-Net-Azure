using System;

namespace JicoDotNet.Inventory.Core.Custom.Interface
{
    public interface IPurchaseOrderDetailType
    {
        int Id { get; set; }
        long PurchaseOrderDetailId { get; set; }
        long PurchaseOrderId { get; set; }
        string PurchaseOrderNumber { get; set; }
        string AmendmentNumber { get; set; }
        long ProductId { get; set; }
        string HSNSAC { get; set; }
        decimal Amount { get; set; }
        decimal DiscountPercentage { get; set; }
        decimal DiscountAmount { get; set; }
        decimal Price { get; set; }
        decimal Quantity { get; set; }
        decimal SubTotal { get; set; }
        decimal TaxPercentage { get; set; }
        decimal TaxAmount { get; set; }
        decimal Total { get; set; }
        string Description { get; set; }
        DateTime TransactionDate { get; set; }
        bool IsActive { get; set; }


        string RequestId { get; set; }
    }
}
