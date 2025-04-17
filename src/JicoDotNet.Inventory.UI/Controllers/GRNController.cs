using JicoDotNet.Inventory.BusinessLayer.BLL;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.Core.Models;
using JicoDotNet.Inventory.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using JicoDotNet.Inventory.Controllers;

namespace JicoDotNet.Inventory.UI.Controllers
{
    public class GRNController : BaseController
    {
        [SessionAuthenticate]
        public ActionResult Index()
        {
            try
            {
                GoodsReceiveNoteModels goodsReceiveNoteModels = new GoodsReceiveNoteModels()
                {
                    _goodsReceiveNotes = new GoodsReceiveNoteLogic(LogicHelper).GetGRNs()
                };
                return View(goodsReceiveNoteModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult ReceiveByPO()
        {
            try
            {
                GoodsReceiveNoteModels goodsReceiveNoteModels = new GoodsReceiveNoteModels();
                GoodsReceiveNoteLogic goodsReceiveNoteLogic = new GoodsReceiveNoteLogic(LogicHelper);
                PurchaseOrderLogic orderLogic = new PurchaseOrderLogic(LogicHelper);

                if (string.IsNullOrEmpty(UrlParameterId))
                {
                    goodsReceiveNoteModels._purchaseOrders = goodsReceiveNoteLogic.GetForGRN();
                }
                else
                {
                    if (goodsReceiveNoteLogic.GetForGRN(Convert.ToInt64(UrlParameterId)) != null)
                        goodsReceiveNoteModels._purchaseOrder = orderLogic.GetForDetail(Convert.ToInt64(UrlParameterId));

                    if (goodsReceiveNoteModels._purchaseOrder == null)
                    {
                        ReturnMessage = new ReturnObject()
                        {
                            Status = false,
                            Message = "Purchase Order not found!"
                        };
                        return RedirectToAction("ReceiveByPO", new { id = string.Empty });
                    }

                    goodsReceiveNoteModels._config = new ConfigarationManager(LogicHelper).GetConfig();
                    goodsReceiveNoteModels._wareHouses = new WareHouseLogic(LogicHelper).Get().Where(a => a.IsActive && a.BranchId == goodsReceiveNoteModels._purchaseOrder.BranchId).ToList();
                    goodsReceiveNoteModels._goodsReceiveNoteDetails = new GoodsReceiveNoteLogic(LogicHelper).GetGRNDetails(Convert.ToInt64(UrlParameterId));
                    goodsReceiveNoteModels._products = new ProductLogic(LogicHelper).GetIn();

                    // if previous goods received Partially/full in previous
                    if (goodsReceiveNoteModels._goodsReceiveNoteDetails.Count > 0)
                    {
                        foreach (PurchaseOrderDetail purchaseOrderDetail in goodsReceiveNoteModels._purchaseOrder.PurchaseOrderDetails.ToList())
                        {
                            GoodsReceiveNoteDetail dtl = goodsReceiveNoteModels._goodsReceiveNoteDetails
                                .Where(a => a.PurchaseOrderDetailId == purchaseOrderDetail.PurchaseOrderDetailId).FirstOrDefault();
                            if (dtl != null)
                            {
                                if (purchaseOrderDetail.Quantity > dtl.ReceivedQuantity)
                                {
                                    // This item are yet to received.
                                    purchaseOrderDetail.ReceivedQuantity = dtl.ReceivedQuantity;
                                    purchaseOrderDetail.Quantity = purchaseOrderDetail.Quantity - dtl.ReceivedQuantity;
                                }
                                else
                                {
                                    // This Item is received
                                    goodsReceiveNoteModels._purchaseOrder.PurchaseOrderDetails.Remove(purchaseOrderDetail);
                                    goodsReceiveNoteModels._goodsReceiveNoteDetails.Remove(dtl);
                                }
                            }
                        }
                    }
                }
                return View(goodsReceiveNoteModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        [SessionAuthenticate]
        public ActionResult ReceiveByPO(GoodsReceiveNote goodsReceiveNote)
        {
            try
            {
                #region Data Tracking...
                DataTrackingLogicSet(goodsReceiveNote);
                #endregion

                GoodsReceiveNoteLogic goodsReceiveNoteLogic = new GoodsReceiveNoteLogic(LogicHelper);
                GoodsReceiveNote GRNobj = JsonConvert.DeserializeObject<GoodsReceiveNote>(goodsReceiveNoteLogic.Receive(goodsReceiveNote));
                if (GRNobj == null || GRNobj.GRNId < 1)
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Status = false,
                        Message = "Something went Wrong!!"
                    };
                    return RedirectToAction("ReceiveByPO", new { id = string.Empty });
                }
                return RedirectToAction("Detail", new { id = UrlIdEncrypt(GRNobj.GRNId, false) });
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
                GoodsReceiveNoteModels goodsReceiveNoteModels = new GoodsReceiveNoteModels()
                {
                    _goodsReceiveNote = new GoodsReceiveNoteLogic(LogicHelper).GetForDetail(Convert.ToInt64(UrlParameterId))
                };
                if (goodsReceiveNoteModels._goodsReceiveNote != null)
                {
                    goodsReceiveNoteModels._config = new ConfigarationManager(LogicHelper).GetConfig();
                    goodsReceiveNoteModels._companyAddress = new Company()
                    {
                        CompanyName = SessionCompany.CompanyName,
                        GSTNumber = SessionCompany.GSTNumber,
                        GSTStateCode = SessionCompany.GSTStateCode,
                        IsGSTRegistered = SessionCompany.IsGSTRegistered,
                        StateCode = SessionCompany.StateCode,

                        Address = LogicHelper.AppSettings.CompanyAddress,
                        City = LogicHelper.AppSettings.CompanyCity,
                        Email = LogicHelper.AppSettings.CompanyEmail,
                        PINCode = LogicHelper.AppSettings.CompanyPINCode,
                        Mobile = LogicHelper.AppSettings.CompanyMobile,
                        WebsiteUrl = LogicHelper.AppSettings.CompanyWebsite,
                    };
                    goodsReceiveNoteModels._branch = new BranchLogic(LogicHelper).Get().FirstOrDefault(a => a.BranchId == goodsReceiveNoteModels._goodsReceiveNote.BranchId);
                    goodsReceiveNoteModels._purchaseOrder = new PurchaseOrderLogic(LogicHelper).GetForDetail(goodsReceiveNoteModels._goodsReceiveNote.PurchaseOrderId);
                    return View(goodsReceiveNoteModels);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult ReceiveDirect()
        {
            try
            {
                GoodsReceiveNoteModels goodsReceiveNoteModels = new GoodsReceiveNoteModels
                {
                    _wareHouses = new WareHouseLogic(LogicHelper).Get(true),
                    _vendors = new VendorLogic(LogicHelper).Get(true),
                    _purchaseTypes = new PurchaseOrderLogic(LogicHelper).TypeGet(true),
                    _company = SessionCompany,
                    _YesNo = GenericLogic.YesNo(),
                    _config = new ConfigarationManager(LogicHelper).GetConfig()
                };
                return View(goodsReceiveNoteModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpGet]
        public JsonResult ProductIn()
        {
            try
            {
                return Json(new ProductLogic(LogicHelper).GetIn().ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToJson(ex);
            }
        }

        [HttpPost]
        public PartialViewResult ReceiveDirectBlock(IList<Product> products)
        {
            try
            {
                if (products == null)
                    products = new List<Product>();
                GoodsReceiveNoteModels goodsReceiveNoteModels = new GoodsReceiveNoteModels
                {
                    _products = products,
                    _len = Convert.ToInt32(UrlParameterId)
                };
                return PartialView("_PartialReceiveDirectBlock", goodsReceiveNoteModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToPartial(ex);
            }
        }

        [HttpPost]
        public PartialViewResult ReceiveDirectProduct(short Gst, List<Product> products)
        {
            try
            {
                // Gst : 1 = GST enabled
                // Gst : 0 = GST disabled
                if (products == null)
                    products = new List<Product>();
                GoodsReceiveNoteModels goodsReceiveNoteModels = new GoodsReceiveNoteModels
                {
                    _product = products.Where(a => a.ProductId == Convert.ToInt64(UrlParameterId)).FirstOrDefault(),
                    _len = Convert.ToInt32(UrlParameterId2)
                };
                // Here "ProductInOut" Prop treat as GST applicable or not, to avoid declare extra property & extra memory allocation
                goodsReceiveNoteModels._product.ProductInOut = Gst;
                return PartialView("_PartialReceiveDirectDetails", goodsReceiveNoteModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToPartial(ex);
            }
        }

        [HttpPost]
        public ActionResult ReceiveDirect(GoodsReceiveNoteDirect goodsReceiveNoteDirect)
        {
            try
            {
                #region Data Tracking...
                DataTrackingLogicSet(goodsReceiveNoteDirect);
                #endregion

                GoodsReceiveNote GRNobj = JsonConvert.DeserializeObject<GoodsReceiveNote>(new GoodsReceiveNoteLogic(LogicHelper).SetDirect(goodsReceiveNoteDirect));
                if (GRNobj == null || GRNobj.GRNId < 1)
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Status = false,
                        Message = "Something Wrong!!"
                    };
                    return RedirectToAction("ReceiveDirect", new { id = string.Empty });
                }
                return RedirectToAction("Detail", new { id = UrlIdEncrypt(GRNobj.GRNId, false) });
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult Return()
        {
            try
            {
                GoodsReceiveNoteModels goodsReceiveNoteModels = new GoodsReceiveNoteModels();
                if (string.IsNullOrEmpty(UrlParameterId))
                {
                    goodsReceiveNoteModels._goodsReceiveNotes = new GoodsReceiveNoteLogic(LogicHelper).GetForReturn();
                    return View(goodsReceiveNoteModels);
                }
                else
                {
                    goodsReceiveNoteModels._wareHouses = new WareHouseLogic(LogicHelper).Get(true);
                    goodsReceiveNoteModels._goodsReceiveNote = new GoodsReceiveNoteLogic(LogicHelper).GetForDetail(Convert.ToInt64(UrlParameterId));
                    goodsReceiveNoteModels._config = new ConfigarationManager(LogicHelper).GetConfig();
                    goodsReceiveNoteModels._purchaseOrder = new PurchaseOrderLogic(LogicHelper).GetForDetail(goodsReceiveNoteModels._goodsReceiveNote.PurchaseOrderId);
                    return View(goodsReceiveNoteModels);
                }
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        [SessionAuthenticate]
        public ActionResult Return(PurchaseReturn purchaseReturn)
        {
            try
            {
                PurchaseOrderLogic purchaseOrder = new PurchaseOrderLogic(LogicHelper);
                purchaseOrder.Return(purchaseReturn);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }
    }
}