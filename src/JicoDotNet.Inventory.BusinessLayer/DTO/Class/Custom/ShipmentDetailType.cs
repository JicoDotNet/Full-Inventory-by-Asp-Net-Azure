using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class.Custom
{
    internal class ShipmentDetailType
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
