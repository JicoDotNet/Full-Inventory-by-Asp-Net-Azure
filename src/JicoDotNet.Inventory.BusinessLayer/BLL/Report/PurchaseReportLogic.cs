using DataAccess.Sql;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class.Report;
using JicoDotNet.Inventory.BusinessLayer.DTO.Core;
using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
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
        public PurchaseReportLogic(ICommonRequestDto CommonObj) : base(CommonObj) { }

        public List<RVendorPurchase> VendorWise(PVendorPurchase vendorPurchase)
        {
            try
            {
                _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
                NameValuePairs nvp = new NameValuePairs
                {
                     
                     
                    new NameValuePair("@VendorTypeId", vendorPurchase.VendorTypeId),
                    new NameValuePair("@VendorId", vendorPurchase.VendorId),
                    new NameValuePair("@StartDate", vendorPurchase.SearchDate.StartDate),
                    new NameValuePair("@EndDate", vendorPurchase.SearchDate.EndDate),
                    new NameValuePair("@QueryType", "BYVENDOR")
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
                NameValuePairs nvp = new NameValuePairs
                {
                     
                     
                    new NameValuePair("@ProductTypeId", productPurchase.ProductTypeId),
                    new NameValuePair("@ProductId", productPurchase.ProductId),
                    new NameValuePair("@StartDate", productPurchase.SearchDate.StartDate),
                    new NameValuePair("@EndDate", productPurchase.SearchDate.EndDate),
                    new NameValuePair("@QueryType", "BYPRODUCT")
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
