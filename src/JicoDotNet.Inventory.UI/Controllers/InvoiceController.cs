using Newtonsoft.Json;
using JicoDotNet.Inventory.BusinessLayer.BLL;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.UI.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace JicoDotNet.Inventory.UIControllers
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
                    _invoiceTypes = new InvoiceLogic(BllCommonLogic).TypeGet()
                };
                if (!string.IsNullOrEmpty(id))
                {
                    invoiceModels._invoiceType = invoiceModels._invoiceTypes.Where(a => a.InvoiceTypeId == Convert.ToInt64(id)).FirstOrDefault();
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
                invoiceType.InvoiceTypeId = id == null ? 0 : Convert.ToInt64(id);

                #region Data Tracking...
                DataTrackingLogicSet(invoiceType);
                #endregion

                InvoiceLogic invoiceLogic = new InvoiceLogic(BllCommonLogic);
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
                if (new LoginManagement(BllCommonLogic).Authenticate(SessionPerson.UserEmail, Context))
                {
                    InvoiceLogic invoiceLogic = new InvoiceLogic(BllCommonLogic);
                    long deactivateId = Convert.ToInt64(invoiceLogic.TypeDeactive(id));
                    return Json(new JsonReturnModels
                    {
                        _isSuccess = true,
                        _returnObject = deactivateId > 0 ? id : "0"
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
                    _invoices = new InvoiceLogic(BllCommonLogic).GetInvoices(),
                    _config = (new ConfigarationManager(BllCommonLogic)).GetConfig(),
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
                InvoiceLogic invoiceLogic = new InvoiceLogic(BllCommonLogic);
                if (string.IsNullOrEmpty(id))
                {
                    invoiceModels._salesOrders = invoiceLogic.GetForEntry();
                }
                else
                {
                    // Retrive SO
                    SalesOrderLogic orderLogic = new SalesOrderLogic(BllCommonLogic);
                    if (invoiceLogic.GetForEntry().Where(a => a.SalesOrderId == Convert.ToInt64(id)).FirstOrDefault() != null)
                        invoiceModels._salesOrder = orderLogic.GetForDetail(Convert.ToInt64(id));

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
                    invoiceModels._config = new ConfigarationManager(BllCommonLogic).GetConfig();
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
                    invoiceModels._invoiceDetails = invoiceLogic.GetInvoiceDetails(Convert.ToInt64(id));
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
                InvoiceLogic invoiceLogic = new InvoiceLogic(BllCommonLogic);
                SalesOrderLogic orderLogic = new SalesOrderLogic(BllCommonLogic);
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
                if (string.IsNullOrEmpty(id))
                    return RedirectToAction("Index");

                InvoiceModels invoiceModels = new InvoiceModels
                {
                    _invoice = new InvoiceLogic(BllCommonLogic).GetForDetail(Convert.ToInt64(id))
                };
                if (invoiceModels._invoice != null)
                {
                    CompanyManagment companyManagment = new CompanyManagment(BllCommonLogic);
                    invoiceModels._config = new ConfigarationManager(BllCommonLogic).GetConfig();
                    invoiceModels._companyAddress = new Company()
                    {
                        CompanyName = SessionCompany.CompanyName,
                        GSTNumber = SessionCompany.GSTNumber,
                        GSTStateCode = SessionCompany.GSTStateCode,
                        IsGSTRegistered = SessionCompany.IsGSTRegistered,
                        StateCode = SessionCompany.StateCode,

                        Address = WebConfigAccess.CompanyAddress,
                        City = WebConfigAccess.CompanyCity,
                        Email = WebConfigAccess.CompanyEmail,
                        PINCode = WebConfigAccess.CompanyPINCode,
                        Mobile = WebConfigAccess.CompanyMobile,
                        WebsiteUrl = WebConfigAccess.CompanyWebsite,
                    };
                    invoiceModels._customer = new CustomerLogic(BllCommonLogic).Get(invoiceModels._invoice.CustomerId);
                    invoiceModels._salesOrder = new SalesOrderLogic(BllCommonLogic).GetForDetail(invoiceModels._invoice.SalesOrderId);
                    invoiceModels._companyBank = companyManagment.BankPrintable();
                    invoiceModels._customPropertyValue = new CustomPropertyLogic(BllCommonLogic).GetValue(ECustomPropertyFor.SalesInvoice, invoiceModels._invoice.InvoiceId);                    
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