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
    public class SalesOrderLogic : ConnectionString
    {
        public SalesOrderLogic(sCommonDto CommonObj) : base(CommonObj) { }

        #region Sales Type
        public string TypeSet(SalesType salesType)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt;
            if (salesType.SalesTypeId > 0)
                qt = "UPDATE";
            else
                qt = "INSERT";

            nameValuePairs nvp = new nameValuePairs
            {
                 
                 
                new nameValuePair("@SalesTypeId", salesType.SalesTypeId),
                new nameValuePair("@SalesTypeName", salesType.SalesTypeName),
                new nameValuePair("@Description", salesType.Description),
                new nameValuePair("@RequestId", CommonObj.RequestId),
                new nameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetSalesType]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public string TypeDeactive(string SalesTypeId)
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString)
                .InsertUpdateDeleteReturnObject("[dbo].[spSetSalesType]", new nameValuePairs
                {
                    new nameValuePair("@SalesTypeId", SalesTypeId),
                     
                     
                    new nameValuePair("@RequestId", CommonObj.RequestId),
                    new nameValuePair("@QueryType", "INACTIVE")
                }, "@OutParam").ToString();
        }

        public List<SalesType> TypeGet(bool? IsActive = null)
        {
            List<SalesType> salesTypes = new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetSalesType]",
                new nameValuePairs
                {
                     
                     
                    new nameValuePair("@QueryType", "ALL")
                }).ToList<SalesType>();
            if (IsActive != null)
            {
                if (IsActive.Value)
                    return salesTypes.Where(a => a.IsActive).ToList();
                if (!IsActive.Value)
                    return salesTypes.Where(a => !a.IsActive).ToList();
            }
            return salesTypes;
        } 
        #endregion

        public Dictionary<string, object> GetForEntry()
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            nameValuePairs nvp = new nameValuePairs
            {
                 
                 
                    new nameValuePair("@QueryType", "ENTRY")
            };
            DataSet dataSet = _sqlDBAccess.GetDataSet("[dbo].[spGetSalesOrder]", nvp);
            Dictionary<string, object> Datas = new Dictionary<string, object>
            {
                { "SalesType", dataSet.Tables[0].ToList<SalesType>() },
                { "Branch", dataSet.Tables[1].ToList<Branch>() },
                { "Customer", dataSet.Tables[2].ToList<Customer>() },
                { "Product", dataSet.Tables[3].ToList<Product>().Where(prod => prod.IsActive &&  (prod.ProductInOut == 0 || prod.ProductInOut == 2)).ToList() }
            };
            // The LINQ statement above has to changed later.

            return Datas;
        }

        public string SetForEntry(SalesOrder salesOrder)
        {
            string ReturnDS = string.Empty;
            List<SalesOrderDetailType> orderDetailTypes = new List<SalesOrderDetailType>();
            int count = 1;
            salesOrder.SalesOrderDetails.ForEach(item =>
            {
                if (item.Quantity > 0)
                {
                    orderDetailTypes.Add(new SalesOrderDetailType()
                    {
                        Id = count++,
                        AmendmentNumber = item.AmendmentNumber,
                        Amount = item.Amount,
                        Description = item.Description,
                        DiscountAmount = item.DiscountAmount,
                        DiscountPercentage = item.DiscountPercentage,
                        HSNSAC = item.HSNSAC,
                        Price = item.Price,
                        ProductId = item.ProductId,
                        SalesOrderId = item.SalesOrderId,
                        SalesOrderNumber = item.SalesOrderNumber,
                        Quantity = item.Quantity,
                        SubTotal = item.SubTotal,
                        TaxAmount = item.TaxAmount,
                        TaxPercentage = item.TaxPercentage,
                        Total = item.Total
                    });
                }
            });
            if (orderDetailTypes.Count > 0)
            {
                _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
                nameValuePairs nvp = new nameValuePairs
                {
                     
                     
                    new nameValuePair("@QuotationId", salesOrder.QuotationId),
                    new nameValuePair("@SalesTypeId", salesOrder.SalesTypeId),
                    new nameValuePair("@BranchId", salesOrder.BranchId),
                    new nameValuePair("@CustomerId", salesOrder.CustomerId),
                    new nameValuePair("@SalesOrderDate", salesOrder.SalesOrderDate),
                    new nameValuePair("@SalesOrderNumber", "SO-"),
                    new nameValuePair("@AmendmentNumber", salesOrder.AmendmentNumber),
                    new nameValuePair("@AmendmentDate", salesOrder.AmendmentDate > salesOrder.SalesOrderDate? (object)salesOrder.AmendmentDate: DBNull.Value),
                    new nameValuePair("@CustomerPONumber", salesOrder.CustomerPONumber),
                    new nameValuePair("@CustomerPODate", salesOrder.CustomerPODate),
                    new nameValuePair("@DeliveryDate", salesOrder.DeliveryDate > salesOrder.SalesOrderDate? (object)salesOrder.DeliveryDate: DBNull.Value),
                    new nameValuePair("@SalesOrderAmount", salesOrder.SalesOrderAmount),
                    new nameValuePair("@SalesOrderTaxAmount", salesOrder.SalesOrderTaxAmount),
                    new nameValuePair("@SalesOrderTotalAmount", salesOrder.SalesOrderTotalAmount),
                    new nameValuePair("@TandC", salesOrder.TandC),
                    new nameValuePair("@Remarks", salesOrder.Remarks),
                    new nameValuePair("@SalesOrderDetails", orderDetailTypes.ToDataTable()),
                    new nameValuePair("@RequestId", CommonObj.RequestId),
                    new nameValuePair("@QueryType", "ENTRY")
                };
                ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetSalesOrder]", nvp, "@OutParam").ToString();
            }
            return ReturnDS;
        }

        /// <summary>
        /// Delete or Inactive SO, if there are no Invoice Generated or Goods Shipped 
        /// </summary>
        public string Deactive(long SalesOrderId)
        {
            try
            {
                _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
                nameValuePairs nvp = new nameValuePairs
                {
                    new nameValuePair("@SalesOrderId", SalesOrderId),
                     
                     
                    new nameValuePair("@RequestId", CommonObj.RequestId),
                    new nameValuePair("@QueryType", "DELETE")
                };
                string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetSalesOrder]", nvp, "@OutParam").ToString();
                return ReturnDS;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SalesOrder> GetForShipment()
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            nameValuePairs nvp = new nameValuePairs
            {
                 
                 
                    new nameValuePair("@QueryType", "SHIPENTRY")
            };
            return _sqlDBAccess.GetData("[dbo].[spGetSalesOrder]", nvp).ToList<SalesOrder>();
        }
        
        public List<SalesOrder> GetSOs()
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            nameValuePairs nvp = new nameValuePairs()
                {
                     
                     
                    new nameValuePair("@QueryType", "LIST")
                };
            return _sqlDBAccess.GetData("[dbo].[spGetSalesOrder]", nvp).ToList<SalesOrder>();
        }

        public SalesOrder GetForDetail(long SalesOrderId)
        {            
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            nameValuePairs nvp = new nameValuePairs()
            {
                 
                 
                new nameValuePair("@SalesOrderId", SalesOrderId),
                new nameValuePair("@QueryType", "DETAIL")
            };
            DataSet ds = _sqlDBAccess.GetDataSet("[dbo].[spGetSalesOrder]", nvp);
            SalesOrder salesOrder;
            salesOrder = ds.Tables[0].FirstOrDefault<SalesOrder>();
            if (salesOrder != null)
            {
                salesOrder.SalesOrderDetails = new List<SalesOrderDetail>();
                salesOrder.SalesOrderDetails = ds.Tables[1].Rows.Count > 0 ? ds.Tables[1].ToList<SalesOrderDetail>() : null;
                if (salesOrder.SalesOrderDetails == null)
                    salesOrder = null;
            }
            return salesOrder;
        }

        /// <summary>
        /// SO Amendment
        /// </summary>
        /// <returns>SalesOrderId & AmendmentNumber</returns>
        public string SetForAmendment(SalesOrder salesOrder)
        {
            try
            {
                string ReturnDS = string.Empty;
                List<SalesOrderDetailType> orderDetailTypes = new List<SalesOrderDetailType>();
                int count = 1;
                salesOrder.SalesOrderDetails.ForEach(item =>
                {
                    if (item.Quantity > 0)
                    {
                        orderDetailTypes.Add(new SalesOrderDetailType()
                        {
                            Id = count++,
                            AmendmentNumber = item.AmendmentNumber,
                            Amount = item.Amount,
                            Description = item.Description,
                            DiscountAmount = item.DiscountAmount,
                            DiscountPercentage = item.DiscountPercentage,
                            HSNSAC = item.HSNSAC,
                            Price = item.Price,
                            ProductId = item.ProductId,
                            SalesOrderDetailId = item.SalesOrderDetailId,
                            SalesOrderId = item.SalesOrderId,
                            SalesOrderNumber = item.SalesOrderNumber,
                            Quantity = item.Quantity,
                            SubTotal = item.SubTotal,
                            TaxAmount = item.TaxAmount,
                            TaxPercentage = item.TaxPercentage,
                            Total = item.Total
                        });
                    }
                });
                if (orderDetailTypes.Count > 0)
                {
                    _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
                    nameValuePairs nvp = new nameValuePairs
                    {
                        new nameValuePair("@SalesOrderId", salesOrder.SalesOrderId),
                         
                         
                        new nameValuePair("@AmendmentNumber", ""),
                        new nameValuePair("@AmendmentDate", salesOrder.AmendmentDate),
                        new nameValuePair("@SalesOrderAmount", salesOrder.SalesOrderAmount),
                        new nameValuePair("@SalesOrderTaxAmount", salesOrder.SalesOrderTaxAmount),
                        new nameValuePair("@SalesOrderTotalAmount", salesOrder.SalesOrderTotalAmount),
                        new nameValuePair("@TandC", salesOrder.TandC),
                        new nameValuePair("@Remarks", salesOrder.Remarks),
                        new nameValuePair("@SalesOrderDetails", orderDetailTypes.ToDataTable()),
                        new nameValuePair("@RequestId", CommonObj.RequestId),
                        new nameValuePair("@QueryType", "AMENDMENT")
                    };
                    ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetSalesOrder]", nvp, "@OutParam").ToString();
                }
                return ReturnDS;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
