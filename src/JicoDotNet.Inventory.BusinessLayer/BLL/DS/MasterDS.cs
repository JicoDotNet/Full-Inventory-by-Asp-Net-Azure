using DataAccess.Sql;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO.Core;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class MasterDS : ConnectionString
    {
        public MasterDS(ICommonRequestDto CommonObj) : base(CommonObj) { }

        public HomeMasterCount CountHome()
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString).GetFirstOrDefaultRow("[dbo].[spDsMaster]",
                new NameValuePairs
                {
                    new NameValuePair("@QueryType", "HOMECOUNT")
                }).FirstOrDefault<HomeMasterCount>();
        }

        public ReportMasterCount CountReport()
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString).GetFirstOrDefaultRow("[dbo].[spDsMaster]",
                new NameValuePairs
                {
                    new NameValuePair("@QueryType", "REPORTCOUNT")
                }).FirstOrDefault<ReportMasterCount>();
        }
    }
}
