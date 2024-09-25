using DataAccess.Sql;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.BusinessLayer.DTO.SP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class VendorLogic : ConnectionString
    {
        public VendorLogic(sCommonDto CommonObj) : base(CommonObj) { }

        #region Vendor Type
        public string TypeSet(VendorType vendorType)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = string.Empty;
            if (vendorType.VendorTypeId > 0)
                qt = "UPDATE";
            else
                qt = "INSERT";

            nameValuePairs nvp = new nameValuePairs
            {
                 
                 
                new nameValuePair("@VendorTypeId", vendorType.VendorTypeId),
                new nameValuePair("@VendorTypeName", vendorType.VendorTypeName),
                new nameValuePair("@Description", vendorType.Description),
                new nameValuePair("@RequestId", CommonObj.RequestId),
                new nameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetVendorType]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public string TypeDeactive(string VendorTypeId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = "INACTIVE";

            nameValuePairs nvp = new nameValuePairs
            {
                new nameValuePair("@VendorTypeId", VendorTypeId),
                 
                new nameValuePair("@RequestId", CommonObj.RequestId),
                new nameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetVendorType]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public List<VendorType> TypeGet()
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetVendorType]",
                new nameValuePairs
                {
                     
                     
                    new nameValuePair("@QueryType", "ALL")
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

            nameValuePairs nvp = new nameValuePairs
            {
                 
                 
                new nameValuePair("@VendorId", vendor.VendorId),
                new nameValuePair("@VendorTypeId", vendor.VendorTypeId),                
                new nameValuePair("@CompanyName", vendor.CompanyName),
                new nameValuePair("@CompanyType", vendor.CompanyType),
                new nameValuePair("@StateCode", vendor.StateCode),
                new nameValuePair("@IsGSTRegistered", vendor.IsGSTRegistered),
                new nameValuePair("@GSTStateCode", vendor.IsGSTRegistered? (object)GenericLogic.GstStateCode(vendor.GSTNumber) : DBNull.Value),
                new nameValuePair("@GSTNumber", vendor.IsGSTRegistered? (object)vendor.GSTNumber?.ToUpper() : DBNull.Value),
                new nameValuePair("@PANNumber", vendor.PANNumber?.ToUpper()),
                new nameValuePair("@ContactPerson", vendor.ContactPerson),
                new nameValuePair("@Email", vendor.Email),
                new nameValuePair("@Mobile", vendor.Mobile),

                new nameValuePair("@RequestId", CommonObj.RequestId),
                new nameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetVendor]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public string Deactive(string VendorId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = "INACTIVE";

            nameValuePairs nvp = new nameValuePairs
            {
                new nameValuePair("@VendorId", VendorId),
                 
                new nameValuePair("@RequestId", CommonObj.RequestId),
                new nameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetVendor]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public List<Vendor> Get(bool? IsActive = null)
        {
            List<Vendor> vendors = new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetVendor]",
                new nameValuePairs
                {
                     
                     
                    new nameValuePair("@QueryType", "ALL")
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

            nameValuePairs nvp = new nameValuePairs
            {
                 
                 
                new nameValuePair("@VendorId", vendorBank.VendorId),
                new nameValuePair("@VendorBankId", vendorBank.VendorBankId),

                new nameValuePair("@AccountName", vendorBank.AccountName),
                new nameValuePair("@AccountNumber", vendorBank.AccountNumber?.ToUpper()),
                new nameValuePair("@BankName", vendorBank.BankName),
                new nameValuePair("@IFSC", vendorBank.IFSC?.ToUpper()),
                new nameValuePair("@MICR", vendorBank.MICR?.ToUpper()),
                new nameValuePair("@BranchName ", vendorBank.BranchName),
                new nameValuePair("@BranchAddress", vendorBank.BranchAddress),

                new nameValuePair("@RequestId", CommonObj.RequestId),
                new nameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetVendorBank]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public string BankDeactive(string VendorId, string VendorBankId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = "INACTIVE";

            nameValuePairs nvp = new nameValuePairs
            {
                new nameValuePair("@VendorId", VendorId),
                new nameValuePair("@VendorBankId", VendorBankId),
                 
                 
                new nameValuePair("@RequestId", CommonObj.RequestId),
                new nameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetVendorBank]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public List<VendorBank> BankGet(long VendorId, bool? IsActive = null)
        {
            List<VendorBank> vendorBanks = new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetVendorBank]",
                new nameValuePairs
                {
                     
                     
                    new nameValuePair("@VendorId", VendorId),
                    new nameValuePair("@QueryType", "ALL")
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
