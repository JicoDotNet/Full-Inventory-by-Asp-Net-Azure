using System;
using System.Globalization;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;

namespace JicoDotNet.Inventory.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AntiForgeryConfig.CookieName = "ASP.NET_SessionId";

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ModelBinders.Binders.Add(typeof(DateTime), new CustomDateModelBinder());
            ModelBinders.Binders.Add(typeof(DateTime?), new CustomDateModelBinder());
        }
    }

    internal sealed class CustomDateModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (value.RawValue.GetType() != typeof(DateTime) && value.RawValue.GetType() != typeof(DateTime?))
            {
                string displayFormat = bindingContext.ModelMetadata.DisplayFormatString;
                if (string.IsNullOrEmpty(displayFormat))
                    displayFormat = GenericLogic.DateMaskFormat;

                if (!string.IsNullOrEmpty(displayFormat)
                    && value != null
                    && !string.IsNullOrEmpty(value.AttemptedValue))
                {
                    if (DateTime.TryParseExact(value.AttemptedValue,
                        displayFormat,
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out DateTime date))
                    {
                        return date;
                    }
                    else
                    {
                        bindingContext.ModelState.AddModelError(
                            bindingContext.ModelName,
                            string.Format("{0} is an invalid date format(" + GenericLogic.DateMaskFormat + ").", value.AttemptedValue)
                        );
                    }
                }
            }
            return base.BindModel(controllerContext, bindingContext);
        }
    }

    #region Cookie Details
    /** Cookie Variable Documentation
     * |----------------------|-------------------------|
     * |         Name         |         Purpose         |
     * |----------------------|-------------------------|
     * |.AspNetCore.Session   |Session Person           |
     * |----------------------|-------------------------|
     * |.AspNetCore.Company   |Session Company          |
     * |----------------------|-------------------------|
     * |ASP.NET_SessionId     |Anti Forgery Token       |
     * |----------------------|-------------------------|
     * |JSESSIONID            |Session- default(asp.net)|
     * |----------------------|-------------------------|
     **/
    #endregion
}
