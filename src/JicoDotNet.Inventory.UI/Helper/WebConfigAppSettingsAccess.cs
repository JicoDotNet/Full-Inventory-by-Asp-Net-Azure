using JicoDotNet.Inventory.Core.Common;
using System.Web.Configuration;

namespace System.Web.Mvc
{
    public sealed class WebConfigAppSettingsAccess : WebConfigAppSettings
    {
        public WebConfigAppSettingsAccess()
        {
            UserFullName = (WebConfigurationManager.AppSettings["UserFullName"] != null && !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["UserFullName"]?.ToString())) ? WebConfigurationManager.AppSettings["UserFullName"]?.ToString() : null;
            UserEmail = (WebConfigurationManager.AppSettings["UserEmail"] != null && !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["UserEmail"]?.ToString())) ? WebConfigurationManager.AppSettings["UserEmail"]?.ToString() : null;
            Password = (WebConfigurationManager.AppSettings["Password"] != null && !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["Password"]?.ToString())) ? WebConfigurationManager.AppSettings["Password"]?.ToString() : null;
            AllowedStartIP = (WebConfigurationManager.AppSettings["AllowedStartIP"] != null && !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["AllowedStartIP"]?.ToString())) ? WebConfigurationManager.AppSettings["AllowedStartIP"]?.ToString() : null;
            AllowedEndIP = (WebConfigurationManager.AppSettings["AllowedEndIP"] != null && !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["AllowedEndIP"]?.ToString())) ? WebConfigurationManager.AppSettings["AllowedEndIP"]?.ToString() : null;

            CompanyName = (WebConfigurationManager.AppSettings["CompanyName"] != null && !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["CompanyName"]?.ToString())) ? WebConfigurationManager.AppSettings["CompanyName"]?.ToString() : null;
            GSTNumber = (WebConfigurationManager.AppSettings["GSTNumber"] != null && !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["GSTNumber"]?.ToString())) ? WebConfigurationManager.AppSettings["GSTNumber"]?.ToString() : null;

            CompanyEmail = (WebConfigurationManager.AppSettings["CompanyEmail"] != null && !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["CompanyEmail"]?.ToString())) ? WebConfigurationManager.AppSettings["CompanyEmail"]?.ToString() : null;
            CompanyMobile = (WebConfigurationManager.AppSettings["CompanyMobile"] != null && !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["CompanyMobile"]?.ToString())) ? WebConfigurationManager.AppSettings["CompanyMobile"]?.ToString() : null;
            CompanyAddress = (WebConfigurationManager.AppSettings["CompanyAddress"] != null && !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["CompanyAddress"]?.ToString())) ? WebConfigurationManager.AppSettings["CompanyAddress"]?.ToString() : null;
            CompanyPINCode = (WebConfigurationManager.AppSettings["CompanyPINCode"] != null && !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["CompanyPINCode"]?.ToString())) ? WebConfigurationManager.AppSettings["CompanyPINCode"]?.ToString() : null;
            CompanyCity = (WebConfigurationManager.AppSettings["CompanyCity"] != null && !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["CompanyCity"]?.ToString())) ? WebConfigurationManager.AppSettings["CompanyCity"]?.ToString() : null;
            CompanyWebsite = (WebConfigurationManager.AppSettings["CompanyWebsite"] != null && !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["CompanyWebsite"]?.ToString())) ? WebConfigurationManager.AppSettings["CompanyWebsite"]?.ToString() : null;
        }
    }
}