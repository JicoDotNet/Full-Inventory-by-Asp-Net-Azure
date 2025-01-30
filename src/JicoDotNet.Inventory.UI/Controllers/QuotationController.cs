using JicoDotNet.Inventory.BusinessLayer.BLL;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.Core.Models;
using JicoDotNet.Inventory.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using JicoDotNet.Inventory.Controllers;

namespace JicoDotNet.Inventory.UI.Controllers
{
    public class QuotationController : BaseController
    {
        [SessionAuthenticate]
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult PartialIndex()
        {
            try
            {
                QuotationModels purchaseOrderModels = new QuotationModels()
                {
                    _quotations = new QuotationLogic(LogicHelper).GetQuotations(),
                    _config = new ConfigarationManager(LogicHelper).GetConfig(),
                    _company = SessionCompany
                };
                return PartialView("_PartialIndex", purchaseOrderModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToPartial(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult Generate()
        {
            try
            {
                QuotationModels salesOrderModels = new QuotationModels()
                {
                    _customers = new CustomerLogic(LogicHelper).GetNonRetail(true),
                    _company = SessionCompany,
                    _config = new ConfigarationManager(LogicHelper).GetConfig()
                };
                return View(salesOrderModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpGet]
        public PartialViewResult QuotationItems(short Gst)
        {
            try
            {
                QuotationModels models = new QuotationModels()
                {
                    _config = new ConfigarationManager(LogicHelper).GetConfig(),
                    _isGstEnabled = Gst == 1
                };
                return PartialView("_PartialQuotaItemDetails", models);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToPartial(ex);
            }
        }

        [HttpPost]
        public ActionResult Generate(Quotation quotation)
        {
            try
            {
                #region Data Tracking...
                DataTrackingLogicSet(quotation);
                #endregion

                string obj = new QuotationLogic(LogicHelper).SetForEntry(quotation);
                Quotation QoObj = JsonConvert.DeserializeObject<Quotation>(obj);

                if (QoObj != null && QoObj.QuotationId > 0)
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Status = true,
                        Message = "Quotation generated successfully!!"
                    };
                    return RedirectToAction("Detail", "Quotation", new { id = UrlIdEncrypt(QoObj.QuotationId, false), id2 = string.Empty });
                }
                else
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Status = false,
                        Message = "Something went wrong!!"
                    };
                }
                return RedirectToAction("Detail", "Quotation", new { id = UrlIdEncrypt(UrlParameterId, false), id2 = "Draft" });
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

                QuotationModels salesOrderModels = new QuotationModels()
                {
                    _quotation = new QuotationLogic(LogicHelper).GetForDetail(Convert.ToInt64(UrlParameterId))
                };
                if (salesOrderModels._quotation != null)
                {
                    CompanyManagment companyLogic = new CompanyManagment(LogicHelper);
                    salesOrderModels._config = new ConfigarationManager(LogicHelper).GetConfig();
                    salesOrderModels._companyAddress = new Company()
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
                    salesOrderModels._customer = new CustomerLogic(LogicHelper).Get(salesOrderModels._quotation.CustomerId);
                    salesOrderModels._companyBank = companyLogic.BankPrintable();
                    return View(salesOrderModels);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult Confirm()
        {
            try
            {
                if (string.IsNullOrEmpty(UrlParameterId))
                    return RedirectToAction("Index");

                QuotationModels salesOrderModels = new QuotationModels()
                {
                    _quotation = new QuotationLogic(LogicHelper).GetForDetail(Convert.ToInt64(UrlParameterId))
                };
                if (salesOrderModels._quotation != null)
                {
                    salesOrderModels._config = new ConfigarationManager(LogicHelper).GetConfig();
                    salesOrderModels._company = SessionCompany;
                    salesOrderModels._salesTypes = new SalesOrderLogic(LogicHelper).TypeGet(true);
                    salesOrderModels._branches = new BranchLogic(LogicHelper).Get(true);
                    salesOrderModels._customer = new CustomerLogic(LogicHelper).Get(salesOrderModels._quotation.CustomerId);
                    return View(salesOrderModels);
                }
                ReturnMessage = new ReturnObject()
                {
                    Status = false,
                    Message = "Something went wrong!!"
                };
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public ActionResult Confirm(SalesOrder salesOrder)
        {
            try
            {
                if (string.IsNullOrEmpty(UrlParameterId))
                    return RedirectToAction("Index");

                #region Data Tracking...
                DataTrackingLogicSet(salesOrder);
                #endregion

                Quotation quotation = new QuotationLogic(LogicHelper).GetForDetail(Convert.ToInt64(UrlParameterId));
                salesOrder.QuotationId = quotation.QuotationId;
                salesOrder.IsGstAllowed = quotation.IsGstAllowed;
                salesOrder.CustomerId = quotation.CustomerId;
                salesOrder.SalesOrderAmount = quotation.QuotationAmount;
                salesOrder.SalesOrderTaxAmount = quotation.QuotationTaxAmount;
                salesOrder.SalesOrderTotalAmount = quotation.QuotationTotalAmount;
                salesOrder.SalesOrderDetails = new List<SalesOrderDetail>();
                foreach (QuotationDetail qd in quotation.QuotationDetails)
                {
                    salesOrder.SalesOrderDetails.Add(new SalesOrderDetail
                    {
                        ProductId = qd.ProductId,
                        Description = qd.Description,
                        HSNSAC = qd.HSNSAC,
                        Amount = qd.Amount,
                        DiscountPercentage = qd.DiscountPercentage,
                        DiscountAmount = qd.DiscountAmount,
                        Price = qd.Price,
                        Quantity = qd.Quantity,
                        SubTotal = qd.SubTotal,
                        TaxPercentage = qd.TaxPercentage,
                        TaxAmount = qd.TaxAmount,
                        Total = qd.Total,
                    });
                }


                string obj = new SalesOrderLogic(LogicHelper).SetForEntry(salesOrder);
                SalesOrder SoObj = JsonConvert.DeserializeObject<SalesOrder>(obj);

                if (SoObj != null && SoObj.SalesOrderId > 0)
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Status = true,
                        Message = "Sales Order generated successfully!!"
                    };
                    return RedirectToAction("OrderDetail", "Sales", new { id = UrlIdEncrypt(SoObj.SalesOrderId, false), id2 = string.Empty });
                }
                else
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Status = false,
                        Message = "Something went wrong!!"
                    };
                }
                return RedirectToAction("Confirm", "Quotation", new { id = UrlIdEncrypt(UrlParameterId, false) });
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public JsonResult Deactivate(string Context)
        {
            try
            {
                if (new LoginManagement(LogicHelper).Authenticate(SessionPerson.UserEmail, Context))
                {
                    QuotationLogic quotationLogic = new QuotationLogic(LogicHelper);
                    long deactivateId = Convert.ToInt64(quotationLogic.Deactive(Convert.ToInt64(UrlParameterId)));
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
    }
}