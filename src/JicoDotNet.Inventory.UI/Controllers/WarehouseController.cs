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
                    _wareHouses = new WareHouseLogic(BllCommonLogic).Get(),
                    _branches = new BranchLogic(BllCommonLogic).Get().Where(a => a.IsActive).ToList(),
                    
                };
                wareHouseModels._isRetailEligible = true;

                if (!string.IsNullOrEmpty(id))
                {
                    wareHouseModels._wareHouse = wareHouseModels._wareHouses.Where(a => a.WareHouseId == Convert.ToInt64(id)).FirstOrDefault();
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
                wareHouse.WareHouseId = id == null ? 0 : Convert.ToInt64(id);

                #region Data Tracking...
                DataTrackingLogicSet(wareHouse);
                #endregion

                WareHouseLogic wareHouseLogic = new WareHouseLogic(BllCommonLogic);
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
                if (new LoginManagement(BllCommonLogic).Authenticate(SessionPerson.UserEmail, Context))
                {
                    WareHouseLogic wareHouseLogic = new WareHouseLogic(BllCommonLogic);
                    long deactivateId = Convert.ToInt64(wareHouseLogic.Deactive(id));
                    return Json(new JsonReturnModels
                    {
                        _isSuccess = true,
                        _returnObject = deactivateId > 0 ? id : "0"
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
