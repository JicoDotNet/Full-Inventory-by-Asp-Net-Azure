using System;
using System.IO;
using System.Text;
using System.Web.Configuration;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using JicoDotNet.Inventory.BusinessLayer.BLL;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.BusinessLayer.DTO.SP;
using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using JicoDotNet.Inventory.UI.Models;

namespace System.Web.Mvc
{
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BaseController()
        {
            this.BllCommonLogic = new sCommonDto
            {
                SqlConnectionString = DBConnection.SqlServer,
                NoSqlConnectionString = DBConnection.AzureStorage,
                RequestId = Guid.NewGuid().ToString().Replace("-", "").ToUpper(),
            };
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                #region Set Action Context Varible
                this._filteringContext = filterContext;
                #endregion

                #region Get Route Value
                this.controller = _filteringContext.RouteData.Values["controller"].ToString();
                this.action = _filteringContext.RouteData.Values["action"].ToString();
                this.id = _filteringContext.RouteData.Values["id"]?.ToString();
                this.id2 = _filteringContext.RouteData.Values["id2"]?.ToString();
                this.id = id == null ? null : UrlIdDecrypt(id);
                this.baseUrl = _filteringContext.HttpContext.Request.Url.Scheme
                    + "://" + _filteringContext.HttpContext.Request.Url.Authority;
                #endregion

                #region Cookie Manage for Session Person
                HttpCookie cookie = _filteringContext.RequestContext.HttpContext.Request.Cookies[".AspNetCore.Session"];
                if (cookie != null)
                {
                    // Increase the expire date of session Person cookie
                    cookie.Expires = GenericLogic.IstNow.AddDays(1).AddSeconds(-1);
                    Response.Cookies.Add(cookie);
                    try
                    {
                        SessionPerson = JsonConvert.DeserializeObject<SessionCredential>(cookie.Value ?? "");
                        Token = SessionPerson?.Token;
                        SessionPerson.Token = null;
                    }
                    catch
                    {
                        Token = null;
                        SessionPerson = null;
                    }
                }
                #endregion

                #region Cookie Manage for Company or Org
                cookie = _filteringContext.RequestContext.HttpContext.Request.Cookies["laravel_session"];
                if (cookie != null)
                {
                    // Increase the expire date of company cookie
                    cookie.Expires = GenericLogic.IstNow.AddDays(1).AddSeconds(-1);
                    Response.Cookies.Add(cookie);

                    try
                    {
                        SessionCompany = JsonConvert.DeserializeObject<CompanyBasic>(cookie.Value ?? "");
                    }
                    catch
                    {
                        SessionCompany = null;
                    }
                }
                #endregion

                #region Set Global value into CommonDto
                BllCommonLogic.Token = Token;
                #endregion
                
                #region TempData Manage
                ReturnMessage = (ReturnObject)TempData["ReturnMessage"];
                InvalidModelObject = (InvalidModel)TempData["InvalidModel"];
                #endregion

                #region Logger Activity
                LogSet(new Logger
                {
                    IPAddress = GetRequestedIp(),
                    DNS = System.Net.Dns.GetHostName(),
                    HttpVerbs = _filteringContext.HttpContext.Request.HttpMethod,
                    Browser = _filteringContext.HttpContext.Request.Browser.Browser,
                    BrowserType = _filteringContext.HttpContext.Request.Browser.Type,
                    BrowserVersion = _filteringContext.HttpContext.Request.Browser.Version,
                    AbsoluteUri = _filteringContext.HttpContext.Request.Url.AbsoluteUri,
                    MacAddress = _filteringContext.HttpContext.Request.Headers["X-Forwarded-For"],
                    Controller = controller,
                    Action = action,
                    Id = id,
                    Id2 = id2,
                    IsMobile = _filteringContext.HttpContext.Request.Browser.IsMobileDevice,
                    OSType = _filteringContext.HttpContext.Request.Browser.Platform
                });
                #endregion

                #region View Bag for Rezor View
                ViewBag.SessionPerson = this.SessionPerson;
                ViewBag.Company = SessionCompany;
                #endregion

                base.OnActionExecuting(_filteringContext);
                return;
            }
            catch (Exception e)
            {
                ErrorLoggingToView(e);
                _filteringContext.Result =
                                    RedirectToAction("Index", "Error", new { returnUrl = _filteringContext.HttpContext.Request.RawUrl, Ex = e });
                return;
            }
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            #region Set Global Varible
            _filteredContext = filterContext;
            #endregion

            TempData["ReturnMessage"] = ReturnMessage;
            TempData["InvalidModel"] = InvalidModelObject;
            base.OnActionExecuted(filterContext);
        }

        protected void AbandonSession()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
            Response.Cookies["ASP.NET_SessionId"].Expires = GenericLogic.IstNow.AddMonths(-20);

            Response.Cookies[".AspNetCore.Session"].Value = string.Empty;
            Response.Cookies[".AspNetCore.Session"].Expires = GenericLogic.IstNow.AddMonths(-20);

            Response.Cookies["laravel_session"].Value = string.Empty;
            Response.Cookies["laravel_session"].Expires = GenericLogic.IstNow.AddMonths(-20);

            Response.Cookies["sessionid"].Value = string.Empty;
            Response.Cookies["sessionid"].Expires = GenericLogic.IstNow.AddMonths(-20);
        }

        protected string GetRequestedIp()
        {
            string ip = Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                if (string.IsNullOrEmpty(ip))
                    ip = null;
            }
            if (ip == "::1")
                ip = "127.0.0.1";
            return ip;
        }

        public void DataTrackingLogicSet(object _object)
        {
            #pragma warning disable CS4014
            DataTrackingLogic.Set(_object, BllCommonLogic);
            #pragma warning restore CS4014
        }

        private void LogSet(Logger LogObject)
        {
            TrackingLogic.Log(LogObject, BllCommonLogic);
        }

        protected string UrlIdDecrypt(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    return Encoding.ASCII.GetString(Convert.FromBase64String(id));
                }
                else
                {
                    return null;
                }
            }
            catch (FormatException)
            {
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected string UrlIdEncrypt(object id, bool isEscape = true)
        {
            if (id != null)
            {
                string _id = id?.ToString();
                if (!string.IsNullOrEmpty(_id))
                {
                    var IdBytes = Encoding.UTF8.GetBytes(_id);
                    if (isEscape)
                        return Uri.EscapeDataString(Convert.ToBase64String(IdBytes));
                    else
                        return Convert.ToBase64String(IdBytes);
                }
            }
            return null;
        }

        protected RedirectToRouteResult ErrorLoggingToView(Exception ex)
        {
            TrackErrorLogging(ex);
            TempData["Error"] = new JicoDotNet.Inventory.UI.Models.ErrorModels
            {
                ErrorStatus = 500,
                ErrorCode = BllCommonLogic?.RequestId,
                RequestId = BllCommonLogic?.RequestId,
                Message = ex.Message
            };
            return RedirectToAction("Index", "Error", new { Area = string.Empty, id = BllCommonLogic?.RequestId });
        }

        protected PartialViewResult ErrorLoggingToPartial(Exception ex)
        {
            TrackErrorLogging(ex);
            return PartialView("_PartialErrorBlock", new JicoDotNet.Inventory.UI.Models.ErrorModels
            {
                ErrorStatus = 500,
                ErrorCode = BllCommonLogic?.RequestId,
                RequestId = BllCommonLogic?.RequestId,
                Message = ex.Message
            });
        }

        protected JsonResult ErrorLoggingToJson(Exception ex)
        {
            TrackErrorLogging(ex);
            JsonReturnModels model = new JsonReturnModels
            {
                _isSuccess = false,
                _returnObject = new JicoDotNet.Inventory.UI.Models.ErrorModels
                {
                    ErrorStatus = 500,
                    ErrorCode = BllCommonLogic?.RequestId,
                    RequestId = BllCommonLogic?.RequestId
                },
                _redirectURL = Url.Action("Index", "Error", new { Area = string.Empty, id = BllCommonLogic?.RequestId })
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        protected void ErrorLogging(Exception ex)
        {
            TrackErrorLogging(ex);
        }

        private void TrackErrorLogging(Exception ex)
        {
            try
            {
                string message = string.Empty;
                message += Environment.NewLine;
                message += "-------------------------------------------------\n";
                message += string.Format("Time: {0}", GenericLogic.IstNow.ToString("dd/MMM/yyyy hh:mm:ss tt"));
                message += Environment.NewLine;
                message += string.Format("RequestId: {0}", BllCommonLogic?.RequestId);
                message += Environment.NewLine;
                message += "-------------------------------------------------";
                message += Environment.NewLine;
                message += string.Format("Message: {0}", ex?.Message);
                message += Environment.NewLine;
                message += string.Format("StackTrace: {0}", ex?.StackTrace);
                message += Environment.NewLine;
                message += string.Format("Source: {0}", ex?.Source);
                message += Environment.NewLine;
                message += string.Format("TargetSite: {0}", ex?.TargetSite?.ToString());
                message += Environment.NewLine;
                message += string.Format("HResult: {0}", ex?.HResult);
                message += Environment.NewLine;
                message += string.Format("Path: {0}", Request?.HttpMethod + " :-> /" + controller + "/" + action + "/" + id + "/" + id2);
                message += Environment.NewLine;
                message += string.Format("Data: {0}", JsonConvert.SerializeObject(ex?.Data));
                message += Environment.NewLine;
                message += string.Format("InnerException: {0}", JsonConvert.SerializeObject(ex?.InnerException));
                message += Environment.NewLine;
                message += "___________________________________________________________\n";
                message += "===========================================================\n";
                message += Environment.NewLine;

                string path = Server.MapPath("~/ErrorLog");                
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path += "/Error.log";
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(message);
                    writer.Close();
                }
            }
            catch
            {
                return;
            }
        }

        protected bool SessionValidate()
        {
            try
            {
                HttpCookie cookie = Request.Cookies["sessionid"];
                if (cookie != null)
                {
                    // Increase the expire date of session cookie
                    cookie.Expires = GenericLogic.IstNow.AddMinutes(30).AddSeconds(-1);                    
                    Response.Cookies.Add(cookie);

                    SessionPerson = JsonConvert.DeserializeObject<SessionCredential>(cookie.Value ?? "");
                    Token = SessionPerson?.Token;
                    //SessionPerson.Token = null;
                    BllCommonLogic.Token = SessionPerson.Token;
                    return true;
                }
            }
            catch
            {                
                return false;
            }
            return false;
        }

        #region private variable
        private ActionExecutingContext _filteringContext;
        private ActionExecutedContext _filteredContext;
        #endregion

        #region protected Property
        protected string controller { get; private set; }
        protected string action { get; private set; }
        protected string id { get; set; }
        protected string id2 { get; set; }
        protected string baseUrl { get; private set; }

        protected SessionCredential SessionPerson { get; private set; }
        protected CompanyBasic SessionCompany { get; private set; }
        protected string Token { get; private set; }

        protected ReturnObject ReturnMessage { get; set; }
        protected InvalidModel InvalidModelObject { get; set; }
        protected sCommonDto BllCommonLogic { get; private set; }
        #endregion

        #region Cookie Details
        /**
         * Cookie Variable Documentation
         * |----------------------|-------------------------|
         * |         Name         |         Purpose         |
         * |----------------------|-------------------------|
         * |.AspNetCore.Session   |Session Person           |
         * |----------------------|-------------------------|
         * |sessionid             |Session Person (b4 login)|
         * |----------------------|-------------------------|
         * |laravel_session       |Session Company          |
         * |----------------------|-------------------------|
         * |ASP.NET_SessionId     |Anti Forgery Token       |
         * |----------------------|-------------------------|
         * |JSESSIONID            |Session- default(asp.net)|
         * |----------------------|-------------------------|
         * |Session.cookie        |[BLANK - can use later]  |
         * |----------------------|-------------------------|
         **/
        #endregion
    }
}