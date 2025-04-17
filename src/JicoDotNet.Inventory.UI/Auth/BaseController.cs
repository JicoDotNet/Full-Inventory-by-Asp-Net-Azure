using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Models;
using JicoDotNet.Inventory.Logging;
using JicoDotNet.Inventory.UI.Models;
using System.Text;
using System.Web.Mvc;
using System;
using System.Web;
using JicoDotNet.Inventory.Core.Common;

namespace JicoDotNet.Inventory.Controllers
{
    public abstract class BaseController : Controller
    {
        #region Properties
        protected string ControllerName { get; private set; }
        protected string ActionName { get; private set; }
        protected string UrlParameterId { get; set; }
        protected string UrlParameterId2 { get; set; }

        protected ICompanyBasic SessionCompany { get; private set; }
        protected IReturnObject ReturnMessage { get; set; }
        protected IInvalidModel InvalidModelObject { get; set; }
        public ISessionCredential SessionPerson { get; private set; }
        protected ICommonLogicHelper LogicHelper { get; private set; }


        private ActionExecutingContext _filteringContext;
        private ActionExecutedContext _filteredContext;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        protected BaseController() 
        {
            LogicHelper = new CommonLogicHelper()
            {
                SqlConnectionString = WebConfigDbConnection.SqlServer,
                NoSqlConnectionString = WebConfigDbConnection.AzureStorage,
                RequestId = Guid.NewGuid().ToString().Replace("-", "").ToUpper(),
                AppSettings = new WebConfigAppSettingsAccess(),
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
                this.ControllerName = _filteringContext.RouteData.Values["controller"].ToString();
                this.ActionName = _filteringContext.RouteData.Values["action"].ToString();
                this.UrlParameterId = _filteringContext.RouteData.Values["id"]?.ToString();
                this.UrlParameterId2 = _filteringContext.RouteData.Values["id2"]?.ToString();
                this.UrlParameterId = UrlParameterId == null ? null : UrlIdDecrypt(UrlParameterId);
                #endregion

                #region Cookie Manage for Session Person
                SetSessionPerson(HttpContext);
                #endregion

                #region Cookie Manage for Company
                SessionCompany = this.HttpContext.GetCookie<CompanyBasic>(".AspNetCore.Company");
                #endregion

                #region Set Token Global value into CommonDto
                LogicHelper.Token = SessionPerson?.Token;
                #endregion

                #region TempData Manage
                ReturnMessage = (ReturnObject)TempData["ReturnMessage"];
                InvalidModelObject = (InvalidModel)TempData["InvalidModel"];
                #endregion

                #region Audit Logger
                AuditLogMaintain(new Logger
                {
                    IPAddress = GetRequestedIp(),
                    DNS = System.Net.Dns.GetHostName(),
                    HttpVerbs = _filteringContext.HttpContext.Request.HttpMethod,
                    Browser = _filteringContext.HttpContext.Request.Browser.Browser,
                    BrowserType = _filteringContext.HttpContext.Request.Browser.Type,
                    BrowserVersion = _filteringContext.HttpContext.Request.Browser.Version,
                    AbsoluteUri = _filteringContext.HttpContext.Request.Url != null
                        ? _filteringContext.HttpContext.Request.Url.AbsoluteUri
                        : string.Empty,
                    MacAddress = _filteringContext.HttpContext.Request.Headers["X-Forwarded-For"],
                    Controller = ControllerName,
                    Action = ActionName,
                    Id = UrlParameterId,
                    Id2 = UrlParameterId2,
                    IsMobileDevice = _filteringContext.HttpContext.Request.Browser.IsMobileDevice,
                    OSType = _filteringContext.HttpContext.Request.Browser.Platform
                });
                #endregion

                #region View Bag for Rezor View
                ViewBag.SessionPerson = this.SessionPerson;
                ViewBag.Company = this.SessionCompany;
                #endregion

                base.OnActionExecuting(_filteringContext);
            }
            catch (Exception e)
            {
                ErrorLogging(e);
                _filteringContext.Result =
                                    RedirectToAction("Index", "Error", new { returnUrl = _filteringContext.HttpContext.Request.RawUrl, Ex = e });
            }
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            #region Set Global Varible
            _filteredContext = filterContext;
            #endregion

            TempData["ReturnMessage"] = ReturnMessage;
            TempData["InvalidModel"] = InvalidModelObject;
            base.OnActionExecuted(_filteredContext);
        }

        protected void SetSessionPerson(HttpContextBase httpContext)
        {
            #region Cookie Manage for Session Person
            ISessionToken sessionToken = httpContext.GetCookie<SessionToken>(".AspNetCore.Session");
            if (sessionToken != null && sessionToken.Key == "InventoryApp")
            {
                SessionPerson = sessionToken;
            }

            #endregion
        }

        protected void SetSessionCookie(ISessionCredential credential)
        {
            SessionToken sessionToken = new SessionToken
            {
                Key = "InventoryApp",
                Token = credential.Token,
                TokenDate = credential.TokenDate,
                UserEmail = credential.UserEmail,
                UserFullName = credential.UserFullName,
            };
            HttpContext.SetCookie(".AspNetCore.Session", sessionToken);
        }   

        protected void AbandonSession()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            HttpContext.DeleteCookie("ASP.NET_SessionId");
            HttpContext.DeleteCookie(".AspNetCore.Session");
            HttpContext.DeleteCookie(".AspNetCore.Company");
        }

        protected string GetRequestedIp()
        {
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                if (string.IsNullOrEmpty(ip))
                    ip = null;
            }
            if (ip != null && (ip == "::1" || ip.Contains("localhost")))
                ip = "127.0.0.1";
            return ip;
        }

        protected void DataTrackingLogicSet(object objectValue)
        {
            DataTrackingLogic.Set(objectValue, LogicHelper);
        }

        private void AuditLogMaintain(ILogger logObject)
        {
            AuditLogLogic.AuditLog(logObject, LogicHelper);
        }

        protected string UrlIdDecrypt(string urlId)
        {
            try
            {
                return !string.IsNullOrEmpty(urlId) ? Encoding.ASCII.GetString(Convert.FromBase64String(urlId)) : null;
            }
            catch (FormatException)
            {
                return urlId;
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
                byte[] idBytes = Encoding.UTF8.GetBytes(id.ToString());
                return isEscape ? Uri.EscapeDataString(Convert.ToBase64String(idBytes)) : Convert.ToBase64String(idBytes);
            }
            return null;
        }

        protected RedirectToRouteResult ErrorLoggingToView(Exception ex)
        {
            ErrorLogging(ex);
            TempData["Error"] = new ErrorModels
            {
                ErrorStatus = 500,
                ErrorCode = LogicHelper?.RequestId,
                RequestId = LogicHelper?.RequestId,
                Message = ex.Message
            };
            return RedirectToAction("Index", "Error", new { Area = string.Empty, id = LogicHelper?.RequestId });
        }

        protected PartialViewResult ErrorLoggingToPartial(Exception ex)
        {
            ErrorLogging(ex);
            return PartialView("_PartialErrorBlock", new ErrorModels
            {
                ErrorStatus = 500,
                ErrorCode = LogicHelper?.RequestId,
                RequestId = LogicHelper?.RequestId,
                Message = ex.Message
            });
        }

        protected JsonResult ErrorLoggingToJson(Exception ex)
        {
            ErrorLogging(ex);
            JsonReturnModels model = new JsonReturnModels
            {
                _isSuccess = false,
                _returnObject = new ErrorModels
                {
                    ErrorStatus = 500,
                    ErrorCode = LogicHelper?.RequestId,
                    RequestId = LogicHelper?.RequestId
                },
                _redirectURL = Url.Action("Index", "Error", new { Area = string.Empty, id = LogicHelper?.RequestId })
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        protected void ErrorLogging(Exception ex)
        {
            try
            {
                ErrorLogLogic.Logging(new ErrorLog()
                {
                    Exception = ex,
                    ActionName = ActionName,
                    ControllerName = ControllerName,
                    FolderPath = Server.MapPath("~/ErrorLog"),
                    HttpMethod = Request?.HttpMethod,
                    RequestId = LogicHelper?.RequestId,
                    UrlParameterId = UrlParameterId,
                    UrlParameterId2 = UrlParameterId2
                });
            }
            catch
            {
                // ignored
            }
        }
    }
}