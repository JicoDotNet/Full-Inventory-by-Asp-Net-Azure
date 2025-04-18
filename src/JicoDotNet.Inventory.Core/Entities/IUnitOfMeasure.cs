using JicoDotNet.Inventory.Core.Entities.Inner;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IUnitOfMeasure : IGenericDescription
    {
        long UnitOfMeasureId { get; set; }
        string UnitOfMeasureName { get; set; }
    }
}
