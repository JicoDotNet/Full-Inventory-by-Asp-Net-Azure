﻿using DataAccess.Sql;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class TaxingLogic : DBManager
    {
        public TaxingLogic(ICommonLogicHelper CommonObj) : base(CommonObj) { }

        public List<TDSPay> GetTDSOuts()
        {
            return new SqlDBAccess(CommonLogicObj.SqlConnectionString).GetData(CommonLogicObj.SqlSchema + ".[spGetTDSPay]",
                new NameValuePairs
                {
                    new NameValuePair("@QueryType", "UNPAID")
                }).ToList<TDSPay>();
        }

        public string SetTDSOut(TDSPay tDSPay)
        {
            try
            {
                NameValuePairs nvp = new NameValuePairs
                {
                    new NameValuePair("@PayDate", tDSPay.PayDate),
                    new NameValuePair("@TDSPayId", tDSPay.TDSPayId),
                    new NameValuePair("@RequestId", CommonLogicObj.RequestId),
                    new NameValuePair("@QueryType", "PAY")
                };
                
                return _sqlDBAccess.DataManipulation(CommonLogicObj.SqlSchema + ".[spSetTDSPay]", nvp, "@OutParam").ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TDSReceive> GetTDSIns()
        {
            return new SqlDBAccess(CommonLogicObj.SqlConnectionString).GetData(CommonLogicObj.SqlSchema + ".[spGetTDSReceive]",
                new NameValuePairs
                {
                    new NameValuePair("@QueryType", "UNRECEIVED")
                }).ToList<TDSReceive>();
        }

        public string SetTDSIn(TDSReceive tDSReceive)
        {
            try
            {
                NameValuePairs nvp = new NameValuePairs
                {
                    new NameValuePair("@ReceivedDate", tDSReceive.ReceivedDate),
                    new NameValuePair("@TDSReceiveId", tDSReceive.TDSReceiveId),
                    new NameValuePair("@RequestId", CommonLogicObj.RequestId),
                    new NameValuePair("@QueryType", "RECEIVE")
                };
                
                return _sqlDBAccess.DataManipulation(CommonLogicObj.SqlSchema + ".[spSetTDSReceive]", nvp, "@OutParam").ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
