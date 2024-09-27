using System;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IPurchaseOrder
    {
        long PurchaseOrderId { get; set; }

        long PurchaseTypeId { get; set; }
        long BranchId { get; set; }
        long VendorId { get; set; }
        bool IsGstAllowed { get; set; }

        DateTime PurchaseOrderDate { get; set; }
        string PurchaseOrderNumber { get; set; }

        string AmendmentNumber { get; set; }
        DateTime? AmendmentDate { get; set; }

        DateTime? DeliveryDate { get; set; }

        decimal PurchaseOrderAmount { get; set; }
        decimal PurchaseOrderTaxAmount { get; set; }
        decimal PurchaseOrderTotalAmount { get; set; }

        string TandC { get; set; }
        string Remarks { get; set; }
    }
}
