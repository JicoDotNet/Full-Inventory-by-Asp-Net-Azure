using DataAccess.Sql;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Custom;
using JicoDotNet.Inventory.Core.Custom.Interface;
using JicoDotNet.Authentication.Interfaces;
using JicoDotNet.Inventory.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class PaymentLogic : ConnectionString
    {
        public PaymentLogic(ICommonRequestDto commonObj) : base(commonObj) { }

        #region Payment Type
        public string TypeSet(PaymentType paymentType)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = string.Empty;
            if (paymentType.PaymentTypeId > 0)
                qt = "UPDATE";
            else
                qt = "INSERT";

            NameValuePairs nvp = new NameValuePairs
            {

                new NameValuePair("@PaymentTypeId", paymentType.PaymentTypeId),
                new NameValuePair("@PaymentTypeName", paymentType.PaymentTypeName),
                new NameValuePair("@Description", paymentType.Description),

                new NameValuePair("@RequestId", CommonObj.RequestId),
                new NameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.DataManipulation(GenericLogic.SqlSchema + ".[spSetPaymentType]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public string TypeDeactive(string paymentTypeId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = "INACTIVE";

            NameValuePairs nvp = new NameValuePairs
            {
                new NameValuePair("@PaymentTypeId", paymentTypeId),


                new NameValuePair("@RequestId", CommonObj.RequestId),
                new NameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.DataManipulation(GenericLogic.SqlSchema + ".[spSetPaymentType]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public List<PaymentType> TypeGet()
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString).GetData(GenericLogic.SqlSchema + ".[spGetPaymentType]",
                new NameValuePairs
                {


                    new NameValuePair("@QueryType", "ALL")
                }).ToList<PaymentType>();
        }
        #endregion

        #region Payment Out
        public string SetOut(PaymentOut paymentOut)
        {
            try
            {
                List<IPaymentOutDetailType> payOutDtlTypes = new List<IPaymentOutDetailType>();
                int count = 1;
                paymentOut.PaymentOutDetails.ForEach(e =>
                {
                    if (e.Amount > 0)
                    {
                        payOutDtlTypes.Add(new PaymentOutDetailType()
                        {
                            Id = count++,
                            BillId = e.BillId,
                            BillNumber = e.BillNumber,
                            Amount = e.Amount,
                            IsFullPaid = e.IsFullPaid,
                            PaymentDate = paymentOut.PaymentDate,
                            Description = e.Description
                        });
                    }
                });
                if (payOutDtlTypes.Count > 0)
                {
                    return new SqlDBAccess(CommonObj.SqlConnectionString)
                        .DataManipulation(GenericLogic.SqlSchema + ".[spSetPaymentOut]", new NameValuePairs
                        {


                        new NameValuePair("@VendorId", paymentOut.VendorId),
                        new NameValuePair("@VendorBankId", paymentOut.VendorBankId),

                        new NameValuePair("@IsTDSApplicable", paymentOut.IsTDSApplicable),
                        new NameValuePair("@TDSPercentage", paymentOut.IsTDSApplicable?
                                                            paymentOut.TDSPercentage : default(decimal?)),
                        new NameValuePair("@TDSAmount", paymentOut.IsTDSApplicable?
                                                            paymentOut.TotalAmount * paymentOut.TDSPercentage / 100 : default(decimal?)),
                        new NameValuePair("@PayAmount", paymentOut.IsTDSApplicable?
                                                            paymentOut.TotalAmount - (paymentOut.TotalAmount * paymentOut.TDSPercentage / 100)
                                                                    : paymentOut.TotalAmount),

                        new NameValuePair("@TotalAmount", paymentOut.TotalAmount),
                        new NameValuePair("@PaymentDate", paymentOut.PaymentDate),
                        new NameValuePair("@PaymentMode", paymentOut.PaymentMode),
                        new NameValuePair("@ReferenceNo", paymentOut.ReferenceNo),
                        new NameValuePair("@Remarks", paymentOut.Remarks),
                        new NameValuePair("@ChequeNo", paymentOut.ChequeNo),
                        new NameValuePair("@ChequeDate", paymentOut.ChequeDate),
                        new NameValuePair("@ChequeIFSC", paymentOut.ChequeIFSC),
                        new NameValuePair("@PaymentOutDetail", payOutDtlTypes.ToDataTable()),
                        new NameValuePair("@RequestId", CommonObj.RequestId),
                        new NameValuePair("@QueryType", "INSERT")
                        },
                        "@OutParam"
                    ).ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PaymentOutDetail> GetPaymentOutDetails(long vendorId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            NameValuePairs nvp = new NameValuePairs()
                {


                    new NameValuePair("@VendorId", vendorId),
                    new NameValuePair("@QueryType", "COMULTATIVE")
                };
            List<PaymentOutDetail> pntoutdtl = _sqlDBAccess.GetData(GenericLogic.SqlSchema + ".[spGetPaymentOut]", nvp).ToList<PaymentOutDetail>();
            return pntoutdtl;
        }

        public List<PaymentOut> GetPaymentOuts()
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString).GetData(GenericLogic.SqlSchema + ".[spGetPaymentOut]",
                new NameValuePairs
                {


                    new NameValuePair("@QueryType", "LIST")
                }).ToList<PaymentOut>();
        }
        #endregion

        #region Payment In
        public string SetIn(PaymentIn paymentIn)
        {
            List<IPaymentInDetailType> payInDtlTypes = new List<IPaymentInDetailType>();
            int count = 1;
            paymentIn.PaymentInDetails.ForEach(e =>
            {
                if (e.InvoiceId > 0)
                {
                    payInDtlTypes.Add(new PaymentInDetailType()
                    {
                        Id = count++,
                        InvoiceId = e.InvoiceId,
                        InvoiceNumber = e.InvoiceNumber,
                        Amount = e.Amount,
                        IsFullReceived = e.IsFullReceived,
                        PaymentDate = paymentIn.PaymentDate,
                        Description = e.Description
                    });
                }
            });
            if (payInDtlTypes.Count > 0)
            {
                return new SqlDBAccess(CommonObj.SqlConnectionString)
                    .DataManipulation(GenericLogic.SqlSchema + ".[spSetPaymentIn]", new NameValuePairs
                    {


                        new NameValuePair("@CustomerId", paymentIn.CustomerId),
                        new NameValuePair("@CompanyBankId", paymentIn.CompanyBankId),

                        new NameValuePair("@IsTDSApplicable", paymentIn.IsTDSApplicable),
                        new NameValuePair("@TDSPercentage", paymentIn.IsTDSApplicable?
                                                            paymentIn.TDSPercentage : default(decimal?)),
                        new NameValuePair("@TDSAmount", paymentIn.IsTDSApplicable?
                                                            paymentIn.TotalAmount * paymentIn.TDSPercentage / 100 : default(decimal?)),
                        new NameValuePair("@ReceiveAmount", paymentIn.IsTDSApplicable?
                                                            paymentIn.TotalAmount - (paymentIn.TotalAmount * paymentIn.TDSPercentage / 100)
                                                                    : paymentIn.TotalAmount),

                        new NameValuePair("@TotalAmount", paymentIn.TotalAmount),
                        new NameValuePair("@PaymentDate", paymentIn.PaymentDate),
                        new NameValuePair("@PaymentMode", paymentIn.PaymentMode),
                        new NameValuePair("@ReferenceNo", paymentIn.ReferenceNo),
                        new NameValuePair("@Remarks", paymentIn.Remarks),
                        new NameValuePair("@ChequeNo", paymentIn.ChequeNo),
                        new NameValuePair("@ChequeDate", paymentIn.ChequeDate),
                        new NameValuePair("@ChequeIFSC", paymentIn.ChequeIFSC),
                        new NameValuePair("@PaymentInDetail", payInDtlTypes.ToDataTable()),
                        new NameValuePair("@RequestId", CommonObj.RequestId),
                        new NameValuePair("@QueryType", "INSERT")
                    },
                    "@OutParam"
                ).ToString();
            }
            else
            {
                return "-1";
            }
        }

        public List<PaymentInDetail> GetPaymentInDetails(long customerId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            NameValuePairs nvp = new NameValuePairs()
                {


                    new NameValuePair("@CustomerId", customerId),
                    new NameValuePair("@QueryType", "COMULTATIVE")
                };
            List<PaymentInDetail> pntIndtl = _sqlDBAccess.GetData(GenericLogic.SqlSchema + ".[spGetPaymentIn]", nvp).ToList<PaymentInDetail>();
            return pntIndtl;
        }

        public List<PaymentIn> GetPaymentIns()
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString).GetData(GenericLogic.SqlSchema + ".[spGetPaymentIn]",
                new NameValuePairs
                {


                    new NameValuePair("@QueryType", "LIST")
                }).ToList<PaymentIn>();
        }
        #endregion
    }
}
