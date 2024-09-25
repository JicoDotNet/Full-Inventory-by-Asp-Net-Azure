using DataAccess.AzureStorage;
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
using System.Web;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class CompanyManagment : ConnectionString
    {
        public CompanyManagment(sCommonDto CommonObj) : base(CommonObj) { }
        public string BankSet(CompanyBank companyBank)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = string.Empty;
            if (companyBank.CompanyBankId > 0)
                qt = "UPDATE";
            else
                qt = "INSERT";

            nameValuePairs nvp = new nameValuePairs
            {
                new nameValuePair("@CompanyBankId", companyBank.CompanyBankId),

                new nameValuePair("@AccountName", companyBank.AccountName),
                new nameValuePair("@AccountNumber", companyBank.AccountNumber?.ToUpper()),
                new nameValuePair("@BankName", companyBank.BankName),
                new nameValuePair("@IFSC", companyBank.IFSC?.ToUpper()),
                new nameValuePair("@MICR", companyBank.MICR?.ToUpper()),
                new nameValuePair("@BranchName ", companyBank.BranchName),
                new nameValuePair("@BranchAddress", companyBank.BranchAddress),

                new nameValuePair("@RequestId", CommonObj.RequestId),
                new nameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetCompanyBank]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }
        public string BankDeactive(long CompanyBankId)
        { 
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = "INACTIVE";

            nameValuePairs nvp = new nameValuePairs
            {
                new nameValuePair("@CompanyBankId", CompanyBankId),
                new nameValuePair("@RequestId", CommonObj.RequestId),
                new nameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetCompanyBank]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }
        public List<CompanyBank> BankGet(bool? IsActive = null)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            List<CompanyBank> companyBanks = _sqlDBAccess.GetData("[dbo].[spGetCompanyBank]",
                new nameValuePairs
                {
                    new nameValuePair("@QueryType", "ALL")
                }).ToList<CompanyBank>();
            if (IsActive != null)
            {
                if (IsActive.Value)
                    return companyBanks.Where(a => a.IsActive).ToList();
                if (!IsActive.Value)
                    return companyBanks.Where(a => !a.IsActive).ToList();
            }
            return companyBanks;
        }
        public string BankPrintability(CompanyBank companyBank, bool IsPrintable)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            nameValuePairs nvp = new nameValuePairs
            {
                new nameValuePair("@CompanyBankId", companyBank.CompanyBankId),

                new nameValuePair("@IsPrintable", IsPrintable),

                new nameValuePair("@RequestId", CommonObj.RequestId),
                new nameValuePair("@QueryType", "PRINTABILITY")
            };
            string v = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetCompanyBank]", nvp, "@OutParam").ToString();
            return v;
        }
        public CompanyBank BankPrintable()
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            CompanyBank companyBank = _sqlDBAccess.GetFirstOrDefaultRow("[dbo].[spGetCompanyBank]",
                new nameValuePairs
                {
                    new nameValuePair("@QueryType", "PRINTABLE")
                }).FirstOrDefault<CompanyBank>();
            return companyBank;
        }
    }
}
