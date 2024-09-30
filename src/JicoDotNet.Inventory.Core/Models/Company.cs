using JicoDotNet.Inventory.Core.Entities;
using System;

namespace JicoDotNet.Inventory.Core.Models
{
    public class Company : ICompany, IDtoHeader
    {
        public string CompanyName { get; set; }
        public string StateCode { get; set; }
        public bool IsGSTRegistered { get; set; }
        public string GSTStateCode { get; set; }
        public string GSTNumber { get; set; }

        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string PINCode { get; set; }
        public string City { get; set; }
        public string WebsiteUrl { get; set; }
        public string LogoUrl { get; set; }

        public bool IsActive { get; set; }
        public DateTime TransactionDate { get; set; }
        public string RequestId { get; set; }
    }
}
