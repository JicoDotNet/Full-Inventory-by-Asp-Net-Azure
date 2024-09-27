namespace JicoDotNet.Inventory.Core.Entities
{
    public interface ISalesType
    {
        long SalesTypeId { get; set; }
         
        string SalesTypeName { get; set; }
        string Description { get; set; }
    }
}
