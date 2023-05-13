using JicoDotNet.Inventory.BusinessLayer.BLL;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JicoDotNet.Inventory.UIControllers
{
    public class ConfigurationController : BaseController
    {
        [SessionAuthenticate]
        public ActionResult Setup()
        {
            try
            {

                ConfigarationManager configarationManager = new ConfigarationManager(BllCommonLogic);
                ConfigModels configModels = new ConfigModels
                {
                    _YesNo = GenericLogic.YesNo(),
                    _config = configarationManager.GetConfig(),
                    _company = new Company()
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
                        }
                };
                return View(configModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public ActionResult Setup(Config config)
        {
            try
            {
                #region Data Tracking...
                DataTrackingLogicSet(config);
                #endregion

                ConfigarationManager configarationManager = new ConfigarationManager(BllCommonLogic);                
                configarationManager.SetConfig(config);
                ReturnMessage = new ReturnObject()
                {
                    Message = "Success",
                    Status = true
                };
                return RedirectToAction("Detail", "Company", new { id = UrlIdEncrypt(id, false) });
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult Details()
        {
            try
            {
                ConfigModels configModels = new ConfigModels
                {
                    _company = SessionCompany,
                    _config = new ConfigarationManager(BllCommonLogic).GetConfig(),
                    _YesNo = GenericLogic.YesNo()
                };
                return View(configModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult Default()
        {
            ConfigModels configModels = new ConfigModels
            {
                _config = new ConfigarationManager(BllCommonLogic).GetConfig(),
            };
            return View(configModels);
        }

        [HttpPost]
        public ActionResult Default(Config config)
        {
            try
            {
                #region Data Tracking...
                DataTrackingLogicSet(config);
                #endregion

                ConfigarationManager configarationManager = new ConfigarationManager(BllCommonLogic);
                configarationManager.SetConfig(config);
                ReturnMessage = new ReturnObject()
                {
                    Message = "Success",
                    Status = true
                };
                return RedirectToAction("Default", "Configuration");
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }
    }
}