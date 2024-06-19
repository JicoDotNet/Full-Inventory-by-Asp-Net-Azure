using DataAccess.AzureStorage;
using DataAccess.Sql;
using Newtonsoft.Json;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class.Custom;
using JicoDotNet.Inventory.BusinessLayer.DTO.SP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class InvoiceLogic : ConnectionString
    {
        public InvoiceLogic(sCommonDto CommonObj) : base(CommonObj) { }

        #region Payment Type
        public string TypeSet(InvoiceType invoiceType)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = string.Empty;
            if (invoiceType.InvoiceTypeId > 0)
                qt = "UPDATE";
            else
                qt = "INSERT";

            NameValuePairs nvp = new NameValuePairs
            {
                 
                new NameValuePair("@InvoiceTypeId", invoiceType.InvoiceTypeId),
                new NameValuePair("@InvoiceTypeName", invoiceType.InvoiceTypeName),
                new NameValuePair("@Description", invoiceType.Description),
                 
                new NameValuePair("@RequestId", CommonObj.RequestId),
                new NameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetInvoiceType]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public string TypeDeactive(string invoiceTypeId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = "INACTIVE";

            NameValuePairs nvp = new NameValuePairs
            {
                new NameValuePair("@InvoiceTypeId", invoiceTypeId),
                 
                 
                new NameValuePair("@RequestId", CommonObj.RequestId),
                new NameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetInvoiceType]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public List<InvoiceType> TypeGet(bool? IsActive = null)
        {
            List<InvoiceType> invoiceTypes = new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetInvoiceType]",
                new NameValuePairs
                {
                     
                     
                    new NameValuePair("@QueryType", "ALL")
                }).ToList<InvoiceType>();

            if (IsActive != null)
            {
                if (IsActive.Value)
                    return invoiceTypes.Where(a => a.IsActive).ToList();
                if (!IsActive.Value)
                    return invoiceTypes.Where(a => !a.IsActive).ToList();
            }
            return invoiceTypes;
        }
        #endregion

        #region Invoice
        public List<Invoice> GetInvoices()
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            NameValuePairs nvp = new NameValuePairs()
            {
                 
                 
                new NameValuePair("@QueryType", "LIST")
            };
            List<Invoice> invoices = _sqlDBAccess.GetData("[dbo].[spGetInvoice]", nvp).ToList<Invoice>();
            return invoices;
        }
        public List<Invoice> GetRetailInvoices()
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            NameValuePairs nvp = new NameValuePairs()
            {
                 
                 
                new NameValuePair("@QueryType", "RETAIL")
            };
            List<Invoice> invoices = _sqlDBAccess.GetData("[dbo].[spGetInvoice]", nvp).ToList<Invoice>();
            return invoices;
        }

        public List<SalesOrder> GetForEntry()
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            NameValuePairs nvp = new NameValuePairs()
                {
                     
                     
                    new NameValuePair("@QueryType", "ENTRY")
                };
            List<SalesOrder> salesOrders = _sqlDBAccess.GetData("[dbo].[spGetInvoice]", nvp).ToList<SalesOrder>();
            return salesOrders;
        }

        public List<InvoiceDetail> GetInvoiceDetails(long salesOrderId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            NameValuePairs nvp = new NameValuePairs()
                {
                     
                     
                    new NameValuePair("@SalesOrderId", salesOrderId),
                    new NameValuePair("@QueryType", "COMULTATIVE")
                };
            List<InvoiceDetail> invDtl = _sqlDBAccess.GetData("[dbo].[spGetInvoice]", nvp).ToList<InvoiceDetail>();
            return invDtl;
        }

        public List<Invoice> GetInvoicesForPayment(long customerId)
        {
            try
            {
                _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
                NameValuePairs nvp = new NameValuePairs()
                {
                     
                     
                    new NameValuePair("@customerId", customerId),
                    new NameValuePair("@QueryType", "PAYMENT")
                };
                List<Invoice> invs = _sqlDBAccess.GetData("[dbo].[spGetInvoice]", nvp).ToList<Invoice>();
                return invs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Invoice Set(Invoice invoice, Dictionary<string, object> dynamicFormValue)
        {
            try
            {
                invoice = Set(invoice);

                // Custom Property Set
                CustomPropertyLogic propertyLogic = new CustomPropertyLogic(CommonObj);
                propertyLogic.SetValue(ECustomPropertyFor.SalesInvoice, dynamicFormValue, invoice.InvoiceId, invoice.InvoiceNumber);
                //

                return invoice;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Invoice Set(Invoice invoice)
        {
            try
            {
                List<InvoiceDetailType> invoiceDetailsTypes = new List<InvoiceDetailType>();
                int count = 1;
                invoice.InvoicedAmount = 0;
                invoice.TaxAmount = 0;
                invoice.TotalAmount = 0;

                invoice.InvoiceDetails.ForEach(invoiceItem =>
                {
                    if (invoiceItem.InvoicedQuantity > 0)
                    {
                        InvoiceDetailType invoiceDetailType = new InvoiceDetailType
                        {
                            Id = count++,
                            SalesOrderDetailId = invoiceItem.SalesOrderDetailId,
                            ProductId = invoiceItem.ProductId,
                            HSNSAC = invoiceItem.HSNSAC,
                            Price = invoiceItem.Price,
                            InvoicedQuantity = invoiceItem.InvoicedQuantity,
                            TaxPercentage = invoiceItem.TaxPercentage,
                            SubTotal = invoiceItem.Price * invoiceItem.InvoicedQuantity,
                            Description = invoiceItem.Description,

                            // Set 0 as default
                            CGSTAmount = 0,
                            SGSTAmount = 0,
                            IGSTAmount = 0,
                            Total = 0
                        };

                        if (invoice.IsGstApplicable)
                        {
                            if (invoice.GSTType == (short)EGSTType.CGSTSGST)
                            {
                                invoiceDetailType.CGSTAmount = invoiceDetailType.SubTotal * (invoiceDetailType.TaxPercentage / 100) / 2;
                                invoiceDetailType.SGSTAmount = invoiceDetailType.SubTotal * (invoiceDetailType.TaxPercentage / 100) / 2;
                                invoiceDetailType.Total = invoiceDetailType.SubTotal + invoiceDetailType.CGSTAmount + invoiceDetailType.SGSTAmount;
                            }
                            if (invoice.GSTType == (short)EGSTType.IGST)
                            {
                                invoiceDetailType.IGSTAmount = invoiceDetailType.SubTotal * (invoiceDetailType.TaxPercentage / 100);
                                invoiceDetailType.Total = invoiceDetailType.SubTotal + invoiceDetailType.IGSTAmount;
                            }
                        }
                        else
                        {
                            invoiceDetailType.Total = invoiceDetailType.SubTotal;
                        }

                        // Header value Set
                        invoice.InvoicedAmount += invoiceDetailType.SubTotal;
                        invoice.TaxAmount += invoiceDetailType.CGSTAmount + invoiceDetailType.SGSTAmount + invoiceDetailType.IGSTAmount;
                        invoice.TotalAmount += invoiceDetailType.Total;

                        invoiceDetailsTypes.Add(invoiceDetailType);
                    }
                });

                if (invoiceDetailsTypes.Count > 0)
                {
                    _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
                    NameValuePairs nvp = new NameValuePairs()
                    {
                         
                         
                        new NameValuePair("@InvoiceTypeId", invoice.InvoiceTypeId),
                        new NameValuePair("@InvoiceDate", invoice.InvoiceDate),
                        new NameValuePair("@InvoiceDueDate", invoice.InvoiceDueDate),

                        new NameValuePair("@IsCustomizedInvoiceNumber", invoice.IsCustomizedInvoiceNumber),
                        new NameValuePair("@InvoiceNumber", invoice.IsCustomizedInvoiceNumber? invoice.InvoiceNumber : "INV-"),

                        new NameValuePair("@CustomerId", invoice.CustomerId),
                        new NameValuePair("@SalesOrderId", invoice.SalesOrderId),
                        new NameValuePair("@VehicleNumber", invoice.VehicleNumber),
                        new NameValuePair("@HandOverPerson", invoice.HandOverPerson),
                        new NameValuePair("@HandOverPersonMobile", invoice.HandOverPersonMobile),
                        new NameValuePair("@Remarks", invoice.Remarks),
                        new NameValuePair("@IsGstApplicable", invoice.IsGstApplicable),
                        new NameValuePair("@GSTNumber", invoice.GSTNumber),
                        new NameValuePair("@GSTStateCode", invoice.GSTStateCode),
                        new NameValuePair("@GSTType", invoice.GSTType),
                        new NameValuePair("@InvoicedAmount", invoice.InvoicedAmount),
                        new NameValuePair("@TaxAmount", invoice.TaxAmount),
                        new NameValuePair("@TotalAmount", invoice.TotalAmount),
                        new NameValuePair("@IsFullInvoiced", invoice.IsFullInvoiced),
                        new NameValuePair("@InvoiceDetail", invoiceDetailsTypes.ToDataTable()),

                        new NameValuePair("@RequestId", CommonObj.RequestId),
                        new NameValuePair("@QueryType", "INSERT")
                    };
                    string invoiceReturn = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetInvoice]", nvp, "@OutParam").ToString();
                    return JsonConvert.DeserializeObject<Invoice>(invoiceReturn);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Invoice GetForDetail(long InvoiceId)
        {
            Invoice invoice = new Invoice();
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            NameValuePairs nvp = new NameValuePairs()
            {
                 
                 
                new NameValuePair("@InvoiceId", InvoiceId),
                new NameValuePair("@QueryType", "DETAIL")
            };
            DataSet ds = _sqlDBAccess.GetDataSet("[dbo].[spGetInvoice]", nvp);
            invoice = ds.Tables[0].FirstOrDefault<Invoice>();
            if (invoice != null)
            {
                invoice.InvoiceDetails = new List<InvoiceDetail>();
                invoice.InvoiceDetails = ds.Tables[1].Rows.Count > 0 ? ds.Tables[1].ToList<InvoiceDetail>() : null;
                if (invoice.InvoiceDetails == null)
                    invoice = null;
            }
            return invoice;
        }

        public bool Available(string CustomInvoiceNumber)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);

            NameValuePairs nvp = new NameValuePairs
            {
                 
                 
                new NameValuePair("@InvoiceNumber", CustomInvoiceNumber),
                new NameValuePair("@QueryType", "AVAILABLE")
            };

            DataTable dt = _sqlDBAccess.GetData("[dbo].[spGetInvoice]", nvp);
            if (dt != null)
                if (dt.Rows.Count == 1)
                    if ((int)dt.Rows[0]["InvoiceId"] == 1)
                        return true;

            return false;
        }

        public string GetHTMLDesign()
        {
            _tableManager = new ExecuteTableManager("Design", CommonObj.NoSqlConnectionString);
            HtmlDesign htmlDesign = _tableManager.RetrieveEntity<HtmlDesign>("").FirstOrDefault();
            return htmlDesign.InvoiceHtml;
        }

        public void SetHTMLDesign(string InvoiceHtmlPath)
        {
            
        }
        #endregion
    }
}
