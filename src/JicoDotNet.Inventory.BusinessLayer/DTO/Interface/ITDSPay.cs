using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
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
