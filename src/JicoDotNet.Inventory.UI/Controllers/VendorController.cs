using JicoDotNet.Inventory.BusinessLayer.BLL;
using JicoDotNet.Inventory.Core.Models;
using JicoDotNet.Inventory.UI.Models;
using System;
using System.Linq;
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
                    _vendorTypes = new VendorLogic(LogicHelper).TypeGet()
                };
                if (!string.IsNullOrEmpty(UrlParameterId))
                {
                    vendorModels._vendorType = vendorModels._vendorTypes.Where(a => a.VendorTypeId == Convert.ToInt64(UrlParameterId)).FirstOrDefault();
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
                vendorType.VendorTypeId = UrlParameterId == null ? 0 : Convert.ToInt64(UrlParameterId);

                #region Data Tracking...
                DataTrackingLogicSet(vendorType);
                #endregion

                VendorLogic vendorLogic = new VendorLogic(LogicHelper);
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
                if (new LoginManagement(LogicHelper).Authenticate(SessionPerson.UserEmail, Context))
                {
                    VendorLogic vendorTypeLogic = new VendorLogic(LogicHelper);
                    long deactivateId = Convert.ToInt64(vendorTypeLogic.TypeDeactive(UrlParameterId));
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

        #region Vendor
        [SessionAuthenticate]
        public ActionResult Index()
        {
            try
            {
                VendorLogic vendorLogic = new VendorLogic(LogicHelper);
                VendorModels vendorModels = new VendorModels()
                {
                    _vendors = vendorLogic.Get(),
                    _vendorTypes = vendorLogic.TypeGet().Where(a => a.IsActive).ToList(),
                    _companyType = GenericLogic.CompanyType(),
                    _state = GenericLogic.State(),
                    _YesNo = GenericLogic.YesNo()
                };
                if (!string.IsNullOrEmpty(UrlParameterId))
                {
                    vendorModels._vendor = vendorModels._vendors.Where(a => a.VendorId == Convert.ToInt64(UrlParameterId)).FirstOrDefault();
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
                vendor.VendorId = UrlParameterId == null ? 0 : Convert.ToInt64(UrlParameterId);

                #region Data Tracking...
                DataTrackingLogicSet(vendor);
                #endregion

                VendorLogic vendorLogic = new VendorLogic(LogicHelper);
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
                if (new LoginManagement(LogicHelper).Authenticate(SessionPerson.UserEmail, Context))
                {
                    VendorLogic vendorLogic = new VendorLogic(LogicHelper);
                    long deactivateId = Convert.ToInt64(vendorLogic.Deactive(UrlParameterId));
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

        #region Vendor Bank
        [SessionAuthenticate]
        public ActionResult Bank()
        {
            try
            {
                if (string.IsNullOrEmpty(UrlParameterId))
                {
                    return RedirectToAction("Index", "Vendor", new { id = string.Empty });
                }
                VendorLogic vendorLogic = new VendorLogic(LogicHelper);
                VendorModels vendorModels = new VendorModels()
                {
                    _vendor = vendorLogic.Get().FirstOrDefault(a => a.VendorId == Convert.ToInt64(UrlParameterId)),
                    _vendorBanks = vendorLogic.BankGet(Convert.ToInt64(UrlParameterId))
                };
                if (!string.IsNullOrEmpty(UrlParameterId2))
                {
                    vendorModels._vendorBank = vendorModels._vendorBanks.Where(a => a.VendorBankId == Convert.ToInt64(UrlParameterId2)).FirstOrDefault();
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
                if (string.IsNullOrEmpty(UrlParameterId))
                {
                    return RedirectToAction("Index", "Vendor", new { id = string.Empty });
                }
                vendorBank.VendorId = Convert.ToInt64(UrlParameterId);
                vendorBank.VendorBankId = UrlParameterId2 == null ? 0 : Convert.ToInt64(UrlParameterId2);

                #region Data Tracking...
                DataTrackingLogicSet(vendorBank);
                #endregion

                VendorLogic vendorLogic = new VendorLogic(LogicHelper);
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

                return RedirectToAction("Bank", new { id = UrlIdEncrypt(UrlParameterId, false), id2 = string.Empty });
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
                if (new LoginManagement(LogicHelper).Authenticate(SessionPerson.UserEmail, Context))
                {
                    VendorLogic vendorLogic = new VendorLogic(LogicHelper);
                    long deactivateId = Convert.ToInt64(vendorLogic.BankDeactive(UrlParameterId, UrlParameterId2));
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
    }
}