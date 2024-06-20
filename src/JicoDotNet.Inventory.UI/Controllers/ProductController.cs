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
    public class ProductController : BaseController
    {
        [SessionAuthenticate]
        public ActionResult Index()
        {            
            return View();
        }

        [HttpGet]
        public PartialViewResult PartialIndex()
        {
            try
            {
                ProductModels productModels = new ProductModels()
                {
                    _products = new ProductLogic(LogicHelper).Get()
                };
                return PartialView("_PartialIndex", productModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToPartial(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult Add()
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    // Limit Quota Checking
                    ProductModels productModels = new ProductModels()
                    {
                        _productTypes = new ProductLogic(LogicHelper).TypeGet().Where(a => a.IsActive).ToList(),
                        _unitOfMeasures = new UnitOfMeasureLogic(LogicHelper).Get().Where(a => a.IsActive).ToList(),
                        _YesNo = GenericLogic.YesNo(),
                        _ProductCategory = GenericLogic.ProductCategory(),
                        _config = new ConfigarationManager(LogicHelper).GetConfig()
                    };
                    return View(productModels);
                }
                return RedirectToAction("Index", new { id = string.Empty });
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult Edit()
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    ProductModels productModels = new ProductModels()
                    {
                        _productTypes = new ProductLogic(LogicHelper).TypeGet().Where(a => a.IsActive).ToList(),
                        _unitOfMeasures = new UnitOfMeasureLogic(LogicHelper).Get().Where(a => a.IsActive).ToList(),
                        _YesNo = GenericLogic.YesNo(),
                        _ProductCategory = GenericLogic.ProductCategory(),
                        _config = new ConfigarationManager(LogicHelper).GetConfig(),
                        _product = new ProductLogic(LogicHelper).Get().FirstOrDefault(a => a.ProductId == Convert.ToInt64(id))
                    };
                    return View("Add", productModels);
                }
                return RedirectToAction("Index", new { id = string.Empty });
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public ActionResult Add(Product product, bool HaveOpeningStock = false)
        {
            try
            {
                product.ProductId = id == null ? 0 : Convert.ToInt64(id);

                #region Data Tracking...
                DataTrackingLogicSet(product);
                #endregion

                ProductLogic productLogic = new ProductLogic(LogicHelper);

                #region Image Upload
                if (Request.Files.Count > 0)
                    if (Request.Files["ProductImageUrl"] != null)
                        product.ProductImageUrl = productLogic.UploadImage(Request.Files["ProductImageUrl"]);
                #endregion
                long PId = Convert.ToInt64(productLogic.Set(product));
                if (PId > 0)
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
                if (HaveOpeningStock)
                    return RedirectToAction("OpeningStock", new { id = UrlIdEncrypt(PId, false) });
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
                    ProductLogic productLogic = new ProductLogic(LogicHelper);
                    long deactivateId = Convert.ToInt64(productLogic.Deactive(id));
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

        [SessionAuthenticate]
        public ActionResult OpeningStock()
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    return RedirectToAction("Index");

                ProductModels productModels = new ProductModels();
                if (new StockLogic(LogicHelper).TotalNonOpeningStockQuantity(Convert.ToInt64(id)) == 0)
                {
                    productModels._product = new ProductLogic(LogicHelper).Get(true)
                        .FirstOrDefault(a => a.ProductId == Convert.ToInt64(id) && a.IsGoods);
                    productModels._wareHouses = new WareHouseLogic(LogicHelper).Get(true);
                }
                return View(productModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public ActionResult OpeningStock(List<StockDetail> stockDetails)
        {
            try
            {
                if (Convert.ToInt64(new StockLogic(LogicHelper).AddOpeningStock(stockDetails, Convert.ToInt64(id))) > 0)
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Message = "Opening Stock Entry Successful",
                        Status = true
                    };
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Message = "Unsuccess",
                        Status = false
                    };
                }
                return RedirectToAction("OpeningStock");
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        public PartialViewResult AddOpeningStock(bool HasBatchNo,
            bool HasExpirationDate,
            bool IsPerishableProduct,
            string UnitOfMeasureName, long WareHouseId, int len)
        {
            try
            {
                Product product = new Product()
                {
                    HasBatchNo = HasBatchNo,
                    HasExpirationDate = HasExpirationDate,
                    IsPerishableProduct = IsPerishableProduct,
                    UnitOfMeasureName = UnitOfMeasureName,
                    ProductName = WareHouseId.ToString(),   // This is Temp using to excape extra memory using.
                    ProductId = len                         // This is Temp using to excape extra memory using.
                };
                return PartialView("_PartialOpeningStock", product);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToPartial(ex);
            }
        }

        [SessionAuthenticate]
        public ActionResult BulkOpeningStock()
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    return RedirectToAction("Index");

                ProductModels productModels = new ProductModels();
                if (new StockLogic(LogicHelper).TotalNonOpeningStockQuantity(Convert.ToInt64(id)) == 0)
                {
                    productModels._product = new ProductLogic(LogicHelper).Get(true)
                        .FirstOrDefault(a => a.ProductId == Convert.ToInt64(id) && a.IsGoods);
                    productModels._wareHouses = new WareHouseLogic(LogicHelper).Get(true);
                }
                return View(productModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [SessionAuthenticate]
        public void DownloadCSV()
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    return;

                Product product = new ProductLogic(LogicHelper).Get(true)
                            .FirstOrDefault(a => a.ProductId == Convert.ToInt64(id) && a.IsGoods);
                if (product == null)
                    return;

                //Build the CSV file data as a Comma separated string.
                string csv = string.Empty;
                csv += "Product Name,Current Stock,Goods Receive Date";
                if (product.HasBatchNo)
                {
                    csv += ",Batch Number";
                }
                if (product.HasExpirationDate)
                {
                    csv += ",Expiry Date";
                }
                csv += ",Remarks";

                //Add new line.
                csv += "\r\n";
                csv += product.ProductName?.Replace(",", ";") + ",1,01-01-2020";
                if (product.HasBatchNo)
                {
                    csv += ",BATCH0001";
                }
                if (product.HasExpirationDate)
                {
                    csv += ",31-12-2025";
                }
                csv += ",Stock Remarks will come here";

                //Download the CSV file.
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=SampleCSV.csv");
                Response.Charset = "";
                Response.ContentType = "application/text";
                Response.Output.Write(csv);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                ErrorLogging(ex);
                return;
            }
        }

        [HttpPost]
        public ActionResult BulkOpeningStock(FormCollection blkForm)
        {
            try
            {
                List<StockDetail> stockDetails = new List<StockDetail>();
                StockLogic stockLogic = new StockLogic(LogicHelper);

                #region CSV Upload
                try
                {
                    if (Request.Files.Count > 0)
                        if (Request.Files["CSV"] != null)
                        {
                            // Create a Stream object.
                            System.IO.Stream stream = Request.Files["CSV"].InputStream;
                            // Find number of bytes in stream.
                            int streamLength = Convert.ToInt32(stream.Length);
                            // Create a byte array.
                            byte[] streamArr = new byte[streamLength];
                            // Read stream into byte array.
                            stream.Read(streamArr, 0, streamLength);
                            //Read the contents of CSV file.
                            string csvData = System.Text.Encoding.UTF8.GetString(streamArr);

                            //Execute a loop over the rows.
                            string[] rows = csvData.Split('\n');
                            for (int i = 1; i < rows.Length; i++)
                            {
                                string row = rows[i].Replace("\r", "");
                                if (!string.IsNullOrEmpty(row))
                                {
                                    string[] SplitRowData = row.Split(',');

                                    if (!string.IsNullOrEmpty(blkForm["Stock"])
                                        && !string.IsNullOrEmpty(SplitRowData[Convert.ToInt64(blkForm["Stock"])]))
                                    {
                                        DateTime GRNDate = new DateTime();
                                        if (blkForm["GRNDateFormat"] != null
                                            && !string.IsNullOrEmpty(blkForm["GRNDateFormat"])
                                            && blkForm["GRNDate"] != null
                                            && !string.IsNullOrEmpty(blkForm["GRNDate"])
                                            && !string.IsNullOrEmpty(SplitRowData[Convert.ToInt64(blkForm["GRNDate"])]))
                                        {
                                            GRNDate = DateTime.ParseExact(SplitRowData[Convert.ToInt64(blkForm["GRNDate"])], blkForm["GRNDateFormat"].ToString(),
                                                System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
                                        }
                                        else
                                        {
                                            GRNDate = GenericLogic.IstNow;
                                        }
                                        DateTime? ExpiryDate = null;
                                        if (blkForm["ExpiryDateFormat"] != null
                                            && !string.IsNullOrEmpty(blkForm["ExpiryDateFormat"])
                                            && blkForm["ExpiryDate"] != null
                                            && !string.IsNullOrEmpty(blkForm["ExpiryDate"])
                                            && !string.IsNullOrEmpty(SplitRowData[Convert.ToInt64(blkForm["ExpiryDate"])]))
                                        {
                                            ExpiryDate = new DateTime();
                                            ExpiryDate = DateTime.ParseExact(SplitRowData[Convert.ToInt64(blkForm["ExpiryDate"])], blkForm["ExpiryDateFormat"].ToString(),
                                                System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
                                        }
                                        stockDetails.Add(new StockDetail
                                        {
                                            Stock = Convert.ToDecimal(SplitRowData[Convert.ToInt64(blkForm["Stock"])]),
                                            ProductId = Convert.ToInt64(id),
                                            WareHouseId = Convert.ToInt64(blkForm["WareHouseId"]),

                                            GRNDate = GRNDate,
                                            BatchNo = !string.IsNullOrEmpty(blkForm["BatchNo"]) ? SplitRowData[Convert.ToInt64(blkForm["BatchNo"])] : string.Empty,
                                            ExpiryDate = ExpiryDate,

                                            IsPerishable = blkForm["IsPerishable"] != null && Convert.ToBoolean(blkForm["IsPerishable"]),
                                            Remarks = !string.IsNullOrEmpty(blkForm["Remarks"]) ? SplitRowData[Convert.ToInt64(blkForm["Remarks"])] : string.Empty
                                        });
                                    }
                                }
                            }
                        }
                }
                catch (Exception ex)
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Message = ex.Message,
                        Status = false
                    };
                    return RedirectToAction("BulkOpeningStock", new { id = UrlIdEncrypt(id, false) });
                }
                #endregion

                if (Convert.ToInt64(stockLogic.AddOpeningStock(stockDetails, Convert.ToInt64(id))) > 0)
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Message = "Opening Stock Entry Successful",
                        Status = true
                    };
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ReturnMessage = new ReturnObject()
                    {
                        Message = "Unsuccess",
                        Status = false
                    };
                }
                return RedirectToAction("BulkOpeningStock", new { id = UrlIdEncrypt(id, false) });
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        #region Product Type
        [SessionAuthenticate]
        public ActionResult Type()
        {
            try
            {
                ProductModels productModels = new ProductModels()
                {
                    _productTypes = new ProductLogic(LogicHelper).TypeGet()
                };
                if (!string.IsNullOrEmpty(id))
                {
                    productModels._productType = productModels._productTypes.Where(a => a.ProductTypeId == Convert.ToInt64(id)).FirstOrDefault();
                }
                return View(productModels);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToView(ex);
            }
        }

        [HttpPost]
        public ActionResult Type(ProductType productType)
        {
            try
            {
                productType.ProductTypeId = id == null ? 0 : Convert.ToInt64(id);

                #region Data Tracking...
                DataTrackingLogicSet(productType);
                #endregion

                ProductLogic productTypeLogic = new ProductLogic(LogicHelper);
                if (Convert.ToInt64(productTypeLogic.TypeSet(productType)) > 0)
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
                    ProductLogic productTypeLogic = new ProductLogic(LogicHelper);
                    long deactivateId = Convert.ToInt64(productTypeLogic.TypeDeactive(id));
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

        #region Get Filtred Product as Json
        [HttpGet]
        public JsonResult In()
        {
            try
            {
                return Json(new ProductLogic(LogicHelper)
                    .GetIn()
                    .Select(p => new ProductFiltered()
                    {
                        label = p.ProductName +
                            (string.IsNullOrEmpty(p.ProductCode) ? "" : " (" + p.ProductCode + ")" +
                            (string.IsNullOrEmpty(p.Brand) ? "" : " of " + p.Brand)),
                        value = p.ProductName +
                            (string.IsNullOrEmpty(p.ProductCode) ? "" : " (" + p.ProductCode + ")" +
                            (string.IsNullOrEmpty(p.Brand) ? "" : " of " + p.Brand)),
                        ProductId = p.ProductId,
                        HSNSAC = p.HSNSAC,
                        Description = p.Description,
                        UnitOfMeasureName = p.UnitOfMeasureName,
                        TaxPercentage = p.TaxPercentage,
                        PurchasePrice = p.PurchasePrice,
                        IsGoods = p.IsGoods
                    }).ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToJson(ex);
            } 
        }
        [HttpGet]
        public JsonResult Out()
        {
            try
            {
                return Json(new ProductLogic(LogicHelper)
                    .GetOut()
                    .Select(p => new ProductFiltered()
                    {
                        label = p.ProductName +
                            (string.IsNullOrEmpty(p.ProductCode) ? "" : " (" + p.ProductCode + ")" +
                            (string.IsNullOrEmpty(p.Brand) ? "" : " of " + p.Brand)),
                        value = p.ProductName + 
                            (string.IsNullOrEmpty(p.ProductCode) ? "" : " (" + p.ProductCode + ")" +
                            (string.IsNullOrEmpty(p.Brand)? "":" of " + p.Brand)),
                        ProductId = p.ProductId,
                        HSNSAC = p.HSNSAC,
                        Description = p.Description,
                        UnitOfMeasureName = p.UnitOfMeasureName,
                        TaxPercentage = p.TaxPercentage,
                        SalePrice = p.SalePrice,
                        IsGoods = p.IsGoods
                    }).ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToJson(ex);
            }
        }

        /// <summary>
        /// Only Shipment Direct
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GoodsOut()
        {
            try
            {
                return Json(new ProductLogic(LogicHelper)
                    .GetOut().Where(a => a.IsGoods)
                    .Select(p => new ProductFiltered()
                    {
                        label = p.ProductName +
                            (string.IsNullOrEmpty(p.ProductCode) ? "" : " (" + p.ProductCode + ")" +
                            (string.IsNullOrEmpty(p.Brand) ? "" : " of " + p.Brand)),
                        value = p.ProductName +
                            (string.IsNullOrEmpty(p.ProductCode) ? "" : " (" + p.ProductCode + ")" +
                            (string.IsNullOrEmpty(p.Brand) ? "" : " of " + p.Brand)),
                        ProductId = p.ProductId,
                        HSNSAC = p.HSNSAC,
                        Description = p.Description,
                        UnitOfMeasureName = p.UnitOfMeasureName,
                        TaxPercentage = p.TaxPercentage,
                        SalePrice = p.SalePrice,
                        IsGoods = p.IsGoods
                    }).ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ErrorLoggingToJson(ex);
            }
        }
        #endregion
    }
}