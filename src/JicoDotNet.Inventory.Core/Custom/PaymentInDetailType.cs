using JicoDotNet.Inventory.Core.Custom.Interface;
using System;

namespace JicoDotNet.Inventory.Core.Custom
{
    public class PaymentInDetailType : IPaymentInDetailType
    {
        public int Id { get; set; }
        public long InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal Amount { get; set; }
        public bool IsFullReceived { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Description { get; set; }
    }
}
