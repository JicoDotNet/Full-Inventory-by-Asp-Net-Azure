using DataAccess.Sql;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class.Report;
using JicoDotNet.Inventory.BusinessLayer.DTO.Core;
using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.BLL.Report
{
    public class TaxReportLogic : ConnectionString
    {
        public TaxReportLogic(ICommonRequestDto CommonObj) : base(CommonObj) { }

        public List<RGSTOutput> GSTOutputs(PTax tax)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            NameValuePairs nvp = new NameValuePairs
            {
                 
                 
                new NameValuePair("@StartDate", tax.SearchDate.StartDate),
                new NameValuePair("@EndDate", tax.SearchDate.EndDate),
                new NameValuePair("@PaymentStatus", tax.PaymentStatus),
                new NameValuePair("@QueryType", "OUTPUTGST")
            };
            return _sqlDBAccess.GetData("[dbo].[spRpGST]", nvp).ToList<RGSTOutput>();
        }

        public List<RGSTInput> GSTInputs(PTax tax)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            NameValuePairs nvp = new NameValuePairs
            {
                 
                 
                new NameValuePair("@StartDate", tax.SearchDate.StartDate),
                new NameValuePair("@EndDate", tax.SearchDate.EndDate),
                new NameValuePair("@PaymentStatus", tax.PaymentStatus),
                new NameValuePair("@QueryType", "INPUTGST")
            };
            return _sqlDBAccess.GetData("[dbo].[spRpGST]", nvp).ToList<RGSTInput>();
        }
    }
}
