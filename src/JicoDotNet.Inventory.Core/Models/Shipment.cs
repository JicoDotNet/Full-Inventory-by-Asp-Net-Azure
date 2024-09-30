using JicoDotNet.Inventory.Core.Entities;
using System;
using System.Collections.Generic;

namespace JicoDotNet.Inventory.Core.Models
{
    public class Shipment : IShipment, IShipmentType, IWareHouse, IDtoHeader
    {
        public long ShipmentId { get; set; }
        public long ShipmentTypeId { get; set; }
        public DateTime ShipmentDate { get; set; }
        public string ShipmentNumber { get; set; }
        public bool IsDirect { get; set; }
        public bool IsFullShipped { get; set; }
        public long SalesOrderId { get; set; }
        public string SalesOrderNumber { get; set; }
        public long WareHouseId { get; set; }
        public string Remarks { get; set; }
        public string VehicleNumber { get; set; }
        public string HandOverPerson { get; set; }
        public string HandOverPersonMobile { get; set; }

        public string ShipmentTypeName { get; set; }

        public long BranchId { get; set; }
        public string WareHouseName { get; set; }
        public bool IsRetailCounter { get; set; }
        public string Description { get; set; }


        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }


        public string RequestId { get; set; }

        public List<ShipmentDetail> ShipmentDetails { get; set; }

        /// <summary>
        /// null - Not billed, false - partially received, true - full billed :: only gor Direct GRN
        /// It is to populate the grid of PO List
        /// </summary>
        public bool? InvoicedStatus { get; set; }
    }
}
