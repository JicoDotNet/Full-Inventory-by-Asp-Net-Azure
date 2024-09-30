using Newtonsoft.Json;
using JicoDotNet.Inventory.BusinessLayer.BLL;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using System.Web.Routing;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Common.Auth;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Models;

// ReSharper disable once CheckNamespace
namespace System.Web.Mvc
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false)]
    public class SessionAuthenticate : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            #region RouteValueDictionary for Logout
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
                ISessionCredential sessionCredential = filterContext.RequestContext.HttpContext.GetCookie<SessionCredential>(".AspNetCore.Session");
                string Token;
                if (sessionCredential != null)
                {
                    Token = sessionCredential?.Token;

                    #region Get Session Value & check exists or not
                    if (!string.IsNullOrEmpty(Token))
                    {
                        sessionCredential = new TokenManagement(new CommonRequestDto
                        {
                            NoSqlConnectionString = WebConfigDbConnection.AzureStorage,
                            SqlConnectionString = WebConfigDbConnection.SqlServer
                        }).GetCredential(Token);
                        if (sessionCredential != null)
                        {
                            if (filterContext.RequestContext.HttpContext.GetCookie<CompanyBasic>(".AspNetCore.Company") != null)
                            {
                                base.OnActionExecuting(filterContext);
                                return;
                            }                            
                        }
                    }
                    #endregion
                }
                #endregion
            }
            catch (Exception e)
            {
                LogoutRouteObj.Add("Ex", e);
                filterContext.Result = new RedirectToRouteResult(LogoutRouteObj);
                return;
            }
            filterContext.Result = new RedirectToRouteResult(LogoutRouteObj);
            return;
        }
    }
}