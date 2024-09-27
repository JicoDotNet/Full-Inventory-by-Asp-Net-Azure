using System;

namespace JicoDotNet.Inventory.Core.Custom.Interface
{
    public interface IPaymentOutDetailType
    {
        int Id { get; set; }
        long BillId { get; set; }
        string BillNumber { get; set; }
        decimal Amount { get; set; }
        bool IsFullPaid { get; set; }
        DateTime PaymentDate { get; set; }
        string Description { get; set; }
    }
}
