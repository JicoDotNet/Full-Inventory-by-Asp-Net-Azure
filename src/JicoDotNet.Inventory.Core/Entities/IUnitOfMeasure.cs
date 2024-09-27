namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IUnitOfMeasure
    {
        long UnitOfMeasureId { get; set; }
        string UnitOfMeasureName { get; set; }
        string Description { get; set; }
    }
}
