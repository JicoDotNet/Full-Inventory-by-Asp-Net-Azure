using JicoDotNet.Inventory.Core.Entities;
using System;

namespace JicoDotNet.Inventory.Core.Models
{
    public class ShipmentType : IShipmentType, IDtoHeader
    {
        public long ShipmentTypeId { get; set; }
        public string ShipmentTypeName { get; set; }
        public string Description { get; set; }

        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }
        public string RequestId { get; set; }
    }
}
