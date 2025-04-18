﻿using JicoDotNet.Inventory.Core.Entities.Inner;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IPurchaseOrderDetail : IGenericDescription
    {
        long PurchaseOrderDetailId { get; set; }

        long PurchaseOrderId { get; set; }
        string PurchaseOrderNumber { get; set; }
        string AmendmentNumber { get; set; }

        long ProductId { get; set; }

        decimal Amount { get; set; }
        decimal DiscountPercentage { get; set; }
        decimal DiscountAmount { get; set; }
        decimal Price { get; set; }
        decimal Quantity { get; set; }
        decimal SubTotal { get; set; }
        decimal TaxPercentage { get; set; }
        decimal TaxAmount { get; set; }
        decimal Total { get; set; }
    }
}
