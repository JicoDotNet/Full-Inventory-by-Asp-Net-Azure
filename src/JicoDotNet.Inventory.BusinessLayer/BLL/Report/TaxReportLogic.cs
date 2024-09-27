﻿using DataAccess.Sql;
using JicoDotNet.Inventory.BusinessLayer.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Report;
using JicoDotNet.Inventory.Core.Report.Interface;

namespace JicoDotNet.Inventory.BusinessLayer.BLL.Report
{
    public class TaxReportLogic : ConnectionString
    {
        public TaxReportLogic(ICommonRequestDto commonObj) : base(commonObj) { }

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
            return _sqlDBAccess.GetData("[dbo].[spRpGST]", nvp).ToList<ResponseGSTOutputResult>();
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
            return _sqlDBAccess.GetData("[dbo].[spRpGST]", nvp).ToList<ResponseGSTInputResult>();
        }
    }
}
