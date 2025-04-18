using JicoDotNet.Inventory.Core.Entities.Inner;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface ICustomerType : IGenericDescription
    {
        long CustomerTypeId { get; set; }
        string CustomerTypeName { get; set; }
    }
}
