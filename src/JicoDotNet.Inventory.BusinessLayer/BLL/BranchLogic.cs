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
    public class BranchLogic : ConnectionString
    {
        public BranchLogic(sCommonDto CommonObj) : base(CommonObj) { }

        public string Set(Branch branch)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = string.Empty;
            if (branch.BranchId > 0)
                qt = "UPDATE";
            else
                qt = "INSERT";

            nameValuePairs nvp = new nameValuePairs
            {
                 
                new nameValuePair("@BranchId", branch.BranchId),
                 
                new nameValuePair("@BranchName", branch.BranchName),
                new nameValuePair("@BranchCode", branch.BranchCode),
                new nameValuePair("@Address", branch.Address),
                new nameValuePair("@City", branch.City),
                new nameValuePair("@State", branch.State),
                new nameValuePair("@PostalCode", branch.PostalCode),
                new nameValuePair("@ContactPerson", branch.ContactPerson),
                new nameValuePair("@Email", branch.Email),
                new nameValuePair("@Phone", branch.Phone),
                new nameValuePair("@Description", branch.Description),
                new nameValuePair("@RequestId", CommonObj.RequestId),
                new nameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetBranch]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public string Deactive(string BranchId)
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString)
                .InsertUpdateDeleteReturnObject("[dbo].[spSetBranch]", new nameValuePairs
                {
                    new nameValuePair("@BranchId", BranchId),                     
                    new nameValuePair("@RequestId", CommonObj.RequestId),
                    new nameValuePair("@QueryType", "INACTIVE")
                }, "@OutParam").ToString();
        }

        public List<Branch> Get(bool? IsActive = null)
        {
            List<Branch> branchs = new SqlDBAccess(CommonObj.SqlConnectionString)
                .GetData("[dbo].[spGetBranch]",
                new nameValuePairs
                {
                     
                     
                    new nameValuePair("@QueryType", "ALL")
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
