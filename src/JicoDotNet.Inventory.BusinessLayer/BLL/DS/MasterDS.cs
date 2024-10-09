using DataAccess.Sql;
using JicoDotNet.Authentication.Interfaces;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Authentication.Interfaces;
using JicoDotNet.Inventory.Core.Models.DS;
using System;
using System.Data;
using System.Linq;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class MasterDS : ConnectionString
    {
        public MasterDS(ICommonRequestDto CommonObj) : base(CommonObj) { }

        public HomeMasterCount CountHome()
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString).GetFirstOrDefaultData(GenericLogic.SqlSchema + ".[spDsMaster]",
                new NameValuePairs
                {
                    new NameValuePair("@QueryType", "HOMECOUNT")
                }).FirstOrDefault<HomeMasterCount>();
        }

        public ReportMasterCount CountReport()
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString).GetFirstOrDefaultData(GenericLogic.SqlSchema + ".[spDsMaster]",
                new NameValuePairs
                {
                    new NameValuePair("@QueryType", "REPORTCOUNT")
                }).FirstOrDefault<ReportMasterCount>();
        }
    }
}
