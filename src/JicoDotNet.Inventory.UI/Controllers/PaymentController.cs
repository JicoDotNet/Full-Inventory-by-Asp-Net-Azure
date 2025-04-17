using JicoDotNet.Inventory.BusinessLayer.BLL;
using JicoDotNet.Inventory.Core.Models;
using JicoDotNet.Inventory.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using JicoDotNet.Inventory.Controllers;

namespace JicoDotNet.Inventory.UI.Controllers
{
    public class PaymentController : BaseController
    {
        #region Payment Type
        [SessionAuthenticate]
        public ActionResult Type()
        {
            try
            {
                PaymentModels paymentModels = new PaymentModels()
                {
                    _paymentTypes = new PaymentLogic(LogicHelper).TypeGet()
                };
                if (!string.IsNullOrEmpty(UrlParameterId))
                {
                    paymentModels._paymentType = paymentModels._paymentTypes.Where(a => a.PaymentTypeId == Convert.ToInt64(UrlParameterId)).FirstOrDefault();
                }
                return View(paymentModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public ActionResult Type(PaymentType paymentType)
        {
            try
            {
                paymentType.PaymentTypeId = UrlParameterId == null ? 0 : Convert.ToInt64(UrlParameterId);

                #region Data Tracking...
                DataTrackingLogicSet(paymentType);
                #endregion

                PaymentLogic PaymentLogic = new PaymentLogic(LogicHelper);
                if (Convert.ToInt64(PaymentLogic.TypeSet(paymentType)) > 0)
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
                    PaymentLogic paymentLogic = new PaymentLogic(LogicHelper);
                    long deactivateId = Convert.ToInt64(paymentLogic.TypeDeactive(UrlParameterId));
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

        #region Make Payment
        [SessionAuthenticate]
        public ActionResult Made()
        {
            try
            {
                PaymentModels paymentModels = new PaymentModels
                {
                    _paymentOuts = new PaymentLogic(LogicHelper).GetPaymentOuts(),
                    _paymentMode = GenericLogic.PaymentMode(),
                    _config = new ConfigarationManager(LogicHelper).GetConfig()
                };
                return View(paymentModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult Make()
        {
            try
            {
                PaymentModels paymentModels = new PaymentModels();
                paymentModels._config = new ConfigarationManager(LogicHelper).GetConfig();
                if (string.IsNullOrEmpty(UrlParameterId))
                {
                    paymentModels._vendors = new VendorLogic(LogicHelper).Get(true);
                }
                else
                {
                    paymentModels._paymentMode = GenericLogic.PaymentMode();
                    paymentModels._vendor = new VendorLogic(LogicHelper).Get().FirstOrDefault(a => a.VendorId == Convert.ToInt64(UrlParameterId));
                    paymentModels._vendorBanks = new VendorLogic(LogicHelper).BankGet(Convert.ToInt64(UrlParameterId), true);
                    paymentModels._bills = new BillLogic(LogicHelper).GetBillsForPayment(Convert.ToInt64(UrlParameterId));

                    paymentModels._paymentOutDetails = new List<PaymentOutDetail>();
                    if (paymentModels._bills.Count > 0)
                    {
                        paymentModels._paymentOutDetails = new PaymentLogic(LogicHelper).GetPaymentOutDetails(Convert.ToInt64(UrlParameterId));
                    }

                    // if previously Paid
                    if (paymentModels._paymentOutDetails.Count > 0)
                    {
                        foreach (Bill bill in paymentModels._bills.ToList())
                        {
                            PaymentOutDetail dtl = paymentModels._paymentOutDetails
                                .Where(a => a.BillId == bill.BillId).FirstOrDefault();
                            if (dtl != null)
                            {
                                if (bill.TotalAmount > dtl.Amount)
                                {
                                    // This bill is yet to paid all.
                                    bill.PaidAmount = dtl.Amount;
                                    bill.TotalAmount = bill.TotalAmount - dtl.Amount;
                                }
                                else
                                {
                                    // This Bill is paid all
                                    paymentModels._paymentOutDetails.Remove(dtl);
                                    paymentModels._bills.Remove(bill);
                                }
                            }
                        }
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
        public ActionResult Make(PaymentOut paymentOut)
        {
            try
            {
                #region Data Tracking...
                DataTrackingLogicSet(paymentOut);
                #endregion

                PaymentLogic paymentLogic = new PaymentLogic(LogicHelper);
                if (Convert.ToInt64(paymentLogic.SetOut(paymentOut)) > 0)
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Message = "Payment made successfull",
                        Status = true
                    };
                }
                else
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Message = "Somethig went wrong",
                        Status = false
                    };
                }
                return RedirectToAction("Made");
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult Bill()
        {
            try
            {
                if (string.IsNullOrEmpty(UrlParameterId))
                    return RedirectToAction("Make");

                PaymentModels paymentModels = new PaymentModels
                {
                    _config = new ConfigarationManager(LogicHelper).GetConfig()
                };
                paymentModels._bill = new BillLogic(LogicHelper).GetBill(Convert.ToInt64(UrlParameterId));
                paymentModels._vendor = new VendorLogic(LogicHelper).Get(true).FirstOrDefault(a => a.VendorId == paymentModels._bill.VendorId);
                paymentModels._vendorBanks = new VendorLogic(LogicHelper).BankGet(paymentModels._bill.VendorId, true);
                paymentModels._paymentMode = GenericLogic.PaymentMode();
                if (paymentModels._bill != null)
                {
                    paymentModels._paymentOutDetail = new PaymentLogic(LogicHelper)
                        .GetPaymentOutDetails(paymentModels._bill.VendorId)
                        .FirstOrDefault(a => a.BillId == paymentModels._bill.BillId);
                }

                // if previous Paid
                if (paymentModels._paymentOutDetail != null)
                {
                    if (paymentModels._bill.TotalAmount > paymentModels._paymentOutDetail.Amount)
                    {
                        // This bill is yet to paid all.
                        paymentModels._bill.PaidAmount = paymentModels._paymentOutDetail.Amount;
                        paymentModels._bill.TotalAmount = paymentModels._bill.TotalAmount - paymentModels._paymentOutDetail.Amount;
                    }
                    else
                    {
                        // This Bill is paid all
                        paymentModels._paymentOutDetail = null;
                        paymentModels._bill = null;
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
        public ActionResult Bill(PaymentOut paymentOut)
        {
            try
            {
                if (string.IsNullOrEmpty(UrlParameterId))
                    return RedirectToAction("Make");
                foreach (PaymentOutDetail outDetail in paymentOut.PaymentOutDetails)
                {
                    outDetail.Amount = paymentOut.TotalAmount;
                    outDetail.Description = paymentOut.Remarks;
                }
                try
                {
                    #region Data Tracking...
                    DataTrackingLogicSet(paymentOut);
                    #endregion

                    PaymentLogic paymentLogic = new PaymentLogic(LogicHelper);
                    if (Convert.ToInt64(paymentLogic.SetOut(paymentOut)) > 0)
                    {
                        ReturnMessage = new ReturnObject()
                        {
                            Message = "Payment made successfull",
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
                    return RedirectToAction("Made");
                }
                catch (Exception ex)
                {
                    return ErrorLoggingToView(ex);
                }
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }
        #endregion

        #region Receive Payment
        [SessionAuthenticate]
        public ActionResult Received()
        {
            try
            {
                PaymentModels paymentModels = new PaymentModels
                {
                    _paymentIns = new PaymentLogic(LogicHelper).GetPaymentIns(),
                    _paymentMode = GenericLogic.PaymentMode(),
                    _config = new ConfigarationManager(LogicHelper).GetConfig()
                };
                return View(paymentModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult Receive()
        {
            try
            {
                PaymentModels paymentModels = new PaymentModels
                {
                    _config = new ConfigarationManager(LogicHelper).GetConfig()
                };
                if (string.IsNullOrEmpty(UrlParameterId))
                {
                    paymentModels._customers = new CustomerLogic(LogicHelper).GetNonRetail(true);
                }
                else
                {
                    paymentModels._paymentMode = GenericLogic.PaymentMode();
                    paymentModels._customer = new CustomerLogic(LogicHelper).Get(Convert.ToInt64(UrlParameterId), true);
                    paymentModels._companyBanks = new CompanyManagment(LogicHelper).BankGet(true);
                    paymentModels._invoices = new InvoiceLogic(LogicHelper).GetInvoicesForPayment(Convert.ToInt64(UrlParameterId));

                    paymentModels._paymentInDetails = new List<PaymentInDetail>();
                    if (paymentModels._invoices.Count > 0)
                    {
                        paymentModels._paymentInDetails = new PaymentLogic(LogicHelper).GetPaymentInDetails(Convert.ToInt64(UrlParameterId));
                    }

                    // if previously Received
                    if (paymentModels._paymentInDetails.Count > 0)
                    {
                        foreach (Invoice invoice in paymentModels._invoices.ToList())
                        {
                            PaymentInDetail dtl = paymentModels._paymentInDetails
                                .FirstOrDefault(a => a.InvoiceId == invoice.InvoiceId);
                            if (dtl != null)
                            {
                                if (invoice.TotalAmount > dtl.Amount)
                                {
                                    // This invoice is yet to receive all.
                                    invoice.ReceivedAmount = dtl.Amount;
                                    invoice.TotalAmount = invoice.TotalAmount - dtl.Amount;
                                }
                                else
                                {
                                    // This invoice is received all
                                    paymentModels._paymentInDetails.Remove(dtl);
                                    paymentModels._invoices.Remove(invoice);
                                }
                            }
                        }
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
        public ActionResult Receive(PaymentIn paymentIn)
        {
            try
            {
                #region Data Tracking...
                DataTrackingLogicSet(paymentIn);
                #endregion

                PaymentLogic paymentLogic = new PaymentLogic(LogicHelper);
                if (Convert.ToInt64(paymentLogic.SetIn(paymentIn)) > 0)
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Message = "Payment received successfull",
                        Status = true
                    };
                }
                else
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Message = "Somethig went wrong",
                        Status = false
                    };
                }
                return RedirectToAction("Received");
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
                if (string.IsNullOrEmpty(UrlParameterId))
                    return RedirectToAction("Receive");

                PaymentModels paymentModels = new PaymentModels
                {
                    _config = new ConfigarationManager(LogicHelper).GetConfig()
                };
                paymentModels._invoice = new InvoiceLogic(LogicHelper).GetInvoices().FirstOrDefault(a => a.InvoiceId == Convert.ToInt64(UrlParameterId));
                paymentModels._customer = new CustomerLogic(LogicHelper).Get(paymentModels._invoice.CustomerId, true);
                paymentModels._companyBanks = new CompanyManagment(LogicHelper).BankGet(true);
                paymentModels._paymentMode = GenericLogic.PaymentMode();
                if (paymentModels._invoice != null)
                {
                    paymentModels._paymentInDetail = new PaymentLogic(LogicHelper)
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
        public ActionResult Invoice(PaymentIn paymentIn)
        {
            try
            {
                if (string.IsNullOrEmpty(UrlParameterId))
                    return RedirectToAction("Receive", new { id = string.Empty });
                foreach (PaymentInDetail inDetail in paymentIn.PaymentInDetails)
                {
                    inDetail.Amount = paymentIn.TotalAmount;
                    inDetail.Description = paymentIn.Remarks;
                }

                #region Data Tracking...
                DataTrackingLogicSet(paymentIn);
                #endregion

                PaymentLogic paymentLogic = new PaymentLogic(LogicHelper);
                if (Convert.ToInt64(paymentLogic.SetIn(paymentIn)) > 0)
                {
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
                return RedirectToAction("Received");
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }
        #endregion
    }
}