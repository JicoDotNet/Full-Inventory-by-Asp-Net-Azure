namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IGoodsReceiveNoteDetail : IProductAttribute
    {
        long GRNDetailId { get; set; }
        long GRNId { get; set; }
        string GRNNumber { get; set; }
        long PurchaseOrderDetailId { get; set; }
        long ProductId { get; set; }
        decimal ReceivedQuantity { get; set; }
        string Description { get; set; }        
    }
}
