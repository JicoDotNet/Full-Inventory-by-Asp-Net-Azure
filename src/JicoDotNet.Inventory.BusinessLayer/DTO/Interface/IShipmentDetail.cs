using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface IShipmentDetail : IProductAttribute
    {
        long ShipmentDetailId { get; set; }
        long ShipmentId { get; set; }
        string ShipmentNumber { get; set; }
        long SalesOrderDetailId { get; set; }
        long ProductId { get; set; }
        decimal ShippedQuantity { get; set; }
        long StockDetailId { get; set; }
        string Description { get; set; }
    }
}
