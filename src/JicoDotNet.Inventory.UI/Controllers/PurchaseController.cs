using Newtonsoft.Json;
using JicoDotNet.Inventory.BusinessLayer.BLL;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using JicoDotNet.Inventory.BusinessLayer.Common;

namespace JicoDotNet.Inventory.UIControllers
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
                    _purchaseTypes = new PurchaseOrderLogic(BllCommonLogic).TypeGet()
                };
                if (!string.IsNullOrEmpty(id))
                {
                    purchaseModels._purchaseType = purchaseModels._purchaseTypes.Where(a => a.PurchaseTypeId == Convert.ToInt64(id)).FirstOrDefault();
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
                purchaseType.PurchaseTypeId = id == null ? 0 : Convert.ToInt64(id);

                #region Data Tracking...
                DataTrackingLogicSet(purchaseType);
                #endregion

                PurchaseOrderLogic purchaseTypeLogic = new PurchaseOrderLogic(BllCommonLogic);
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
                if (new LoginManagement(BllCommonLogic).Authenticate(SessionPerson.UserEmail, Context))
                {
                    PurchaseOrderLogic purchaseTypeLogic = new PurchaseOrderLogic(BllCommonLogic);
                    long deactivateId = Convert.ToInt64(purchaseTypeLogic.TypeDeactive(id));
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
                    _purchaseOrders = new PurchaseOrderLogic(BllCommonLogic).GetPOs(),
                    _config = new ConfigarationManager(BllCommonLogic).GetConfig()
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
                Dictionary<string, object> Datas = new PurchaseOrderLogic(BllCommonLogic).GetForEntry();
                PurchaseOrderModels purchaseOrderModels = new PurchaseOrderModels()
                {
                    _purchaseTypes = (List<PurchaseType>)Datas["PurchaseType"],
                    _branches = (List<Branch>)Datas["Branch"],
                    _vendors = (List<Vendor>)Datas["Vendor"],
                    _products = (List<Product>)Datas["Product"],
                    _company = SessionCompany,
                    _config = new ConfigarationManager(BllCommonLogic).GetConfig()
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
                DraftManagment draftManagment = new DraftManagment(BllCommonLogic);
                string ObjectId = draftManagment.SetAsDraft(purchaseOrder, EDraft.PO);
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
                    _config = new ConfigarationManager(BllCommonLogic).GetConfig()
                };
                // id == true :: Vendor is GST Registered. PO should be with Tax
                if (id == "true")
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
                if (string.IsNullOrEmpty(id))
                    return RedirectToAction("Index");

                PurchaseOrderModels purchaseOrderModels;
                if (id2 == "Draft")
                {
                    DraftManagment draftManagment = new DraftManagment(BllCommonLogic);
                    purchaseOrderModels = new PurchaseOrderModels()
                    {
                        _purchaseOrder = draftManagment.GetFromDraft<PurchaseOrder>(id, EDraft.PO),
                        _draftId = id
                    };
                    if (purchaseOrderModels._purchaseOrder != null)
                    {
                        purchaseOrderModels._config = new ConfigarationManager(BllCommonLogic).GetConfig();
                        purchaseOrderModels._companyAddress = new Company()
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
                        purchaseOrderModels._vendor = new VendorLogic(BllCommonLogic).Get().FirstOrDefault(a => a.VendorId == purchaseOrderModels._purchaseOrder.VendorId);
                        return View(purchaseOrderModels);
                    }
                }
                else
                {
                    purchaseOrderModels = new PurchaseOrderModels()
                    {
                        _purchaseOrder = new PurchaseOrderLogic(BllCommonLogic).GetForDetail(Convert.ToInt64(id))
                    };
                    if (purchaseOrderModels._purchaseOrder != null)
                    {
                        purchaseOrderModels._config = new ConfigarationManager(BllCommonLogic).GetConfig();
                        purchaseOrderModels._companyAddress = new Company()
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
                        purchaseOrderModels._vendor = new VendorLogic(BllCommonLogic).Get().FirstOrDefault(a => a.VendorId == purchaseOrderModels._purchaseOrder.VendorId);
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
                DraftManagment draftManagment = new DraftManagment(BllCommonLogic);
                if (string.IsNullOrEmpty(id))
                {
                    return RedirectToAction("Order");
                }
                PurchaseOrderModels purchaseOrderModels = new PurchaseOrderModels()
                {
                    _purchaseOrder = draftManagment.GetFromDraft<PurchaseOrder>(id, EDraft.PO)
                };

                #region Data Tracking...
                DataTrackingLogicSet(purchaseOrderModels._purchaseOrder);
                #endregion

                string obj = new PurchaseOrderLogic(BllCommonLogic).SetForEntry(purchaseOrderModels._purchaseOrder);
                PurchaseOrder POobj = JsonConvert.DeserializeObject<PurchaseOrder>(obj);

                if (POobj != null && POobj.PurchaseOrderId > 0)
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Status = true,
                        Message = "Purchase Order generated successfully!!"
                    };
                    draftManagment.DeleteDraft(id, EDraft.PO);
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
                return RedirectToAction("OrderDetail", "Purchase", new { id = UrlIdEncrypt(id, false), id2 = "Draft" });
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
                    PurchaseOrderLogic purchaseOrderLogic = new PurchaseOrderLogic(BllCommonLogic);
                    long deactivateId = Convert.ToInt64(purchaseOrderLogic.Deactive(Convert.ToInt64(id)));
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

                PurchaseOrderModels purchaseOrderModels = new PurchaseOrderModels()
                {
                    _company = SessionCompany,
                    _purchaseOrder = new PurchaseOrderLogic(BllCommonLogic).GetForDetail(Convert.ToInt64(id))
                };
                purchaseOrderModels._config = new ConfigarationManager(BllCommonLogic).GetConfig();
                purchaseOrderModels._vendor = new VendorLogic(BllCommonLogic).Get().FirstOrDefault(a => a.VendorId == purchaseOrderModels._purchaseOrder.VendorId);
                if (purchaseOrderModels._purchaseOrder == null || string.IsNullOrEmpty(purchaseOrderModels._purchaseOrder.PurchaseOrderNumber))
                    return RedirectToAction("Index");

                if (id2?.ToLower() == "quantity")
                {
                    if (purchaseOrderModels._purchaseOrder.GoodsReceivedStatus == null)
                        return View("_QuantityAmendment", purchaseOrderModels);
                }
                if (id2?.ToLower() == "price")
                {
                    if (purchaseOrderModels._purchaseOrder.BilledStatus == null)
                        return View("_PriceAmendment", purchaseOrderModels);
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public ActionResult Amendment(PurchaseOrder purchaseOrder)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    return RedirectToAction("Index");

                purchaseOrder.PurchaseOrderId = Convert.ToInt64(id);
                string obj = new PurchaseOrderLogic(BllCommonLogic).SetForAmendment(purchaseOrder);
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
                return RedirectToAction("OrderDetail", "Purchase", new { id = UrlIdEncrypt(id, false), id2 = string.Empty });                
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }
        #endregion PO
    }
}