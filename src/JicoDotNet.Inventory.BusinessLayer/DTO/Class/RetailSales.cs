using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class RetailSales : ShipmentDirect, ICustomer
    {
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

        public long InvoiceTypeId { get; set; }

        public string InvoiceNumber { get; set; }
        public bool IsCustomizedInvoiceNumber { get; set; }
    }
}
