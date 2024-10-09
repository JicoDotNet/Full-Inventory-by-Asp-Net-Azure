using DataAccess.Sql;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Report;
using JicoDotNet.Inventory.Core.Report.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace JicoDotNet.Inventory.BusinessLayer.BLL.Report
{
    public class TaxReportLogic : ConnectionString
    {
        public TaxReportLogic(ICommonLogicHelper commonObj) : base(commonObj) { }

        public List<ResponseGSTOutputResult> GSTOutputs(IRequestTaxParam tax)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            NameValuePairs nvp = new NameValuePairs
            {


                new NameValuePair("@StartDate", tax.SearchDate.StartDate),
                new NameValuePair("@EndDate", tax.SearchDate.EndDate),
                new NameValuePair("@PaymentStatus", tax.PaymentStatus),
                new NameValuePair("@QueryType", "OUTPUTGST")
            };
            return _sqlDBAccess.GetData(GenericLogic.SqlSchema + ".[spRpGST]", nvp).ToList<ResponseGSTOutputResult>();
        }

        public List<ResponseGSTInputResult> GSTInputs(IRequestTaxParam tax)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            NameValuePairs nvp = new NameValuePairs
            {


                new NameValuePair("@StartDate", tax.SearchDate.StartDate),
                new NameValuePair("@EndDate", tax.SearchDate.EndDate),
                new NameValuePair("@PaymentStatus", tax.PaymentStatus),
                new NameValuePair("@QueryType", "INPUTGST")
            };
            return _sqlDBAccess.GetData(GenericLogic.SqlSchema + ".[spRpGST]", nvp).ToList<ResponseGSTInputResult>();
        }
    }
}
