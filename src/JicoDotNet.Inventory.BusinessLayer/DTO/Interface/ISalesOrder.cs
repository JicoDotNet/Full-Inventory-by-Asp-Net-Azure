using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface ISalesOrder
    {
        long SalesOrderId { get; set; }

        long? QuotationId { get; set; }
        long SalesTypeId { get; set; }
         
        long BranchId { get; set; }
        long CustomerId { get; set; }
        bool IsGstAllowed { get; set; }

        DateTime SalesOrderDate { get; set; }
        string SalesOrderNumber { get; set; }

        string AmendmentNumber { get; set; }
        DateTime? AmendmentDate { get; set; }

        string CustomerPONumber { get; set; }
        DateTime? CustomerPODate { get; set; }
        DateTime? DeliveryDate { get; set; }

        decimal SalesOrderAmount { get; set; }
        decimal SalesOrderTaxAmount { get; set; }
        decimal SalesOrderTotalAmount { get; set; }

        string TandC { get; set; }
        string Remarks { get; set; }
    }
}