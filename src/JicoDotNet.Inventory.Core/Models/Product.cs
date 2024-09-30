using JicoDotNet.Inventory.Core.Entities;
using System;

namespace JicoDotNet.Inventory.Core.Models
{
    public class Product : IProduct
    {
        public long ProductId { get; set; }

        public long ProductTypeId { get; set; }
        public short ProductInOut { get; set; }
        public string Brand { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string HSNSAC { get; set; }
        public decimal TaxPercentage { get; set; }
        public string Description { get; set; }
        public bool IsPerishableProduct { get; set; }
        public bool HasExpirationDate { get; set; }
        public bool HasBatchNo { get; set; }
        public string ProductImageUrl { get; set; }
        public long UnitOfMeasureId { get; set; }

        public bool IsGoods { get; set; }
        public string SKU { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }

        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }
        public string RequestId { get; set; }

        public string ProductTypeName { get; set; }

        public string UnitOfMeasureName { get; set; }
    }
}
