using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JicoDotNet.Inventory.Core.Models;

namespace JicoDotNet.Inventory.UI.Models
{
    public class CustomerModels
    {
        public CustomerType _customerType { get; set; }
        public List<CustomerType> _customerTypes { get; set; }

        public Customer _customer { get; set; }
        public List<Customer> _customers { get; set; }
        public bool IsRetail { get; set; }

        public Dictionary<string, string> _state { get; set; }
        public Dictionary<string, string> _companyType { get; set; }
        public IDictionary<bool, string> _YesNo { get; set; }
    }
}