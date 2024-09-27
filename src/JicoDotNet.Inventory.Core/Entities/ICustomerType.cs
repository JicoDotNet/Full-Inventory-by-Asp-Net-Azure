namespace JicoDotNet.Inventory.Core.Entities
{
    public interface ICustomerType
    {
        long CustomerTypeId { get; set; }
        string CustomerTypeName { get; set; }
        string Description { get; set; }
    }
}
