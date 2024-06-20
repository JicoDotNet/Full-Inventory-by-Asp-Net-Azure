using Newtonsoft.Json;
using JicoDotNet.Inventory.BusinessLayer.BLL;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.BusinessLayer.DTO.Core;
using System.Web.Configuration;
using System.Web.Routing;

namespace System.Web.Mvc
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false)]
    public class SessionAuthenticate : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            #region RouteValueDictionary
            RouteValueDictionary LogoutRouteObj =
                    new RouteValueDictionary
                    {
                        { "action", "Index" },
                        { "controller", "Logout" },
                        { "Area", string.Empty },
                        { "returnUrl", filterContext.HttpContext.Request.RawUrl }
                    };
            #endregion

            try
            {
                #region Cookie Manage For Session
                HttpCookie cookie = filterContext.RequestContext.HttpContext.Request.Cookies[".AspNetCore.Session"];
                string Token;
                if (cookie != null)
                {
                    Token = JsonConvert.DeserializeObject<SessionCredential>(cookie.Value)?.Token;
                }
                else
                {
                    filterContext.Result =
                        new RedirectToRouteResult(LogoutRouteObj);
                    return;
                }
                #endregion

                #region Get Session Value & check exists or not
                SessionCredential _SessionKey;
                if (string.IsNullOrEmpty(Token))
                {
                    filterContext.Result =
                        new RedirectToRouteResult(LogoutRouteObj);
                    return;
                }
                else
                    _SessionKey = new TokenManagement(new CommonRequestDto
                    {
                        NoSqlConnectionString = WebConfigDBConnection.AzureStorage,
                        SqlConnectionString = WebConfigDBConnection.SqlServer
                    }).GetCredential(Token);

                // If session exists
                if (_SessionKey != null)
                {
                    #region Update time of Session Cookie
                    // Increase the expire date of session cookie
                    cookie.Expires = GenericLogic.IstNow.AddDays(1).AddSeconds(-1);
                    filterContext.HttpContext.Response.Cookies.Add(cookie);

                    #endregion
                    base.OnActionExecuting(filterContext);
                }
                else
                {
                    filterContext.Result =
                        new RedirectToRouteResult(LogoutRouteObj);
                    return;
                }
                //filterContext.Result = new ViewResult
                //{
                //    ViewName = "_PartialViewDoNotHaveAccess"                            
                //};
                #endregion

                #region Cookie Manage For Company
                HttpCookie cookiecom = filterContext.RequestContext.HttpContext.Request.Cookies["laravel_session"];
                if (cookiecom != null && !string.IsNullOrEmpty(cookiecom.Value))
                    JsonConvert.DeserializeObject<Company>(cookie.Value);
                else
                {
                    filterContext.Result =
                        new RedirectToRouteResult(new RouteValueDictionary
                                {
                                        { "action", "Choose" },
                                        { "controller", "Company" },
                                        { "Area", string.Empty },
                                        { "returnUrl", filterContext.HttpContext.Request.RawUrl }
                                });
                    return;
                }
                #endregion
            }
            catch (Exception e)
            {
                LogoutRouteObj.Add("Ex", e);
                filterContext.Result =
                            new RedirectToRouteResult(LogoutRouteObj);
                return;
            }
        }
    }
}