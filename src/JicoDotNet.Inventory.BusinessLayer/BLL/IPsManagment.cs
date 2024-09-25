using DataAccess.AzureStorage;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.BusinessLayer.DTO.SP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    //public class IPsManagment : ConnectionString
    //{
    //    public IPsManagment(sCommonDto CommonObj) : base(CommonObj) { }

    //    public void SetIP(AllowIPs iPs)
    //    {
    //        _tableManager = new ExecuteTableManager("AllowedIPs", CommonObj.NoSqlConnectionString);
    //        iPs.PartitionKey = "MyCompany";
    //        iPs.RowKey = Convert.ToInt64(iPs.StartIP.Replace(".", "")).ToString("X")
    //            + Convert.ToInt64(iPs.EndIP.Replace(".", "")).ToString("X")
    //            + iPs.UserID.ToString("X")
    //            + GenericLogic.IstNow.TimeStamp().ToString("X");            
    //        iPs.RequestId = CommonObj.RequestId;

    //        _tableManager.InsertEntity(iPs);
    //    }

    //    public List<AllowIPs> GetIPs()
    //    {
    //        _tableManager = new ExecuteTableManager("AllowedIPs", CommonObj.NoSqlConnectionString);
    //        string Qry = "IsActive eq true";

    //        return _tableManager.RetrieveEntity<AllowIPs>(Qry);
    //    }

    //    public void DeleteIP(AllowIPs iPs)
    //    {
    //        _tableManager = new ExecuteTableManager("AllowedIPs", CommonObj.NoSqlConnectionString);
    //        string Qry = "RowKey eq '" + iPs.RowKey + "' and IsActive eq true";
    //        List<AllowIPs> ps = _tableManager.RetrieveEntity<AllowIPs>(Qry);
    //        foreach(AllowIPs allow in ps)
    //        {
    //            allow.IsActive = false;
    //            _tableManager.InsertEntity(allow, false);
    //        }
    //    }
    //}
}
