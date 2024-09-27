using JicoDotNet.Inventory.Core.Custom.Interface;
using System;

namespace JicoDotNet.Inventory.Core.Custom
{
    public class PaymentOutDetailType : IPaymentOutDetailType
    {
        public int Id { get; set; }
        public long BillId { get; set; }
        public string BillNumber { get; set; }
        public decimal Amount { get; set; }
        public bool IsFullPaid { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Description { get; set; }
    }
}
