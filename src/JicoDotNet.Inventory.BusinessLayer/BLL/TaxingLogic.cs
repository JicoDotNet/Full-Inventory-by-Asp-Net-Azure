using DataAccess.Sql;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.BusinessLayer.DTO.SP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class TaxingLogic : ConnectionString
    {
        public TaxingLogic(sCommonDto CommonObj) : base(CommonObj) { }

        public List<TDSPay> GetTDSOuts()
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetTDSPay]",
                new nameValuePairs
                {
                     
                     
                    new nameValuePair("@QueryType", "UNPAID")
                }).ToList<TDSPay>();
        }

        public string SetTDSOut(TDSPay tDSPay)
        {
            try
            {
                nameValuePairs nvp = new nameValuePairs
                {
                     
                     
                    new nameValuePair("@PayDate", tDSPay.PayDate),
                    new nameValuePair("@TDSPayId", tDSPay.TDSPayId),
                    new nameValuePair("@RequestId", CommonObj.RequestId),
                    new nameValuePair("@QueryType", "PAY")
                };
                _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
                return _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetTDSPay]", nvp, "@OutParam").ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TDSReceive> GetTDSIns()
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetTDSReceive]",
                new nameValuePairs
                {
                     
                     
                    new nameValuePair("@QueryType", "UNRECEIVED")
                }).ToList<TDSReceive>();
        }

        public string SetTDSIn(TDSReceive tDSReceive)
        {
            try
            {
                nameValuePairs nvp = new nameValuePairs
                {
                     
                     
                    new nameValuePair("@ReceivedDate", tDSReceive.ReceivedDate),
                    new nameValuePair("@TDSReceiveId", tDSReceive.TDSReceiveId),
                    new nameValuePair("@RequestId", CommonObj.RequestId),
                    new nameValuePair("@QueryType", "RECEIVE")
                };
                _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
                return _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetTDSReceive]", nvp, "@OutParam").ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
