using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class TDSReceive : PaymentIn, ITDSReceive, IActivity, IStatus,   IHRequest
    {
        public long TDSReceiveId { get; set; }
        public new decimal TDSAmount { get; set; }
        public bool IsReceived { get; set; }
        public DateTime? ReceivedDate { get; set; }
    }
}
