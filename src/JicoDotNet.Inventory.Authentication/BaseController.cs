using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using JicoDotNet.Authentication.Entities;
using JicoDotNet.Authentication.Interfaces;
using Newtonsoft.Json;

namespace System.Web.Mvc
{
    public abstract class LicenseController : Controller
    {
        #region Properties
        protected ISessionCredential SessionPerson { get; private set; }
        protected ICommonLogicHelper LogicHelper { get; }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        protected LicenseController(ICommonLogicHelper commonRequest)
        {
            CommonLogicHelper commonLogicHelper = (CommonLogicHelper)commonRequest;            
            commonLogicHelper.SetSchema("[SingleIB]");
            LogicHelper = commonLogicHelper;
        }

        protected void SetSessionPerson(ISessionCredential sessionPerson)
        {
            SessionPerson = sessionPerson;
        }
    }
}
