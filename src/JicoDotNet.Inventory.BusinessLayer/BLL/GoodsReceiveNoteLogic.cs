using DataAccess.Sql;
using JicoDotNet.Inventory.BusinessLayer.Common;
using System;
using System.Collections.Generic;
using System.Data;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Custom;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Models;
using JicoDotNet.Inventory.Core.Custom.Interface;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class GoodsReceiveNoteLogic : ConnectionString
    {
        public GoodsReceiveNoteLogic(ICommonRequestDto CommonObj) : base(CommonObj) { }

        public List<GoodsReceiveNote> GetGRNs()
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString).GetData(GenericLogic.SqlSchema + ".[spGetGRN]",
                new NameValuePairs
                {
                     
                     
                    new NameValuePair("@QueryType", "LIST")
                }).ToList<GoodsReceiveNote>();
        }

        public List<PurchaseOrder> GetForGRN()
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            NameValuePairs nvp = new NameValuePairs()
            {
                 
                 
                new NameValuePair("@QueryType", "ENTRY")
            };
            List<PurchaseOrder> purchaseOrders = _sqlDBAccess.GetData(GenericLogic.SqlSchema + ".[spGetGRN]", nvp).ToList<PurchaseOrder>();
            return purchaseOrders;
        }

        public PurchaseOrder GetForGRN(long purchaseOrderId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            NameValuePairs nvp = new NameValuePairs()
            {
                 
                 
                new NameValuePair("@PurchaseOrderId", purchaseOrderId),
                new NameValuePair("@QueryType", "ENTRYSINGLE")
            };
            PurchaseOrder purchaseOrder = _sqlDBAccess.GetData(GenericLogic.SqlSchema + ".[spGetGRN]", nvp).FirstOrDefault<PurchaseOrder>();
            return purchaseOrder;
        }

        public GoodsReceiveNote GetForDetail(long GRNId)
        {
            GoodsReceiveNote goodsReceiveNote = new GoodsReceiveNote();
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            NameValuePairs nvp = new NameValuePairs()
            {
                 
                 
                new NameValuePair("@GRNId", GRNId),
                new NameValuePair("@QueryType", "DETAIL")
            };
            DataSet ds = _sqlDBAccess.GetDataSet(GenericLogic.SqlSchema + ".[spGetGRN]", nvp);
            goodsReceiveNote = ds.Tables[0].FirstOrDefault<GoodsReceiveNote>();
            if (goodsReceiveNote != null)
            {
                goodsReceiveNote.GoodsReceiveNoteDetails = new List<GoodsReceiveNoteDetail>();
                goodsReceiveNote.GoodsReceiveNoteDetails = ds.Tables[1].Rows.Count > 0 ? ds.Tables[1].ToList<GoodsReceiveNoteDetail>() : null;
                if (goodsReceiveNote.GoodsReceiveNoteDetails == null)
                    goodsReceiveNote = null;
            }
            return goodsReceiveNote;
        }

        public List<GoodsReceiveNoteDetail> GetGRNDetails(long purchaseOrderId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            NameValuePairs nvp = new NameValuePairs()
                {
                     
                     
                    new NameValuePair("@PurchaseOrderId", purchaseOrderId),
                    new NameValuePair("@QueryType", "COMULTATIVE")
                };
            List<GoodsReceiveNoteDetail> grndtl = _sqlDBAccess.GetData(GenericLogic.SqlSchema + ".[spGetGRN]", nvp).ToList<GoodsReceiveNoteDetail>();
            return grndtl;
        }

        public string Receive(GoodsReceiveNote goodsReceiveNote)
        {
            List<IGRNDetailType> grnDetailTypes = new List<IGRNDetailType>();
            int count = 1;
            goodsReceiveNote.GoodsReceiveNoteDetails.ForEach(grnD =>
            {
                if (grnD.ReceivedQuantity > 0)
                {
                    grnDetailTypes.Add(new GRNDetailType()
                    {
                        Id = count++,
                        PurchaseOrderDetailId = grnD.PurchaseOrderDetailId,
                        ProductId = grnD.ProductId,                        
                        ReceivedQuantity = grnD.ReceivedQuantity,
                        Description = grnD.Description,
                        IsPerishable = grnD.IsPerishable,
                        BatchNo = grnD.BatchNo?.Trim(),
                        ExpiryDate = grnD.ExpiryDate?.AddDays(1).AddSeconds(-1)
                    });
                }
            });
            if (grnDetailTypes.Count > 0)
            {
                return new SqlDBAccess(CommonObj.SqlConnectionString)
                    .DataManipulation(GenericLogic.SqlSchema + ".[spSetGRN]", new NameValuePairs
                    {
                        new NameValuePair("@PurchaseOrderId", goodsReceiveNote.PurchaseOrderId),
                         
                         
                        new NameValuePair("@GRNNumber", "GRN-"),
                        new NameValuePair("@GRNDate", goodsReceiveNote.GRNDate),
                        new NameValuePair("@IsDirect", false),
                        new NameValuePair("@IsFullReceived", goodsReceiveNote.IsFullReceived),
                        new NameValuePair("@VendorDONumber", goodsReceiveNote.VendorDONumber),
                        new NameValuePair("@VendorInvoiceNumber", goodsReceiveNote.VendorInvoiceNumber),
                        new NameValuePair("@VendorInvoiceDate", goodsReceiveNote.VendorInvoiceDate > new DateTime(2001, 1, 1)?
                                                                (object)goodsReceiveNote.VendorInvoiceDate : DBNull.Value),
                        new NameValuePair("@WareHouseId", goodsReceiveNote.WareHouseId),
                        new NameValuePair("@Remarks", goodsReceiveNote.Remarks),
                        new NameValuePair("@RequestId", CommonObj.RequestId),
                        new NameValuePair("@GRNDetail", grnDetailTypes.ToDataTable()),
                        new NameValuePair("@QueryType", "INSERT")
                    },
                    "@OutParam"
                ).ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public string SetDirect(GoodsReceiveNoteDirect goodsReceiveNoteDirect)
        {
            List<IGRNDetailType> grnDetailTypes = new List<IGRNDetailType>();
            List<IPurchaseOrderDetailType> orderDetailTypes = new List<IPurchaseOrderDetailType>();
            int count = 1;
            goodsReceiveNoteDirect.GoodsReceiveNoteDirectDetails.ForEach(grnD =>
            {
                if (grnD.ReceivedQuantity > 0)
                {
                    grnDetailTypes.Add(new GRNDetailType
                    {
                        Id = count++,
                        PurchaseOrderDetailId = grnD.PurchaseOrderDetailId,
                        ProductId = grnD.ProductId,
                        ReceivedQuantity = grnD.ReceivedQuantity,
                        Description = grnD.Description,
                        IsPerishable = grnD.IsPerishable,
                        BatchNo = grnD.BatchNo?.Trim(),
                        ExpiryDate = grnD.ExpiryDate?.AddDays(1).AddSeconds(-1)
                    });

                    orderDetailTypes.Add(new PurchaseOrderDetailType()
                    {
                        AmendmentNumber = grnD.AmendmentNumber,
                        Amount = grnD.Amount,
                        Description = grnD.Description,
                        DiscountAmount = grnD.DiscountAmount,
                        DiscountPercentage = grnD.DiscountPercentage,
                        HSNSAC = grnD.HSNSAC,
                        IsActive = grnD.IsActive,
                        Price = grnD.Price,
                        ProductId = grnD.ProductId,
                        PurchaseOrderDetailId = grnD.PurchaseOrderDetailId,
                        PurchaseOrderId = grnD.PurchaseOrderId,
                        PurchaseOrderNumber = grnD.PurchaseOrderNumber,
                        Quantity = grnD.ReceivedQuantity,
                        RequestId = grnD.RequestId,
                        SubTotal = grnD.SubTotal,
                        TaxAmount = grnD.TaxAmount,
                        TaxPercentage = grnD.TaxPercentage,
                        Total = grnD.Total,
                        TransactionDate = grnD.TransactionDate
                    });
                }
            });

            if (grnDetailTypes.Count > 0 && orderDetailTypes.Count > 0)
            {
                _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
                NameValuePairs nvp = new NameValuePairs()
                {
                     
                     

                    //PO
                    new NameValuePair("@PurchaseTypeId", goodsReceiveNoteDirect.PurchaseTypeId),
                    new NameValuePair("@VendorId", goodsReceiveNoteDirect.VendorId),
                    new NameValuePair("@PurchaseOrderDate", goodsReceiveNoteDirect.GRNDate),
                    new NameValuePair("@PurchaseOrderAmount", goodsReceiveNoteDirect.PurchaseOrderAmount),
                    new NameValuePair("@PurchaseOrderTaxAmount", goodsReceiveNoteDirect.PurchaseOrderTaxAmount),
                    new NameValuePair("@PurchaseOrderTotalAmount", goodsReceiveNoteDirect.PurchaseOrderTotalAmount),                    
                    new NameValuePair("@PurchaseOrderDetails", orderDetailTypes.ToDataTable()),

                    //GRN                       
                    new NameValuePair("@GRNNumber", "GRN-"),
                    new NameValuePair("@GRNDate", goodsReceiveNoteDirect.GRNDate),
                    new NameValuePair("@IsDirect", true),
                    new NameValuePair("@IsFullReceived", true),
                    new NameValuePair("@VendorDONumber", goodsReceiveNoteDirect.VendorDONumber),
                    new NameValuePair("@VendorInvoiceNumber", goodsReceiveNoteDirect.VendorInvoiceNumber),
                    new NameValuePair("@VendorInvoiceDate", goodsReceiveNoteDirect.VendorInvoiceDate > new DateTime(2001, 1, 1)?
                                                            (object)goodsReceiveNoteDirect.VendorInvoiceDate : DBNull.Value),
                    new NameValuePair("@WareHouseId", goodsReceiveNoteDirect.WareHouseId),                    
                    new NameValuePair("@GRNDetail", grnDetailTypes.ToDataTable()),

                    // Common
                    new NameValuePair("@Remarks", goodsReceiveNoteDirect.Remarks),

                    new NameValuePair("@RequestId", CommonObj.RequestId),                    
                    new NameValuePair("@QueryType", "INSERT")
                };
                return _sqlDBAccess.DataManipulation(GenericLogic.SqlSchema + ".[spSetGRNDirect]", nvp, "@OutParam").ToString();
            }
            return string.Empty;
        }

        public List<GoodsReceiveNote> GetForReturn()
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            NameValuePairs nvp = new NameValuePairs()
            {
                 
                 
                new NameValuePair("@QueryType", "RETURN")
            };
            List<GoodsReceiveNote> purchaseOrders = _sqlDBAccess.GetData(GenericLogic.SqlSchema + ".[spGetGRN]", nvp).ToList<GoodsReceiveNote>();
            return purchaseOrders;
        }
    }
}
