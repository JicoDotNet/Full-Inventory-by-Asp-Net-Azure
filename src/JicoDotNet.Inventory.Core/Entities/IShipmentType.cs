using JicoDotNet.Inventory.Core.Entities.Inner;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IShipmentType : IGenericDescription
    {
        long ShipmentTypeId { get; set; }
        string ShipmentTypeName { get; set; }
    }
}
