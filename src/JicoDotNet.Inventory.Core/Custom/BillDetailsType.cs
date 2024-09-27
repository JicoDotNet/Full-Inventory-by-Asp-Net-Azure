using JicoDotNet.Inventory.Core.Custom.Interface;

namespace JicoDotNet.Inventory.Core.Custom
{
    public class BillDetailsType : IBillDetailsType
    {
        public int Id { get; set; }
        public long PurchaseOrderDetailId { get; set; }
        public long ProductId { get; set; }
        public string HSNSAC { get; set; }
        public decimal Price { get; set; }
        public decimal BilledQuantity { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal CGSTAmount { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal IGSTAmount { get; set; }
        public decimal Total { get; set; }
        public string Description { get; set; }
    }
}
