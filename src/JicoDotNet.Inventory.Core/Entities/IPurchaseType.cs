using JicoDotNet.Inventory.Core.Entities.Inner;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IPurchaseType : IGenericDescription
    {
        long PurchaseTypeId { get; set; }
        string PurchaseTypeName { get; set; }
    }
}
