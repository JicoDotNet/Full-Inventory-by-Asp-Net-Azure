using DataAccess.Sql;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class.Custom;
using JicoDotNet.Inventory.BusinessLayer.DTO.SP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class PaymentLogic : ConnectionString
    {
        public PaymentLogic(sCommonDto CommonObj) : base(CommonObj) { }

        #region Payment Type
        public string TypeSet(PaymentType paymentType)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = string.Empty;
            if (paymentType.PaymentTypeId > 0)
                qt = "UPDATE";
            else
                qt = "INSERT";

            nameValuePairs nvp = new nameValuePairs
            {
                 
                new nameValuePair("@PaymentTypeId", paymentType.PaymentTypeId),
                new nameValuePair("@PaymentTypeName", paymentType.PaymentTypeName),
                new nameValuePair("@Description", paymentType.Description),
                 
                new nameValuePair("@RequestId", CommonObj.RequestId),
                new nameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetPaymentType]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public string TypeDeactive(string paymentTypeId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = "INACTIVE";

            nameValuePairs nvp = new nameValuePairs
            {
                new nameValuePair("@PaymentTypeId", paymentTypeId),
                 
                 
                new nameValuePair("@RequestId", CommonObj.RequestId),
                new nameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetPaymentType]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public List<PaymentType> TypeGet()
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetPaymentType]",
                new nameValuePairs
                {
                     
                     
                    new nameValuePair("@QueryType", "ALL")
                }).ToList<PaymentType>();
        }
        #endregion

        #region Payment Out
        public string SetOut(PaymentOut paymentOut)
        {
            try
            {
                List<PaymentOutDetailType> payOutDtlTyps = new List<PaymentOutDetailType>();
                int count = 1;
                paymentOut.PaymentOutDetails.ForEach(e =>
                {
                    if (e.Amount > 0)
                    {
                        payOutDtlTyps.Add(new PaymentOutDetailType()
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
                if (payOutDtlTyps.Count > 0)
                {
                    return new SqlDBAccess(CommonObj.SqlConnectionString)
                        .InsertUpdateDeleteReturnObject("[dbo].[spSetPaymentOut]", new nameValuePairs
                        {
                         
                         
                        new nameValuePair("@VendorId", paymentOut.VendorId),
                        new nameValuePair("@VendorBankId", paymentOut.VendorBankId),

                        new nameValuePair("@IsTDSApplicable", paymentOut.IsTDSApplicable),
                        new nameValuePair("@TDSPercentage", paymentOut.IsTDSApplicable?
                                                            paymentOut.TDSPercentage : default(decimal?)),
                        new nameValuePair("@TDSAmount", paymentOut.IsTDSApplicable?
                                                            paymentOut.TotalAmount * paymentOut.TDSPercentage / 100 : default(decimal?)),
                        new nameValuePair("@PayAmount", paymentOut.IsTDSApplicable?
                                                            paymentOut.TotalAmount - (paymentOut.TotalAmount * paymentOut.TDSPercentage / 100)
                                                                    : paymentOut.TotalAmount),

                        new nameValuePair("@TotalAmount", paymentOut.TotalAmount),
                        new nameValuePair("@PaymentDate", paymentOut.PaymentDate),
                        new nameValuePair("@PaymentMode", paymentOut.PaymentMode),
                        new nameValuePair("@ReferenceNo", paymentOut.ReferenceNo),
                        new nameValuePair("@Remarks", paymentOut.Remarks),
                        new nameValuePair("@ChequeNo", paymentOut.ChequeNo),
                        new nameValuePair("@ChequeDate", paymentOut.ChequeDate),
                        new nameValuePair("@ChequeIFSC", paymentOut.ChequeIFSC),
                        new nameValuePair("@PaymentOutDetail", payOutDtlTyps.ToDataTable()),
                        new nameValuePair("@RequestId", CommonObj.RequestId),
                        new nameValuePair("@QueryType", "INSERT")
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
            nameValuePairs nvp = new nameValuePairs()
                {
                     
                     
                    new nameValuePair("@VendorId", vendorId),
                    new nameValuePair("@QueryType", "COMULTATIVE")
                };
            List<PaymentOutDetail> pntoutdtl = _sqlDBAccess.GetData("[dbo].[spGetPaymentOut]", nvp).ToList<PaymentOutDetail>();
            return pntoutdtl;
        }

        public List<PaymentOut> GetPaymentOuts()
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetPaymentOut]",
                new nameValuePairs
                {
                     
                     
                    new nameValuePair("@QueryType", "LIST")
                }).ToList<PaymentOut>();
        }
        #endregion

        #region Payment In
        public string SetIn(PaymentIn paymentIn)
        {
            List<PaymentInDetailType> payInDtlTyps = new List<PaymentInDetailType>();
            int count = 1;
            paymentIn.PaymentInDetails.ForEach(e =>
            {
                if (e.InvoiceId > 0)
                {
                    payInDtlTyps.Add(new PaymentInDetailType()
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
            if (payInDtlTyps.Count > 0)
            {
                return new SqlDBAccess(CommonObj.SqlConnectionString)
                    .InsertUpdateDeleteReturnObject("[dbo].[spSetPaymentIn]", new nameValuePairs
                    {
                         
                         
                        new nameValuePair("@CustomerId", paymentIn.CustomerId),
                        new nameValuePair("@CompanyBankId", paymentIn.CompanyBankId),

                        new nameValuePair("@IsTDSApplicable", paymentIn.IsTDSApplicable),
                        new nameValuePair("@TDSPercentage", paymentIn.IsTDSApplicable?
                                                            paymentIn.TDSPercentage : default(decimal?)),
                        new nameValuePair("@TDSAmount", paymentIn.IsTDSApplicable?
                                                            paymentIn.TotalAmount * paymentIn.TDSPercentage / 100 : default(decimal?)),
                        new nameValuePair("@ReceiveAmount", paymentIn.IsTDSApplicable?
                                                            paymentIn.TotalAmount - (paymentIn.TotalAmount * paymentIn.TDSPercentage / 100)
                                                                    : paymentIn.TotalAmount),

                        new nameValuePair("@TotalAmount", paymentIn.TotalAmount),
                        new nameValuePair("@PaymentDate", paymentIn.PaymentDate),
                        new nameValuePair("@PaymentMode", paymentIn.PaymentMode),
                        new nameValuePair("@ReferenceNo", paymentIn.ReferenceNo),
                        new nameValuePair("@Remarks", paymentIn.Remarks),
                        new nameValuePair("@ChequeNo", paymentIn.ChequeNo),
                        new nameValuePair("@ChequeDate", paymentIn.ChequeDate),
                        new nameValuePair("@ChequeIFSC", paymentIn.ChequeIFSC),
                        new nameValuePair("@PaymentInDetail", payInDtlTyps.ToDataTable()),
                        new nameValuePair("@RequestId", CommonObj.RequestId),
                        new nameValuePair("@QueryType", "INSERT")
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
            nameValuePairs nvp = new nameValuePairs()
                {
                     
                     
                    new nameValuePair("@CustomerId", customerId),
                    new nameValuePair("@QueryType", "COMULTATIVE")
                };
            List<PaymentInDetail> pntIndtl = _sqlDBAccess.GetData("[dbo].[spGetPaymentIn]", nvp).ToList<PaymentInDetail>();
            return pntIndtl;
        }

        public List<PaymentIn> GetPaymentIns()
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetPaymentIn]",
                new nameValuePairs
                {
                     
                     
                    new nameValuePair("@QueryType", "LIST")
                }).ToList<PaymentIn>();
        }
        #endregion
    }
}
