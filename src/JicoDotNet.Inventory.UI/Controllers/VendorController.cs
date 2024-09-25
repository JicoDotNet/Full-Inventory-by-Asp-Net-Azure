using JicoDotNet.Inventory.BusinessLayer.BLL;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JicoDotNet.Inventory.UIControllers
{
    public class VendorController : BaseController
    {
        #region Vendor Type
        [SessionAuthenticate]
        public ActionResult Type()
        {
            try
            {
                VendorModels vendorModels = new VendorModels()
                {
                    _vendorTypes = new VendorLogic(BllCommonLogic).TypeGet()
                };
                if (!string.IsNullOrEmpty(id))
                {
                    vendorModels._vendorType = vendorModels._vendorTypes.Where(a => a.VendorTypeId == Convert.ToInt64(id)).FirstOrDefault();
                }
                return View(vendorModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public ActionResult Type(VendorType vendorType)
        {
            try
            {
                vendorType.VendorTypeId = id == null ? 0 : Convert.ToInt64(id);

                #region Data Tracking...
                DataTrackingLogicSet(vendorType);
                #endregion

                VendorLogic vendorLogic = new VendorLogic(BllCommonLogic);
                if (Convert.ToInt64(vendorLogic.TypeSet(vendorType)) > 0)
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
                    VendorLogic vendorTypeLogic = new VendorLogic(BllCommonLogic);
                    long deactivateId = Convert.ToInt64(vendorTypeLogic.TypeDeactive(id));
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

        #region Vendor
        [SessionAuthenticate]
        public ActionResult Index()
        {
            try
            {
                VendorLogic vendorLogic = new VendorLogic(BllCommonLogic);
                VendorModels vendorModels = new VendorModels()
                {
                    _vendors = vendorLogic.Get(),
                    _vendorTypes = vendorLogic.TypeGet().Where(a => a.IsActive).ToList(),
                    _companyType = GenericLogic.CompanyType(),
                    _state = GenericLogic.State(),
                    _YesNo = GenericLogic.YesNo()
                };
                if (!string.IsNullOrEmpty(id))
                {
                    vendorModels._vendor = vendorModels._vendors.Where(a => a.VendorId == Convert.ToInt64(id)).FirstOrDefault();
                }
                return View(vendorModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public ActionResult Index(Vendor vendor)
        {
            try
            {
                vendor.VendorId = id == null ? 0 : Convert.ToInt64(id);

                #region Data Tracking...
                DataTrackingLogicSet(vendor);
                #endregion

                VendorLogic vendorLogic = new VendorLogic(BllCommonLogic);
                if (Convert.ToInt64(vendorLogic.Set(vendor)) > 0)
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

                return RedirectToAction("Index", new { id = string.Empty });
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
                    VendorLogic vendorLogic = new VendorLogic(BllCommonLogic);
                    long deactivateId = Convert.ToInt64(vendorLogic.Deactive(id));
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

        #region Vendor Bank
        [SessionAuthenticate]
        public ActionResult Bank()
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return RedirectToAction("Index", "Vendor", new { id = string.Empty });
                }
                VendorLogic vendorLogic = new VendorLogic(BllCommonLogic);
                VendorModels vendorModels = new VendorModels()
                {
                    _vendor = vendorLogic.Get().FirstOrDefault(a => a.VendorId == Convert.ToInt64(id)),
                    _vendorBanks = vendorLogic.BankGet(Convert.ToInt64(id))
                };
                if (!string.IsNullOrEmpty(id2))
                {
                    vendorModels._vendorBank = vendorModels._vendorBanks.Where(a => a.VendorBankId == Convert.ToInt64(id2)).FirstOrDefault();
                }
                return View(vendorModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public ActionResult Bank(VendorBank vendorBank)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return RedirectToAction("Index", "Vendor", new { id = string.Empty });
                }
                vendorBank.VendorId = Convert.ToInt64(id);
                vendorBank.VendorBankId = id2 == null ? 0 : Convert.ToInt64(id2);

                #region Data Tracking...
                DataTrackingLogicSet(vendorBank);
                #endregion

                VendorLogic vendorLogic = new VendorLogic(BllCommonLogic);
                if (Convert.ToInt64(vendorLogic.BankSet(vendorBank)) > 0)
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

                return RedirectToAction("Bank", new { id = UrlIdEncrypt(id, false), id2 = string.Empty });
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public JsonResult BankDeactivate(string Context)
        {
            try
            {
                if (new LoginManagement(BllCommonLogic).Authenticate(SessionPerson.UserEmail, Context))
                {
                    VendorLogic vendorLogic = new VendorLogic(BllCommonLogic);
                    long deactivateId = Convert.ToInt64(vendorLogic.BankDeactive(id, id2));
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
    }
}