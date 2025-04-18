using JicoDotNet.Inventory.Core.Entities.Inner;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IProductType : IDtoHeader, IGenericDescription
    {
        long ProductTypeId { get; set; }
        string ProductTypeName { get; set; }
    }
}
