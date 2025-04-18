using DataAccess.Sql;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Custom;
using JicoDotNet.Inventory.Core.Custom.Interface;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;


namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class QuotationLogic : DBManager
    {
        public QuotationLogic(ICommonLogicHelper CommonObj) : base(CommonObj) { }

        public string SetForEntry(Quotation quotation)
        {
            try
            {
                string ReturnDS = string.Empty;
                List<IQuotationDetailType> quotationDetailTypes = new List<IQuotationDetailType>();
                int count = 1;
                quotation.QuotationDetails.ForEach(item =>
                {
                    if (item.Quantity > 0)
                    {
                        quotationDetailTypes.Add(new QuotationDetailType()
                        {
                            Id = count++,
                            Amount = item.Amount,
                            Description = item.Description,
                            DiscountAmount = item.DiscountAmount,
                            DiscountPercentage = item.DiscountPercentage,
                            HSNSAC = item.HSNSAC,
                            Price = item.Price,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            SubTotal = item.SubTotal,
                            TaxAmount = item.TaxAmount,
                            TaxPercentage = item.TaxPercentage,
                            Total = item.Total
                        });
                    }
                });
                if (quotationDetailTypes.Count > 0)
                {
                    
                    NameValuePairs nvp = new NameValuePairs
                    {
                        new NameValuePair("@ComapnyIsGSTRegistered", GenericLogic.IsValidGSTNumber(CommonLogicObj.AppSettings.GSTNumber)),

                        new NameValuePair("@CustomerId", quotation.CustomerId),
                        new NameValuePair("@QuotationDate", quotation.QuotationDate),
                        new NameValuePair("@QuotationNumber", "QO-"),
                        new NameValuePair("@QuotationAmount", quotation.QuotationAmount),
                        new NameValuePair("@QuotationTaxAmount", quotation.QuotationTaxAmount),
                        new NameValuePair("@QuotationTotalAmount", quotation.QuotationTotalAmount),
                        new NameValuePair("@TandC", quotation.TandC),
                        new NameValuePair("@Remarks", quotation.Remarks),
                        new NameValuePair("@QuotationDetails", quotationDetailTypes.ToDataTable()),
                        new NameValuePair("@RequestId", CommonLogicObj.RequestId),
                        new NameValuePair("@QueryType", "ENTRY")
                    };
                    ReturnDS = _sqlDBAccess.DataManipulation(CommonLogicObj.SqlSchema + ".[spSetQuotation]",
                        nvp, "@OutParam").ToString();
                }
                return ReturnDS;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Quotation> GetQuotations()
        {
            
            NameValuePairs nvp = new NameValuePairs()
                {
                    new NameValuePair("@QueryType", "LIST")
                };
            return _sqlDBAccess.GetData(CommonLogicObj.SqlSchema + ".[spGetQuotation]", nvp).ToList<Quotation>();
        }

        public Quotation GetForDetail(long QuotationId)
        {
            
            NameValuePairs nvp = new NameValuePairs()
            {
                new NameValuePair("@QuotationId", QuotationId),
                new NameValuePair("@QueryType", "DETAIL")
            };
            DataSet ds = _sqlDBAccess.GetDataSet(CommonLogicObj.SqlSchema + ".[spGetQuotation]", nvp);
            Quotation quotation;
            quotation = ds.Tables[0].FirstOrDefault<Quotation>();
            if (quotation != null)
            {
                quotation.QuotationDetails = new List<QuotationDetail>();
                quotation.QuotationDetails = ds.Tables[1].Rows.Count > 0 ? ds.Tables[1].ToList<QuotationDetail>() : null;
                if (quotation.QuotationDetails == null)
                    quotation = null;
            }
            return quotation;
        }

        /// <summary>
        /// Delete or Inactive Quotation, only possible if no SO Generated
        /// </summary>
        public string Deactive(long QuotationId)
        {
            try
            {                
                NameValuePairs nvp = new NameValuePairs
                {
                    new NameValuePair("@QuotationId", QuotationId),
                    new NameValuePair("@RequestId", CommonLogicObj.RequestId),
                    new NameValuePair("@QueryType", "DELETE")
                };
                string ReturnDS = _sqlDBAccess.DataManipulation(CommonLogicObj.SqlSchema + ".[spSetQuotation]", nvp, "@OutParam").ToString();
                return ReturnDS;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}