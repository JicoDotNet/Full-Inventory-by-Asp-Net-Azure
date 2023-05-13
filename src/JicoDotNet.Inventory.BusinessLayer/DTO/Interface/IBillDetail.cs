using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface IBillDetail
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

        string Description { get; set; }
    }
}
