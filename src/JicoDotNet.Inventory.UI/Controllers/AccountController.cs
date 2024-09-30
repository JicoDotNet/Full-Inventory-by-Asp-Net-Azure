using System;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using JicoDotNet.Inventory.BusinessLayer.BLL;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.Core.Common.Auth;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Enumeration;
using JicoDotNet.Inventory.Core.Models;
using JicoDotNet.Inventory.Logging;
using JicoDotNet.Inventory.UI.Models;
using JicoDotNet.Authentication;
using JicoDotNet.Inventory.Core.Entities.Inner;
using System.Text;
using System.Web.WebPages;

namespace JicoDotNet.Inventory.UI.Controllers
{
    public class AccountController : BaseController
    {
        //private readonly UserAuthenticationService authService;

        //public AccountController()
        //{
        //    var secretKey = "JZwBz0kAnGz7xXqkR3Djq8VB7cXrvYbc1JuzW7Z98Fk=";
        //    authService = new UserAuthenticationService(secretKey);
        //}

        #region Login
        public ActionResult Index(string returnUrl)
        {
            try
            {
                //var token = authService.Authenticate("test");
                //Response.Headers.Add("Authorization", "Bearer " + token);


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
                //var token = HttpContext.Request.Headers["Authorization"];

                //var principal = authService.ValidateToken(token);
                //if (principal != null)
                //{
                //    Console.WriteLine("Token is valid. User: " + principal.Identity.Name);
                //}


                LoginCredentials loginCredentials = new LoginCredentials
                {
                    UserEmail = formCollection["UserEmail"],
                    Password = formCollection["Password"],
                };
                IAccountAuthentication accountAuthenticate = new LoginManagement(LogicHelper)
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
                    _ = AuditLogLogic.LoginLog(LogicHelper);
                    #endregion

                    #region Set Session Cookie & redirect if Login Success
                    if (accountAuthenticate.eLoginStatus == ELoginStatus.Success)
                    {
                        // Session Cookie Set
                        SetSessionCookie(accountAuthenticate.credential);

                        // Get Company Details
                        ICompanyBasic companyBasic = new CompanyBasic
                        {
                            CompanyName = WebConfigAppSettingsAccess.CompanyName,
                            GSTNumber = GenericLogic.IsValidGSTNumber(WebConfigAppSettingsAccess.GSTNumber) ? WebConfigAppSettingsAccess.GSTNumber : null
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
                string errMsg = string.Empty;
                if (accountAuthenticate.eLoginStatus == ELoginStatus.Error)
                {
                    errMsg = "Got an Error. Please Contact Administrator!!";
                }
                if (accountAuthenticate.eLoginStatus == ELoginStatus.IPAddressFormatError)
                {
                    errMsg = "Got an Error in IP. Please Contact Administrator!!";
                }
                if (accountAuthenticate.eLoginStatus == ELoginStatus.UserNameOrPasswordInvalid)
                {
                    errMsg = "Either User Name or Password is incorrect!!";
                }
                if (accountAuthenticate.eLoginStatus == ELoginStatus.UserNotInIPRange)
                {
                    errMsg = "you are not accessible from your IP!!";
                }
                ReturnMessage = new ReturnObject()
                {
                    Status = false,
                    Message = errMsg,
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
            string userEmail;
            if (TempData["UserEmail"] != null)
            {
                userEmail = TempData["UserEmail"].ToString();
            }
            else
                return RedirectToAction("Index", "Logout");
            TokenManagement token = new TokenManagement(LogicHelper);
            return View(token.GetUser(userEmail));
        }

        [HttpPost]
        public ActionResult Delete(SessionCredential sessionCredential)
        {
            if (sessionCredential.UserEmail == UrlParameterId)
            {
                TokenManagement token = new TokenManagement(LogicHelper);
                token.Delete(UrlParameterId);
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

        private void SetSessionCookie(ISessionCredential credential)
        {
            ISessionToken sessionToken = new SessionToken(credential);
            HttpContext.SetCookie(".AspNetCore.Session", sessionToken);
        }

        private void SetCompanyCookie(ICompanyBasic company)
        {
            HttpContext.SetCookie(".AspNetCore.Company", company);
        }
    }
}