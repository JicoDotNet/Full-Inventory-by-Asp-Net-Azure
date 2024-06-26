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
    public class MeasureController : BaseController
    {
        [SessionAuthenticate]
        public ActionResult Unit()
        {
            try
            {
                UnitOfMeasureModels unitOfMeasureModels = new UnitOfMeasureModels()
                {
                    _unitOfMeasures = new UnitOfMeasureLogic(LogicHelper).Get(),
                };
                if (!string.IsNullOrEmpty(UrlParameterId))
                {
                    unitOfMeasureModels._unitOfMeasure = unitOfMeasureModels._unitOfMeasures
                                        .Where(a => a.UnitOfMeasureId == Convert.ToInt64(UrlParameterId)).FirstOrDefault();
                }
                return View(unitOfMeasureModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public ActionResult Unit(UnitOfMeasure unitOfMeasure)
        {
            try
            {
                unitOfMeasure.UnitOfMeasureId = UrlParameterId == null ? 0 : Convert.ToInt64(UrlParameterId);

                #region Data Tracking...
                DataTrackingLogicSet(unitOfMeasure);
                #endregion

                UnitOfMeasureLogic unitOfMeasureLogic = new UnitOfMeasureLogic(LogicHelper);
                if (Convert.ToInt64(unitOfMeasureLogic.Set(unitOfMeasure)) > 0)
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

                return RedirectToAction("Unit", new { id = string.Empty });
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public JsonResult Deactivate(string Context)
        {
            try
            {
                if (new LoginManagement(LogicHelper).Authenticate(SessionPerson.UserEmail, Context))
                {
                    UnitOfMeasureLogic measureLogic = new UnitOfMeasureLogic(LogicHelper);
                    long deactivateId = Convert.ToInt64(measureLogic.Deactive(UrlParameterId));
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
                return ErrorLoggingToJson(ex);
            }
        }
    }
}