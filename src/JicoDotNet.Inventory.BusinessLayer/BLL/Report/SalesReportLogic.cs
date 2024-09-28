using DataAccess.Sql;
using JicoDotNet.Inventory.BusinessLayer.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Report;
using JicoDotNet.Inventory.Core.Report.Interface;

namespace JicoDotNet.Inventory.BusinessLayer.BLL.Report
{
    public class SalesReportLogic : ConnectionString
    {
        public SalesReportLogic(ICommonRequestDto commonObj) : base(commonObj) { }

        public IList<ResponseCustomerSalesResult> CustomerWise(IRequestCustomerSalesParam customerSales)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            NameValuePairs nvp = new NameValuePairs
            {
                 
                 
                new NameValuePair("@CustomerTypeId", customerSales.CustomerTypeId),
                new NameValuePair("@CustomerId", customerSales.CustomerId),
                new NameValuePair("@StartDate", customerSales.SearchDate.StartDate),
                new NameValuePair("@EndDate", customerSales.SearchDate.EndDate),
                new NameValuePair("@ForRetail", customerSales.ForRetail),
                new NameValuePair("@QueryType", "BYCUSTOMER")
            };
            return _sqlDBAccess.GetData(GenericLogic.SqlSchema + ".[spRpSales]", nvp).ToList<ResponseCustomerSalesResult>();
        }

        public IList<ResponseProductSalesResult> ProductWise(IRequestProductSalesParam productSales)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            NameValuePairs nvp = new NameValuePairs
            {
                 
                 
                new NameValuePair("@ProductTypeId", productSales.ProductTypeId),
                new NameValuePair("@ProductId", productSales.ProductId),
                new NameValuePair("@StartDate", productSales.SearchDate.StartDate),
                new NameValuePair("@EndDate", productSales.SearchDate.EndDate),
                new NameValuePair("@ForRetail", productSales.ForRetail),
                new NameValuePair("@QueryType", "BYPRODUCT")
            };
            return _sqlDBAccess.GetData(GenericLogic.SqlSchema + ".[spRpSales]", nvp).ToList<ResponseProductSalesResult>();
        }
    }
}
