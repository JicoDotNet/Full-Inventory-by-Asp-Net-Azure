using JicoDotNet.Inventory.Core.Models;
using JicoDotNet.Inventory.Core.Report;
using System.Collections.Generic;

namespace JicoDotNet.Inventory.UI.Report.Models
{
    public class SalesReportModels
    {
        public List<Customer> _customers { get; set; }
        public List<CustomerType> _customerTypes { get; set; }
        public IList<ResponseCustomerSalesResult> _rCustomerSales { get; set; }


        public IList<Product> _products { get; set; }
        public IList<ProductType> _productTypes { get; set; }
        public IList<ResponseProductSalesResult> _rProductSales { get; set; }
    }
}