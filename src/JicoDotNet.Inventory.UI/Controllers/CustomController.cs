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
    public class CustomController : BaseController
    {
        [SessionAuthenticate]
        public ActionResult Index()
        {
            try
            {
                CustomPropertyLogic propertyLogic = new CustomPropertyLogic(LogicHelper);
                List<CustomProperty> customProperties = propertyLogic.GetMasters();
                Dictionary<ECustomPropertyFor, int> pairs = new Dictionary<ECustomPropertyFor, int>();
                foreach (ECustomPropertyFor customPropertyFor in Enum.GetValues(typeof(ECustomPropertyFor)))
                {
                    if (customPropertyFor > 0)
                        pairs.Add(customPropertyFor, customProperties.Where(a => a.PropertyFor == customPropertyFor.ToString()).Count());
                }
                CustomPropertyModels models = new CustomPropertyModels
                {
                    _propertiesCount = pairs
                };
                return View(models);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult Property()
        {
            try
            {
                if (!string.IsNullOrEmpty(UrlParameterId))
                {
                    Enum.TryParse(UrlParameterId, true, out ECustomPropertyFor customPropertyFor);
                    if (customPropertyFor != ECustomPropertyFor.None)
                    {
                        CustomPropertyModels models = new CustomPropertyModels
                        {
                            _customPropertyFor = customPropertyFor
                        };
                        if (!string.IsNullOrEmpty(UrlParameterId2))
                        {
                            models._rowKey = UrlParameterId2;
                        }
                        return View(models);
                    }
                }
                return RedirectToAction("Index", "Custom");
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        public PartialViewResult PartialProperty()
        {
            try
            {
                if (!string.IsNullOrEmpty(UrlParameterId))
                {
                    Enum.TryParse(UrlParameterId, true, out ECustomPropertyFor customPropertyFor);
                    if (customPropertyFor != ECustomPropertyFor.None)
                    {
                        CustomPropertyLogic propertyLogic = new CustomPropertyLogic(LogicHelper);
                        CustomPropertyModels models = new CustomPropertyModels
                        {
                            _YesNo = GenericLogic.YesNo(),
                            _dataType = GenericLogic.DataType(),
                            _customPropertyFor = customPropertyFor,
                            _customProperties = propertyLogic.GetMaster(customPropertyFor)
                        };
                        if (!string.IsNullOrEmpty(UrlParameterId2))
                            models._customProperty = propertyLogic.GetMaster(customPropertyFor, UrlParameterId2);
                        return PartialView("_PartialProperty", models);
                    }
                }
                return PartialView("Index");
            }
            catch (Exception ex)
            {
                return ErrorLoggingToPartial(ex);
            }
        }

        [HttpPost]
        public ActionResult Property(CustomProperty customProperty)
        {
            try
            {
                if (!string.IsNullOrEmpty(UrlParameterId))
                {
                    Enum.TryParse(UrlParameterId, true, out ECustomPropertyFor customPropertyFor);
                    
                    CustomPropertyLogic propertyLogic = new CustomPropertyLogic(LogicHelper);
                    if (!string.IsNullOrEmpty(UrlParameterId2))
                        customProperty.RowKey = UrlParameterId2;
                    propertyLogic.SetMaster(customProperty, customPropertyFor);
                    return RedirectToAction("Property", new { id = UrlIdEncrypt(customPropertyFor, false), id2 = string.Empty });
                }
                return RedirectToAction("Index", "Custom");
            }
            catch (Exception ex)
            {
                return ErrorLoggingToPartial(ex);
            }
        }

        [HttpPost]
        public JsonResult PropertyDeactivate(string Context)
        {
            try
            {
                if (!string.IsNullOrEmpty(UrlParameterId))
                {
                    Enum.TryParse(UrlParameterId, true, out ECustomPropertyFor customPropertyFor);
                    if (new LoginManagement(LogicHelper).Authenticate(SessionPerson.UserEmail, Context))
                    {
                        CustomPropertyLogic propertyLogic = new CustomPropertyLogic(LogicHelper);
                        bool deactivated = propertyLogic.DeactiveMaster(customPropertyFor, UrlParameterId2);
                        return Json(new JsonReturnModels
                        {
                            _isSuccess = true,
                            _returnObject = deactivated ? UrlParameterId : "0"
                        }, JsonRequestBehavior.AllowGet);
                    }                        
                }
                return Json(new JsonReturnModels
                {
                    _isSuccess = true,
                    _returnObject = "-1"
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToJson(ex);
            }
        }

        [HttpGet]
        public PartialViewResult Bind()
        {
            try
            {
                if (!string.IsNullOrEmpty(UrlParameterId))
                {
                    Enum.TryParse(UrlParameterId, true, out ECustomPropertyFor customPropertyFor);
                    if (customPropertyFor != ECustomPropertyFor.None)
                    {
                        CustomPropertyLogic propertyLogic = new CustomPropertyLogic(LogicHelper);
                        CustomPropertyModels models = new CustomPropertyModels
                        {
                            _YesNo = GenericLogic.YesNo(),
                            _customPropertyFor = customPropertyFor,
                            _customProperties = propertyLogic.GetMaster(customPropertyFor)
                        };
                        return PartialView("_PartialBind", models);
                    }
                }
                return PartialView("_PartialBind", new CustomPropertyModels { _customProperties = new List<CustomProperty>() });
            }
            catch (Exception ex)
            {
                return ErrorLoggingToPartial(ex);
            }
        }
    }
}