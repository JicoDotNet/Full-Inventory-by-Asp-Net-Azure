using System;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface ITDSReceive
    {
        long TDSReceiveId { get; set; }
        long PaymentInId { get; set; }
        long CustomerId { get; set; }
        string PANNumber { get; set; }
        decimal TDSAmount { get; set; }
        bool IsReceived { get; set; }
        DateTime? ReceivedDate { get; set; }
    }
}
