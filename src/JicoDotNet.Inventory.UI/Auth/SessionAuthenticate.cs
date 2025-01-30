using JicoDotNet.Inventory.BusinessLayer.BLL;
using JicoDotNet.Inventory.Core.Models;
using System.Web.Routing;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Entities;

namespace System.Web.Mvc
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false)]
    public class SessionAuthenticate : ActionFilterAttribute
    {
        private ISessionCredential sessionCredential { get; set; }

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
                base.OnActionExecuting(filterContext);

                #region Cookie Manage For Session
                sessionCredential = filterContext.RequestContext.HttpContext.GetCookie<SessionToken>(".AspNetCore.Session");
                if (sessionCredential != null && string.IsNullOrEmpty(sessionCredential.Token))
                {
                    sessionCredential = null;
                }
                #endregion

                #region Get Session Value & check exists or not
                if (sessionCredential != null && !string.IsNullOrEmpty(sessionCredential.Token))
                {
                    if (new TokenManager(new CommonLogicHelper
                    {
                        NoSqlConnectionString = WebConfigDbConnection.AzureStorage,
                        SqlConnectionString = WebConfigDbConnection.SqlServer
                    }).IsValid(sessionCredential.Token, sessionCredential.UserEmail))
                    {
                        if (filterContext.RequestContext.HttpContext.GetCookie<CompanyBasic>(".AspNetCore.Company") != null)
                        {                            
                            return;
                        }
                    }
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