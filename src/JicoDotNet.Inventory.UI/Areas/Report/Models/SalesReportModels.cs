using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JicoDotNet.Inventory.UI.Report.Models
{
    public class SalesReportModels
    {
        public List<Customer> _customers { get; set; }
        public List<CustomerType> _customerTypes { get; set; }
        public List<RCustomerSales> _rCustomerSales { get; set; }


        public List<Product> _products { get; set; }
        public List<ProductType> _productTypes { get; set; }
        public List<RProductSales> _rProductSales { get; set; }
    }
}