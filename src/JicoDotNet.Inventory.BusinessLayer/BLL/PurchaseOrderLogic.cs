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

            NameValuePairs nvp = new NameValuePairs
            {
                 
                 
                new NameValuePair("@PurchaseTypeId", purchaseType.PurchaseTypeId),
                new NameValuePair("@PurchaseTypeName", purchaseType.PurchaseTypeName),
                new NameValuePair("@Description", purchaseType.Description),
                new NameValuePair("@RequestId", CommonObj.RequestId),
                new NameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetPurchaseType]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public string TypeDeactive(string PurchaseTypeId)
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString)
                .InsertUpdateDeleteReturnObject("[dbo].[spSetPurchaseType]", new NameValuePairs
                {
                    new NameValuePair("@PurchaseTypeId", PurchaseTypeId),
                     
                     
                    new NameValuePair("@RequestId", CommonObj.RequestId),
                    new NameValuePair("@QueryType", "INACTIVE")
                }, "@OutParam").ToString();
        }

        public List<PurchaseType> TypeGet(bool? IsActive = null)
        {
            List<PurchaseType> purchaseTypes = new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetPurchaseType]",
                new NameValuePairs
                {
                     
                     
                    new NameValuePair("@QueryType", "ALL")
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
            NameValuePairs nvp = new NameValuePairs
            {
                 
                 
                new NameValuePair("@QueryType", "ENTRY")
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
                NameValuePairs nvp = new NameValuePairs
                {
                    new NameValuePair("@PurchaseOrderId", purchaseOrder.PurchaseOrderId),
                     
                     

                    new NameValuePair("@PurchaseTypeId", purchaseOrder.PurchaseTypeId),
                    new NameValuePair("@BranchId", purchaseOrder.BranchId),
                    new NameValuePair("@VendorId", purchaseOrder.VendorId),
                    new NameValuePair("@PurchaseOrderDate", purchaseOrder.PurchaseOrderDate),

                    new NameValuePair("@PurchaseOrderNumber", "PO-"),

                    new NameValuePair("@AmendmentNumber", purchaseOrder.AmendmentNumber),
                    new NameValuePair("@AmendmentDate", purchaseOrder.AmendmentDate > purchaseOrder.PurchaseOrderDate? (object)purchaseOrder.AmendmentDate: DBNull.Value),
                    new NameValuePair("@DeliveryDate", purchaseOrder.DeliveryDate > purchaseOrder.PurchaseOrderDate? (object)purchaseOrder.DeliveryDate: DBNull.Value),
                    new NameValuePair("@PurchaseOrderAmount", purchaseOrder.PurchaseOrderAmount),
                    new NameValuePair("@PurchaseOrderTaxAmount", purchaseOrder.PurchaseOrderTaxAmount),
                    new NameValuePair("@PurchaseOrderTotalAmount", purchaseOrder.PurchaseOrderTotalAmount),
                    new NameValuePair("@TandC", purchaseOrder.TandC),
                    new NameValuePair("@Remarks", purchaseOrder.Remarks),
                    new NameValuePair("@PurchaseOrderDetails", orderDetailTypes.ToDataTable()),
                    new NameValuePair("@RequestId", CommonObj.RequestId),
                    new NameValuePair("@QueryType", "ENTRY")
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
                NameValuePairs nvp = new NameValuePairs
                {
                    new NameValuePair("@PurchaseOrderId", PurchaseOrderId),
                     
                     
                    new NameValuePair("@RequestId", CommonObj.RequestId),
                    new NameValuePair("@QueryType", "DELETE")
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
            NameValuePairs nvp = new NameValuePairs()
            {
                 
                 
                new NameValuePair("@PurchaseOrderId", PurchaseOrderId),
                new NameValuePair("@QueryType", "DETAIL")
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
            NameValuePairs nvp = new NameValuePairs()
                {
                     
                     
                    new NameValuePair("@QueryType", "LIST")
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
                    NameValuePairs nvp = new NameValuePairs
                    {
                        new NameValuePair("@PurchaseOrderId", purchaseOrder.PurchaseOrderId),
                         
                         
                        new NameValuePair("@AmendmentNumber", ""),
                        new NameValuePair("@AmendmentDate", purchaseOrder.AmendmentDate),
                        new NameValuePair("@PurchaseOrderAmount", purchaseOrder.PurchaseOrderAmount),
                        new NameValuePair("@PurchaseOrderTaxAmount", purchaseOrder.PurchaseOrderTaxAmount),
                        new NameValuePair("@PurchaseOrderTotalAmount", purchaseOrder.PurchaseOrderTotalAmount),
                        new NameValuePair("@TandC", purchaseOrder.TandC),
                        new NameValuePair("@Remarks", purchaseOrder.Remarks),
                        new NameValuePair("@PurchaseOrderDetails", orderDetailTypes.ToDataTable()),
                        new NameValuePair("@RequestId", CommonObj.RequestId),
                        new NameValuePair("@QueryType", "AMENDMENT")
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
                    .InsertUpdateDeleteReturnObject("[dbo].[spSetPurchaseReturn]", new NameValuePairs
                    {
                         
                         
                        new NameValuePair("@GRNId", purchaseReturn.GRNId),
                        new NameValuePair("@PurchaseOrderId", purchaseReturn.PurchaseOrderId),
                        new NameValuePair("@WareHouseId", purchaseReturn.WareHouseId),
                        new NameValuePair("@PurchaseReturnNumber", "PR-"),
                        new NameValuePair("@PurchaseReturnDate", purchaseReturn.PurchaseReturnDate),
                        new NameValuePair("@Reason", purchaseReturn.Reason),
                        new NameValuePair("@Remarks", purchaseReturn.Remarks),
                        new NameValuePair("@IsFullReturned", purchaseReturn.IsFullReturned),
                        new NameValuePair("@PurchaseReturnDetails", PReturnDetailTypes.ToDataTable()),
                        new NameValuePair("@RequestId", CommonObj.RequestId),                        
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
    }
}