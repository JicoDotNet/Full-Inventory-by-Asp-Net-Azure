namespace JicoDotNet.Inventory.Core.Custom.Interface
{
    public interface ISalesOrderDetailType
    {
        int Id { get; set; }
        long SalesOrderDetailId { get; set; }
        long SalesOrderId { get; set; }
        string SalesOrderNumber { get; set; }
        string AmendmentNumber { get; set; }
        long ProductId { get; set; }
        string HSNSAC { get; set; }
        decimal Amount { get; set; }
        decimal DiscountPercentage { get; set; }
        decimal DiscountAmount { get; set; }
        decimal Price { get; set; }
        decimal Quantity { get; set; }
        decimal SubTotal { get; set; }
        decimal TaxPercentage { get; set; }
        decimal TaxAmount { get; set; }
        decimal Total { get; set; }
        string Description { get; set; }
    }
}
