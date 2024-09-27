namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IShipmentType
    {
        long ShipmentTypeId { get; set; }
        string ShipmentTypeName { get; set; }
        string Description { get; set; }
    }
}
