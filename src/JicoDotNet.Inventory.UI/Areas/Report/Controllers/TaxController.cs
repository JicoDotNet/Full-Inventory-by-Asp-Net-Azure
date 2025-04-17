using JicoDotNet.Inventory.BusinessLayer.BLL.Report;
using JicoDotNet.Inventory.Core.Report;
using JicoDotNet.Inventory.UI.Report.Models;
using System;
using System.Web.Mvc;
using JicoDotNet.Inventory.Controllers;

namespace JicoDotNet.Inventory.UI.Areas.Report.Controllers
{
    public class TaxController : BaseController
    {
        // Sales GST = Output
        [SessionAuthenticate]
        public ActionResult Output()
        {
            try
            {
                TaxReportModels taxReportModels = new TaxReportModels
                {
                    _companyBasic = SessionCompany
                };
                if (SessionCompany.IsGSTRegistered)
                {
                    return View();
                }
                else
                {
                    return View("_ViewNotApplicable", taxReportModels);
                }
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public PartialViewResult Output(RequestTaxParam gst)
        {
            try
            {
                if (gst.SearchDate.IsInOneYearRange)
                {
                    TaxReportLogic taxReport = new TaxReportLogic(LogicHelper);
                    TaxReportModels taxReportModels = new TaxReportModels
                    {
                        _rGSTOut = taxReport.GSTOutputs(gst)
                    };
                    return PartialView("_PartialGstOutput", taxReportModels);
                }
                else
                {
                    return PartialView("_PartialDateRangeMismatch", gst.SearchDate);
                }
            }
            catch (Exception ex)
            {
                return ErrorLoggingToPartial(ex);
            }
        }
        // Purchase GST = Input
        [SessionAuthenticate]
        public ActionResult Input()
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

        [HttpPost]
        public PartialViewResult Input(RequestTaxParam gst)
        {
            try
            {
                if (gst.SearchDate.IsInOneYearRange)
                {
                    TaxReportLogic taxReport = new TaxReportLogic(LogicHelper);
                    TaxReportModels taxReportModels = new TaxReportModels
                    {
                        _rGSTIn = taxReport.GSTInputs(gst)
                    };
                    return PartialView("_PartialGstInput", taxReportModels);
                }
                else
                {
                    return PartialView("_PartialDateRangeMismatch", gst.SearchDate);
                }
            }
            catch (Exception ex)
            {
                return ErrorLoggingToPartial(ex);
            }
        }
    }
}