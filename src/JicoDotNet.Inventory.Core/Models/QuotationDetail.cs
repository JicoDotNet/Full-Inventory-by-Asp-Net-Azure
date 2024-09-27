using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Entities.Inner;

namespace JicoDotNet.Inventory.Core.Models
{
    public class QuotationDetail :  Product, IQuotationDetail, IActivity, IStatus, IHRequest
    {
        public long QuotationDetailId { get; set; }
        public long QuotationId { get; set; }
        public string QuotationNumber { get; set; }

        public decimal Amount { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Total { get; set; }
    }
}