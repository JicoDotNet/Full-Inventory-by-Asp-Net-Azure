using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface IPaymentType : IActivity, IStatus,   IHRequest
    {
        long PaymentTypeId { get; set; }
        string PaymentTypeName { get; set; }
        string Description { get; set; }
         
    }
}
