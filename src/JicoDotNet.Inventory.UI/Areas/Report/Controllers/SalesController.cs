using JicoDotNet.Inventory.BusinessLayer.BLL;
using JicoDotNet.Inventory.BusinessLayer.BLL.Report;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class.Report;
using JicoDotNet.Inventory.UI.Report.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JicoDotNet.Inventory.UI.Areas.Report.Controllers
{
    public class SalesController : BaseController
    {
        [SessionAuthenticate]
        public ActionResult Customer()
        {
            try
            {
                CustomerLogic customerLogic = new CustomerLogic(LogicHelper);
                SalesReportModels salesReportModels = new SalesReportModels
                {
                    _customerTypes = customerLogic.TypeGet(),
                    _customers = customerLogic.GetNonRetail()
                };
                return View(salesReportModels);
            }
            catch(Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }
        [HttpPost]
        public PartialViewResult Customer(PCustomerSales customerSales)
        {
            try
            {
                if (customerSales.SearchDate.IsInOneYearRange())
                {
                    SalesReportLogic salesReport = new SalesReportLogic(LogicHelper);
                    SalesReportModels salesReportModels = new SalesReportModels
                    {
                        _rCustomerSales = salesReport.CustomerWise(customerSales)
                    };
                    return PartialView("_PartialCustomerSales", salesReportModels);
                }
                else
                {
                    return PartialView("_PartialDateRangeMismatch", customerSales.SearchDate);
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
                SalesReportModels salesReportModels = new SalesReportModels
                {
                    _productTypes = productLogic.TypeGet(),
                    _products = productLogic.GetOut()
                };
                return View(salesReportModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public PartialViewResult Product(PProductSales productSales)
        {
            try
            {
                if (productSales.SearchDate.IsInOneYearRange())
                {
                    SalesReportLogic salesReport = new SalesReportLogic(LogicHelper);
                    SalesReportModels salesReportModels = new SalesReportModels
                    {
                        _rProductSales = salesReport.ProductWise(productSales)
                    };
                    return PartialView("_PartialProductSales", salesReportModels);
                }
                else
                {
                    return PartialView("_PartialDateRangeMismatch", productSales.SearchDate);
                }
            }
            catch (Exception ex)
            {
                return ErrorLoggingToPartial(ex);
            }
        }
    }
}