using JicoDotNet.Inventory.Core.Entities;
using System;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class CompanyBank : ICompanyBank, IDtoHeader
    {
        public long CompanyBankId { get; set; }

        public string AccountName { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string IFSC { get; set; }
        public string MICR { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }

        public bool IsPrintable { get; set; }

        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }


        public string RequestId { get; set; }
    }
}
