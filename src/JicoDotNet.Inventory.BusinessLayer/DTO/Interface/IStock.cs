using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface IStock
    {
         
        long ProductId { get; set; }
        long WareHouseId { get; set; }
        decimal CurrentStock { get; set; }
        string Remarks { get; set; }
        DateTime GRNOrShipmentDate { get; set; }
        long GRNOrShipmentId { get; set; }
        short StockType { get; set; }
    }
}
