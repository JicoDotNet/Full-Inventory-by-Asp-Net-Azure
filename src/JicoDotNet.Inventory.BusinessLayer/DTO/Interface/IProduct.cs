using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface IProduct
    {
        long ProductId { get; set; }
        long ProductTypeId { get; set; }
        short ProductInOut { get; set; }
        string Brand { get; set; }
        string ProductName { get; set; }
        string ProductCode { get; set; }
        string HSNSAC { get; set; }
        decimal TaxPercentage { get; set; }
        string Description { get; set; }
        bool IsPerishableProduct { get; set; }
        bool HasExpirationDate { get; set; }
        bool HasBatchNo { get; set; }
        string ProductImageUrl { get; set; }
        long UnitOfMeasureId { get; set; }

        bool IsGoods { get; set; }
        string SKU { get; set; }
        decimal PurchasePrice { get; set; }
        decimal SalePrice { get; set; }
    }
}
