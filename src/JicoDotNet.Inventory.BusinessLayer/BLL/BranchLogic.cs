using DataAccess.Sql;
using JicoDotNet.Authentication.Interfaces;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Authentication.Interfaces;
using JicoDotNet.Inventory.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class BranchLogic : ConnectionString
    {
        public BranchLogic(ICommonRequestDto CommonObj) : base(CommonObj) { }

        public string Set(Branch branch)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = string.Empty;
            if (branch.BranchId > 0)
                qt = "UPDATE";
            else
                qt = "INSERT";

            NameValuePairs nvp = new NameValuePairs
            {

                new NameValuePair("@BranchId", branch.BranchId),

                new NameValuePair("@BranchName", branch.BranchName),
                new NameValuePair("@BranchCode", branch.BranchCode),
                new NameValuePair("@Address", branch.Address),
                new NameValuePair("@City", branch.City),
                new NameValuePair("@State", branch.State),
                new NameValuePair("@PostalCode", branch.PostalCode),
                new NameValuePair("@ContactPerson", branch.ContactPerson),
                new NameValuePair("@Email", branch.Email),
                new NameValuePair("@Phone", branch.Phone),
                new NameValuePair("@Description", branch.Description),
                new NameValuePair("@RequestId", CommonObj.RequestId),
                new NameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.DataManipulation(GenericLogic.SqlSchema + ".[spSetBranch]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public string Deactive(string BranchId)
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString)
                .DataManipulation(GenericLogic.SqlSchema + ".[spSetBranch]", new NameValuePairs
                {
                    new NameValuePair("@BranchId", BranchId),
                    new NameValuePair("@RequestId", CommonObj.RequestId),
                    new NameValuePair("@QueryType", "INACTIVE")
                }, "@OutParam").ToString();
        }

        public List<Branch> Get(bool? IsActive = null)
        {
            List<Branch> branchs = new SqlDBAccess(CommonObj.SqlConnectionString)
                .GetData(GenericLogic.SqlSchema + ".[spGetBranch]",
                new NameValuePairs
                {


                    new NameValuePair("@QueryType", "ALL")
                }).ToList<Branch>();
            if (IsActive != null)
            {
                if (IsActive.Value)
                    return branchs.Where(a => a.IsActive).ToList();
                if (!IsActive.Value)
                    return branchs.Where(a => !a.IsActive).ToList();
            }
            return branchs;
        }
    }
}
