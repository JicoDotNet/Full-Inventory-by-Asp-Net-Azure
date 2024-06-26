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
    public class WarehouseController : BaseController
    {
        [SessionAuthenticate]
        public ActionResult Index()
        {
            try
            {
                WareHouseModels wareHouseModels = new WareHouseModels()
                {
                    _wareHouses = new WareHouseLogic(LogicHelper).Get(),
                    _branches = new BranchLogic(LogicHelper).Get().Where(a => a.IsActive).ToList(),
                    
                };
                wareHouseModels._isRetailEligible = true;

                if (!string.IsNullOrEmpty(UrlParameterId))
                {
                    wareHouseModels._wareHouse = wareHouseModels._wareHouses.Where(a => a.WareHouseId == Convert.ToInt64(UrlParameterId)).FirstOrDefault();
                }
                return View(wareHouseModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public ActionResult Index(WareHouse wareHouse)
        {
            try
            {
                wareHouse.WareHouseId = UrlParameterId == null ? 0 : Convert.ToInt64(UrlParameterId);

                #region Data Tracking...
                DataTrackingLogicSet(wareHouse);
                #endregion

                WareHouseLogic wareHouseLogic = new WareHouseLogic(LogicHelper);
                ReturnMessage = Convert.ToInt64(wareHouseLogic.Set(wareHouse)) > 0
                    ? new ReturnObject()
                    {
                        Message = "Success",
                        Status = true
                    }
                    : new ReturnObject()
                    {
                        Message = "Unsuccess",
                        Status = false
                    };

                return RedirectToAction("Index", new { id = string.Empty });
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
                    WareHouseLogic wareHouseLogic = new WareHouseLogic(LogicHelper);
                    long deactivateId = Convert.ToInt64(wareHouseLogic.Deactive(UrlParameterId));
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
