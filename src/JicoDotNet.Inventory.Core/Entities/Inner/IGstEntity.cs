namespace JicoDotNet.Inventory.Core.Entities.Inner
{
    public interface IGstEntity
    {
        bool IsGSTRegistered { get; set; }
        string GSTStateCode { get; set; }
        string GSTNumber { get; set; }
    }
}