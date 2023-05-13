using Newtonsoft.Json;
using JicoDotNet.Inventory.BusinessLayer.BLL;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace JicoDotNet.Inventory.UIControllers
{
    public class AccountController : BaseController
    {
        readonly int TaxPercentage = 0;

        #region Login
        public ActionResult Index(string returnUrl)
        {
            try
            {
                ViewBag.returnUrl = returnUrl;
                return View();
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [AllowAnonymous]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(string returnUrl, FormCollection formCollection)
        {
            try
            {
                LoginCredentials loginCredentials = new LoginCredentials()
                {
                    UserEmail = formCollection["UserEmail"],
                    Password = formCollection["Password"],
                };
                AccountAuthentication accountAuthenticate = new LoginManagement(BllCommonLogic)
                                        .Authenticate(loginCredentials, GetRequestedIp());
               
                if (accountAuthenticate.credential != null)
                {
                    #region Set Token Data & Duplicate finding                    
                    if (accountAuthenticate.eLoginStatus == ELoginStatus.DuplicateLogin)
                    {
                        TempData["UserEmail"] = accountAuthenticate.credential.UserEmail;
                        return RedirectToAction("Duplicate");
                    }
                    #endregion

                    #region Login Track
                    new TrackingLogic(BllCommonLogic).LoginLog(new LoginLog());
                    #endregion

                    #region Set Session Cookie & redirect if Login Success
                    if (accountAuthenticate.eLoginStatus == ELoginStatus.Success)
                    {
                        // Session Cookie Set
                        SetSessionCookie(accountAuthenticate.credential);

                        // Get Company Details
                        CompanyBasic companyBasic = new CompanyBasic()
                        {
                            CompanyName = WebConfigAccess.CompanyName,
                            GSTNumber = GenericLogic.IsValidGSTNumber(WebConfigAccess.GSTNumber) ? WebConfigAccess.GSTNumber : null
                        };
                        companyBasic.IsGSTRegistered = !string.IsNullOrEmpty(companyBasic.GSTNumber);
                        companyBasic.GSTStateCode = companyBasic.IsGSTRegistered ? GenericLogic.GstStateCode(companyBasic.GSTNumber) : null;
                        companyBasic.StateCode = companyBasic.IsGSTRegistered ? GenericLogic.GstStateCode(companyBasic.GSTNumber) : "29";

                        // Set Cookie for Company Details 
                        SetCompanyCookie(companyBasic);

                        // Success Redirection
                        TempData["Url"] = Url.Action("Index", "Home");
                        return RedirectToAction("Auth", "Account");
                    }                    
                    #endregion
                }

                #region Login Error Handling
                string ErrMsg = string.Empty;
                if (accountAuthenticate.eLoginStatus == ELoginStatus.Error)
                {
                    ErrMsg = "Got an Error. Please Contact with Administrator!!";
                }
                if (accountAuthenticate.eLoginStatus == ELoginStatus.IPAddressFormatError)
                {
                    ErrMsg = "Got an Error. Please Contact Your Administrator!!";
                }
                if (accountAuthenticate.eLoginStatus == ELoginStatus.UserNameOrPasswordInvalid)
                {
                    ErrMsg = "Either User Name or Password is incorrect!!";
                }
                if (accountAuthenticate.eLoginStatus == ELoginStatus.UserNotInIPRange)
                {
                    ErrMsg = "you are not accessable from your IP!!";
                }
                ReturnMessage = new ReturnObject()
                {
                    Status = false,
                    Message = ErrMsg,
                };
                InvalidModelObject = new InvalidModel() { ModelData = loginCredentials };
                return RedirectToAction("Index");
                #endregion
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        public ActionResult Duplicate()
        {
            string UserEmail;
            string TenantCode;
            if (TempData["UserEmail"] != null)
            {
                UserEmail = TempData["UserEmail"].ToString();
            }
            else
                return RedirectToAction("Index", "Logout");
            TokenManagement token = new TokenManagement(BllCommonLogic);
            return View(token.GetUser(UserEmail));
        }

        [HttpPost]
        public ActionResult Delete(SessionCredential sessionCredential)
        {
            if (sessionCredential.UserEmail == id)
            {
                TokenManagement token = new TokenManagement(BllCommonLogic);
                token.Delete(id);
            }
            return RedirectToAction("Index", "Logout");
        }
        #endregion

        public ActionResult Auth()
        {
            if (TempData["Url"] != null)
            {
                RedirectModels models = new RedirectModels { _redirectUrl = TempData["Url"]?.ToString() };
                return View(models);
            }
            else
                return RedirectToAction("Index");
        }

        private void SetSessionCookie(SessionCredential credential, string CookieName = ".AspNetCore.Session")
        {
            SessionToken sessionToken = new SessionToken(credential);
            HttpCookie cookie = new HttpCookie(CookieName,
                JsonConvert.SerializeObject(sessionToken))
            {
                Expires = GenericLogic.IstNow.AddDays(1).AddSeconds(-1),
            };
            Response.Cookies.Add(cookie);
        }

        private void RemoveSessionCookie(string CookieName = ".AspNetCore.Session")
        {
            Response.Cookies[CookieName].Value = string.Empty;
            Response.Cookies[CookieName].Expires = GenericLogic.IstNow.AddMonths(-20);
        }

        private bool SetCompanyCookie(CompanyBasic company)
        {
            #region Set Cookie Org
            if (company != null)
            {
                HttpCookie cookieCom = new HttpCookie("laravel_session",
                        JsonConvert.SerializeObject(company))
                {
                    Expires = GenericLogic.IstNow.AddDays(1).AddSeconds(-1),
                };
                Response.Cookies.Add(cookieCom);
                return true;
            }
            else
                return false;
            #endregion
        }
    }
}