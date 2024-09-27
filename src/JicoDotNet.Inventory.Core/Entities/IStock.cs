using System;

namespace JicoDotNet.Inventory.Core.Entities
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
