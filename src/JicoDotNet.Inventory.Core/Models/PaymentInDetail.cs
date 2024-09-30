using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Entities.Inner;
using System;

namespace JicoDotNet.Inventory.Core.Models
{
    public class PaymentInDetail : IPaymentInDetail, IActivity, IStatus, IHRequest
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
