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
    public class PurchaseReportLogic : ConnectionString
    {
        public PurchaseReportLogic(sCommonDto CommonObj) : base(CommonObj) { }

        public List<RVendorPurchase> VendorWise(PVendorPurchase vendorPurchase)
        {
            try
            {
                _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
                nameValuePairs nvp = new nameValuePairs
                {
                     
                     
                    new nameValuePair("@VendorTypeId", vendorPurchase.VendorTypeId),
                    new nameValuePair("@VendorId", vendorPurchase.VendorId),
                    new nameValuePair("@StartDate", vendorPurchase.SearchDate.StartDate),
                    new nameValuePair("@EndDate", vendorPurchase.SearchDate.EndDate),
                    new nameValuePair("@QueryType", "BYVENDOR")
                };
                return _sqlDBAccess.GetData("[dbo].[spRpPurchase]", nvp).ToList<RVendorPurchase>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RProductPurchase> ProductWise(PProductPurchase productPurchase)
        {
            try
            {
                _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
                nameValuePairs nvp = new nameValuePairs
                {
                     
                     
                    new nameValuePair("@ProductTypeId", productPurchase.ProductTypeId),
                    new nameValuePair("@ProductId", productPurchase.ProductId),
                    new nameValuePair("@StartDate", productPurchase.SearchDate.StartDate),
                    new nameValuePair("@EndDate", productPurchase.SearchDate.EndDate),
                    new nameValuePair("@QueryType", "BYPRODUCT")
                };
                return _sqlDBAccess.GetData("[dbo].[spRpPurchase]", nvp).ToList<RProductPurchase>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
