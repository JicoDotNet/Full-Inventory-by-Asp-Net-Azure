using Newtonsoft.Json;
using JicoDotNet.Inventory.BusinessLayer.BLL;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.Core.Models;

namespace JicoDotNet.Inventory.UIControllers
{
    [SessionAuthenticate]
    public class CompanyController : BaseController
    {
        public ActionResult Index()
        {
            try
            {
                return RedirectToAction("Detail");
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        public ActionResult Detail()
        {
            try
            {
                CompanyModels companyModels = new CompanyModels()
                {
                    _company =
                            new Company()
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
                            },

                    _config = new ConfigarationManager(LogicHelper).GetConfig(),
                    _companyBanks = new CompanyManagment(LogicHelper).BankGet(true),
                    _sessionCredential = SessionPerson
                };
                if (companyModels._company != null)
                {
                    return View(companyModels);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        public ActionResult Bank()
        {
            try
            {
                CompanyManagment companyManagment = new CompanyManagment(LogicHelper);
                CompanyModels companyModels = new CompanyModels()
                {
                    _company = new Company()
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
                    },
                    _companyBanks = companyManagment.BankGet(),
                    _state = GenericLogic.State()
                };
                if (!string.IsNullOrEmpty(UrlParameterId))
                {
                    companyModels._companyBank = companyModels._companyBanks.FirstOrDefault(a => a.CompanyBankId == Convert.ToInt64(UrlParameterId));
                }
                return View(companyModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public ActionResult Bank(CompanyBank companyBank)
        {
            try
            {
                if (string.IsNullOrEmpty(UrlParameterId))
                {
                    return RedirectToAction("Index", "Company", new { id = string.Empty });
                }
                companyBank.CompanyBankId = UrlParameterId == null ? 0 : Convert.ToInt64(UrlParameterId);

                #region Data Tracking...
                DataTrackingLogicSet(companyBank);
                #endregion

                CompanyManagment companyManagment = new CompanyManagment(LogicHelper);
                if (Convert.ToInt64(companyManagment.BankSet(companyBank)) > 0)
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

                return RedirectToAction("Bank", new { id = string.Empty });
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
                    CompanyManagment companyManagment = new CompanyManagment(LogicHelper);
                    long deactivateId = Convert.ToInt64(companyManagment.BankDeactive(Convert.ToInt32(UrlParameterId)));
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

        public ActionResult BankPrint()
        {
            if (string.IsNullOrEmpty(UrlParameterId))
            {
                return RedirectToAction("Index", "Company", new { id = string.Empty });
            }
            #region Data Tracking...
            DataTrackingLogicSet(new CompanyBank { CompanyBankId = Convert.ToInt64(UrlParameterId) });
            #endregion

            CompanyManagment companyManagment = new CompanyManagment(LogicHelper);
            if (Convert.ToInt64(companyManagment.BankPrintability(Convert.ToInt64(UrlParameterId), true)) > 0)
            {
                ReturnMessage = new ReturnObject()
                {
                    Message = "Successfully set printability",
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
            return RedirectToAction("Bank", new { id = string.Empty });
        }

        public ActionResult BankUnPrint()
        {
            if (string.IsNullOrEmpty(UrlParameterId))
            {
                return RedirectToAction("Index", "Company", new { id = string.Empty });
            }
            #region Data Tracking...
            DataTrackingLogicSet(new CompanyBank
            {
                CompanyBankId = Convert.ToInt64(UrlParameterId),
            });
            #endregion

            CompanyManagment companyManagment = new CompanyManagment(LogicHelper);
            if (Convert.ToInt64(companyManagment.BankPrintability(Convert.ToInt64(UrlParameterId), false)) > 0)
            {
                ReturnMessage = new ReturnObject()
                {
                    Message = "Successfully remove printability",
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
            return RedirectToAction("Bank", new { id = string.Empty });
        }
    }
}