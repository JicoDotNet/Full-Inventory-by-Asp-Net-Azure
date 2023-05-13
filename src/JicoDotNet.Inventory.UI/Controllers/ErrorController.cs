using JicoDotNet.Inventory.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JicoDotNet.Inventory.UIControllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index(string id)
        {
            ErrorModels errorModels;
            if (TempData["Error"] != null)
            {
                errorModels = (ErrorModels)TempData["Error"];
            }
            else
            {
                errorModels = new ErrorModels
                {
                    ErrorStatus = string.IsNullOrEmpty(id)? 404 : 500,
                    ErrorCode = string.IsNullOrEmpty(id) ? Session.SessionID : id,
                    RequestId = id,
                    Message = string.IsNullOrEmpty(id) ? "Page Not Found!" : "internal server error!"
                };
            }
            return View(errorModels);
        }
    }
}