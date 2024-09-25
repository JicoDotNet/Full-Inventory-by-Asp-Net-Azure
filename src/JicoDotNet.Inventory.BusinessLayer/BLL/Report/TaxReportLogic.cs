using DataAccess.Sql;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class.Report;
using JicoDotNet.Inventory.BusinessLayer.DTO.SP;
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
        public TaxReportLogic(sCommonDto CommonObj) : base(CommonObj) { }

        public List<RGSTOutput> GSTOutputs(PTax tax)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            nameValuePairs nvp = new nameValuePairs
            {
                 
                 
                new nameValuePair("@StartDate", tax.SearchDate.StartDate),
                new nameValuePair("@EndDate", tax.SearchDate.EndDate),
                new nameValuePair("@PaymentStatus", tax.PaymentStatus),
                new nameValuePair("@QueryType", "OUTPUTGST")
            };
            return _sqlDBAccess.GetData("[dbo].[spRpGST]", nvp).ToList<RGSTOutput>();
        }

        public List<RGSTInput> GSTInputs(PTax tax)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            nameValuePairs nvp = new nameValuePairs
            {
                 
                 
                new nameValuePair("@StartDate", tax.SearchDate.StartDate),
                new nameValuePair("@EndDate", tax.SearchDate.EndDate),
                new nameValuePair("@PaymentStatus", tax.PaymentStatus),
                new nameValuePair("@QueryType", "INPUTGST")
            };
            return _sqlDBAccess.GetData("[dbo].[spRpGST]", nvp).ToList<RGSTInput>();
        }
    }
}
