using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JicoDotNet.Inventory.UI.Models
{
    public class SalesOrderModels
    {
        public SalesType _salesType { get; set; }
        public List<SalesType> _salesTypes { get; set; }

        public string _draftId { get; set; }
        public List<Branch> _branches { get; set; }
        public List<Customer> _customers { get; set; }
        public List<Product> _products { get; set; }

        public bool _isGstEnabled { get; set; }
        
        public Customer _customer { get; set; }
        public CompanyBasic _company { get; set; }
        public SalesOrder _salesOrder { get; set; }
        public List<SalesOrder> _salesOrders { get; set; }
        public Config _config { get; internal set; }
        public Company _companyAddress { get; set; }
    }
}