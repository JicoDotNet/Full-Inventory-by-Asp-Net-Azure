using JicoDotNet.Inventory.Core.Entities;
using System;

namespace JicoDotNet.Inventory.Core.Models
{
    public class Customer : ICustomer, IDtoHeader
    {
        public long CustomerId { get; set; }
        public long CustomerTypeId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyType { get; set; }
        public string StateCode { get; set; }
        public bool IsGSTRegistered { get; set; }
        public string GSTStateCode { get; set; }
        public string GSTNumber { get; set; }
        public string PANNumber { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public bool IsRetailCustomer { get; set; }

        public string CustomerTypeName { get; set; }
        public string Description { get; set; }

        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }
        public string RequestId { get; set; }
    }
}
