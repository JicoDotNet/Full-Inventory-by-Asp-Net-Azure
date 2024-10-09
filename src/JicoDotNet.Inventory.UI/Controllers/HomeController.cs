using JicoDotNet.Inventory.BusinessLayer.BLL;
using JicoDotNet.Inventory.UI.Models;
using System;
using System.Web.Mvc;
using JicoDotNet.Inventory.Controllers;
using JicoDotNet.Validator;
using Newtonsoft.Json;

namespace JicoDotNet.Inventory.UI.Controllers
{
    public class HomeController : BaseController
    {
        [SessionAuthenticate]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult HmMaster()
        {
            try
            {
                return PartialView("_PartialHmMaster", new HomeModels
                {
                    _homeMasterCount = new MasterDS(LogicHelper).CountHome(),
                });
            }
            catch (Exception ex)
            {
                return ErrorLoggingToPartial(ex);
            }
        }
    }
}