using JicoDotNet.Inventory.Core.Entities.Inner;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IShipmentDetail : IProductAttribute, IGenericDescription
    {
        long ShipmentDetailId { get; set; }
        long ShipmentId { get; set; }
        string ShipmentNumber { get; set; }
        long SalesOrderDetailId { get; set; }
        long ProductId { get; set; }
        decimal ShippedQuantity { get; set; }
        long StockDetailId { get; set; }
    }
}
