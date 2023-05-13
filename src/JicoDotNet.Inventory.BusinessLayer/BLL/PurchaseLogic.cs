using DataAccess.Sql;
using SelfRnd.MiddleLayer.Common;
using SelfRnd.MiddleLayer.DTO.Class;
using SelfRnd.MiddleLayer.DTO.SP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfRnd.MiddleLayer.BLL
{
    public class PurchaseLogic : ConnectionString
    {
        public PurchaseLogic(sCommonDto CommonObj) : base(CommonObj) { }

        public string TypeSet(PurchaseType purchaseType)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString, CommandType.StoredProcedure);
            string qt = string.Empty;
            if (purchaseType.PurchaseTypeId > 0)
                qt = "UPDATE";
            else
                qt = "INSERT";

            nameValuePairs nvp = new nameValuePairs
            {
                new nameValuePair("@TenantId", CommonObj.TenantId),
                new nameValuePair("@CompanyId", CommonObj.CompanyId),
                new nameValuePair("@PurchaseTypeId", purchaseType.PurchaseTypeId),
                new nameValuePair("@PurchaseTypeName", purchaseType.PurchaseTypeName),
                new nameValuePair("@Description", purchaseType.Description),
                new nameValuePair("@RequestId", CommonObj.RequestId),
                new nameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetPurchaseType]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public string TypeDeactive(string PurchaseTypeId)
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString, CommandType.StoredProcedure)
                .InsertUpdateDeleteReturnObject("[dbo].[spSetPurchaseType]", new nameValuePairs
                {
                    new nameValuePair("@PurchaseTypeId", PurchaseTypeId),
                    new nameValuePair("@TenantId", CommonObj.TenantId),
                    new nameValuePair("@CompanyId", CommonObj.CompanyId),
                    new nameValuePair("@RequestId", CommonObj.RequestId),
                    new nameValuePair("@QueryType", "INACTIVE")
                }, "@OutParam").ToString();
        }

        public List<PurchaseType> TypeGet(bool? IsActive = null)
        {
            List<PurchaseType> purchaseTypes = new SqlDBAccess(CommonObj.SqlConnectionString, CommandType.StoredProcedure).GetData("[dbo].[spGetPurchaseType]",
                new nameValuePairs
                {
                    new nameValuePair("@TenantId", CommonObj.TenantId),
                    new nameValuePair("@CompanyId", CommonObj.CompanyId),
                    new nameValuePair("@QueryType", "ALL")
                }).ToList<PurchaseType>();
            if (IsActive != null)
            {
                if (IsActive.Value)
                    return purchaseTypes.Where(a => a.IsActive).ToList();
                if (!IsActive.Value)
                    return purchaseTypes.Where(a => !a.IsActive).ToList();
            }
            return purchaseTypes;
        }
    }
}
