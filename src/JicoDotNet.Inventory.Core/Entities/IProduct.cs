using JicoDotNet.Inventory.Core.Entities.Inner;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IProduct : IProductType, IUnitOfMeasure, IGenericDescription, IDtoHeader
    {
        long ProductId { get; set; }
        short ProductInOut { get; set; }
        string Brand { get; set; }
        string ProductName { get; set; }
        string ProductCode { get; set; }
        string HSNSAC { get; set; }
        decimal TaxPercentage { get; set; }
        bool IsPerishableProduct { get; set; }
        bool HasExpirationDate { get; set; }
        bool HasBatchNo { get; set; }
        string ProductImageUrl { get; set; }

        bool IsGoods { get; set; }
        string SKU { get; set; }
        decimal PurchasePrice { get; set; }
        decimal SalePrice { get; set; }
    }
}
