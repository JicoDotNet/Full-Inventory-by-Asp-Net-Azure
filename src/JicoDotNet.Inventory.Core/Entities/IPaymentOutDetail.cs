using JicoDotNet.Inventory.Core.Entities.Inner;
using System;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IPaymentOutDetail : IGenericDescription
    {
        long PaymentOutDetailId { get; set; }
        long PaymentOutId { get; set; }
        long BillId { get; set; }
        string BillNumber { get; set; }
        decimal Amount { get; set; }
        bool IsFullPaid { get; set; }
        DateTime PaymentDate { get; set; }
    }
}
