using JicoDotNet.Inventory.Core.Entities;
using System;

namespace JicoDotNet.Inventory.Core.Models
{
    public class PaymentType : IPaymentType, IDtoHeader
    {
        public long PaymentTypeId { get; set; }
        public string PaymentTypeName { get; set; }
        public string Description { get; set; }

        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }
        public string RequestId { get; set; }
    }
}
