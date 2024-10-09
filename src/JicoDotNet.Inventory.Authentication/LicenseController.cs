using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Mvc;
using JicoDotNet.Authentication.Entities;
using JicoDotNet.Authentication.Interfaces;
using JicoDotNet.Inventory.Authentication;
using Newtonsoft.Json;

namespace System.Web.Mvc
{
    public abstract class LicenseController : Controller
    {
        #region Properties
        protected ISessionCredential SessionPerson { get; private set; }
        protected ICommonLogicHelper LogicHelper { get; }

        private string _licenseFilePath;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        protected LicenseController(ICommonLogicHelper commonRequest)
        {
            CommonLogicHelper commonLogicHelper = (CommonLogicHelper)commonRequest;            
            commonLogicHelper.SetSchema("[SingleIB]");
            LogicHelper = commonLogicHelper;
#if !DEBUG
            _licenseFilePath = HostingEnvironment.MapPath("~/license.lic");
#endif
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            AuthLicense lic= LoadLicense();
            base.OnActionExecuting(filterContext);

            //filterContext.Result = Redirect("https://github.com/JicoDotNet/Full-Inventory-by-Asp-Net-Azure/actions");
        }

        protected void SetSessionPerson(ISessionCredential sessionPerson)
        {
            SessionPerson = sessionPerson;
        }

        private AuthLicense LoadLicense()
        {
            string lines;
            using (StreamReader reader = new StreamReader(_licenseFilePath))
            {
                lines = reader.ReadLine();
            }

            return JsonConvert.DeserializeObject<AuthLicense>(lines);
        }

        public bool Validate(AuthLicense license)
        {
            if (license.ExpiryDate < DateTime.Now)
            {
                return false; // License expired
            }

            // Additional license validation (e.g., license key validation, feature checks)
            return true;
        }
    }
}
