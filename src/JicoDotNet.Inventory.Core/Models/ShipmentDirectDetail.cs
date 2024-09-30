using JicoDotNet.Inventory.Core.Entities;
using System;

namespace JicoDotNet.Inventory.Core.Models
{
    public class ShipmentDirectDetail : SalesOrderDetail, IProductAttribute
    {
        public long StockDetailId { get; set; }

        public bool IsPerishable { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string BatchNo { get; set; }
    }
}
