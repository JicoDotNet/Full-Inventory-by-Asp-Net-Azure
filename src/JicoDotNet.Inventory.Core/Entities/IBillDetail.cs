using JicoDotNet.Inventory.Core.Entities.Inner;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IBillDetail : IGenericDescription
    {
        long BillDetailId { get; set; }
        long BillId { get; set; }
        string BillNumber { get; set; }
        long PurchaseOrderDetailId { get; set; }
        long ProductId { get; set; }
        decimal BilledQuantity { get; set; }

        string HSNSAC { get; set; }
        decimal Price { get; set; }

        decimal SubTotal { get; set; }
        decimal TaxPercentage { get; set; }
        decimal CGSTAmount { get; set; }
        decimal SGSTAmount { get; set; }
        decimal IGSTAmount { get; set; }
        decimal Total { get; set; }
    }
}
