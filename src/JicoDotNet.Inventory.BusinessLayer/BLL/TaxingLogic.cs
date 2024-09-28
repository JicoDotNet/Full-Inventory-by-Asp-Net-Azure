using DataAccess.Sql;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Models;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class TaxingLogic : ConnectionString
    {
        public TaxingLogic(ICommonRequestDto CommonObj) : base(CommonObj) { }

        public List<TDSPay> GetTDSOuts()
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString).GetData(GenericLogic.SqlSchema + ".[spGetTDSPay]",
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
                    new NameValuePair("@RequestId", CommonObj.RequestId),
                    new NameValuePair("@QueryType", "PAY")
                };
                _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
                return _sqlDBAccess.DataManipulation(GenericLogic.SqlSchema + ".[spSetTDSPay]", nvp, "@OutParam").ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TDSReceive> GetTDSIns()
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString).GetData(GenericLogic.SqlSchema + ".[spGetTDSReceive]",
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
                    new NameValuePair("@RequestId", CommonObj.RequestId),
                    new NameValuePair("@QueryType", "RECEIVE")
                };
                _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
                return _sqlDBAccess.DataManipulation(GenericLogic.SqlSchema + ".[spSetTDSReceive]", nvp, "@OutParam").ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
