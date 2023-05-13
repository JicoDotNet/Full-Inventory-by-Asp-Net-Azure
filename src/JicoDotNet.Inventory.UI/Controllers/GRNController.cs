using Newtonsoft.Json;
using JicoDotNet.Inventory.BusinessLayer.BLL;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using JicoDotNet.Inventory.BusinessLayer.Common;

namespace JicoDotNet.Inventory.UIControllers
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
                    _goodsReceiveNotes = new GoodsReceiveNoteLogic(BllCommonLogic).GetGRNs()
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
                GoodsReceiveNoteLogic goodsReceiveNoteLogic = new GoodsReceiveNoteLogic(BllCommonLogic);
                PurchaseOrderLogic orderLogic = new PurchaseOrderLogic(BllCommonLogic);

                if (string.IsNullOrEmpty(id))
                {
                    goodsReceiveNoteModels._purchaseOrders = goodsReceiveNoteLogic.GetForGRN();
                }
                else
                {
                    if (goodsReceiveNoteLogic.GetForGRN(Convert.ToInt64(id)) != null)
                        goodsReceiveNoteModels._purchaseOrder = orderLogic.GetForDetail(Convert.ToInt64(id));

                    if (goodsReceiveNoteModels._purchaseOrder == null)
                    {
                        ReturnMessage = new ReturnObject()
                        {
                            Status = false,
                            Message = "Purchase Order not found!"
                        };
                        return RedirectToAction("ReceiveByPO", new { id = string.Empty });
                    }

                    goodsReceiveNoteModels._config = new ConfigarationManager(BllCommonLogic).GetConfig();
                    goodsReceiveNoteModels._wareHouses = new WareHouseLogic(BllCommonLogic).Get().Where(a => a.IsActive && a.BranchId == goodsReceiveNoteModels._purchaseOrder.BranchId).ToList();
                    goodsReceiveNoteModels._goodsReceiveNoteDetails = new GoodsReceiveNoteLogic(BllCommonLogic).GetGRNDetails(Convert.ToInt64(id));
                    goodsReceiveNoteModels._products = new ProductLogic(BllCommonLogic).GetIn();

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

                GoodsReceiveNoteLogic goodsReceiveNoteLogic = new GoodsReceiveNoteLogic(BllCommonLogic);
                GoodsReceiveNote GRNobj = JsonConvert.DeserializeObject<GoodsReceiveNote>(goodsReceiveNoteLogic.Receive(goodsReceiveNote));
                if(GRNobj == null || GRNobj.GRNId < 1)
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
                if (string.IsNullOrEmpty(id))
                {
                    return RedirectToAction("Index");
                }
                GoodsReceiveNoteModels goodsReceiveNoteModels = new GoodsReceiveNoteModels()
                {
                    _goodsReceiveNote = new GoodsReceiveNoteLogic(BllCommonLogic).GetForDetail(Convert.ToInt64(id))
                };
                if (goodsReceiveNoteModels._goodsReceiveNote != null)
                {
                    goodsReceiveNoteModels._config = new ConfigarationManager(BllCommonLogic).GetConfig();
                    goodsReceiveNoteModels._companyAddress = new Company()
                    {
                        CompanyName = SessionCompany.CompanyName,
                        GSTNumber = SessionCompany.GSTNumber,
                        GSTStateCode = SessionCompany.GSTStateCode,
                        IsGSTRegistered = SessionCompany.IsGSTRegistered,
                        StateCode = SessionCompany.StateCode,

                        Address = WebConfigAccess.CompanyAddress,
                        City = WebConfigAccess.CompanyCity,
                        Email = WebConfigAccess.CompanyEmail,
                        PINCode = WebConfigAccess.CompanyPINCode,
                        Mobile = WebConfigAccess.CompanyMobile,
                        WebsiteUrl = WebConfigAccess.CompanyWebsite,
                    };
                    goodsReceiveNoteModels._branch = new BranchLogic(BllCommonLogic).Get().FirstOrDefault(a => a.BranchId == goodsReceiveNoteModels._goodsReceiveNote.BranchId);
                    goodsReceiveNoteModels._purchaseOrder = new PurchaseOrderLogic(BllCommonLogic).GetForDetail(goodsReceiveNoteModels._goodsReceiveNote.PurchaseOrderId);
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
                    _wareHouses = new WareHouseLogic(BllCommonLogic).Get(true),
                    _vendors = new VendorLogic(BllCommonLogic).Get(true),
                    _purchaseTypes = new PurchaseOrderLogic(BllCommonLogic).TypeGet(true),
                    _company = SessionCompany,
                    _YesNo = GenericLogic.YesNo(),
                    _config = new ConfigarationManager(BllCommonLogic).GetConfig()
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
                return Json(new ProductLogic(BllCommonLogic).GetIn().ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToJson(ex);
            }
        }

        [HttpPost]
        public PartialViewResult ReceiveDirectBlock(List<Product> products)
        {
            try
            {
                if (products == null)
                    products = new List<Product>();
                GoodsReceiveNoteModels goodsReceiveNoteModels = new GoodsReceiveNoteModels
                {
                    _products = products,
                    _len = Convert.ToInt32(id)
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
                    _product = products.Where(a => a.ProductId == Convert.ToInt64(id)).FirstOrDefault(),
                    _len = Convert.ToInt32(id2)
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

                GoodsReceiveNote GRNobj = JsonConvert.DeserializeObject<GoodsReceiveNote>(new GoodsReceiveNoteLogic(BllCommonLogic).SetDirect(goodsReceiveNoteDirect));
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
                if (string.IsNullOrEmpty(id))
                {
                    goodsReceiveNoteModels._goodsReceiveNotes = new GoodsReceiveNoteLogic(BllCommonLogic).GetForReturn();
                    return View(goodsReceiveNoteModels);
                }
                else
                {
                    goodsReceiveNoteModels._wareHouses = new WareHouseLogic(BllCommonLogic).Get(true);
                    goodsReceiveNoteModels._goodsReceiveNote = new GoodsReceiveNoteLogic(BllCommonLogic).GetForDetail(Convert.ToInt64(id));
                    goodsReceiveNoteModels._config = new ConfigarationManager(BllCommonLogic).GetConfig();
                    goodsReceiveNoteModels._purchaseOrder = new PurchaseOrderLogic(BllCommonLogic).GetForDetail(goodsReceiveNoteModels._goodsReceiveNote.PurchaseOrderId);
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
                PurchaseOrderLogic purchaseOrder = new PurchaseOrderLogic(BllCommonLogic);
                purchaseOrder.Return(purchaseReturn);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }
    }
}