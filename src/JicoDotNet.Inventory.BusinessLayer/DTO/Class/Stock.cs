using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class Stock : IStock, IWareHouse, IProduct, IUnitOfMeasure, IActivity, IStatus,   IHRequest
    {
         
        public long ProductId { get; set; }
        public long WareHouseId { get; set; }
        public decimal CurrentStock { get; set; }
        public string Remarks { get; set; }
        public DateTime GRNOrShipmentDate { get; set; }
        public long GRNOrShipmentId { get; set; }
        public short StockType { get; set; }

        public long BranchId { get; set; }
        public string WareHouseName { get; set; }
        public bool IsRetailCounter { get; set; }
        public string Description { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public long ProductTypeId { get; set; }
        public short ProductInOut { get; set; }
        public string Brand { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string HSNSAC { get; set; }
        public decimal TaxPercentage { get; set; }
        public bool IsPerishableProduct { get; set; }
        public bool HasExpirationDate { get; set; }
        public bool HasBatchNo { get; set; }
        public string ProductImageUrl { get; set; }
        public long UnitOfMeasureId { get; set; }

        public bool IsGoods { get; set; }
        public string SKU { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }

        public string UnitOfMeasureName { get; set; }

        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }
         
         
        public string RequestId { get; set; }

        public List<StockDetail> StockDetails { get; set; }
    }
}
