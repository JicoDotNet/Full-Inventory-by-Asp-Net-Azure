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
    public class PurchaseController : BaseController
    {
        #region Purchase Type
        [SessionAuthenticate]
        public ActionResult Type()
        {
            try
            {
                PurchaseModels purchaseModels = new PurchaseModels()
                {
                    _purchaseTypes = new PurchaseOrderLogic(LogicHelper).TypeGet()
                };
                if (!string.IsNullOrEmpty(UrlParameterId))
                {
                    purchaseModels._purchaseType = purchaseModels._purchaseTypes.Where(a => a.PurchaseTypeId == Convert.ToInt64(UrlParameterId)).FirstOrDefault();
                }
                return View(purchaseModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public ActionResult Type(PurchaseType purchaseType)
        {
            try
            {
                purchaseType.PurchaseTypeId = UrlParameterId == null ? 0 : Convert.ToInt64(UrlParameterId);

                #region Data Tracking...
                DataTrackingLogicSet(purchaseType);
                #endregion

                PurchaseOrderLogic purchaseTypeLogic = new PurchaseOrderLogic(LogicHelper);
                if (Convert.ToInt64(purchaseTypeLogic.TypeSet(purchaseType)) > 0)
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
                    PurchaseOrderLogic purchaseTypeLogic = new PurchaseOrderLogic(LogicHelper);
                    long deactivateId = Convert.ToInt64(purchaseTypeLogic.TypeDeactive(UrlParameterId));
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

        #region PO
        [SessionAuthenticate]
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult PartialIndex()
        {
            try
            {
                PurchaseOrderModels purchaseOrderModels = new PurchaseOrderModels()
                {
                    _purchaseOrders = new PurchaseOrderLogic(LogicHelper).GetPOs(),
                    _config = new ConfigarationManager(LogicHelper).GetConfig()
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
                Dictionary<string, object> Datas = new PurchaseOrderLogic(LogicHelper).GetForEntry();
                PurchaseOrderModels purchaseOrderModels = new PurchaseOrderModels()
                {
                    _purchaseTypes = (List<PurchaseType>)Datas["PurchaseType"],
                    _branches = (List<Branch>)Datas["Branch"],
                    _vendors = (List<Vendor>)Datas["Vendor"],
                    _products = (List<Product>)Datas["Product"],
                    _company = SessionCompany,
                    _config = new ConfigarationManager(LogicHelper).GetConfig()
                };
                return View(purchaseOrderModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public ActionResult Order(PurchaseOrder purchaseOrder)
        {
            try
            {
                DraftManagement draftManagement = new DraftManagement(LogicHelper);
                string ObjectId = draftManagement.SetAsDraft(purchaseOrder, EDraft.PO);
                return RedirectToAction("OrderDetail", "Purchase", new { id = UrlIdEncrypt(ObjectId, false), id2 = "Draft" });
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpGet]
        public PartialViewResult POItems()
        {
            try
            {
                PurchaseOrderModels purchaseOrderModels = new PurchaseOrderModels()
                {
                    _config = new ConfigarationManager(LogicHelper).GetConfig()
                };
                // id == true :: Vendor is GST Registered. PO should be with Tax
                if (UrlParameterId == "true")
                    return PartialView("_PartialPOItemDetailsWithTax", purchaseOrderModels);
                else
                    return PartialView("_PartialPOItemDetailsNonTax", purchaseOrderModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToPartial(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult OrderDetail()
        {
            try
            {
                if (string.IsNullOrEmpty(UrlParameterId))
                    return RedirectToAction("Index");

                PurchaseOrderModels purchaseOrderModels;
                if (UrlParameterId2 == "Draft")
                {
                    DraftManagement draftManagement = new DraftManagement(LogicHelper);
                    purchaseOrderModels = new PurchaseOrderModels()
                    {
                        _purchaseOrder = draftManagement.GetFromDraft<PurchaseOrder>(UrlParameterId, EDraft.PO),
                        _draftId = UrlParameterId
                    };
                    if (purchaseOrderModels._purchaseOrder != null)
                    {
                        purchaseOrderModels._config = new ConfigarationManager(LogicHelper).GetConfig();
                        purchaseOrderModels._companyAddress = new Company()
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
                        purchaseOrderModels._vendor = new VendorLogic(LogicHelper).Get().FirstOrDefault(a => a.VendorId == purchaseOrderModels._purchaseOrder.VendorId);
                        return View(purchaseOrderModels);
                    }
                }
                else
                {
                    purchaseOrderModels = new PurchaseOrderModels()
                    {
                        _purchaseOrder = new PurchaseOrderLogic(LogicHelper).GetForDetail(Convert.ToInt64(UrlParameterId))
                    };
                    if (purchaseOrderModels._purchaseOrder != null)
                    {
                        purchaseOrderModels._config = new ConfigarationManager(LogicHelper).GetConfig();
                        purchaseOrderModels._companyAddress = new Company()
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
                        purchaseOrderModels._vendor = new VendorLogic(LogicHelper).Get().FirstOrDefault(a => a.VendorId == purchaseOrderModels._purchaseOrder.VendorId);
                        return View(purchaseOrderModels);
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult ConfirmPO()
        {
            try
            {
                DraftManagement draftManagement = new DraftManagement(LogicHelper);
                if (string.IsNullOrEmpty(UrlParameterId))
                {
                    return RedirectToAction("Order");
                }
                PurchaseOrderModels purchaseOrderModels = new PurchaseOrderModels()
                {
                    _purchaseOrder = draftManagement.GetFromDraft<PurchaseOrder>(UrlParameterId, EDraft.PO)
                };

                #region Data Tracking...
                DataTrackingLogicSet(purchaseOrderModels._purchaseOrder);
                #endregion

                string obj = new PurchaseOrderLogic(LogicHelper).SetForEntry(purchaseOrderModels._purchaseOrder);
                PurchaseOrder POobj = JsonConvert.DeserializeObject<PurchaseOrder>(obj);

                if (POobj != null && POobj.PurchaseOrderId > 0)
                {
                    ReturnMessage = new ReturnObject
                    {
                        Status = true,
                        Message = "Purchase Order generated successfully!!"
                    };
                    draftManagement.DeleteDraft(UrlParameterId, EDraft.PO);
                    return RedirectToAction("OrderDetail", "Purchase", new { id = UrlIdEncrypt(POobj.PurchaseOrderId, false), id2 = string.Empty });
                }
                else
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Status = false,
                        Message = "Something went wrong!!"
                    };
                }
                return RedirectToAction($"OrderDetail", "Purchase", new { id = UrlIdEncrypt(UrlParameterId, false), id2 = "Draft" });
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public JsonResult Deactivate(string context)
        {
            try
            {
                if (new LoginManagement(LogicHelper).Authenticate(SessionPerson.UserEmail, context))
                {
                    PurchaseOrderLogic purchaseOrderLogic = new PurchaseOrderLogic(LogicHelper);
                    long deactivateId = Convert.ToInt64(purchaseOrderLogic.Deactive(Convert.ToInt64(UrlParameterId)));
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

                PurchaseOrderModels purchaseOrderModels = new PurchaseOrderModels()
                {
                    _company = SessionCompany,
                    _purchaseOrder = new PurchaseOrderLogic(LogicHelper).GetForDetail(Convert.ToInt64(UrlParameterId))
                };
                purchaseOrderModels._config = new ConfigarationManager(LogicHelper).GetConfig();
                purchaseOrderModels._vendor = new VendorLogic(LogicHelper).Get().FirstOrDefault(a => a.VendorId == purchaseOrderModels._purchaseOrder.VendorId);
                if (purchaseOrderModels._purchaseOrder == null || string.IsNullOrEmpty(purchaseOrderModels._purchaseOrder.PurchaseOrderNumber))
                    return RedirectToAction("Index");

                if (UrlParameterId2?.ToLower() == "quantity")
                {
                    if (purchaseOrderModels._purchaseOrder.GoodsReceivedStatus == null)
                        return View("_QuantityAmendment", purchaseOrderModels);
                }
                if (UrlParameterId2?.ToLower() == "price")
                {
                    if (purchaseOrderModels._purchaseOrder.BilledStatus == null)
                        return View("_PriceAmendment", purchaseOrderModels);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public ActionResult Amendment(PurchaseOrder purchaseOrder)
        {
            try
            {
                if (string.IsNullOrEmpty(UrlParameterId))
                    return RedirectToAction("Index");

                purchaseOrder.PurchaseOrderId = Convert.ToInt64(UrlParameterId);
                string obj = new PurchaseOrderLogic(LogicHelper).SetForAmendment(purchaseOrder);
                PurchaseOrder POobj = JsonConvert.DeserializeObject<PurchaseOrder>(obj);

                if (POobj != null && POobj.PurchaseOrderId > 0)
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Status = true,
                        Message = "PO amendment successfully!!"
                    };
                    return RedirectToAction("OrderDetail", "Purchase", new { id = UrlIdEncrypt(POobj.PurchaseOrderId, false), id2 = string.Empty });
                }
                else
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Status = false,
                        Message = "Something went wrong!!"
                    };
                }
                return RedirectToAction("OrderDetail", "Purchase", new { id = UrlIdEncrypt(UrlParameterId, false), id2 = string.Empty });
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }
        #endregion PO
    }
}