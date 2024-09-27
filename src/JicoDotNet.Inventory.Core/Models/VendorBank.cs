using System;
using JicoDotNet.Inventory.Core.Entities;

namespace JicoDotNet.Inventory.Core.Models
{
    public class VendorBank : IVendorBank, IDtoHeader
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
