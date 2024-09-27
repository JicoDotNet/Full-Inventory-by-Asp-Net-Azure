using System;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Entities.Inner;

namespace JicoDotNet.Inventory.Core.Models
{
    public class TDSReceive : PaymentIn, ITDSReceive, IActivity, IStatus, IHRequest
    {
        public long TDSReceiveId { get; set; }
        public new decimal TDSAmount { get; set; }
        public bool IsReceived { get; set; }
        public DateTime? ReceivedDate { get; set; }
    }
}
