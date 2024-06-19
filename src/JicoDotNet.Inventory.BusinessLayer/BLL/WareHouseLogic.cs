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
    public class WareHouseLogic: ConnectionString
    {
        public WareHouseLogic(sCommonDto CommonObj) : base(CommonObj) { }

        public List<WareHouse> Get(bool? IsActive = null)
        {
            List<WareHouse> wareHouses = new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetWareHouse]",
                new NameValuePairs
                {
                     
                     
                    new NameValuePair("@QueryType", "ALL")
                }).ToList<WareHouse>();
            if (IsActive != null)
            {
                if (IsActive.Value)
                    return wareHouses.Where(a => a.IsActive).ToList();
                if (!IsActive.Value)
                    return wareHouses.Where(a => !a.IsActive).ToList();
            }
            return wareHouses;
        }

        public object Set(WareHouse wareHouse)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = string.Empty;
            if (wareHouse.WareHouseId > 0)
                qt = "UPDATE";
            else
                qt = "INSERT";

            NameValuePairs nvp = new NameValuePairs
            {
                new NameValuePair("@WareHouseId", wareHouse.WareHouseId),
                 
                 
                new NameValuePair("@BranchId", wareHouse.BranchId),
                new NameValuePair("@WareHouseName", wareHouse.WareHouseName),
                new NameValuePair("@IsRetailCounter", wareHouse.IsRetailCounter),
                new NameValuePair("@Description", wareHouse.Description),
                new NameValuePair("@RequestId", CommonObj.RequestId),
                new NameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetWareHouse]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public string Deactive(string wareHouseId)
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString)
                .InsertUpdateDeleteReturnObject("[dbo].[spSetWareHouse]", new NameValuePairs
                {
                    new NameValuePair("@WareHouseId", wareHouseId),
                     
                    new NameValuePair("@RequestId", CommonObj.RequestId),
                    new NameValuePair("@QueryType", "INACTIVE")
                }, "@OutParam").ToString();
        }
    }
}
