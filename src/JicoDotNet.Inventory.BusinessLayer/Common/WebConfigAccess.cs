using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace JicoDotNet.Inventory.BusinessLayer.Common
{
    public sealed class WebConfigAccess
    {
        public static string AzureStorageConnection { get { if (WebConfigurationManager.AppSettings["AzureStorageConnection"] != null && !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["AzureStorageConnection"]?.ToString())) { return WebConfigurationManager.AppSettings["AzureStorageConnection"]?.ToString(); } return null; } }
        public static string SqlServerConnection { get { if (WebConfigurationManager.AppSettings["SqlServerConnection"] != null && !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["SqlServerConnection"]?.ToString())) { return WebConfigurationManager.AppSettings["SqlServerConnection"]?.ToString(); } return null; } }
        public static string UserFullName { get { if (WebConfigurationManager.AppSettings["UserFullName"] != null && !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["UserFullName"]?.ToString())) { return WebConfigurationManager.AppSettings["UserFullName"]?.ToString(); } return null; } }
        public static string UserEmail { get { if (WebConfigurationManager.AppSettings["UserEmail"] != null && !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["UserEmail"]?.ToString())) { return WebConfigurationManager.AppSettings["UserEmail"]?.ToString(); } return null; } }
        public static string Password { get { if (WebConfigurationManager.AppSettings["Password"] != null && !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["Password"]?.ToString())) { return WebConfigurationManager.AppSettings["Password"]?.ToString(); } return null; } }
        public static string AllowedStartIP { get { if (WebConfigurationManager.AppSettings["AllowedStartIP"] != null && !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["AllowedStartIP"]?.ToString())) { return WebConfigurationManager.AppSettings["AllowedStartIP"]?.ToString(); } return null; } }
        public static string AllowedEndIP { get { if (WebConfigurationManager.AppSettings["AllowedEndIP"] != null && !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["AllowedEndIP"]?.ToString())) { return WebConfigurationManager.AppSettings["AllowedEndIP"]?.ToString(); } return null; } }
                
        public static string CompanyName { get { if (WebConfigurationManager.AppSettings["CompanyName"] != null && !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["CompanyName"]?.ToString())) { return WebConfigurationManager.AppSettings["CompanyName"]?.ToString(); } return null; } }
        public static string GSTNumber { get { if (WebConfigurationManager.AppSettings["GSTNumber"] != null && !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["GSTNumber"]?.ToString())) { return WebConfigurationManager.AppSettings["GSTNumber"]?.ToString(); } return null; } }

        public static string CompanyEmail { get { if (WebConfigurationManager.AppSettings["CompanyEmail"] != null && !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["CompanyEmail"]?.ToString())) { return WebConfigurationManager.AppSettings["CompanyEmail"]?.ToString(); } return null; } }
        public static string CompanyMobile { get { if (WebConfigurationManager.AppSettings["CompanyMobile"] != null && !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["CompanyMobile"]?.ToString())) { return WebConfigurationManager.AppSettings["CompanyMobile"]?.ToString(); } return null; } }
        public static string CompanyAddress { get { if (WebConfigurationManager.AppSettings["CompanyAddress"] != null && !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["CompanyAddress"]?.ToString())) { return WebConfigurationManager.AppSettings["CompanyAddress"]?.ToString(); } return null; } }
        public static string CompanyPINCode { get { if (WebConfigurationManager.AppSettings["CompanyPINCode"] != null && !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["CompanyPINCode"]?.ToString())) { return WebConfigurationManager.AppSettings["CompanyPINCode"]?.ToString(); } return null; } }
        public static string CompanyCity { get { if (WebConfigurationManager.AppSettings["CompanyCity"] != null && !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["CompanyCity"]?.ToString())) { return WebConfigurationManager.AppSettings["CompanyCity"]?.ToString(); } return null; } }
        public static string CompanyWebsite { get { if (WebConfigurationManager.AppSettings["CompanyWebsite"] != null && !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["CompanyWebsite"]?.ToString())) { return WebConfigurationManager.AppSettings["CompanyWebsite"]?.ToString(); } return null; } }
    }
}