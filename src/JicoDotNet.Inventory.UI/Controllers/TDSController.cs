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
    public class TDSController : BaseController
    {
        [SessionAuthenticate]
        public ActionResult Pay()
        {
            try
            {
                TDSModels taxingModels = new TDSModels
                {
                    _tDSPays = new TaxingLogic(LogicHelper).GetTDSOuts(),
                    _config = new ConfigarationManager(LogicHelper).GetConfig(),
                    _state = GenericLogic.State()
                };
                return View(taxingModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public ActionResult Pay(TDSPay tDSPay)
        {
            try
            {
                if (tDSPay.TDSPayId == Convert.ToInt64(id))
                {
                    if (!string.IsNullOrEmpty(new TaxingLogic(LogicHelper).SetTDSOut(tDSPay)))
                    {
                        ReturnMessage = new ReturnObject
                        {
                            Status = true,
                            Message = "TDS Payment record successfully inserted!!"
                        };
                        return RedirectToAction("Pay", new { id = string.Empty });
                    }
                }
                ReturnMessage = new ReturnObject
                {
                    Status = false,
                    Message = "Something goes wrong!!"
                };
                return RedirectToAction("Pay", new { id = string.Empty });
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult Receive()
        {
            try
            {
                TDSModels taxingModels = new TDSModels
                {
                    _tDSReceives = new TaxingLogic(LogicHelper).GetTDSIns(),
                    _config = new ConfigarationManager(LogicHelper).GetConfig(),
                    _state = GenericLogic.State()
                };
                return View(taxingModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public ActionResult Receive(TDSReceive tDSReceive)
        {
            try
            {
                if (tDSReceive.TDSReceiveId == Convert.ToInt64(id))
                {
                    if (!string.IsNullOrEmpty(new TaxingLogic(LogicHelper).SetTDSIn(tDSReceive)))
                    {
                        ReturnMessage = new ReturnObject
                        {
                            Status = true,
                            Message = "TDS Receive record successfully inserted!!"
                        };
                        return RedirectToAction("Receive", new { id = string.Empty });
                    }
                }
                ReturnMessage = new ReturnObject
                {
                    Status = false,
                    Message = "Something goes wrong!!"
                };
                return RedirectToAction("Receive", new { id = string.Empty });
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }
    }
}