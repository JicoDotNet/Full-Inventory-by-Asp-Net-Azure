using DataAccess.Sql;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Models;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class VendorLogic : ConnectionString
    {
        public VendorLogic(ICommonRequestDto CommonObj) : base(CommonObj) { }

        #region Vendor Type
        public string TypeSet(VendorType vendorType)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = string.Empty;
            if (vendorType.VendorTypeId > 0)
                qt = "UPDATE";
            else
                qt = "INSERT";

            NameValuePairs nvp = new NameValuePairs
            {
                 
                 
                new NameValuePair("@VendorTypeId", vendorType.VendorTypeId),
                new NameValuePair("@VendorTypeName", vendorType.VendorTypeName),
                new NameValuePair("@Description", vendorType.Description),
                new NameValuePair("@RequestId", CommonObj.RequestId),
                new NameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.DataManipulation("[dbo].[spSetVendorType]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public string TypeDeactive(string VendorTypeId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = "INACTIVE";

            NameValuePairs nvp = new NameValuePairs
            {
                new NameValuePair("@VendorTypeId", VendorTypeId),
                 
                new NameValuePair("@RequestId", CommonObj.RequestId),
                new NameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.DataManipulation("[dbo].[spSetVendorType]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public List<VendorType> TypeGet()
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetVendorType]",
                new NameValuePairs
                {
                     
                     
                    new NameValuePair("@QueryType", "ALL")
                }).ToList<VendorType>();
        }
        #endregion

        #region Vendor
        public string Set(Vendor vendor)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt;
            if (vendor.VendorId > 0)
                qt = "UPDATE";
            else
                qt = "INSERT";

            NameValuePairs nvp = new NameValuePairs
            {
                 
                 
                new NameValuePair("@VendorId", vendor.VendorId),
                new NameValuePair("@VendorTypeId", vendor.VendorTypeId),                
                new NameValuePair("@CompanyName", vendor.CompanyName),
                new NameValuePair("@CompanyType", vendor.CompanyType),
                new NameValuePair("@StateCode", vendor.StateCode),
                new NameValuePair("@IsGSTRegistered", vendor.IsGSTRegistered),
                new NameValuePair("@GSTStateCode", vendor.IsGSTRegistered? (object)GenericLogic.GstStateCode(vendor.GSTNumber) : DBNull.Value),
                new NameValuePair("@GSTNumber", vendor.IsGSTRegistered? (object)vendor.GSTNumber?.ToUpper() : DBNull.Value),
                new NameValuePair("@PANNumber", vendor.PANNumber?.ToUpper()),
                new NameValuePair("@ContactPerson", vendor.ContactPerson),
                new NameValuePair("@Email", vendor.Email),
                new NameValuePair("@Mobile", vendor.Mobile),

                new NameValuePair("@RequestId", CommonObj.RequestId),
                new NameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.DataManipulation("[dbo].[spSetVendor]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public string Deactive(string VendorId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = "INACTIVE";

            NameValuePairs nvp = new NameValuePairs
            {
                new NameValuePair("@VendorId", VendorId),
                 
                new NameValuePair("@RequestId", CommonObj.RequestId),
                new NameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.DataManipulation("[dbo].[spSetVendor]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public List<Vendor> Get(bool? IsActive = null)
        {
            List<Vendor> vendors = new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetVendor]",
                new NameValuePairs
                {
                     
                     
                    new NameValuePair("@QueryType", "ALL")
                }).ToList<Vendor>();
            if (IsActive != null)
            {
                if (IsActive.Value)
                    return vendors.Where(a => a.IsActive).ToList();
                if (!IsActive.Value)
                    return vendors.Where(a => !a.IsActive).ToList();
            }
            return vendors;
        }
        #endregion

        #region Vendor Bank
        public string BankSet(VendorBank vendorBank)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = string.Empty;
            if (vendorBank.VendorBankId > 0)
                qt = "UPDATE";
            else
                qt = "INSERT";

            NameValuePairs nvp = new NameValuePairs
            {
                 
                 
                new NameValuePair("@VendorId", vendorBank.VendorId),
                new NameValuePair("@VendorBankId", vendorBank.VendorBankId),

                new NameValuePair("@AccountName", vendorBank.AccountName),
                new NameValuePair("@AccountNumber", vendorBank.AccountNumber?.ToUpper()),
                new NameValuePair("@BankName", vendorBank.BankName),
                new NameValuePair("@IFSC", vendorBank.IFSC?.ToUpper()),
                new NameValuePair("@MICR", vendorBank.MICR?.ToUpper()),
                new NameValuePair("@BranchName ", vendorBank.BranchName),
                new NameValuePair("@BranchAddress", vendorBank.BranchAddress),

                new NameValuePair("@RequestId", CommonObj.RequestId),
                new NameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.DataManipulation("[dbo].[spSetVendorBank]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public string BankDeactive(string VendorId, string VendorBankId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = "INACTIVE";

            NameValuePairs nvp = new NameValuePairs
            {
                new NameValuePair("@VendorId", VendorId),
                new NameValuePair("@VendorBankId", VendorBankId),
                 
                 
                new NameValuePair("@RequestId", CommonObj.RequestId),
                new NameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.DataManipulation("[dbo].[spSetVendorBank]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public List<VendorBank> BankGet(long VendorId, bool? IsActive = null)
        {
            List<VendorBank> vendorBanks = new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetVendorBank]",
                new NameValuePairs
                {
                     
                     
                    new NameValuePair("@VendorId", VendorId),
                    new NameValuePair("@QueryType", "ALL")
                }).ToList<VendorBank>();
            if (IsActive != null)
            {
                if (IsActive.Value)
                    return vendorBanks.Where(a => a.IsActive).ToList();
                if (!IsActive.Value)
                    return vendorBanks.Where(a => !a.IsActive).ToList();
            }
            return vendorBanks;
        }
        #endregion
    }
}
