using JicoDotNet.Inventory.Core.Custom.Interface;

namespace JicoDotNet.Inventory.Core.Custom
{
    public class InvoiceDetailType : IInvoiceDetailType
    {
        public int Id { get; set; }
        public long SalesOrderDetailId { get; set; }
        public long ProductId { get; set; }
        public string HSNSAC { get; set; }
        public decimal Price { get; set; }
        public decimal InvoicedQuantity { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal CGSTAmount { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal IGSTAmount { get; set; }
        public decimal Total { get; set; }
        public string Description { get; set; }
    }
}
