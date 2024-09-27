namespace JicoDotNet.Inventory.Core.Models.DS
{
    public class HomeMasterCount
    {
        public long User { get; set; }
        public long Company { get; set; }
        public long UnitOfMeasure { get; set; }
        public long Product { get; set; }
        public long Warehouse { get; set; }

        public long Bill { get; set; }
        public long BillType { get; set; }
        public long Invoice { get; set; }
        public long InvoiceType { get; set; }
        public long PurchaseType { get; set; }
        public long SalesType { get; set; }
        public long PurchaseOrder { get; set; }
        public long SalesOrder { get; set; }

        public long Branch { get; set; }
        public long ProductType { get; set; }
        public long VendorType { get; set; }
        public long Vendor { get; set; }
        public long CustomerType { get; set; }
        public long Customer { get; set; }
        public long ShipmentType { get; set; }
    }
}
