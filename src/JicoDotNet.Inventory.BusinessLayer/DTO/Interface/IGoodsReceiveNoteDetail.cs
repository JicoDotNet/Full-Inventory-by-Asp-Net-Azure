using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
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
