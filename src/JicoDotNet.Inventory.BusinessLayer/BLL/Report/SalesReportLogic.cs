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
            nameValuePairs nvp = new nameValuePairs
            {
                 
                 
                new nameValuePair("@CustomerTypeId", customerSales.CustomerTypeId),
                new nameValuePair("@CustomerId", customerSales.CustomerId),
                new nameValuePair("@StartDate", customerSales.SearchDate.StartDate),
                new nameValuePair("@EndDate", customerSales.SearchDate.EndDate),
                new nameValuePair("@ForRetail", customerSales.ForRetail),
                new nameValuePair("@QueryType", "BYCUSTOMER")
            };
            return _sqlDBAccess.GetData("[dbo].[spRpSales]", nvp).ToList<RCustomerSales>();
        }

        public List<RProductSales> ProductWise(PProductSales productSales)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            nameValuePairs nvp = new nameValuePairs
            {
                 
                 
                new nameValuePair("@ProductTypeId", productSales.ProductTypeId),
                new nameValuePair("@ProductId", productSales.ProductId),
                new nameValuePair("@StartDate", productSales.SearchDate.StartDate),
                new nameValuePair("@EndDate", productSales.SearchDate.EndDate),
                new nameValuePair("@ForRetail", productSales.ForRetail),
                new nameValuePair("@QueryType", "BYPRODUCT")
            };
            return _sqlDBAccess.GetData("[dbo].[spRpSales]", nvp).ToList<RProductSales>();
        }
    }
}
