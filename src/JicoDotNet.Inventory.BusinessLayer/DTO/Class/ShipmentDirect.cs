using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    /// <summary>
    /// This class Only for Model POST from HTML to Controller and Logic. 
    /// </summary>
    public class ShipmentDirect : Shipment, ISalesOrder
    {
        public long? QuotationId { get; set; }
        public long SalesTypeId { get; set; }
        public long CustomerId { get; set; }
        public bool IsGstAllowed { get; set; }

        public DateTime SalesOrderDate { get; set; }
         
        public string AmendmentNumber { get; set; }
        public DateTime? AmendmentDate { get; set; }

        public string CustomerPONumber { get; set; }
        public DateTime? CustomerPODate { get; set; }
        public DateTime? DeliveryDate { get; set; }
         
        public decimal SalesOrderAmount { get; set; }
        public decimal SalesOrderTaxAmount { get; set; }
        public decimal SalesOrderTotalAmount { get; set; }
         
        public string TandC { get; set; }

        // This prop(s) are for Shipment Direct it self
        public bool IsInvoiceGenerated { get; set; }
        public bool IsReceived { get; set; }

        public List<ShipmentDirectDetail> ShipmentDirectDetails { get; set; }
    }
}
