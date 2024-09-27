using JicoDotNet.Inventory.Core.Custom.Interface;

namespace JicoDotNet.Inventory.Core.Custom
{
    public class QuotationDetailType : IQuotationDetailType
    {
        public int Id { get; set; }
        public long ProductId { get; set; }
        public string HSNSAC { get; set; }
        public decimal Amount { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Total { get; set; }
        public string Description { get; set; }
    }
}
