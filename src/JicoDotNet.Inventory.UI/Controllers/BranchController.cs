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
    public class BranchController : BaseController
    {
        [SessionAuthenticate]
        public ActionResult Index()
        {
            try
            {
                BranchModels branchModels = new BranchModels()
                {
                    _branches = new BranchLogic(LogicHelper).Get(),
                    _State = GenericLogic.State()
                };
                if (!string.IsNullOrEmpty(UrlParameterId))
                {
                    branchModels._branch = branchModels._branches.Where(a => a.BranchId == Convert.ToInt64(UrlParameterId)).FirstOrDefault();
                }
                return View(branchModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public ActionResult Index(Branch branch)
        {
            try
            {
                branch.BranchId = UrlParameterId == null ? 0 : Convert.ToInt64(UrlParameterId);

                #region Data Tracking...
                DataTrackingLogicSet(branch);
                #endregion

                BranchLogic branchLogic = new BranchLogic(LogicHelper);
                ReturnMessage = Convert.ToInt64(branchLogic.Set(branch)) > 0
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
                    BranchLogic branchLogic = new BranchLogic(LogicHelper);
                    long deactivateId = Convert.ToInt64(branchLogic.Deactive(UrlParameterId));
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