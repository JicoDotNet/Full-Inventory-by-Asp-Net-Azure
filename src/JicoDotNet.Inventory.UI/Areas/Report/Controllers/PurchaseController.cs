﻿using JicoDotNet.Inventory.BusinessLayer.BLL;
using JicoDotNet.Inventory.BusinessLayer.BLL.Report;
using JicoDotNet.Inventory.Core.Report;
using JicoDotNet.Inventory.UI.Report.Models;
using System;
using System.Web.Mvc;
using JicoDotNet.Inventory.Controllers;

namespace JicoDotNet.Inventory.UI.Areas.Report.Controllers
{
    public class PurchaseController : BaseController
    {
        [SessionAuthenticate]
        public ActionResult Vendor()
        {
            try
            {
                VendorLogic vendorLogic = new VendorLogic(LogicHelper);
                PurchaseReportModels purchaseReportModels = new PurchaseReportModels
                {
                    _vendorTypes = vendorLogic.TypeGet(),
                    _vendors = vendorLogic.Get()
                };
                return View(purchaseReportModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }
        [HttpPost]
        public PartialViewResult Vendor(RequestVendorPurchaseParam vendorPurchase)
        {
            try
            {
                if (vendorPurchase.SearchDate.IsInOneYearRange)
                {
                    PurchaseReportLogic purchaseReport = new PurchaseReportLogic(LogicHelper);
                    PurchaseReportModels salesReportModels = new PurchaseReportModels
                    {
                        _rVendorPurchase = purchaseReport.VendorWise(vendorPurchase)
                    };
                    return PartialView("_PartialVendorPurchase", salesReportModels);
                }
                else
                {
                    return PartialView("_PartialDateRangeMismatch", vendorPurchase.SearchDate);
                }
            }
            catch (Exception ex)
            {
                return ErrorLoggingToPartial(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult Product()
        {
            try
            {
                ProductLogic productLogic = new ProductLogic(LogicHelper);
                PurchaseReportModels salesReportModels = new PurchaseReportModels
                {
                    _productTypes = productLogic.TypeGet(),
                    _products = productLogic.GetIn()
                };
                return View(salesReportModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public PartialViewResult Product(RequestProductPurchaseParam productPurchase)
        {
            try
            {
                if (productPurchase.SearchDate.IsInOneYearRange)
                {
                    PurchaseReportLogic salesReport = new PurchaseReportLogic(LogicHelper);
                    PurchaseReportModels salesReportModels = new PurchaseReportModels
                    {
                        _rProductPurchase = salesReport.ProductWise(productPurchase)
                    };
                    return PartialView("_PartialProductPurchase", salesReportModels);
                }
                else
                {
                    return PartialView("_PartialDateRangeMismatch", productPurchase.SearchDate);
                }
            }
            catch (Exception ex)
            {
                return ErrorLoggingToPartial(ex);
            }
        }
    }
}