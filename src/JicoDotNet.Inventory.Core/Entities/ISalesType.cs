using JicoDotNet.Inventory.Core.Entities.Inner;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface ISalesType : IGenericDescription
    {
        long SalesTypeId { get; set; }

        string SalesTypeName { get; set; }
    }
}
