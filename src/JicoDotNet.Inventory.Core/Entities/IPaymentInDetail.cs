using JicoDotNet.Inventory.Core.Entities.Inner;
using System;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IPaymentInDetail : IGenericDescription
    {
        long PaymentInDetailId { get; set; }
        long PaymentInId { get; set; }
        long InvoiceId { get; set; }
        string InvoiceNumber { get; set; }
        decimal Amount { get; set; }
        bool IsFullReceived { get; set; }
        DateTime PaymentDate { get; set; }
    }
}
