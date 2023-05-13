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
                    _salesTypes = new SalesOrderLogic(BllCommonLogic).TypeGet()
                };
                if (!string.IsNullOrEmpty(id))
                {
                    salesModels._salesType = salesModels._salesTypes.FirstOrDefault(a => a.SalesTypeId == Convert.ToInt64(id));
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
                salesType.SalesTypeId = id == null ? 0 : Convert.ToInt64(id);

                #region Data Tracking...
                DataTrackingLogicSet(salesType);
                #endregion

                SalesOrderLogic salesTypeLogic = new SalesOrderLogic(BllCommonLogic);
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
                if (new LoginManagement(BllCommonLogic).Authenticate(SessionPerson.UserEmail, Context))
                {
                    SalesOrderLogic salesTypeLogic = new SalesOrderLogic(BllCommonLogic);
                    long deactivateId = Convert.ToInt64(salesTypeLogic.TypeDeactive(id));
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
                    _salesOrders = new SalesOrderLogic(BllCommonLogic).GetSOs(),
                    _config = new ConfigarationManager(BllCommonLogic).GetConfig(),
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
                Dictionary<string, object> Datas = new SalesOrderLogic(BllCommonLogic).GetForEntry();
                SalesOrderModels salesOrderModels = new SalesOrderModels()
                {
                    _salesTypes = (List<SalesType>)Datas["SalesType"],
                    _branches = (List<Branch>)Datas["Branch"],
                    _customers = (List<Customer>)Datas["Customer"],
                    _products = (List<Product>)Datas["Product"],
                    _company = SessionCompany,
                    _config = new ConfigarationManager(BllCommonLogic).GetConfig()
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
            DraftManagment draftManagment = new DraftManagment(BllCommonLogic);
            try
            {
                string ObjectId = draftManagment.SetAsDraft(salesOrder, EDraft.SO);
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
                if (string.IsNullOrEmpty(id))
                    return RedirectToAction("Index");

                SalesOrderModels salesOrderModels;
                if (id2 == "Draft")
                {
                    DraftManagment draftManagment = new DraftManagment(BllCommonLogic);
                    salesOrderModels = new SalesOrderModels()
                    {
                        _salesOrder = draftManagment.GetFromDraft<SalesOrder>(id, EDraft.SO),
                        _draftId = id
                    };
                    if (salesOrderModels._salesOrder != null)
                    {
                        salesOrderModels._config = new ConfigarationManager(BllCommonLogic).GetConfig();
                        salesOrderModels._companyAddress = new Company()
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
                        salesOrderModels._customer = new CustomerLogic(BllCommonLogic).Get(salesOrderModels._salesOrder.CustomerId);
                        return View(salesOrderModels);
                    }
                }
                else
                {
                    salesOrderModels = new SalesOrderModels()
                    {
                        _salesOrder = new SalesOrderLogic(BllCommonLogic).GetForDetail(Convert.ToInt64(id))
                    };
                    if (salesOrderModels._salesOrder != null)
                    {
                        salesOrderModels._config = new ConfigarationManager(BllCommonLogic).GetConfig();
                        salesOrderModels._companyAddress = new Company()
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
                        salesOrderModels._customer = new CustomerLogic(BllCommonLogic).Get(salesOrderModels._salesOrder.CustomerId);
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
                    _config = (new ConfigarationManager(BllCommonLogic)).GetConfig(),
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
                DraftManagment draftManagment = new DraftManagment(BllCommonLogic);
                if (string.IsNullOrEmpty(id))
                {
                    return RedirectToAction("Order");
                }
                SalesOrderModels salesOrderModels = new SalesOrderModels()
                {
                    _salesOrder = draftManagment.GetFromDraft<SalesOrder>(id, EDraft.SO)
                };

                #region Data Tracking...
                DataTrackingLogicSet(salesOrderModels._salesOrder);
                #endregion

                string obj = new SalesOrderLogic(BllCommonLogic).SetForEntry(salesOrderModels._salesOrder);
                SalesOrder SoObj = JsonConvert.DeserializeObject<SalesOrder>(obj);

                if (SoObj != null && SoObj.SalesOrderId > 0)
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Status = true,
                        Message = "Sales Order generated successfully!!"
                    };
                    draftManagment.DeleteDraft(id, EDraft.SO);
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
                return RedirectToAction("OrderDetail", "Sales", new { id = UrlIdEncrypt(id, false), id2 = "Draft" });
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
                if (new LoginManagement(BllCommonLogic).Authenticate(SessionPerson.UserEmail, Context))
                {
                    SalesOrderLogic salesOrderLogic = new SalesOrderLogic(BllCommonLogic);
                    long deactivateId = Convert.ToInt64(salesOrderLogic.Deactive(Convert.ToInt64(id)));
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

        [SessionAuthenticate]
        public ActionResult Amendment()
        {
            try
            {
                if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(id2))
                    return RedirectToAction("Index");

                SalesOrderModels salesOrderModels = new SalesOrderModels()
                {
                    _company = SessionCompany,
                    _salesOrder = new SalesOrderLogic(BllCommonLogic).GetForDetail(Convert.ToInt64(id))
                };
                salesOrderModels._config = new ConfigarationManager(BllCommonLogic).GetConfig();
                salesOrderModels._customer = new CustomerLogic(BllCommonLogic).Get(salesOrderModels._salesOrder.CustomerId);
                if (salesOrderModels._salesOrder == null || string.IsNullOrEmpty(salesOrderModels._salesOrder.SalesOrderNumber))
                    return RedirectToAction("Index");

                if (id2?.ToLower() == "quantity")
                {
                    if (salesOrderModels._salesOrder.ShippedStatus == null)
                        return View("_QuantityAmendment", salesOrderModels);
                }
                if (id2?.ToLower() == "price")
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
                if (string.IsNullOrEmpty(id))
                    return RedirectToAction("Index");

                salesOrder.SalesOrderId = Convert.ToInt64(id);
                string obj = new SalesOrderLogic(BllCommonLogic).SetForAmendment(salesOrder);
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
                return RedirectToAction("OrderDetail", "Sales", new { id = UrlIdEncrypt(id, false), id2 = string.Empty });
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }
        #endregion
    }
}