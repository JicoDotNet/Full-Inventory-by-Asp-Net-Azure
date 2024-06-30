using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JicoDotNet.Inventory.BusinessLayer.BLL;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.UI.Models;
using JicoDotNet.Inventory.UI.Report.Models;

namespace JicoDotNet.Inventory.UI.Areas.Report.Controllers
{
    public class HomeController : BaseController
    {
        [SessionAuthenticate]
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ErrorLoggingToView(ex);
                return RedirectToAction("Index", "Error", new { Area = string.Empty });
            }
        }

        [HttpGet]
        public PartialViewResult RpMaster()
        {
            try
            {
                return PartialView("_PartialRpMaster", new ReportModels
                {
                    _reportMasterCount = new MasterDS(LogicHelper).CountReport(),
                    _config = new ConfigarationManager(LogicHelper).GetConfig()
                });
            }
            catch (Exception ex)
            {
                ErrorLoggingToView(ex);
                return PartialView("_PartialErrorBlock");
            }
        }
    }
}