using System;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IQuotation
    {
        long QuotationId { get; set; }

        long CustomerId { get; set; }
        bool IsGstAllowed { get; set; }
        DateTime QuotationDate { get; set; }
        string QuotationNumber { get; set; }

        decimal QuotationAmount { get; set; }
        decimal QuotationTaxAmount { get; set; }
        decimal QuotationTotalAmount { get; set; }

        string TandC { get; set; }
        string Remarks { get; set; }
    }
}