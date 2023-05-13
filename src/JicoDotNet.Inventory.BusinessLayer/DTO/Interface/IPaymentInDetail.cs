using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface IPaymentInDetail
    {
        long PaymentInDetailId { get; set; }
        long PaymentInId { get; set; }
        long InvoiceId { get; set; }
        string InvoiceNumber { get; set; }
        decimal Amount { get; set; }
        bool IsFullReceived { get; set; }
        DateTime PaymentDate { get; set; }
        string Description { get; set; }
    }
}
