using JicoDotNet.Inventory.BusinessLayer.BLL;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JicoDotNet.Inventory.UIControllers
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