using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
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
