namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IInvoiceType
    {
        long InvoiceTypeId { get; set; }
        string InvoiceTypeName { get; set; }
        string Description { get; set; }

    }
}
