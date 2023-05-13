using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    interface IInvoiceType : IActivity, IStatus,   IHRequest
    {
        long InvoiceTypeId { get; set; }
        string InvoiceTypeName { get; set; }
        string Description { get; set; }
         
    }
}
