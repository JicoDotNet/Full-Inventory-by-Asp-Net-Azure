using JicoDotNet.Inventory.BusinessLayer.BLL;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JicoDotNet.Inventory.Core.Models;

namespace JicoDotNet.Inventory.UIControllers
{
    public class CustomerController : BaseController
    {
        #region Customer Type
        [SessionAuthenticate]
        public ActionResult Type()
        {
            try
            {
                CustomerModels customerModels = new CustomerModels()
                {
                    _customerTypes = new CustomerLogic(LogicHelper).TypeGet()
                };
                if (!string.IsNullOrEmpty(UrlParameterId))
                {
                    customerModels._customerType = customerModels._customerTypes.Where(a => a.CustomerTypeId == Convert.ToInt64(UrlParameterId)).FirstOrDefault();
                }
                return View(customerModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public ActionResult Type(CustomerType customerType)
        {
            try
            {
                customerType.CustomerTypeId = UrlParameterId == null ? 0 : Convert.ToInt64(UrlParameterId);

                #region Data Tracking...
                DataTrackingLogicSet(customerType);
                #endregion

                CustomerLogic customerLogic = new CustomerLogic(LogicHelper);
                if (Convert.ToInt64(customerLogic.TypeSet(customerType)) > 0)
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Message = "Success",
                        Status = true
                    };
                }
                else
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Message = "Unsuccess",
                        Status = false
                    };
                }

                return RedirectToAction("Type", new { id = string.Empty });
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public JsonResult TypeDeactivate(string Context)
        {
            try
            {
                if (new LoginManagement(LogicHelper).Authenticate(SessionPerson.UserEmail, Context))
                {
                    CustomerLogic customerTypeLogic = new CustomerLogic(LogicHelper);
                    if (Convert.ToInt64(customerTypeLogic.TypeDeactive(UrlParameterId)) > 0)
                        return Json(UrlParameterId, JsonRequestBehavior.AllowGet);
                    else
                        return Json(0, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(-1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToJson(ex);
            }
        }
        #endregion

        #region Customer
        [SessionAuthenticate]
        public ActionResult Index()
        {
            try
            {
                CustomerLogic customerLogic = new CustomerLogic(LogicHelper);
                CustomerModels customerModels = new CustomerModels()
                {
                    _customers = customerLogic.GetNonRetail(),
                    _customerTypes = customerLogic.TypeGet(true).ToList(),
                    _companyType = GenericLogic.CompanyType(),
                    _state = GenericLogic.State(),
                    _YesNo = GenericLogic.YesNo(),
                    IsRetail = false
                };
                if (!string.IsNullOrEmpty(UrlParameterId))
                {
                    customerModels._customer = customerModels._customers.Where(a => a.CustomerId == Convert.ToInt64(UrlParameterId)).FirstOrDefault();
                }
                return View(customerModels);
            }
            catch (Exception ex)
            {
                ErrorLoggingToView(ex);
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        [SessionAuthenticate]
        public ActionResult Index(Customer customer)
        {
            try
            {
                customer.CustomerId = UrlParameterId == null ? 0 : Convert.ToInt64(UrlParameterId);

                #region Data Tracking...
                DataTrackingLogicSet(customer);
                #endregion

                CustomerLogic customerLogic = new CustomerLogic(LogicHelper);
                if (Convert.ToInt64(customerLogic.Set(customer)) > 0)
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Message = "Success",
                        Status = true
                    };
                }
                else
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Message = "Unsuccess",
                        Status = false
                    };
                }

                return RedirectToAction("Index", new { id = string.Empty });
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        [SessionAuthenticate]
        public JsonResult Deactivate(string Context)
        {
            try
            {
                if (new LoginManagement(LogicHelper).Authenticate(SessionPerson.UserEmail, Context))
                {
                    CustomerLogic customerLogic = new CustomerLogic(LogicHelper);
                    long deactivateId = Convert.ToInt64(customerLogic.Deactive(UrlParameterId));
                    return Json(new JsonReturnModels
                    {
                        _isSuccess = true,
                        _returnObject = deactivateId > 0 ? UrlParameterId : "0"
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new JsonReturnModels
                    {
                        _isSuccess = true,
                        _returnObject = "-1"
                    }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ErrorLoggingToView(ex);
                throw ex;
            }
        }

        [SessionAuthenticate]
        public ActionResult Retail()
        {
            try
            {
                CustomerLogic customerLogic = new CustomerLogic(LogicHelper);
                CustomerModels customerModels = new CustomerModels()
                {
                    _customers = customerLogic.GetRetail(),
                    _customerTypes = customerLogic.TypeGet(true).ToList(),
                    _companyType = GenericLogic.CompanyType(),
                    _state = GenericLogic.State(),
                    _YesNo = GenericLogic.YesNo(),
                    IsRetail = true
                };
                if (!string.IsNullOrEmpty(UrlParameterId))
                {
                    customerModels._customer = customerModels._customers.Where(a => a.CustomerId == Convert.ToInt64(UrlParameterId)).FirstOrDefault();
                }
                return View("Index", customerModels);
            }
            catch (Exception ex)
            {
                ErrorLoggingToView(ex);
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        [SessionAuthenticate]
        public ActionResult Retail(Customer customer)
        {
            try
            {
                customer.CustomerId = UrlParameterId == null ? 0 : Convert.ToInt64(UrlParameterId);

                #region Data Tracking...
                DataTrackingLogicSet(customer);
                #endregion

                CustomerLogic customerLogic = new CustomerLogic(LogicHelper);
                if (Convert.ToInt64(customerLogic.Set(customer)) > 0)
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Message = "Success",
                        Status = true
                    };
                }
                else
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Message = "Unsuccess",
                        Status = false
                    };
                }
                return RedirectToAction("Retail", new { id = string.Empty });
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }
        #endregion
    }
}