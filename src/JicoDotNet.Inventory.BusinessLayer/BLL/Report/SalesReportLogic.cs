using DataAccess.Sql;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class.Report;
using JicoDotNet.Inventory.BusinessLayer.DTO.SP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.BLL.Report
{
    public class SalesReportLogic : ConnectionString
    {
        public SalesReportLogic(sCommonDto CommonObj) : base(CommonObj) { }

        public List<RCustomerSales> CustomerWise(PCustomerSales customerSales)
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
            return _sqlDBAccess.GetData("[dbo].[spRpSales]", nvp).ToList<RCustomerSales>();
        }

        public List<RProductSales> ProductWise(PProductSales productSales)
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
            return _sqlDBAccess.GetData("[dbo].[spRpSales]", nvp).ToList<RProductSales>();
        }
    }
}
