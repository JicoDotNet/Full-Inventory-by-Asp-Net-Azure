using JicoDotNet.Inventory.Core.Custom.Interface;
using System;

namespace JicoDotNet.Inventory.Core.Custom
{
    public class ShipmentDetailType : IShipmentDetailType
    {
        public int Id { get; set; }
        public long SalesOrderDetailId { get; set; }
        public long ProductId { get; set; }
        public decimal ShippedQuantity { get; set; }
        public long StockDetailId { get; set; }
        public bool IsPerishable { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string BatchNo { get; set; }
        public string Description { get; set; }
    }
}
