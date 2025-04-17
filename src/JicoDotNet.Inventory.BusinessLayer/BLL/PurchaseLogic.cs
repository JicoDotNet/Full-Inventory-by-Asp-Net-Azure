using DataAccess.Sql;
using DataAccess.Sql.Entity;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class PurchaseLogic : DBManager
    {
        public PurchaseLogic(ICommonLogicHelper commonObj) : base(commonObj) { }

        public string TypeSet(PurchaseType purchaseType)
        {
            string qt = string.Empty;
            if (purchaseType.PurchaseTypeId > 0)
                qt = "UPDATE";
            else
                qt = "INSERT";

            INameValuePairs nvp = new NameValuePairs
            {
                new NameValuePair("@PurchaseTypeId", purchaseType.PurchaseTypeId),
                new NameValuePair("@PurchaseTypeName", purchaseType.PurchaseTypeName),
                new NameValuePair("@Description", purchaseType.Description),
                new NameValuePair("@RequestId", CommonLogicObj.RequestId),
                new NameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.DataManipulation("[dbo].[spSetPurchaseType]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public string TypeDeactive(string PurchaseTypeId)
        {
            return _sqlDBAccess.DataManipulation("[dbo].[spSetPurchaseType]", new NameValuePairs
                {
                    new NameValuePair("@PurchaseTypeId", PurchaseTypeId),
                    new NameValuePair("@RequestId", CommonLogicObj.RequestId),
                    new NameValuePair("@QueryType", "INACTIVE")
                }, "@OutParam").ToString();
        }

        public List<PurchaseType> TypeGet(bool? IsActive = null)
        {
            List<PurchaseType> purchaseTypes = _sqlDBAccess.GetData("[dbo].[spGetPurchaseType]",
                new NameValuePairs
                {
                    new NameValuePair("@QueryType", "ALL")
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
