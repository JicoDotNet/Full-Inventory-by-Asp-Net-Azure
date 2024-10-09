using DataAccess.Sql;
using JicoDotNet.Authentication.Interfaces;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.Core.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class CompanyManagment : ConnectionString
    {
        public CompanyManagment(ICommonRequestDto CommonObj) : base(CommonObj) { }
        public string BankSet(CompanyBank companyBank)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = string.Empty;
            if (companyBank.CompanyBankId > 0)
                qt = "UPDATE";
            else
                qt = "INSERT";

            NameValuePairs nvp = new NameValuePairs
            {
                new NameValuePair("@CompanyBankId", companyBank.CompanyBankId),

                new NameValuePair("@AccountName", companyBank.AccountName),
                new NameValuePair("@AccountNumber", companyBank.AccountNumber?.ToUpper()),
                new NameValuePair("@BankName", companyBank.BankName),
                new NameValuePair("@IFSC", companyBank.IFSC?.ToUpper()),
                new NameValuePair("@MICR", companyBank.MICR?.ToUpper()),
                new NameValuePair("@BranchName ", companyBank.BranchName),
                new NameValuePair("@BranchAddress", companyBank.BranchAddress),

                new NameValuePair("@RequestId", CommonObj.RequestId),
                new NameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.DataManipulation(GenericLogic.SqlSchema + ".[spSetCompanyBank]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }
        public string BankDeactive(long CompanyBankId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = "INACTIVE";

            NameValuePairs nvp = new NameValuePairs
            {
                new NameValuePair("@CompanyBankId", CompanyBankId),
                new NameValuePair("@RequestId", CommonObj.RequestId),
                new NameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.DataManipulation(GenericLogic.SqlSchema + ".[spSetCompanyBank]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }
        public List<CompanyBank> BankGet(bool? IsActive = null)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            List<CompanyBank> companyBanks = _sqlDBAccess.GetData(GenericLogic.SqlSchema + ".[spGetCompanyBank]",
                new NameValuePairs
                {
                    new NameValuePair("@QueryType", "ALL")
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
        public string BankPrintability(long CompanyBankId, bool IsPrintable)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            NameValuePairs nvp = new NameValuePairs
            {
                new NameValuePair("@CompanyBankId", CompanyBankId),

                new NameValuePair("@IsPrintable", IsPrintable),

                new NameValuePair("@RequestId", CommonObj.RequestId),
                new NameValuePair("@QueryType", "PRINTABILITY")
            };
            string v = _sqlDBAccess.DataManipulation(GenericLogic.SqlSchema + ".[spSetCompanyBank]", nvp, "@OutParam").ToString();
            return v;
        }
        public CompanyBank BankPrintable()
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            CompanyBank companyBank = _sqlDBAccess.GetFirstOrDefaultData(GenericLogic.SqlSchema + ".[spGetCompanyBank]",
                new NameValuePairs
                {
                    new NameValuePair("@QueryType", "PRINTABLE")
                }).FirstOrDefault<CompanyBank>();
            return companyBank;
        }
    }
}
