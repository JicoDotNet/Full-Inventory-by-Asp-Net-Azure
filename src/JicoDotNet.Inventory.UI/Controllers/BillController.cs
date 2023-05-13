using Newtonsoft.Json;
using JicoDotNet.Inventory.BusinessLayer.BLL;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.UI.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace JicoDotNet.Inventory.UIControllers
{
    public class BillController : BaseController
    {
        #region Bill Type
        [SessionAuthenticate]
        public ActionResult Type()
        {
            try
            {
                BillModels billModels = new BillModels()
                {
                    _billTypes = new BillLogic(BllCommonLogic).TypeGet()
                };
                if (!string.IsNullOrEmpty(id))
                {
                    billModels._billType = billModels._billTypes.Where(a => a.BillTypeId == Convert.ToInt64(id)).FirstOrDefault();
                }
                return View(billModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public ActionResult Type(BillType billType)
        {
            try
            {
                billType.BillTypeId = id == null ? 0 : Convert.ToInt64(id);

                #region Data Tracking...
                DataTrackingLogicSet(billType);
                #endregion

                BillLogic billLogic = new BillLogic(BllCommonLogic);
                if (Convert.ToInt64(billLogic.TypeSet(billType)) > 0)
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
                if (new LoginManagement(BllCommonLogic).Authenticate(SessionPerson.UserEmail, Context))
                {
                    BillLogic billLogic = new BillLogic(BllCommonLogic);
                    long deactivateId = Convert.ToInt64(billLogic.TypeDeactive(id));
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
        #endregion

        #region Bill Generate
        [SessionAuthenticate]
        public ActionResult Index()
        {
            try
            {
                BillModels billModels = new BillModels()
                {
                    _bills = new BillLogic(BllCommonLogic).GetBills(),
                    _config = (new ConfigarationManager(BllCommonLogic)).GetConfig()
                };
                return View(billModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult Generate()
        {
            try
            {
                BillModels billModels = new BillModels();
                BillLogic billLogic = new BillLogic(BllCommonLogic);
                if (string.IsNullOrEmpty(id))
                {
                    billModels._purchaseOrders = billLogic.GetForEntry();
                }
                else
                {                    
                    // Retrive PO
                    PurchaseOrderLogic orderLogic = new PurchaseOrderLogic(BllCommonLogic);
                    if (billLogic.GetForEntry(Convert.ToInt64(id)) != null)
                        billModels._purchaseOrder = orderLogic.GetForDetail(Convert.ToInt64(id));

                    // -- _purchaseOrder Check
                    if (billModels._purchaseOrder == null)
                    {
                        ReturnMessage = new ReturnObject()
                        {
                            Status = false,
                            Message = "Purchase Order not found!"
                        };
                        return RedirectToAction("Generate", new { id = string.Empty });
                    }

                    billModels._billTypes = billLogic.TypeGet(true);
                    billModels._config = new ConfigarationManager(BllCommonLogic).GetConfig();

                    // Checking Vendor is GST registred or not.
                    billModels.GSTType = EGSTType.None;
                    if (billModels._purchaseOrder.IsGstAllowed)
                    {
                        billModels.GSTType = GSTLogic.GetType(SessionCompany.IsGSTRegistered ? SessionCompany.GSTStateCode : SessionCompany.StateCode, billModels._purchaseOrder.IsGstAllowed ? billModels._purchaseOrder.GSTStateCode : billModels._purchaseOrder.StateCode);
                    }

                    // Previous Bill details- if partially billed
                    billModels._billDetails = billLogic.GetBillDetails(Convert.ToInt64(id));
                    // Check previous Bill
                    if (billModels._billDetails.Count > 0)
                    {
                        foreach (PurchaseOrderDetail purchaseOrderDetail in billModels._purchaseOrder.PurchaseOrderDetails.ToList())
                        {
                            BillDetail dtl = billModels._billDetails
                                .Where(a => a.PurchaseOrderDetailId == purchaseOrderDetail.PurchaseOrderDetailId).FirstOrDefault();
                            if (dtl != null)
                            {
                                if (purchaseOrderDetail.Quantity > dtl.BilledQuantity)
                                {
                                    // This item are yet to billed
                                    purchaseOrderDetail.ReceivedQuantity = dtl.BilledQuantity;
                                    purchaseOrderDetail.Quantity = purchaseOrderDetail.Quantity - dtl.BilledQuantity;
                                }
                                else
                                {
                                    // This Item is full billed
                                    billModels._purchaseOrder.PurchaseOrderDetails.Remove(purchaseOrderDetail);
                                    billModels._billDetails.Remove(dtl);
                                }
                            }
                        }
                    }
                }
                return View(billModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public ActionResult Generate(Bill bill)
        {
            try
            {
                BillLogic billLogic = new BillLogic(BllCommonLogic);
                Bill rerurnBill = JsonConvert.DeserializeObject<Bill>(billLogic.Set(bill));
                if (rerurnBill == null || rerurnBill.BillId < 1)
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Status = false,
                        Message = "Something went Wrong!!"
                    };
                    return RedirectToAction("Generate", new { id = string.Empty });
                }
                else
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Status = true,
                        Message = "Bill Generated!!"
                    };
                    return RedirectToAction("Detail", new { id = UrlIdEncrypt(rerurnBill.BillId, false) });
                }
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
                BillModels billModels = new BillModels
                {
                    _bill = new BillLogic(BllCommonLogic).GetForDetail(Convert.ToInt64(id))
                };
                if (billModels._bill != null)
                {
                    billModels._config = new ConfigarationManager(BllCommonLogic).GetConfig();
                    billModels._companyAddress = new Company()
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
                    billModels._vendor = new VendorLogic(BllCommonLogic).Get().FirstOrDefault(a => a.VendorId == billModels._bill.VendorId);
                    billModels._purchaseOrder = new PurchaseOrderLogic(BllCommonLogic).GetForDetail(billModels._bill.PurchaseOrderId);
                    return View(billModels);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }
        #endregion
    }
}