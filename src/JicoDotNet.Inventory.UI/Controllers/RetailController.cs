using Newtonsoft.Json;
using JicoDotNet.Inventory.BusinessLayer.BLL;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using JicoDotNet.Inventory.BusinessLayer.Common;

namespace JicoDotNet.Inventory.UIControllers
{
    public class RetailController : BaseController
    {
        [SessionAuthenticate]
        public ActionResult Index()
        {
            try
            {
                InvoiceModels invoiceModels = new InvoiceModels()
                {
                    _invoices = new InvoiceLogic(BllCommonLogic).GetRetailInvoices(),
                    _config = new ConfigarationManager(BllCommonLogic).GetConfig(),
                    _company = SessionCompany,
                };
                return View(invoiceModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult Sales()
        {
            try
            {
                RetailModels retailModels = new RetailModels()
                {
                    _company = SessionCompany,
                    _wareHouses = new WareHouseLogic(BllCommonLogic).Get(true).Where(a => a.IsRetailCounter).ToList(),
                    _companyType = GenericLogic.CompanyType(),
                    _state = GenericLogic.State(),
                    _YesNo = GenericLogic.YesNo(),
                    _config = new ConfigarationManager(BllCommonLogic).GetConfig()
                };
                return View(retailModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public ActionResult Sales(FormCollection form, RetailSales retailSales)
        {
            try
            {
                #region Data Tracking...
                DataTrackingLogicSet(retailSales);
                #endregion

                // reset other details
                retailSales.CompanyName = string.IsNullOrEmpty(retailSales.CompanyName) ? retailSales.Mobile : retailSales.CompanyName;
                retailSales.ContactPerson = retailSales.ContactPerson;
                retailSales.HandOverPerson = retailSales.ContactPerson;
                retailSales.HandOverPersonMobile = retailSales.Mobile;

                long Id = new RetailLogic(BllCommonLogic).Set(retailSales, 
                    SessionCompany, 
                    form.AllKeys.ToDictionary(key => key, value => (object)form[value]), 
                    out short Rtpe);

                if(Rtpe == 2)
                {
                    return RedirectToAction("Payment", "Retail", new { id = UrlIdEncrypt(Id, false) });
                }
                // Kind of error if Rtype is 1
                else if (Rtpe == 1)
                {
                    return RedirectToAction("Generate", "Invoice", new { id = UrlIdEncrypt(Id, false) });
                }
                return RedirectToAction("Sales");
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpGet]
        public PartialViewResult RetailItems(short Gst)
        {
            try
            {
                SalesOrderModels salesOrderModels = new SalesOrderModels()
                {
                    _config = new ConfigarationManager(BllCommonLogic).GetConfig(),
                    _isGstEnabled = Gst == 1
                };
                return PartialView("_PartialRetailDetails", salesOrderModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToPartial(ex);
            }
        }

        [HttpGet]
        public PartialViewResult PartialStockDetailRetail()
        {
            try
            {
                ShipmentModels shipmentModels = new ShipmentModels();
                if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(id2))
                {
                    shipmentModels._config = new ConfigarationManager(BllCommonLogic).GetConfig();
                    shipmentModels._stocks = new StockLogic(BllCommonLogic).GetDetail(new Stock { WareHouseId = Convert.ToInt64(id), ProductId = Convert.ToInt64(id2) });
                }

                return PartialView("_PartialStockDetailRetail", shipmentModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToPartial(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult Payment()
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    return RedirectToAction("Sales");

                PaymentModels paymentModels = new PaymentModels
                {
                    _config = new ConfigarationManager(BllCommonLogic).GetConfig()
                };
                paymentModels._invoice = new InvoiceLogic(BllCommonLogic).GetForDetail(Convert.ToInt64(id));
                paymentModels._customer = new CustomerLogic(BllCommonLogic).Get(paymentModels._invoice.CustomerId, true);
                paymentModels._companyBanks = new CompanyManagment(BllCommonLogic).BankGet(true);
                paymentModels._paymentMode = GenericLogic.PaymentMode();
                if (paymentModels._invoice != null)
                {
                    paymentModels._paymentInDetail = new PaymentLogic(BllCommonLogic)
                        .GetPaymentInDetails(paymentModels._invoice.CustomerId)
                        .FirstOrDefault(a => a.InvoiceId == paymentModels._invoice.InvoiceId);
                }

                // if previous Received
                if (paymentModels._paymentInDetail != null)
                {
                    if (paymentModels._invoice.TotalAmount > paymentModels._paymentInDetail.Amount)
                    {
                        // This invoice is yet to receive all.
                        paymentModels._invoice.ReceivedAmount = paymentModels._paymentInDetail.Amount;
                        paymentModels._invoice.TotalAmount = paymentModels._invoice.TotalAmount - paymentModels._paymentInDetail.Amount;
                    }
                    else
                    {
                        // This invoice is received all
                        paymentModels._paymentInDetail = null;
                        paymentModels._invoice = null;
                    }
                }
                return View(paymentModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public ActionResult Payment(PaymentIn paymentIn)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    return RedirectToAction("Sales", new { id = string.Empty });

                foreach (PaymentInDetail inDetail in paymentIn.PaymentInDetails)
                {
                    inDetail.Amount = paymentIn.TotalAmount;
                    inDetail.Description = paymentIn.Remarks;
                }

                #region Data Tracking...
                DataTrackingLogicSet(paymentIn);
                #endregion

                PaymentLogic paymentLogic = new PaymentLogic(BllCommonLogic);
                if (Convert.ToInt64(paymentLogic.SetIn(paymentIn)) > 0)
                {
                    SMSSendTrack sMSSend = new SMSSendTrack
                    {
                        MobileNo = paymentIn.Mobile,
                        IsMobileNoChanged = false,
                        IsResend = false,
                    };

                    ReturnMessage = new ReturnObject()
                    {
                        Message = "Payment receive successfull",
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
                return RedirectToAction("Invoice", new { id = UrlIdEncrypt(id, false) });
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult Invoice()
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return RedirectToAction("Index");
                }
                InvoiceLogic invoiceLogic = new InvoiceLogic(BllCommonLogic);
                InvoiceModels invoiceModels = new InvoiceModels
                {
                    _invoice = invoiceLogic.GetForDetail(Convert.ToInt64(id))
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

                        Address = WebConfigAppSettingsAccess.CompanyAddress,
                        City = WebConfigAppSettingsAccess.CompanyCity,
                        Email = WebConfigAppSettingsAccess.CompanyEmail,
                        PINCode = WebConfigAppSettingsAccess.CompanyPINCode,
                        Mobile = WebConfigAppSettingsAccess.CompanyMobile,
                        WebsiteUrl = WebConfigAppSettingsAccess.CompanyWebsite,
                    };
                    invoiceModels._customer = new CustomerLogic(BllCommonLogic).Get(invoiceModels._invoice.CustomerId);
                    invoiceModels._salesOrder = new SalesOrderLogic(BllCommonLogic).GetForDetail(invoiceModels._invoice.SalesOrderId);
                    invoiceModels._companyBank = companyManagment.BankPrintable();
                    invoiceModels._invoiceHtml = invoiceLogic.GetHTMLDesign();
                    invoiceModels._customPropertyValue = new CustomPropertyLogic(BllCommonLogic).GetValue(ECustomPropertyFor.RetailSalesInvoice, invoiceModels._invoice.InvoiceId);                    
                    return View(invoiceModels);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        #region SMS Send for Retail
        [SessionAuthenticate]
        public ActionResult SMS()
        {
            if (!string.IsNullOrEmpty(id))
            {
                SMSBankLogic bankLogic = new SMSBankLogic(BllCommonLogic);
                SMSBank sMSBank = bankLogic.Get();
                if (sMSBank != null && sMSBank.Balance > 0)
                {
                    InvoiceModels invoiceModels = new InvoiceModels
                    {
                        _invoice = new InvoiceLogic(BllCommonLogic).GetForDetail(Convert.ToInt64(id))
                    };
                    if (invoiceModels._invoice != null)
                    {
                        invoiceModels._customer = new CustomerLogic(BllCommonLogic).Get(invoiceModels._invoice.CustomerId);
                        return View(invoiceModels);
                    }
                }
                else
                {
                    return View(new InvoiceModels());
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [SessionAuthenticate]
        public ActionResult SMSSending(SMSSendTrack sMSSend)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }
            sMSSend.IsResend = true;
            return RedirectToAction("SmsSent", new { id = UrlIdEncrypt(id, false) });
        }

        [SessionAuthenticate]
        public ActionResult SmsSent()
        {
            if (!string.IsNullOrEmpty(id))
            {
            }
            return RedirectToAction("Index");
        } 
        #endregion
    }
}