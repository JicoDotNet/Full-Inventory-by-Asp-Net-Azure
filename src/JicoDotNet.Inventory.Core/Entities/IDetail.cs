namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IDetail
    {
        long ProductId { get; set; }

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
