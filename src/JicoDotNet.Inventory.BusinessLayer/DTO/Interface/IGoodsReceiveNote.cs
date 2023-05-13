using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface IGoodsReceiveNote
    {
        long GRNId { get; set; }
        long WareHouseId { get; set; }
        DateTime GRNDate { get; set; }
        string GRNNumber { get; set; }

        bool IsDirect { get; set; }
        bool IsFullReceived { get; set; }

        long PurchaseOrderId { get; set; }
        string PurchaseOrderNumber { get; set; }

        string VendorDONumber { get; set; }
        string VendorInvoiceNumber { get; set; }
        DateTime? VendorInvoiceDate { get; set; }        
        string Remarks { get; set; }
    }
}