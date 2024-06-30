using JicoDotNet.Inventory.BusinessLayer.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JicoDotNet.Inventory.UIControllers
{
    public class LogoutController : BaseController
    {
        public ActionResult Index(string returnUrl)
        {
            SessionValidate();
            if (SessionPerson != null)
                if (!string.IsNullOrEmpty(SessionPerson.UserEmail))
                {
                    TokenManagement token = new TokenManagement(LogicHelper);
                    token.Delete(SessionPerson.UserEmail);
                }
            AbandonSession();
            return RedirectToAction("Index", "Account", new { returnUrl });
        }
    }
}