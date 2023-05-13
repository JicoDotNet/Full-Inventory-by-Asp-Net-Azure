using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class PaymentInDetail : IPaymentInDetail, IActivity, IStatus,   IHRequest
    {
        public long PaymentInDetailId { get; set; }
        public long PaymentInId { get; set; }
        public long InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal Amount { get; set; }
        public bool IsFullReceived { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Description { get; set; }

         
        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }
         
         
        public string RequestId { get; set; }
    }
}
