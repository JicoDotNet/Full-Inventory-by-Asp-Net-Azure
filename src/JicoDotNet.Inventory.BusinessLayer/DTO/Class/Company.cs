using Microsoft.WindowsAzure.Storage.Table;
using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class Company : CompanyBasic, ICompany, IActivity, IStatus, IHRequest
    {
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string PINCode { get; set; }
        public string City { get; set; }
        public string WebsiteUrl { get; set; }         
         
        public bool IsActive { get; set; }
        public DateTime TransactionDate { get; set; }
        public string RequestId { get; set; }
    }
}
