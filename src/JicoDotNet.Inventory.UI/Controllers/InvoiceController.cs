using JicoDotNet.Inventory.BusinessLayer.BLL;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.Core.Enumeration;
using JicoDotNet.Inventory.Core.Models;
using JicoDotNet.Inventory.UI.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using JicoDotNet.Inventory.Controllers;

namespace JicoDotNet.Inventory.UI.Controllers
{
    public class InvoiceController : BaseController
    {
        #region Invoice Type
        [SessionAuthenticate]
        public ActionResult Type()
        {
            try
            {
                InvoiceModels invoiceModels = new InvoiceModels()
                {
                    _invoiceTypes = new InvoiceLogic(LogicHelper).TypeGet()
                };
                if (!string.IsNullOrEmpty(UrlParameterId))
                {
                    invoiceModels._invoiceType = invoiceModels._invoiceTypes.Where(a => a.InvoiceTypeId == Convert.ToInt64(UrlParameterId)).FirstOrDefault();
                }
                return View(invoiceModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public ActionResult Type(InvoiceType invoiceType)
        {
            try
            {
                invoiceType.InvoiceTypeId = UrlParameterId == null ? 0 : Convert.ToInt64(UrlParameterId);

                #region Data Tracking...
                DataTrackingLogicSet(invoiceType);
                #endregion

                InvoiceLogic invoiceLogic = new InvoiceLogic(LogicHelper);
                if (Convert.ToInt64(invoiceLogic.TypeSet(invoiceType)) > 0)
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Message = "Success",
                        Status = true
                    };
                }
                else
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Message = "Unsuccess",
                        Status = false
                    };
                }

                return RedirectToAction("Type", new { id = string.Empty });
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public JsonResult TypeDeactivate(string Context)
        {
            try
            {
                if (new LoginManagement(LogicHelper).Authenticate(SessionPerson.UserEmail, Context))
                {
                    InvoiceLogic invoiceLogic = new InvoiceLogic(LogicHelper);
                    long deactivateId = Convert.ToInt64(invoiceLogic.TypeDeactive(UrlParameterId));
                    return Json(new JsonReturnModels
                    {
                        _isSuccess = true,
                        _returnObject = deactivateId > 0 ? UrlParameterId : "0"
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new JsonReturnModels
                    {
                        _isSuccess = true,
                        _returnObject = "-1"
                    }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToJson(ex);
            }
        }
        #endregion

        #region Invoice Generate
        [SessionAuthenticate]
        public ActionResult Index()
        {
            try
            {
                InvoiceModels invoiceModels = new InvoiceModels()
                {
                    _invoices = new InvoiceLogic(LogicHelper).GetInvoices(),
                    _config = (new ConfigarationManager(LogicHelper)).GetConfig(),
                    _company = SessionCompany
                };
                return View(invoiceModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult Generate()
        {
            try
            {
                InvoiceModels invoiceModels = new InvoiceModels();
                InvoiceLogic invoiceLogic = new InvoiceLogic(LogicHelper);
                if (string.IsNullOrEmpty(UrlParameterId))
                {
                    invoiceModels._salesOrders = invoiceLogic.GetForEntry();
                }
                else
                {
                    // Retrive SO
                    SalesOrderLogic orderLogic = new SalesOrderLogic(LogicHelper);
                    if (invoiceLogic.GetForEntry().Where(a => a.SalesOrderId == Convert.ToInt64(UrlParameterId)).FirstOrDefault() != null)
                        invoiceModels._salesOrder = orderLogic.GetForDetail(Convert.ToInt64(UrlParameterId));

                    // -- _salesOrder Check
                    if (invoiceModels._salesOrder == null)
                    {
                        ReturnMessage = new ReturnObject()
                        {
                            Status = false,
                            Message = "Sales Order not found!"
                        };
                        return RedirectToAction("Generate", new { id = string.Empty });
                    }

                    invoiceModels._invoiceTypes = invoiceLogic.TypeGet(true);
                    invoiceModels._config = new ConfigarationManager(LogicHelper).GetConfig();
                    invoiceModels._company = SessionCompany;

                    // Checking Company is GST registred or not.
                    invoiceModels.GSTType = EGSTType.None;
                    if (invoiceModels._salesOrder.IsGstAllowed)
                    {
                        invoiceModels.GSTType = GSTLogic.GetType(invoiceModels._salesOrder.IsGSTRegistered ?
                                invoiceModels._salesOrder.GSTStateCode :
                                invoiceModels._salesOrder.StateCode,
                            SessionCompany.IsGSTRegistered ?
                                SessionCompany.GSTStateCode :
                                SessionCompany.StateCode);
                    }

                    // Previous Invoice details- if partially invoiced
                    invoiceModels._invoiceDetails = invoiceLogic.GetInvoiceDetails(Convert.ToInt64(UrlParameterId));
                    // Check previous Invoice
                    if (invoiceModels._invoiceDetails.Count > 0)
                    {
                        foreach (SalesOrderDetail salesOrderDetail in invoiceModels._salesOrder.SalesOrderDetails.ToList())
                        {
                            InvoiceDetail invDtl = invoiceModels._invoiceDetails
                                .Where(a => a.SalesOrderDetailId == salesOrderDetail.SalesOrderDetailId).FirstOrDefault();
                            if (invDtl != null)
                            {
                                if (salesOrderDetail.Quantity > invDtl.InvoicedQuantity)
                                {
                                    // This item are yet to invoiced
                                    salesOrderDetail.ShippedQuantity = invDtl.InvoicedQuantity;
                                    salesOrderDetail.Quantity -= invDtl.InvoicedQuantity;
                                }
                                else
                                {
                                    // This Item is full invoiced
                                    invoiceModels._salesOrder.SalesOrderDetails.Remove(salesOrderDetail);
                                    invoiceModels._invoiceDetails.Remove(invDtl);
                                }
                            }
                        }
                    }
                }
                return View(invoiceModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public ActionResult Generate(FormCollection form, Invoice invoice)
        {
            try
            {
                // Retrive SO
                InvoiceLogic invoiceLogic = new InvoiceLogic(LogicHelper);
                SalesOrderLogic orderLogic = new SalesOrderLogic(LogicHelper);
                if (invoiceLogic.GetForEntry().FirstOrDefault(a => a.SalesOrderId == invoice.SalesOrderId) == null)
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Status = false,
                        Message = "Sales Order not found!"
                    };
                    return RedirectToAction("Generate", new { id = string.Empty });
                }

                Invoice rerurnInvoice = invoiceLogic.Set(invoice, form.AllKeys.ToDictionary(key => key, value => (object)form[value]));
                if (rerurnInvoice == null || rerurnInvoice.InvoiceId < 1)
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Status = false,
                        Message = "Something went Wrong!!"
                    };
                    return RedirectToAction("Generate", new { id = string.Empty });
                }
                else
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Status = true,
                        Message = "Invoice Generated!!"
                    };
                    return RedirectToAction("Detail", new { id = UrlIdEncrypt(rerurnInvoice.InvoiceId, false) });
                }
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult Detail()
        {
            try
            {
                if (string.IsNullOrEmpty(UrlParameterId))
                    return RedirectToAction("Index");

                InvoiceModels invoiceModels = new InvoiceModels
                {
                    _invoice = new InvoiceLogic(LogicHelper).GetForDetail(Convert.ToInt64(UrlParameterId))
                };
                if (invoiceModels._invoice != null)
                {
                    CompanyManagment companyManagment = new CompanyManagment(LogicHelper);
                    invoiceModels._config = new ConfigarationManager(LogicHelper).GetConfig();
                    invoiceModels._companyAddress = new Company()
                    {
                        CompanyName = SessionCompany.CompanyName,
                        GSTNumber = SessionCompany.GSTNumber,
                        GSTStateCode = SessionCompany.GSTStateCode,
                        IsGSTRegistered = SessionCompany.IsGSTRegistered,
                        StateCode = SessionCompany.StateCode,

                        Address = WebConfigAppSettingsAccess.CompanyAddress,
                        City = WebConfigAppSettingsAccess.CompanyCity,
                        Email = WebConfigAppSettingsAccess.CompanyEmail,
                        PINCode = WebConfigAppSettingsAccess.CompanyPINCode,
                        Mobile = WebConfigAppSettingsAccess.CompanyMobile,
                        WebsiteUrl = WebConfigAppSettingsAccess.CompanyWebsite,
                    };
                    invoiceModels._customer = new CustomerLogic(LogicHelper).Get(invoiceModels._invoice.CustomerId);
                    invoiceModels._salesOrder = new SalesOrderLogic(LogicHelper).GetForDetail(invoiceModels._invoice.SalesOrderId);
                    invoiceModels._companyBank = companyManagment.BankPrintable();
                    invoiceModels._customPropertyValue = new CustomPropertyLogic(LogicHelper).GetValue(ECustomPropertyFor.SalesInvoice, invoiceModels._invoice.InvoiceId);
                    return View(invoiceModels);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }
        #endregion
    }
}