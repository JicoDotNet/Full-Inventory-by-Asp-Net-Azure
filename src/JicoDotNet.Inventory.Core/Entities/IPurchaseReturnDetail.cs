namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IPurchaseReturnDetail
    {
        long PurchaseReturnDetailId { get; set; }
        string PurchaseReturnNumber { get; set; }
        decimal ReturnedQuantity { get; set; }
    }
}