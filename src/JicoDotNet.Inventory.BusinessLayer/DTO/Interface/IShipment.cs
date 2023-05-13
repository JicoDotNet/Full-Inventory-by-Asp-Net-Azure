using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface IShipment
    {
        long ShipmentId { get; set; }
        long ShipmentTypeId { get; set; }
        DateTime ShipmentDate { get; set; }
        string ShipmentNumber { get; set; }
        bool IsDirect { get; set; }
        bool IsFullShipped { get; set; }
        long SalesOrderId { get; set; }
        string SalesOrderNumber { get; set; }
        long WareHouseId { get; set; }
        string Remarks { get; set; }
        string VehicleNumber { get; set; }
        string HandOverPerson { get; set; }
        string HandOverPersonMobile { get; set; }
    }
}
