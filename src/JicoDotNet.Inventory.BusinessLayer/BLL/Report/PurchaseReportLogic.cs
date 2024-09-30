using DataAccess.Sql;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Report;
using JicoDotNet.Inventory.Core.Report.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace JicoDotNet.Inventory.BusinessLayer.BLL.Report
{
    public class PurchaseReportLogic : ConnectionString
    {
        public PurchaseReportLogic(ICommonRequestDto commonObj) : base(commonObj) { }

        public List<ResponseVendorPurchaseResult> VendorWise(IRequestVendorPurchaseParam vendorPurchase)
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
                return _sqlDBAccess.GetData(GenericLogic.SqlSchema + ".[spRpPurchase]", nvp).ToList<ResponseVendorPurchaseResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ResponseProductPurchaseResult> ProductWise(IRequestProductPurchaseParam productPurchase)
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
                return _sqlDBAccess.GetData(GenericLogic.SqlSchema + ".[spRpPurchase]", nvp).ToList<ResponseProductPurchaseResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
