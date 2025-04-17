using JicoDotNet.Inventory.Core.Entities.Inner;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IInvoiceType : IGenericDescription
    {
        long InvoiceTypeId { get; set; }
        string InvoiceTypeName { get; set; }
    }
}
