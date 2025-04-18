using JicoDotNet.Inventory.Core.Entities.Inner;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IBillType : IDtoHeader, IGenericDescription
    {
        long BillTypeId { get; set; }
        string BillTypeName { get; set; }
    }
}
