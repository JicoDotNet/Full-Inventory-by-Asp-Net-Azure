using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using JicoDotNet.Authentication.Interfaces;
using Newtonsoft.Json;

namespace System.Web.Mvc
{
    public abstract class LicenseController : Controller
    {
        #region Properties
        protected ISessionCredential SessionPerson { get; private set; }
        protected ICommonRequestDto LogicHelper { get; }



        private ActionExecutingContext _filteringContext;
        private ActionExecutedContext _filteredContext;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        protected LicenseController(ICommonRequestDto commonRequest)
        {
            LogicHelper = commonRequest;
        }

        protected void SetSessionPerson(ISessionCredential sessionPerson)
        {
            SessionPerson = sessionPerson;
        }

        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    try
        //    {
        //        #region Set Action Context Varible
        //        this._filteringContext = filterContext;
        //        #endregion

        //        #region Get Route Value
        //        this.ControllerName = _filteringContext.RouteData.Values["controller"].ToString();
        //        this.ActionName = _filteringContext.RouteData.Values["action"].ToString();
        //        this.UrlParameterId = _filteringContext.RouteData.Values["id"]?.ToString();
        //        this.UrlParameterId2 = _filteringContext.RouteData.Values["id2"]?.ToString();
        //        this.UrlParameterId = UrlParameterId == null ? null : UrlIdDecrypt(UrlParameterId);
        //        #endregion

        //        #region Cookie Manage for Session Person
        //        SessionPerson = this.HttpContext.GetCookie<SessionCredential>(".AspNetCore.Session");
        //        #endregion

        //        #region Cookie Manage for Company
        //        SessionCompany = this.HttpContext.GetCookie<CompanyBasic>(".AspNetCore.Company");
        //        #endregion

        //        #region Set Token Global value into CommonDto
        //        LogicHelper.Token = SessionPerson?.Token;
        //        #endregion

        //        #region TempData Manage
        //        ReturnMessage = (ReturnObject)TempData["ReturnMessage"];
        //        InvalidModelObject = (InvalidModel)TempData["InvalidModel"];
        //        #endregion

        //        #region Audit Logger
        //        AuditLogMaintain(new Logger
        //        {
        //            IPAddress = GetRequestedIp(),
        //            DNS = Net.Dns.GetHostName(),
        //            HttpVerbs = _filteringContext.HttpContext.Request.HttpMethod,
        //            Browser = _filteringContext.HttpContext.Request.Browser.Browser,
        //            BrowserType = _filteringContext.HttpContext.Request.Browser.Type,
        //            BrowserVersion = _filteringContext.HttpContext.Request.Browser.Version,
        //            AbsoluteUri = _filteringContext.HttpContext.Request.Url != null
        //                ? _filteringContext.HttpContext.Request.Url.AbsoluteUri
        //                : string.Empty,
        //            MacAddress = _filteringContext.HttpContext.Request.Headers["X-Forwarded-For"],
        //            Controller = ControllerName,
        //            Action = ActionName,
        //            Id = UrlParameterId,
        //            Id2 = UrlParameterId2,
        //            IsMobileDevice = _filteringContext.HttpContext.Request.Browser.IsMobileDevice,
        //            OSType = _filteringContext.HttpContext.Request.Browser.Platform
        //        });
        //        #endregion

        //        #region View Bag for Rezor View
        //        ViewBag.SessionPerson = this.SessionPerson;
        //        ViewBag.Company = this.SessionCompany;
        //        #endregion

        //        base.OnActionExecuting(_filteringContext);
        //    }
        //    catch (Exception e)
        //    {
        //        ErrorLoggingToView(e);
        //        _filteringContext.Result =
        //                            RedirectToAction("Index", "Error", new { returnUrl = _filteringContext.HttpContext.Request.RawUrl, Ex = e });
        //    }
        //}

        //protected override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    #region Set Global Varible
        //    _filteredContext = filterContext;
        //    #endregion

        //    TempData["ReturnMessage"] = ReturnMessage;
        //    TempData["InvalidModel"] = InvalidModelObject;
        //    base.OnActionExecuted(_filteredContext);
        //}

        //protected void AbandonSession()
        //{
        //    Session.Clear();
        //    Session.Abandon();
        //    Session.RemoveAll();

        //    HttpContext.DeleteCookie("ASP.NET_SessionId");
        //    HttpContext.DeleteCookie(".AspNetCore.Session");
        //    HttpContext.DeleteCookie(".AspNetCore.Company");
        //}

        //protected string GetRequestedIp()
        //{
        //    string ip = Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        //    if (string.IsNullOrEmpty(ip))
        //    {
        //        ip = Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        //        if (string.IsNullOrEmpty(ip))
        //            ip = null;
        //    }
        //    if (ip != null && (ip == "::1" || ip.Contains("localhost")))
        //        ip = "127.0.0.1";
        //    return ip;
        //}

        //protected void DataTrackingLogicSet(object objectValue)
        //{
        //    DataTrackingLogic.Set(objectValue, LogicHelper);
        //}

        //private void AuditLogMaintain(ILogger logObject)
        //{
        //    AuditLogLogic.AuditLog(logObject, LogicHelper);
        //}

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

        //protected RedirectToRouteResult ErrorLoggingToView(Exception ex)
        //{
        //    TrackErrorLogging(ex);
        //    TempData["Error"] = new ErrorModels
        //    {
        //        ErrorStatus = 500,
        //        ErrorCode = LogicHelper?.RequestId,
        //        RequestId = LogicHelper?.RequestId,
        //        Message = ex.Message
        //    };
        //    return RedirectToAction("Index", "Error", new { Area = string.Empty, id = LogicHelper?.RequestId });
        //}

        //protected PartialViewResult ErrorLoggingToPartial(Exception ex)
        //{
        //    TrackErrorLogging(ex);
        //    return PartialView("_PartialErrorBlock", new ErrorModels
        //    {
        //        ErrorStatus = 500,
        //        ErrorCode = LogicHelper?.RequestId,
        //        RequestId = LogicHelper?.RequestId,
        //        Message = ex.Message
        //    });
        //}

        //protected JsonResult ErrorLoggingToJson(Exception ex)
        //{
        //    TrackErrorLogging(ex);
        //    JsonReturnModels model = new JsonReturnModels
        //    {
        //        _isSuccess = false,
        //        _returnObject = new ErrorModels
        //        {
        //            ErrorStatus = 500,
        //            ErrorCode = LogicHelper?.RequestId,
        //            RequestId = LogicHelper?.RequestId
        //        },
        //        _redirectURL = Url.Action("Index", "Error", new { Area = string.Empty, id = LogicHelper?.RequestId })
        //    };
        //    return Json(model, JsonRequestBehavior.AllowGet);
        //}

        //protected void ErrorLogging(Exception ex)
        //{
        //    TrackErrorLogging(ex);
        //}

        //private void TrackErrorLogging(Exception ex)
        //{
        //    try
        //    {
        //        string message = string.Empty;
        //        message += Environment.NewLine;
        //        message += "-------------------------------------------------\n";
        //        message += $"Time: {GenericLogic.IstNow:dd/MMM/yyyy hh:mm:ss tt}";
        //        message += Environment.NewLine;
        //        message += $"RequestId: {LogicHelper?.RequestId}";
        //        message += Environment.NewLine;
        //        message += "-------------------------------------------------";
        //        message += Environment.NewLine;
        //        message += $"Message: {ex?.Message}";
        //        message += Environment.NewLine;
        //        message += $"StackTrace: {ex?.StackTrace}";
        //        message += Environment.NewLine;
        //        message += $"Source: {ex?.Source}";
        //        message += Environment.NewLine;
        //        message += $"TargetSite: {ex?.TargetSite}";
        //        message += Environment.NewLine;
        //        message += $"HResult: {ex?.HResult}";
        //        message += Environment.NewLine;
        //        message += $"HttpMethod: {Request?.HttpMethod}";
        //        message += Environment.NewLine;
        //        message +=
        //            $"Path: {Request?.HttpMethod + " :-> /" + ControllerName + "/" + ActionName + "/" + UrlParameterId + "/" + UrlParameterId2}";
        //        message += Environment.NewLine;
        //        message += $"Data: {JsonConvert.SerializeObject(ex?.Data)}";
        //        message += Environment.NewLine;
        //        message += $"InnerException: {JsonConvert.SerializeObject(ex?.InnerException)}";
        //        message += Environment.NewLine;
        //        message += "___________________________________________________________\n";
        //        message += "===========================================================\n";
        //        message += Environment.NewLine;

        //        string path = Server.MapPath("~/ErrorLog");
        //        if (!Directory.Exists(path))
        //        {
        //            Directory.CreateDirectory(path);
        //        }
        //        path += "/Error.log";
        //        using (StreamWriter writer = new StreamWriter(path, true))
        //        {
        //            writer.WriteLine(message);
        //            writer.Close();
        //        }
        //    }
        //    catch
        //    {
        //        // ignored
        //    }
        //}
    }
}
