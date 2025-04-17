using JicoDotNet.Inventory.BusinessLayer.BLL;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.Core.Enumeration;
using JicoDotNet.Inventory.Core.Models;
using JicoDotNet.Inventory.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using JicoDotNet.Inventory.Controllers;

namespace JicoDotNet.Inventory.UI.Controllers
{
    public class SalesController : BaseController
    {
        #region SO Type
        [SessionAuthenticate]
        public ActionResult Type()
        {
            try
            {
                SalesOrderModels salesModels = new SalesOrderModels()
                {
                    _salesTypes = new SalesOrderLogic(LogicHelper).TypeGet()
                };
                if (!string.IsNullOrEmpty(UrlParameterId))
                {
                    salesModels._salesType = salesModels._salesTypes.FirstOrDefault(a => a.SalesTypeId == Convert.ToInt64(UrlParameterId));
                }
                return View(salesModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public ActionResult Type(SalesType salesType)
        {
            try
            {
                salesType.SalesTypeId = UrlParameterId == null ? 0 : Convert.ToInt64(UrlParameterId);

                #region Data Tracking...
                DataTrackingLogicSet(salesType);
                #endregion

                SalesOrderLogic salesTypeLogic = new SalesOrderLogic(LogicHelper);
                if (Convert.ToInt64(salesTypeLogic.TypeSet(salesType)) > 0)
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
                    SalesOrderLogic salesTypeLogic = new SalesOrderLogic(LogicHelper);
                    long deactivateId = Convert.ToInt64(salesTypeLogic.TypeDeactive(UrlParameterId));
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

        #region SO
        [SessionAuthenticate]
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult PartialIndex()
        {
            try
            {
                SalesOrderModels purchaseOrderModels = new SalesOrderModels()
                {
                    _salesOrders = new SalesOrderLogic(LogicHelper).GetSOs(),
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
        public ActionResult Order()
        {
            try
            {
                Dictionary<string, object> Datas = new SalesOrderLogic(LogicHelper).GetForEntry();
                SalesOrderModels salesOrderModels = new SalesOrderModels()
                {
                    _salesTypes = (List<SalesType>)Datas["SalesType"],
                    _branches = (List<Branch>)Datas["Branch"],
                    _customers = (List<Customer>)Datas["Customer"],
                    _products = (List<Product>)Datas["Product"],
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

        [HttpPost]
        public ActionResult Order(SalesOrder salesOrder)
        {
            DraftManagement draftManagement = new DraftManagement(LogicHelper);
            try
            {
                string ObjectId = draftManagement.SetAsDraft(salesOrder, EDraft.SO);
                return RedirectToAction("OrderDetail", "Sales", new { id = UrlIdEncrypt(ObjectId, false), id2 = "Draft" });
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult OrderDetail()
        {
            try
            {
                if (string.IsNullOrEmpty(UrlParameterId))
                    return RedirectToAction("Index");

                SalesOrderModels salesOrderModels;
                if (UrlParameterId2 == "Draft")
                {
                    DraftManagement draftManagement = new DraftManagement(LogicHelper);
                    salesOrderModels = new SalesOrderModels()
                    {
                        _salesOrder = draftManagement.GetFromDraft<SalesOrder>(UrlParameterId, EDraft.SO),
                        _draftId = UrlParameterId
                    };
                    if (salesOrderModels._salesOrder != null)
                    {
                        salesOrderModels._config = new ConfigarationManager(LogicHelper).GetConfig();
                        salesOrderModels._companyAddress = new Company()
                        {
                            CompanyName = SessionCompany.CompanyName,
                            GSTNumber = SessionCompany.GSTNumber,
                            GSTStateCode = SessionCompany.GSTStateCode,
                            IsGSTRegistered = SessionCompany.IsGSTRegistered,
                            StateCode = SessionCompany.StateCode,

                            Address = LogicHelper.AppSettings.CompanyAddress,
                            City = LogicHelper.AppSettings.CompanyCity,
                            Email = LogicHelper.AppSettings.CompanyEmail,
                            PINCode = LogicHelper.AppSettings.CompanyPINCode,
                            Mobile = LogicHelper.AppSettings.CompanyMobile,
                            WebsiteUrl = LogicHelper.AppSettings.CompanyWebsite,
                        };
                        salesOrderModels._customer = new CustomerLogic(LogicHelper).Get(salesOrderModels._salesOrder.CustomerId);
                        return View(salesOrderModels);
                    }
                }
                else
                {
                    salesOrderModels = new SalesOrderModels()
                    {
                        _salesOrder = new SalesOrderLogic(LogicHelper).GetForDetail(Convert.ToInt64(UrlParameterId))
                    };
                    if (salesOrderModels._salesOrder != null)
                    {
                        salesOrderModels._config = new ConfigarationManager(LogicHelper).GetConfig();
                        salesOrderModels._companyAddress = new Company()
                        {
                            CompanyName = SessionCompany.CompanyName,
                            GSTNumber = SessionCompany.GSTNumber,
                            GSTStateCode = SessionCompany.GSTStateCode,
                            IsGSTRegistered = SessionCompany.IsGSTRegistered,
                            StateCode = SessionCompany.StateCode,

                            Address = LogicHelper.AppSettings.CompanyAddress,
                            City = LogicHelper.AppSettings.CompanyCity,
                            Email = LogicHelper.AppSettings.CompanyEmail,
                            PINCode = LogicHelper.AppSettings.CompanyPINCode,
                            Mobile = LogicHelper.AppSettings.CompanyMobile,
                            WebsiteUrl = LogicHelper.AppSettings.CompanyWebsite,
                        };
                        salesOrderModels._customer = new CustomerLogic(LogicHelper).Get(salesOrderModels._salesOrder.CustomerId);
                        return View(salesOrderModels);
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpGet]
        public PartialViewResult SOItems(short Gst)
        {
            try
            {
                SalesOrderModels salesOrderModels = new SalesOrderModels()
                {
                    _config = (new ConfigarationManager(LogicHelper)).GetConfig(),
                    _isGstEnabled = Gst == 1
                };
                return PartialView("_PartialSOItemDetails", salesOrderModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToPartial(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult ConfirmSO()
        {
            try
            {
                DraftManagement draftManagement = new DraftManagement(LogicHelper);
                if (string.IsNullOrEmpty(UrlParameterId))
                {
                    return RedirectToAction("Order");
                }
                SalesOrderModels salesOrderModels = new SalesOrderModels()
                {
                    _salesOrder = draftManagement.GetFromDraft<SalesOrder>(UrlParameterId, EDraft.SO)
                };

                #region Data Tracking...
                DataTrackingLogicSet(salesOrderModels._salesOrder);
                #endregion

                string obj = new SalesOrderLogic(LogicHelper).SetForEntry(salesOrderModels._salesOrder);
                SalesOrder SoObj = JsonConvert.DeserializeObject<SalesOrder>(obj);

                if (SoObj != null && SoObj.SalesOrderId > 0)
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Status = true,
                        Message = "Sales Order generated successfully!!"
                    };
                    draftManagement.DeleteDraft(UrlParameterId, EDraft.SO);
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
                return RedirectToAction("OrderDetail", "Sales", new { id = UrlIdEncrypt(UrlParameterId, false), id2 = "Draft" });
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
                    SalesOrderLogic salesOrderLogic = new SalesOrderLogic(LogicHelper);
                    long deactivateId = Convert.ToInt64(salesOrderLogic.Deactive(Convert.ToInt64(UrlParameterId)));
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

        [SessionAuthenticate]
        public ActionResult Amendment()
        {
            try
            {
                if (string.IsNullOrEmpty(UrlParameterId) || string.IsNullOrEmpty(UrlParameterId2))
                    return RedirectToAction("Index");

                SalesOrderModels salesOrderModels = new SalesOrderModels()
                {
                    _company = SessionCompany,
                    _salesOrder = new SalesOrderLogic(LogicHelper).GetForDetail(Convert.ToInt64(UrlParameterId))
                };
                salesOrderModels._config = new ConfigarationManager(LogicHelper).GetConfig();
                salesOrderModels._customer = new CustomerLogic(LogicHelper).Get(salesOrderModels._salesOrder.CustomerId);
                if (salesOrderModels._salesOrder == null || string.IsNullOrEmpty(salesOrderModels._salesOrder.SalesOrderNumber))
                    return RedirectToAction("Index");

                if (UrlParameterId2?.ToLower() == "quantity")
                {
                    if (salesOrderModels._salesOrder.ShippedStatus == null)
                        return View("_QuantityAmendment", salesOrderModels);
                }
                if (UrlParameterId2?.ToLower() == "price")
                {
                    if (salesOrderModels._salesOrder.InvoicedStatus == null)
                        return View("_PriceAmendment", salesOrderModels);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public ActionResult Amendment(SalesOrder salesOrder)
        {
            try
            {
                if (string.IsNullOrEmpty(UrlParameterId))
                    return RedirectToAction("Index");

                salesOrder.SalesOrderId = Convert.ToInt64(UrlParameterId);
                string obj = new SalesOrderLogic(LogicHelper).SetForAmendment(salesOrder);
                SalesOrder POobj = JsonConvert.DeserializeObject<SalesOrder>(obj);

                if (POobj != null && POobj.SalesOrderId > 0)
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Status = true,
                        Message = "PO amendment successfully!!"
                    };
                    return RedirectToAction("OrderDetail", "Sales", new { id = UrlIdEncrypt(POobj.SalesOrderId, false), id2 = string.Empty });
                }
                else
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Status = false,
                        Message = "Something went wrong!!"
                    };
                }
                return RedirectToAction("OrderDetail", "Sales", new { id = UrlIdEncrypt(UrlParameterId, false), id2 = string.Empty });
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }
        #endregion
    }
}