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
    public class PurchaseOrderLogic : ConnectionString
    {
        public PurchaseOrderLogic(sCommonDto CommonObj) : base(CommonObj) { }

        #region PO Type
        public string TypeSet(PurchaseType purchaseType)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = string.Empty;
            if (purchaseType.PurchaseTypeId > 0)
                qt = "UPDATE";
            else
                qt = "INSERT";

            nameValuePairs nvp = new nameValuePairs
            {
                 
                 
                new nameValuePair("@PurchaseTypeId", purchaseType.PurchaseTypeId),
                new nameValuePair("@PurchaseTypeName", purchaseType.PurchaseTypeName),
                new nameValuePair("@Description", purchaseType.Description),
                new nameValuePair("@RequestId", CommonObj.RequestId),
                new nameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetPurchaseType]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public string TypeDeactive(string PurchaseTypeId)
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString)
                .InsertUpdateDeleteReturnObject("[dbo].[spSetPurchaseType]", new nameValuePairs
                {
                    new nameValuePair("@PurchaseTypeId", PurchaseTypeId),
                     
                     
                    new nameValuePair("@RequestId", CommonObj.RequestId),
                    new nameValuePair("@QueryType", "INACTIVE")
                }, "@OutParam").ToString();
        }

        public List<PurchaseType> TypeGet(bool? IsActive = null)
        {
            List<PurchaseType> purchaseTypes = new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetPurchaseType]",
                new nameValuePairs
                {
                     
                     
                    new nameValuePair("@QueryType", "ALL")
                }).ToList<PurchaseType>();
            if (IsActive != null)
            {
                if (IsActive.Value)
                    return purchaseTypes.Where(a => a.IsActive).ToList();
                if (!IsActive.Value)
                    return purchaseTypes.Where(a => !a.IsActive).ToList();
            }
            return purchaseTypes;
        } 
        #endregion

        public Dictionary<string, object> GetForEntry()
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            nameValuePairs nvp = new nameValuePairs
            {
                 
                 
                new nameValuePair("@QueryType", "ENTRY")
            };
            DataSet dataSet = _sqlDBAccess.GetDataSet("[dbo].[spGetPurchaseOrder]", nvp);
            Dictionary<string, object> Datas = new Dictionary<string, object>
            {
                { "PurchaseType", dataSet.Tables[0].ToList<PurchaseType>() },
                { "Branch", dataSet.Tables[1].ToList<Branch>() },
                { "Vendor", dataSet.Tables[2].ToList<Vendor>() },
                { "Product", dataSet.Tables[3].ToList<Product>().Where(prod => prod.IsActive &&  (prod.ProductInOut == 0 || prod.ProductInOut == 1)).ToList() }
            };
            return Datas;
        }

        public string SetForEntry(PurchaseOrder purchaseOrder)
        {
            string ReturnDS = string.Empty;
            List<PurchaseOrderDetailType> orderDetailTypes = new List<PurchaseOrderDetailType>();
            int count = 1;
            purchaseOrder.PurchaseOrderDetails.ForEach(item =>
            {
                if (item.Quantity > 0)
                {
                    orderDetailTypes.Add(new PurchaseOrderDetailType()
                    {
                        Id = count++,
                        AmendmentNumber = item.AmendmentNumber,
                        Amount = item.Amount,
                        Description = item.Description,
                        DiscountAmount = item.DiscountAmount,
                        DiscountPercentage = item.DiscountPercentage,
                        HSNSAC = item.HSNSAC,
                        IsActive = item.IsActive,
                        Price = item.Price,
                        ProductId = item.ProductId,
                        PurchaseOrderDetailId = item.PurchaseOrderDetailId,
                        PurchaseOrderId = item.PurchaseOrderId,
                        PurchaseOrderNumber = item.PurchaseOrderNumber,
                        Quantity = item.Quantity,
                        RequestId = item.RequestId,
                        SubTotal = item.SubTotal,
                        TaxAmount = item.TaxAmount,
                        TaxPercentage = item.TaxPercentage,
                        Total = item.Total,
                        TransactionDate = item.TransactionDate
                    });
                }
            });
            if (orderDetailTypes.Count > 0)
            {
                _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
                nameValuePairs nvp = new nameValuePairs
                {
                    new nameValuePair("@PurchaseOrderId", purchaseOrder.PurchaseOrderId),
                     
                     

                    new nameValuePair("@PurchaseTypeId", purchaseOrder.PurchaseTypeId),
                    new nameValuePair("@BranchId", purchaseOrder.BranchId),
                    new nameValuePair("@VendorId", purchaseOrder.VendorId),
                    new nameValuePair("@PurchaseOrderDate", purchaseOrder.PurchaseOrderDate),

                    new nameValuePair("@PurchaseOrderNumber", "PO-"),

                    new nameValuePair("@AmendmentNumber", purchaseOrder.AmendmentNumber),
                    new nameValuePair("@AmendmentDate", purchaseOrder.AmendmentDate > purchaseOrder.PurchaseOrderDate? (object)purchaseOrder.AmendmentDate: DBNull.Value),
                    new nameValuePair("@DeliveryDate", purchaseOrder.DeliveryDate > purchaseOrder.PurchaseOrderDate? (object)purchaseOrder.DeliveryDate: DBNull.Value),
                    new nameValuePair("@PurchaseOrderAmount", purchaseOrder.PurchaseOrderAmount),
                    new nameValuePair("@PurchaseOrderTaxAmount", purchaseOrder.PurchaseOrderTaxAmount),
                    new nameValuePair("@PurchaseOrderTotalAmount", purchaseOrder.PurchaseOrderTotalAmount),
                    new nameValuePair("@TandC", purchaseOrder.TandC),
                    new nameValuePair("@Remarks", purchaseOrder.Remarks),
                    new nameValuePair("@PurchaseOrderDetails", orderDetailTypes.ToDataTable()),
                    new nameValuePair("@RequestId", CommonObj.RequestId),
                    new nameValuePair("@QueryType", "ENTRY")
                };
                ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetPurchaseOrder]", nvp, "@OutParam").ToString();
            }
            return ReturnDS;
        }

        /// <summary>
        /// Delete or Inactive PO, if there are no Bills Generated or Goods Received 
        /// </summary>
        public string Deactive(long PurchaseOrderId)
        {
            try
            {
                _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
                nameValuePairs nvp = new nameValuePairs
                {
                    new nameValuePair("@PurchaseOrderId", PurchaseOrderId),
                     
                     
                    new nameValuePair("@RequestId", CommonObj.RequestId),
                    new nameValuePair("@QueryType", "DELETE")
                };
                string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetPurchaseOrder]", nvp, "@OutParam").ToString();
                return ReturnDS;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PurchaseOrder GetForDetail(long PurchaseOrderId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            nameValuePairs nvp = new nameValuePairs()
            {
                 
                 
                new nameValuePair("@PurchaseOrderId", PurchaseOrderId),
                new nameValuePair("@QueryType", "DETAIL")
            };
            DataSet ds = _sqlDBAccess.GetDataSet("[dbo].[spGetPurchaseOrder]", nvp);
            PurchaseOrder purchaseOrder = ds.Tables[0].FirstOrDefault<PurchaseOrder>();
            if (purchaseOrder != null)
            {
                purchaseOrder.PurchaseOrderDetails = new List<PurchaseOrderDetail>();
                purchaseOrder.PurchaseOrderDetails = ds.Tables[1].Rows.Count > 0 ? ds.Tables[1].ToList<PurchaseOrderDetail>() : null;
                if (purchaseOrder.PurchaseOrderDetails == null)
                    purchaseOrder = null;
            }
            return purchaseOrder;
        }

        public List<PurchaseOrder> GetPOs()
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            nameValuePairs nvp = new nameValuePairs()
                {
                     
                     
                    new nameValuePair("@QueryType", "LIST")
                };
            return _sqlDBAccess.GetData("[dbo].[spGetPurchaseOrder]", nvp).ToList<PurchaseOrder>();
        }

        /// <summary>
        /// PO Amendment
        /// </summary>
        /// <returns>PurchaseOrderId & AmendmentNumber</returns>
        public string SetForAmendment(PurchaseOrder purchaseOrder)
        {
            try
            {
                string ReturnDS = string.Empty;
                List<PurchaseOrderDetailType> orderDetailTypes = new List<PurchaseOrderDetailType>();
                int count = 1;
                purchaseOrder.PurchaseOrderDetails.ForEach(item =>
                {
                    if (item.Quantity > 0)
                    {
                        orderDetailTypes.Add(new PurchaseOrderDetailType()
                        {
                            Id = count++,
                            AmendmentNumber = item.AmendmentNumber,
                            Amount = item.Amount,
                            Description = item.Description,
                            DiscountAmount = item.DiscountAmount,
                            DiscountPercentage = item.DiscountPercentage,
                            HSNSAC = item.HSNSAC,
                            IsActive = item.IsActive,
                            Price = item.Price,
                            ProductId = item.ProductId,
                            PurchaseOrderDetailId = item.PurchaseOrderDetailId,
                            PurchaseOrderId = item.PurchaseOrderId,
                            PurchaseOrderNumber = item.PurchaseOrderNumber,
                            Quantity = item.Quantity,
                            RequestId = item.RequestId,
                            SubTotal = item.SubTotal,
                            TaxAmount = item.TaxAmount,
                            TaxPercentage = item.TaxPercentage,
                            Total = item.Total,
                            TransactionDate = item.TransactionDate
                        });
                    }
                });
                if (orderDetailTypes.Count > 0)
                {
                    _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
                    nameValuePairs nvp = new nameValuePairs
                    {
                        new nameValuePair("@PurchaseOrderId", purchaseOrder.PurchaseOrderId),
                         
                         
                        new nameValuePair("@AmendmentNumber", ""),
                        new nameValuePair("@AmendmentDate", purchaseOrder.AmendmentDate),
                        new nameValuePair("@PurchaseOrderAmount", purchaseOrder.PurchaseOrderAmount),
                        new nameValuePair("@PurchaseOrderTaxAmount", purchaseOrder.PurchaseOrderTaxAmount),
                        new nameValuePair("@PurchaseOrderTotalAmount", purchaseOrder.PurchaseOrderTotalAmount),
                        new nameValuePair("@TandC", purchaseOrder.TandC),
                        new nameValuePair("@Remarks", purchaseOrder.Remarks),
                        new nameValuePair("@PurchaseOrderDetails", orderDetailTypes.ToDataTable()),
                        new nameValuePair("@RequestId", CommonObj.RequestId),
                        new nameValuePair("@QueryType", "AMENDMENT")
                    };
                    ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetPurchaseOrder]", nvp, "@OutParam").ToString();
                }
                return ReturnDS;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public string Return(PurchaseReturn purchaseReturn)
        {
            List<PurchaseReturnDetailType> PReturnDetailTypes = new List<PurchaseReturnDetailType>();
            int count = 1; 
            purchaseReturn.PurchaseReturnDetails.ForEach(porD =>
            {
                if (porD.ReturnedQuantity > 0)
                {
                    PReturnDetailTypes.Add(new PurchaseReturnDetailType()
                    {
                        Id = count++,
                        GRNDetailId = porD.GRNDetailId,
                        PurchaseOrderDetailId = porD.PurchaseOrderDetailId,
                        ProductId = porD.ProductId,
                        ReturnedQuantity = porD.ReturnedQuantity,
                        Description = porD.Description,
                        IsPerishable = porD.IsPerishable,
                        BatchNo = porD.BatchNo?.Trim(),
                        ExpiryDate = porD.ExpiryDate?.AddDays(1).AddSeconds(-1)
                    });
                }
            });
            if (PReturnDetailTypes.Count > 0)
            {
                return new SqlDBAccess(CommonObj.SqlConnectionString)
                    .InsertUpdateDeleteReturnObject("[dbo].[spSetPurchaseReturn]", new nameValuePairs
                    {
                         
                         
                        new nameValuePair("@GRNId", purchaseReturn.GRNId),
                        new nameValuePair("@PurchaseOrderId", purchaseReturn.PurchaseOrderId),
                        new nameValuePair("@WareHouseId", purchaseReturn.WareHouseId),
                        new nameValuePair("@PurchaseReturnNumber", "PR-"),
                        new nameValuePair("@PurchaseReturnDate", purchaseReturn.PurchaseReturnDate),
                        new nameValuePair("@Reason", purchaseReturn.Reason),
                        new nameValuePair("@Remarks", purchaseReturn.Remarks),
                        new nameValuePair("@IsFullReturned", purchaseReturn.IsFullReturned),
                        new nameValuePair("@PurchaseReturnDetails", PReturnDetailTypes.ToDataTable()),
                        new nameValuePair("@RequestId", CommonObj.RequestId),                        
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
    }
}