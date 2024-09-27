namespace JicoDotNet.Inventory.Core.Custom.Interface
{
    public interface IInvoiceDetailType
    {
        int Id { get; set; }
        long SalesOrderDetailId { get; set; }
        long ProductId { get; set; }
        string HSNSAC { get; set; }
        decimal Price { get; set; }
        decimal InvoicedQuantity { get; set; }
        decimal SubTotal { get; set; }
        decimal TaxPercentage { get; set; }
        decimal CGSTAmount { get; set; }
        decimal SGSTAmount { get; set; }
        decimal IGSTAmount { get; set; }
        decimal Total { get; set; }
        string Description { get; set; }
    }
}
