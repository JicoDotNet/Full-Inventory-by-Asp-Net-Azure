using System;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface ITDSPay
    {
        long TDSPayId { get; set; }
        long PaymentOutId { get; set; }
        long VendorId { get; set; }
        string PANNumber { get; set; }
        decimal TDSAmount { get; set; }
        bool IsPaid { get; set; }
        DateTime? PayDate { get; set; }
    }
}
