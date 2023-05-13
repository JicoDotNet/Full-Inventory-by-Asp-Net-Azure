using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface IPaymentOutDetail
    {
        long PaymentOutDetailId { get; set; }
        long PaymentOutId { get; set; }
        long BillId { get; set; }
        string BillNumber { get; set; }
        decimal Amount { get; set; }
        bool IsFullPaid { get; set; }
        DateTime PaymentDate { get; set; }
        string Description { get; set; }
    }
}
