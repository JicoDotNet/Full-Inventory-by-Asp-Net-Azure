namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IPurchaseType
    {
        long PurchaseTypeId { get; set; }
        string PurchaseTypeName { get; set; }
        string Description { get; set; }
    }
}
