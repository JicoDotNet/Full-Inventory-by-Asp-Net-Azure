using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class VendorBank : IVendorBank, IBank, IActivity, IStatus,   IHRequest
    {
        public long VendorBankId { get; set; }
        public long VendorId { get; set; }

        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public string IFSC { get; set; }
        public string MICR { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }

         
        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }
         
         
        public string RequestId { get; set; }
    }
}
