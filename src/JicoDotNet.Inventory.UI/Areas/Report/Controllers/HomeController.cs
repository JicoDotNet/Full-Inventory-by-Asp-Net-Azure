﻿using JicoDotNet.Inventory.BusinessLayer.BLL;
using JicoDotNet.Inventory.UI.Report.Models;
using JicoDotNet.Inventory.Controllers;
using System;
using System.Web.Mvc;

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
                return ErrorLoggingToView(ex);
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
                ErrorLogging(ex);
                return PartialView("_PartialErrorBlock");
            }
        }
    }
}