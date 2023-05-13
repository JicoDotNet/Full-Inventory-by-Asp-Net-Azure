using DataAccess.Sql;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class.Custom;
using JicoDotNet.Inventory.BusinessLayer.DTO.SP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class GoodsReceiveNoteLogic : ConnectionString
    {
        public GoodsReceiveNoteLogic(sCommonDto CommonObj) : base(CommonObj) { }

        public List<GoodsReceiveNote> GetGRNs()
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetGRN]",
                new nameValuePairs
                {
                     
                     
                    new nameValuePair("@QueryType", "LIST")
                }).ToList<GoodsReceiveNote>();
        }

        public List<PurchaseOrder> GetForGRN()
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            nameValuePairs nvp = new nameValuePairs()
            {
                 
                 
                new nameValuePair("@QueryType", "ENTRY")
            };
            List<PurchaseOrder> purchaseOrders = _sqlDBAccess.GetData("[dbo].[spGetGRN]", nvp).ToList<PurchaseOrder>();
            return purchaseOrders;
        }

        public PurchaseOrder GetForGRN(long purchaseOrderId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            nameValuePairs nvp = new nameValuePairs()
            {
                 
                 
                new nameValuePair("@PurchaseOrderId", purchaseOrderId),
                new nameValuePair("@QueryType", "ENTRYSINGLE")
            };
            PurchaseOrder purchaseOrder = _sqlDBAccess.GetData("[dbo].[spGetGRN]", nvp).FirstOrDefault<PurchaseOrder>();
            return purchaseOrder;
        }

        public GoodsReceiveNote GetForDetail(long GRNId)
        {
            GoodsReceiveNote goodsReceiveNote = new GoodsReceiveNote();
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            nameValuePairs nvp = new nameValuePairs()
            {
                 
                 
                new nameValuePair("@GRNId", GRNId),
                new nameValuePair("@QueryType", "DETAIL")
            };
            DataSet ds = _sqlDBAccess.GetDataSet("[dbo].[spGetGRN]", nvp);
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
            nameValuePairs nvp = new nameValuePairs()
                {
                     
                     
                    new nameValuePair("@PurchaseOrderId", purchaseOrderId),
                    new nameValuePair("@QueryType", "COMULTATIVE")
                };
            List<GoodsReceiveNoteDetail> grndtl = _sqlDBAccess.GetData("[dbo].[spGetGRN]", nvp).ToList<GoodsReceiveNoteDetail>();
            return grndtl;
        }

        public string Receive(GoodsReceiveNote goodsReceiveNote)
        {
            List<GRNDetailType> grnDetailTypes = new List<GRNDetailType>();
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
                    .InsertUpdateDeleteReturnObject("[dbo].[spSetGRN]", new nameValuePairs
                    {
                        new nameValuePair("@PurchaseOrderId", goodsReceiveNote.PurchaseOrderId),
                         
                         
                        new nameValuePair("@GRNNumber", "GRN-"),
                        new nameValuePair("@GRNDate", goodsReceiveNote.GRNDate),
                        new nameValuePair("@IsDirect", false),
                        new nameValuePair("@IsFullReceived", goodsReceiveNote.IsFullReceived),
                        new nameValuePair("@VendorDONumber", goodsReceiveNote.VendorDONumber),
                        new nameValuePair("@VendorInvoiceNumber", goodsReceiveNote.VendorInvoiceNumber),
                        new nameValuePair("@VendorInvoiceDate", goodsReceiveNote.VendorInvoiceDate > new DateTime(2001, 1, 1)?
                                                                (object)goodsReceiveNote.VendorInvoiceDate : DBNull.Value),
                        new nameValuePair("@WareHouseId", goodsReceiveNote.WareHouseId),
                        new nameValuePair("@Remarks", goodsReceiveNote.Remarks),
                        new nameValuePair("@RequestId", CommonObj.RequestId),
                        new nameValuePair("@GRNDetails", grnDetailTypes.ToDataTable()),
                        new nameValuePair("@QueryType", "INSERT")
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
            List<GRNDetailType> grnDetailTypes = new List<GRNDetailType>();
            List<PurchaseOrderDetailType> orderDetailTypes = new List<PurchaseOrderDetailType>();
            int count = 1;
            goodsReceiveNoteDirect.GoodsReceiveNoteDirectDetails.ForEach(grnD =>
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
                nameValuePairs nvp = new nameValuePairs()
                {
                     
                     

                    //PO
                    new nameValuePair("@PurchaseTypeId", goodsReceiveNoteDirect.PurchaseTypeId),
                    new nameValuePair("@VendorId", goodsReceiveNoteDirect.VendorId),
                    new nameValuePair("@PurchaseOrderDate", goodsReceiveNoteDirect.GRNDate),
                    new nameValuePair("@PurchaseOrderAmount", goodsReceiveNoteDirect.PurchaseOrderAmount),
                    new nameValuePair("@PurchaseOrderTaxAmount", goodsReceiveNoteDirect.PurchaseOrderTaxAmount),
                    new nameValuePair("@PurchaseOrderTotalAmount", goodsReceiveNoteDirect.PurchaseOrderTotalAmount),                    
                    new nameValuePair("@PurchaseOrderDetails", orderDetailTypes.ToDataTable()),

                    //GRN                       
                    new nameValuePair("@GRNNumber", "GRN-"),
                    new nameValuePair("@GRNDate", goodsReceiveNoteDirect.GRNDate),
                    new nameValuePair("@IsDirect", true),
                    new nameValuePair("@IsFullReceived", true),
                    new nameValuePair("@VendorDONumber", goodsReceiveNoteDirect.VendorDONumber),
                    new nameValuePair("@VendorInvoiceNumber", goodsReceiveNoteDirect.VendorInvoiceNumber),
                    new nameValuePair("@VendorInvoiceDate", goodsReceiveNoteDirect.VendorInvoiceDate > new DateTime(2001, 1, 1)?
                                                            (object)goodsReceiveNoteDirect.VendorInvoiceDate : DBNull.Value),
                    new nameValuePair("@WareHouseId", goodsReceiveNoteDirect.WareHouseId),                    
                    new nameValuePair("@GRNDetail", grnDetailTypes.ToDataTable()),

                    // Common
                    new nameValuePair("@Remarks", goodsReceiveNoteDirect.Remarks),

                    new nameValuePair("@RequestId", CommonObj.RequestId),                    
                    new nameValuePair("@QueryType", "INSERT")
                };
                return _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetGRNDirect]", nvp, "@OutParam").ToString();
            }
            return string.Empty;
        }

        public List<GoodsReceiveNote> GetForReturn()
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            nameValuePairs nvp = new nameValuePairs()
            {
                 
                 
                new nameValuePair("@QueryType", "RETURN")
            };
            List<GoodsReceiveNote> purchaseOrders = _sqlDBAccess.GetData("[dbo].[spGetGRN]", nvp).ToList<GoodsReceiveNote>();
            return purchaseOrders;
        }
    }
}
