using Newtonsoft.Json;
using JicoDotNet.Inventory.BusinessLayer.BLL;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.Core.Models;

namespace JicoDotNet.Inventory.UIControllers
{
    public class ShipmentController : BaseController
    {
        #region Shipment Type
        [SessionAuthenticate]
        public ActionResult Type()
        {
            try
            {
                ShipmentModels shipmentModels = new ShipmentModels()
                {
                    _shipmentTypes = new ShipmentLogic(LogicHelper).TypeGet()
                };
                if (!string.IsNullOrEmpty(UrlParameterId))
                {
                    shipmentModels._shipmentType = shipmentModels._shipmentTypes.Where(a => a.ShipmentTypeId == Convert.ToInt64(UrlParameterId)).FirstOrDefault();
                }
                return View(shipmentModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public ActionResult Type(ShipmentType shipmentType)
        {
            try
            {
                shipmentType.ShipmentTypeId = UrlParameterId == null ? 0 : Convert.ToInt64(UrlParameterId);

                #region Data Tracking...
                DataTrackingLogicSet(shipmentType);
                #endregion

                ShipmentLogic shipmentLogic = new ShipmentLogic(LogicHelper);
                if (Convert.ToInt64(shipmentLogic.TypeSet(shipmentType)) > 0)
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
                    ShipmentLogic shipmentLogic = new ShipmentLogic(LogicHelper);
                    long deactivateId = Convert.ToInt64(shipmentLogic.TypeDeactive(UrlParameterId));
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
        #endregion

        [SessionAuthenticate]
        public ActionResult Index()
        {
            try
            {
                ShipmentModels shipmentModels = new ShipmentModels()
                {
                    _shipments = new ShipmentLogic(LogicHelper).GetShipments()
                };
                return View(shipmentModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult ShippedBySO()
        {
            try
            {
                ShipmentModels shipmentModels = new ShipmentModels();
                if (string.IsNullOrEmpty(UrlParameterId))
                {
                    shipmentModels._salesOrders = new SalesOrderLogic(LogicHelper).GetForShipment();
                    return View(shipmentModels);
                }

                SalesOrderLogic salesOrderLogic = new SalesOrderLogic(LogicHelper);
                if(salesOrderLogic.GetForShipment().FirstOrDefault(so => so.SalesOrderId == Convert.ToInt64(UrlParameterId)) != null)
                {
                    shipmentModels._salesOrder = salesOrderLogic.GetForDetail(Convert.ToInt64(UrlParameterId));
                }

                if (shipmentModels._salesOrder == null)
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Status = false,
                        Message = "Sales Order not found!"
                    };
                    return RedirectToAction("ShippedBySO", new { id = string.Empty });
                }

                shipmentModels._salesOrder.SalesOrderDetails =
                    shipmentModels._salesOrder.SalesOrderDetails.Where(a => a.IsGoods).ToList();

                ShipmentLogic shipmentLogic = new ShipmentLogic(LogicHelper);
                shipmentModels._wareHouses = new WareHouseLogic(LogicHelper).Get(true).Where(a => a.BranchId == shipmentModels._salesOrder.BranchId).ToList();
                shipmentModels._shipmentTypes = shipmentLogic.TypeGet(true);
                shipmentModels._config = new ConfigarationManager(LogicHelper).GetConfig();
                
                return View(shipmentModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">SalesOrderId</param>
        /// <param name="id2">WareHouseId</param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult PartialShippableStockDetail()
        {
            try
            {
                ShipmentModels shipmentModels = new ShipmentModels();
                if (!string.IsNullOrEmpty(UrlParameterId))
                {
                    shipmentModels._salesOrder = new SalesOrderLogic(LogicHelper).GetForDetail(Convert.ToInt64(UrlParameterId));
                    shipmentModels._salesOrder.SalesOrderDetails =
                        shipmentModels._salesOrder.SalesOrderDetails.Where(a => a.IsGoods).ToList();
                    
                    if (shipmentModels._salesOrder != null)
                    {
                        shipmentModels._products = new ProductLogic(LogicHelper).GetOut();
                        shipmentModels._company = SessionCompany;
                        shipmentModels._shipmentDetails = new ShipmentLogic(LogicHelper).GetShipmentDetails(Convert.ToInt64(UrlParameterId));
                        shipmentModels._config = new ConfigarationManager(LogicHelper).GetConfig();

                        List<Stock> stocks = new StockLogic(LogicHelper).GetDetail(new Stock { WareHouseId = Convert.ToInt64(UrlParameterId2) });
                        shipmentModels._stocks = new List<Stock>();
                        foreach (SalesOrderDetail salesOrderDetail in shipmentModels._salesOrder?.SalesOrderDetails)
                        {
                            Stock stock = stocks.Where(a => a.ProductId == salesOrderDetail.ProductId
                                && (shipmentModels._stocks.Where(b => b.ProductId == salesOrderDetail.ProductId) != null)).FirstOrDefault();
                            if (stock != null && stock.StockDetails.Count > 0)
                            {
                                shipmentModels._stocks.Add(stock);
                            }
                        }

                        // if previous goods shipped
                        if (shipmentModels._shipmentDetails.Count > 0)
                        {
                            foreach (SalesOrderDetail salesOrderDetail in shipmentModels._salesOrder.SalesOrderDetails.ToList())
                            {
                                ShipmentDetail dtl = shipmentModels._shipmentDetails
                                    .Where(a => a.SalesOrderDetailId == salesOrderDetail.SalesOrderDetailId).FirstOrDefault();
                                if (dtl != null)
                                {
                                    if (salesOrderDetail.Quantity > dtl.ShippedQuantity)
                                    {
                                        // This item are yet to shipped.
                                        salesOrderDetail.ShippedQuantity = dtl.ShippedQuantity;
                                        salesOrderDetail.Quantity = salesOrderDetail.Quantity - dtl.ShippedQuantity;
                                    }
                                    else
                                    {
                                        // This Item is shipped
                                        shipmentModels._salesOrder.SalesOrderDetails.Remove(salesOrderDetail);
                                        shipmentModels._shipmentDetails.Remove(dtl);
                                    }
                                }
                            }
                        }
                        return PartialView("_PartialShippableStockDetail", shipmentModels);
                    }
                }
                return PartialView("_PartialErrorBlock");
            }
            catch (Exception ex)
            {
                return ErrorLoggingToPartial(ex);
            }
        }

        [HttpPost]
        [SessionAuthenticate]
        public ActionResult ShippedBySO(Shipment shipment)
        {
            try
            {
                #region Data Tracking...
                DataTrackingLogicSet(shipment);
                #endregion

                ShipmentLogic shipmentLogic = new ShipmentLogic(LogicHelper);
                Shipment SHPobj = JsonConvert.DeserializeObject<Shipment>(shipmentLogic.Set(shipment));
                if (SHPobj == null || SHPobj.ShipmentId < 1)
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Status = false,
                        Message = "Something Wrong!!"
                    };
                    return RedirectToAction("ShippedBySO", new { id = string.Empty });
                }
                return RedirectToAction("Detail", new { id = UrlIdEncrypt(SHPobj.ShipmentId, false) });
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult Direct()
        {
            try
            {
                ShipmentModels shipmentModels = new ShipmentModels()
                {
                    _company = SessionCompany,
                    _wareHouses = new WareHouseLogic(LogicHelper).Get(true),
                    _customers = new CustomerLogic(LogicHelper).GetNonRetail(true),
                    _salesTypes = new SalesOrderLogic(LogicHelper).TypeGet(true),
                    _shipmentTypes = new ShipmentLogic(LogicHelper).TypeGet(true),
                    _config = new ConfigarationManager(LogicHelper).GetConfig()
                };
                return View(shipmentModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpGet]
        public PartialViewResult DOItems(short Gst)
        {
            try
            {
                SalesOrderModels salesOrderModels = new SalesOrderModels()
                {
                    _config = new ConfigarationManager(LogicHelper).GetConfig(),
                    _isGstEnabled = Gst == 1
                };
                return PartialView("_PartialDeliveryDirectDetails", salesOrderModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToPartial(ex);
            }
        }

        [HttpGet]
        public PartialViewResult PartialStockDetailDirect()
        {
            try
            {
                ShipmentModels shipmentModels = new ShipmentModels();
                if (!string.IsNullOrEmpty(UrlParameterId) && !string.IsNullOrEmpty(UrlParameterId2))
                {
                    shipmentModels._config = new ConfigarationManager(LogicHelper).GetConfig();
                    shipmentModels._stocks = new StockLogic(LogicHelper).GetDetail(new Stock { WareHouseId = Convert.ToInt64(UrlParameterId), ProductId = Convert.ToInt64(UrlParameterId2) });
                }
                return PartialView("_PartialStockDetailDirect", shipmentModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToPartial(ex);
            }
        }

        [HttpPost]
        public ActionResult Direct(ShipmentDirect shipmentDirect)
        {
            try
            {
                #region Data Tracking...
                DataTrackingLogicSet(shipmentDirect);
                #endregion

                Shipment GRNobj = JsonConvert.DeserializeObject<Shipment>(new ShipmentLogic(LogicHelper).SetDirect(shipmentDirect));
                if (GRNobj == null || GRNobj.ShipmentId < 1)
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Status = false,
                        Message = "Something Wrong!!"
                    };
                    return RedirectToAction("Direct", new { id = string.Empty });
                }
                return RedirectToAction("Detail", new { id = UrlIdEncrypt(GRNobj.ShipmentId, false) });
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult Detail()
        {
            try
            {
                if (string.IsNullOrEmpty(UrlParameterId))
                {
                    return RedirectToAction("Index");
                }
                ShipmentModels shipmentModels = new ShipmentModels()
                {
                    _shipment = new ShipmentLogic(LogicHelper).GetForDetail(Convert.ToInt64(UrlParameterId))
                };
                if (shipmentModels._shipment != null)
                {
                    shipmentModels._config = new ConfigarationManager(LogicHelper).GetConfig();
                    shipmentModels._companyAddress = new Company()
                    {
                        CompanyName = SessionCompany.CompanyName,
                        GSTNumber = SessionCompany.GSTNumber,
                        GSTStateCode = SessionCompany.GSTStateCode,
                        IsGSTRegistered = SessionCompany.IsGSTRegistered,
                        StateCode = SessionCompany.StateCode,

                        Address = WebConfigAppSettingsAccess.CompanyAddress,
                        City = WebConfigAppSettingsAccess.CompanyCity,
                        Email = WebConfigAppSettingsAccess.CompanyEmail,
                        PINCode = WebConfigAppSettingsAccess.CompanyPINCode,
                        Mobile = WebConfigAppSettingsAccess.CompanyMobile,
                        WebsiteUrl = WebConfigAppSettingsAccess.CompanyWebsite,
                    };
                    shipmentModels._branch = new BranchLogic(LogicHelper).Get().FirstOrDefault(a => a.BranchId == shipmentModels._shipment.BranchId);
                    shipmentModels._salesOrder = new SalesOrderLogic(LogicHelper).GetForDetail(shipmentModels._shipment.SalesOrderId);
                    return View(shipmentModels);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }
    }
}