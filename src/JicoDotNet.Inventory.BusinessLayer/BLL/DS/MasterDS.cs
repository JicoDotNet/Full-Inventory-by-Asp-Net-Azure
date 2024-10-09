using DataAccess.Sql;
using JicoDotNet.Validator.Interfaces;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Validator.Interfaces;
using JicoDotNet.Inventory.Core.Models.DS;
using System;
using System.Data;
using System.Linq;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class MasterDS : DBManager
    {
        public MasterDS(ICommonLogicHelper CommonObj) : base(CommonObj) { }

        public HomeMasterCount CountHome()
        {
            return new SqlDBAccess(CommonLogicObj.SqlConnectionString).GetFirstOrDefaultData(CommonLogicObj.SqlSchema + ".[spDsMaster]",
                new NameValuePairs
                {
                    new NameValuePair("@QueryType", "HOMECOUNT")
                }).FirstOrDefault<HomeMasterCount>();
        }

        public ReportMasterCount CountReport()
        {
            return new SqlDBAccess(CommonLogicObj.SqlConnectionString).GetFirstOrDefaultData(CommonLogicObj.SqlSchema + ".[spDsMaster]",
                new NameValuePairs
                {
                    new NameValuePair("@QueryType", "REPORTCOUNT")
                }).FirstOrDefault<ReportMasterCount>();
        }
    }
}
