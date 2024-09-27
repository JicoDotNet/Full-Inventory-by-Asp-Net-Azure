using System;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IBill : IVendor, IDtoHeader
    {
        long BillId { get; set; }
        long PurchaseOrderId { get; set; }
        long BillTypeId { get; set; }
        DateTime BillDate { get; set; }
        DateTime? BillDueDate { get; set; }
        string BillNumber { get; set; }
        bool IsFullBilled { get; set; }

        long VendorId { get; set; }
        bool IsGstApplicable { get; set; }
        string GSTNumber { get; set; }
        string GSTStateCode { get; set; }
        short GSTType { get; set; }

        decimal BilledAmount { get; set; }
        decimal TaxAmount { get; set; }
        decimal TotalAmount { get; set; }

        string VendorDONumber { get; set; }
        string VendorInvoiceNumber { get; set; }
        DateTime? VendorInvoiceDate { get; set; }
        string Remarks { get; set; }

        decimal PaidAmount { get; set; }
        bool? PaymentStatus { get; set; }
    }
}
