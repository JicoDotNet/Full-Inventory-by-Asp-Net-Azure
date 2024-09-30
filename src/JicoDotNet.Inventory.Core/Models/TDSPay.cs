using JicoDotNet.Inventory.Core.Entities;
using System;

namespace JicoDotNet.Inventory.Core.Models
{
    public class TDSPay : PaymentOut, ITDSPay
    {
        public long TDSPayId { get; set; }
        public new decimal TDSAmount { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? PayDate { get; set; }
    }
}
