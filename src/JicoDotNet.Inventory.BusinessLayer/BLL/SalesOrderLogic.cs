using DataAccess.Sql;
using JicoDotNet.Inventory.BusinessLayer.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Custom;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Models;
using JicoDotNet.Inventory.Core.Custom.Interface;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class SalesOrderLogic : ConnectionString
    {
        public SalesOrderLogic(ICommonRequestDto CommonObj) : base(CommonObj) { }

        #region Sales Type
        public string TypeSet(SalesType salesType)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt;
            if (salesType.SalesTypeId > 0)
                qt = "UPDATE";
            else
                qt = "INSERT";

            NameValuePairs nvp = new NameValuePairs
            {
                 
                 
                new NameValuePair("@SalesTypeId", salesType.SalesTypeId),
                new NameValuePair("@SalesTypeName", salesType.SalesTypeName),
                new NameValuePair("@Description", salesType.Description),
                new NameValuePair("@RequestId", CommonObj.RequestId),
                new NameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.DataManipulation(GenericLogic.SqlSchema + ".[spSetSalesType]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public string TypeDeactive(string SalesTypeId)
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString)
                .DataManipulation(GenericLogic.SqlSchema + ".[spSetSalesType]", new NameValuePairs
                {
                    new NameValuePair("@SalesTypeId", SalesTypeId),
                     
                     
                    new NameValuePair("@RequestId", CommonObj.RequestId),
                    new NameValuePair("@QueryType", "INACTIVE")
                }, "@OutParam").ToString();
        }

        public List<SalesType> TypeGet(bool? IsActive = null)
        {
            List<SalesType> salesTypes = new SqlDBAccess(CommonObj.SqlConnectionString).GetData(GenericLogic.SqlSchema + ".[spGetSalesType]",
                new NameValuePairs
                {
                     
                     
                    new NameValuePair("@QueryType", "ALL")
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
            NameValuePairs nvp = new NameValuePairs
            {
                 
                 
                    new NameValuePair("@QueryType", "ENTRY")
            };
            DataSet dataSet = _sqlDBAccess.GetDataSet(GenericLogic.SqlSchema + ".[spGetSalesOrder]", nvp);
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
            List<ISalesOrderDetailType> orderDetailTypes = new List<ISalesOrderDetailType>();
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
                NameValuePairs nvp = new NameValuePairs
                {
                    new NameValuePair("@ComapnyIsGSTRegistered", GenericLogic.IsValidGSTNumber(WebConfigAppSettingsAccess.GSTNumber)),
                    new NameValuePair("@QuotationId", salesOrder.QuotationId),
                    new NameValuePair("@SalesTypeId", salesOrder.SalesTypeId),
                    new NameValuePair("@BranchId", salesOrder.BranchId),
                    new NameValuePair("@CustomerId", salesOrder.CustomerId),
                    new NameValuePair("@SalesOrderDate", salesOrder.SalesOrderDate),
                    new NameValuePair("@SalesOrderNumber", "SO-"),
                    new NameValuePair("@AmendmentNumber", salesOrder.AmendmentNumber),
                    new NameValuePair("@AmendmentDate", salesOrder.AmendmentDate > salesOrder.SalesOrderDate? (object)salesOrder.AmendmentDate: DBNull.Value),
                    new NameValuePair("@CustomerPONumber", salesOrder.CustomerPONumber),
                    new NameValuePair("@CustomerPODate", salesOrder.CustomerPODate),
                    new NameValuePair("@DeliveryDate", salesOrder.DeliveryDate > salesOrder.SalesOrderDate? (object)salesOrder.DeliveryDate: DBNull.Value),
                    new NameValuePair("@SalesOrderAmount", salesOrder.SalesOrderAmount),
                    new NameValuePair("@SalesOrderTaxAmount", salesOrder.SalesOrderTaxAmount),
                    new NameValuePair("@SalesOrderTotalAmount", salesOrder.SalesOrderTotalAmount),
                    new NameValuePair("@TandC", salesOrder.TandC),
                    new NameValuePair("@Remarks", salesOrder.Remarks),
                    new NameValuePair("@SalesOrderDetails", orderDetailTypes.ToDataTable()),
                    new NameValuePair("@RequestId", CommonObj.RequestId),
                    new NameValuePair("@QueryType", "ENTRY")
                };
                ReturnDS = _sqlDBAccess.DataManipulation(GenericLogic.SqlSchema + ".[spSetSalesOrder]", nvp, "@OutParam").ToString();
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
                NameValuePairs nvp = new NameValuePairs
                {
                    new NameValuePair("@SalesOrderId", SalesOrderId),
                     
                     
                    new NameValuePair("@RequestId", CommonObj.RequestId),
                    new NameValuePair("@QueryType", "DELETE")
                };
                string ReturnDS = _sqlDBAccess.DataManipulation(GenericLogic.SqlSchema + ".[spSetSalesOrder]", nvp, "@OutParam").ToString();
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
            NameValuePairs nvp = new NameValuePairs
            {
                 
                 
                    new NameValuePair("@QueryType", "SHIPENTRY")
            };
            return _sqlDBAccess.GetData(GenericLogic.SqlSchema + ".[spGetSalesOrder]", nvp).ToList<SalesOrder>();
        }
        
        public List<SalesOrder> GetSOs()
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            NameValuePairs nvp = new NameValuePairs()
                {
                     
                     
                    new NameValuePair("@QueryType", "LIST")
                };
            return _sqlDBAccess.GetData(GenericLogic.SqlSchema + ".[spGetSalesOrder]", nvp).ToList<SalesOrder>();
        }

        public SalesOrder GetForDetail(long SalesOrderId)
        {            
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            NameValuePairs nvp = new NameValuePairs()
            {
                 
                 
                new NameValuePair("@SalesOrderId", SalesOrderId),
                new NameValuePair("@QueryType", "DETAIL")
            };
            DataSet ds = _sqlDBAccess.GetDataSet(GenericLogic.SqlSchema + ".[spGetSalesOrder]", nvp);
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
                List<ISalesOrderDetailType> orderDetailTypes = new List<ISalesOrderDetailType>();
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
                    NameValuePairs nvp = new NameValuePairs
                    {
                        new NameValuePair("@SalesOrderId", salesOrder.SalesOrderId),

                        new NameValuePair("@ComapnyIsGSTRegistered", GenericLogic.IsValidGSTNumber(WebConfigAppSettingsAccess.GSTNumber)),
                        new NameValuePair("@AmendmentNumber", ""),
                        new NameValuePair("@AmendmentDate", salesOrder.AmendmentDate),
                        new NameValuePair("@SalesOrderAmount", salesOrder.SalesOrderAmount),
                        new NameValuePair("@SalesOrderTaxAmount", salesOrder.SalesOrderTaxAmount),
                        new NameValuePair("@SalesOrderTotalAmount", salesOrder.SalesOrderTotalAmount),
                        new NameValuePair("@TandC", salesOrder.TandC),
                        new NameValuePair("@Remarks", salesOrder.Remarks),
                        new NameValuePair("@SalesOrderDetails", orderDetailTypes.ToDataTable()),
                        new NameValuePair("@RequestId", CommonObj.RequestId),
                        new NameValuePair("@QueryType", "AMENDMENT")
                    };
                    ReturnDS = _sqlDBAccess.DataManipulation(GenericLogic.SqlSchema + ".[spSetSalesOrder]", nvp, "@OutParam").ToString();
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
