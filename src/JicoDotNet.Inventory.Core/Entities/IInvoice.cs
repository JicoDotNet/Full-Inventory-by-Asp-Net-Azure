using System;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IInvoice : ICustomer
    {
        long InvoiceId { get; set; }
        long SalesOrderId { get; set; }
        long InvoiceTypeId { get; set; }
        DateTime InvoiceDate { get; set; }
        DateTime? InvoiceDueDate { get; set; }
        string InvoiceNumber { get; set; }
        bool IsCustomizedInvoiceNumber { get; set; }
        bool IsFullInvoiced { get; set; }

        long CustomerId { get; set; }
        bool IsGstApplicable { get; set; }
        short GSTType { get; set; }

        decimal InvoicedAmount { get; set; }
        decimal TaxAmount { get; set; }
        decimal TotalAmount { get; set; }

        string VehicleNumber { get; set; }
        string HandOverPerson { get; set; }
        string HandOverPersonMobile { get; set; }

        string Remarks { get; set; }
    }
}
