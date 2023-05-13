using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface IPaymentOut
    {
        long PaymentOutId { get; set; }
        long VendorId { get; set; }
        long VendorBankId { get; set; }

        bool IsTDSApplicable { get; set; }
        decimal? TDSPercentage { get; set; }
        decimal? TDSAmount { get; set; }
        decimal PayAmount { get; set; }

        decimal TotalAmount { get; set; }
        DateTime PaymentDate { get; set; }
        short PaymentMode { get; set; }
        string ReferenceNo { get; set; }
        string Remarks { get; set; }

        string ChequeNo { get; set; }
        DateTime? ChequeDate { get; set; }
        string ChequeIFSC { get; set; }
    }
}
