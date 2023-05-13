using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    interface IShipmentType : IActivity, IStatus,   IHRequest
    {
        long ShipmentTypeId { get; set; }
        string ShipmentTypeName { get; set; }
        string Description { get; set; }
         
    }
}
