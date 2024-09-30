using JicoDotNet.Inventory.BusinessLayer.BLL;
using System.Web.Mvc;

namespace JicoDotNet.Inventory.UIControllers
{
    public class LogoutController : BaseController
    {
        public ActionResult Index(string returnUrl)
        {
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