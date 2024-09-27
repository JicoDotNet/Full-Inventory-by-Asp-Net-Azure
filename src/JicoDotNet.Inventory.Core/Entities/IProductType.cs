namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IProductType : IDtoHeader
    {
        long ProductTypeId { get; set; }
        string ProductTypeName { get; set; }
        string Description { get; set; }
    }
}
