using System;

namespace JicoDotNet.Inventory.Core.Custom.Interface
{
    public interface IPaymentInDetailType
    {
        int Id { get; set; }
        long InvoiceId { get; set; }
        string InvoiceNumber { get; set; }
        decimal Amount { get; set; }
        bool IsFullReceived { get; set; }
        DateTime PaymentDate { get; set; }
        string Description { get; set; }
    }
}
