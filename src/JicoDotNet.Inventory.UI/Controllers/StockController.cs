using Newtonsoft.Json;
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
    public class StockController : BaseController
    {
        [SessionAuthenticate]
        public ActionResult Index()
        {
            try
            {
                StockModels stockModels = new StockModels()
                {
                    _products = new ProductLogic(BllCommonLogic).Get(true).Where(a => a.IsGoods).ToList(),
                    _wareHouses = new WareHouseLogic(BllCommonLogic).Get(true)
                };

                // If it's redirect from product page to view product's current stock. 
                // then id is ProductId, id can't be null
                if (!string.IsNullOrEmpty(id))
                    stockModels._productId = Convert.ToInt64(id);

                return View(stockModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public PartialViewResult Current(Stock stock)
        {
            try
            {
                StockModels stockModels = new StockModels()
                {
                    _stocks = new StockLogic(BllCommonLogic).Get(stock)
                };
                return PartialView("_PartialCurrentStock", stockModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToPartial(ex);
            }
        }

        [HttpPost]
        public PartialViewResult Detail(Stock detailView)
        {
            try
            {
                StockModels stockModels = new StockModels()
                {
                    _selectedProduct = new ProductLogic(BllCommonLogic).Get().FirstOrDefault(a => a.ProductId == detailView.ProductId),
                    _stock = new StockLogic(BllCommonLogic).GetDetail(detailView).FirstOrDefault(),
                };
                return PartialView("_PartialCurrentStockDetail", stockModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToPartial(ex);
            }
        }

        [HttpGet]
        [SessionAuthenticate]
        public ActionResult Transfer()
        {
            try
            {
                StockModels stockModels = new StockModels()
                {
                    _products = new ProductLogic(BllCommonLogic).Get(true).Where(a => a.IsGoods).ToList(),
                    _wareHouses = new WareHouseLogic(BllCommonLogic).Get(true)
                };
                return View(stockModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpGet]
        [SessionAuthenticate]
        public ActionResult Search(Stock stock)
        {
            try
            {
                StockModels stockModels = new StockModels()
                {
                    _products = new ProductLogic(BllCommonLogic).Get().Where(a => a.IsActive).ToList(),
                    _wareHouses = new WareHouseLogic(BllCommonLogic).Get().Where(a => a.IsActive).ToList(),
                    _stocks = new StockLogic(BllCommonLogic).GetDetail(stock),
                    _stock = stock
                };
                return View("Transfer", stockModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public ActionResult Transfer(StockTransfer stockTransfer)
        {
            try
            {
                #region Data Tracking...
                DataTrackingLogicSet(stockTransfer);
                #endregion

                StockTransferLogic stockTransferLogic = new StockTransferLogic(BllCommonLogic);
                StockTransfer STobj = JsonConvert.DeserializeObject<StockTransfer>(stockTransferLogic.Set(stockTransfer));
                if (STobj == null || STobj.StockTransferId < 1)
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Status = false,
                        Message = "Something Wrong!!"
                    };
                    return RedirectToAction("Transfer");
                }
                //return RedirectToAction("Index", "Stock", new { id = UrlIdEncrypt(stockTransfer.StockTransferDetails, false) });
                return RedirectToAction("TransferDetail", new { id = UrlIdEncrypt(STobj.StockTransferId, false) });
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult TransferDetail()
        {
            if (!string.IsNullOrEmpty(id))
                return View();
            else
                return RedirectToAction("Transfer");
        }

        [SessionAuthenticate]
        public ActionResult Adjust()
        {
            try
            {
                StockModels stockModels = new StockModels()
                {
                    _products = new ProductLogic(BllCommonLogic).Get(true).Where(a => a.IsGoods).ToList(),
                    _wareHouses = new WareHouseLogic(BllCommonLogic).Get(true),
                    _adjustReasons = new StockAdjustLogic(BllCommonLogic).GetReasons().Where(a => a.IsActive).ToList()
                };

                // If it's redirect from product page to view product's Adjust stock. 
                // then id is ProductId, id can't be null
                if (!string.IsNullOrEmpty(id))
                    stockModels._productId = Convert.ToInt64(id);

                return View(stockModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public PartialViewResult AdjustPartial(StockModels adjustParam)
        {
            try
            {
                if (adjustParam.IsStockIncrease)
                {
                    StockModels stockModels = new StockModels()
                    {
                        _selectedProduct = new ProductLogic(BllCommonLogic).Get().FirstOrDefault(p => p.ProductId == adjustParam.ProductId)
                    };
                    return PartialView("_PartialStockIncrease", stockModels);
                }
                else
                {
                    StockModels stockModels = new StockModels()
                    {
                        _selectedProduct = new ProductLogic(BllCommonLogic).Get().FirstOrDefault(p => p.ProductId == adjustParam.ProductId),
                        _stock = new StockLogic(BllCommonLogic).GetDetail(new Stock() { WareHouseId = adjustParam.WareHouseId, ProductId = adjustParam.ProductId }).FirstOrDefault()
                    };
                    return PartialView("_PartialStockDecrease", stockModels);
                }
            }
            catch (Exception ex)
            {
                return ErrorLoggingToPartial(ex);
            }
        }

        [HttpPost]
        public ActionResult Adjust(StockAdjust model)
        {
            try
            {
                StockAdjustLogic stockAdjustLogic = new StockAdjustLogic(BllCommonLogic);
                StockAdjust stockAdjust = JsonConvert.DeserializeObject<StockAdjust>(stockAdjustLogic.Set(model));

                if (stockAdjust == null || stockAdjust.StockAdjustId < 1)
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Status = false,
                        Message = "Something Wrong!!"
                    };
                    return RedirectToAction("Adjust", new { id = string.Empty });
                }
                return RedirectToAction("AdjustDetail", new { id = UrlIdEncrypt(stockAdjust.StockAdjustId, false) });
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult AdjustDetail()
        {
            if (!string.IsNullOrEmpty(id))
                return View();
            else
                return RedirectToAction("Adjust");
        }
    }
}