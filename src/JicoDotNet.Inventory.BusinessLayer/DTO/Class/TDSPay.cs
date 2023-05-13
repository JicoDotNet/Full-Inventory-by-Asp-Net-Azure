using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class TDSPay : PaymentOut, ITDSPay, IActivity, IStatus,   IHRequest
    {
        public long TDSPayId { get; set; }
        public new decimal TDSAmount { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? PayDate { get; set; }
    }
}
