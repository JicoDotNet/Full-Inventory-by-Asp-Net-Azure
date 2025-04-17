﻿using DataAccess.Sql;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Custom;
using JicoDotNet.Inventory.Core.Custom.Interface;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class ShipmentLogic : DBManager
    {
        public ShipmentLogic(ICommonLogicHelper CommonObj) : base(CommonObj) { }

        #region Payment Type
        public string TypeSet(ShipmentType shipmentType)
        {
            
            string qt = string.Empty;
            if (shipmentType.ShipmentTypeId > 0)
                qt = "UPDATE";
            else
                qt = "INSERT";

            NameValuePairs nvp = new NameValuePairs
            {

                new NameValuePair("@ShipmentTypeId", shipmentType.ShipmentTypeId),
                new NameValuePair("@ShipmentTypeName", shipmentType.ShipmentTypeName),
                new NameValuePair("@Description", shipmentType.Description),

                new NameValuePair("@RequestId", CommonLogicObj.RequestId),
                new NameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.DataManipulation(CommonLogicObj.SqlSchema + ".[spSetShipmentType]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public string TypeDeactive(string ShipmentTypeId)
        {
            
            string qt = "INACTIVE";

            NameValuePairs nvp = new NameValuePairs
            {
                new NameValuePair("@ShipmentTypeId", ShipmentTypeId),


                new NameValuePair("@RequestId", CommonLogicObj.RequestId),
                new NameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.DataManipulation(CommonLogicObj.SqlSchema + ".[spSetShipmentType]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public List<ShipmentType> TypeGet(bool? IsActive = null)
        {
            List<ShipmentType> shipmentTypes = new SqlDBAccess(CommonLogicObj.SqlConnectionString).GetData(CommonLogicObj.SqlSchema + ".[spGetShipmentType]",
                new NameValuePairs
                {


                    new NameValuePair("@QueryType", "ALL")
                }).ToList<ShipmentType>();
            if (IsActive != null)
            {
                if (IsActive.Value)
                    return shipmentTypes.Where(a => a.IsActive).ToList();
                if (!IsActive.Value)
                    return shipmentTypes.Where(a => !a.IsActive).ToList();
            }
            return shipmentTypes;
        }
        #endregion

        public List<Shipment> GetShipments()
        {
            return new SqlDBAccess(CommonLogicObj.SqlConnectionString).GetData(CommonLogicObj.SqlSchema + ".[spGetShipment]",
                new NameValuePairs
                {


                    new NameValuePair("@QueryType", "LIST")
                }).ToList<Shipment>();
        }

        public List<ShipmentDetail> GetShipmentDetails(long salesOrderId)
        {
            
            NameValuePairs nvp = new NameValuePairs()
                {


                    new NameValuePair("@SalesOrderId", salesOrderId),
                    new NameValuePair("@QueryType", "COMULTATIVE")
                };
            List<ShipmentDetail> shpdtl = _sqlDBAccess.GetData(CommonLogicObj.SqlSchema + ".[spGetShipment]", nvp).ToList<ShipmentDetail>();
            return shpdtl;
        }

        public string Set(Shipment shipment)
        {
            try
            {
                List<IShipmentDetailType> shpDetailTypes = new List<IShipmentDetailType>();
                int count = 1;
                if (shipment.ShipmentDetails != null)
                {
                    shipment.ShipmentDetails.ForEach(shpD =>
                    {
                        if (shpD.ShippedQuantity > 0)
                        {
                            shpDetailTypes.Add(new ShipmentDetailType()
                            {
                                Id = count++,
                                SalesOrderDetailId = shpD.SalesOrderDetailId,
                                ProductId = shpD.ProductId,
                                ShippedQuantity = shpD.ShippedQuantity,
                                StockDetailId = shpD.StockDetailId,
                                IsPerishable = shpD.IsPerishable,
                                BatchNo = shpD.BatchNo?.Trim(),
                                ExpiryDate = shpD.ExpiryDate?.AddDays(1).AddSeconds(-1),
                                Description = shpD.Description,
                            });
                        }
                    });
                }

                if (shpDetailTypes.Count > 0)
                {
                    return new SqlDBAccess(CommonLogicObj.SqlConnectionString)
                        .DataManipulation(CommonLogicObj.SqlSchema + ".[spSetShipment]", new NameValuePairs
                        {


                        new NameValuePair("@ShipmentTypeId", shipment.ShipmentTypeId),
                        new NameValuePair("@ShipmentDate", shipment.ShipmentDate),
                        new NameValuePair("@ShipmentNumber", "DO-"),
                        new NameValuePair("@IsDirect", false),
                        new NameValuePair("@IsFullShipped", shipment.IsFullShipped),
                        new NameValuePair("@SalesOrderId", shipment.SalesOrderId),
                        new NameValuePair("@WareHouseId", shipment.WareHouseId),
                        new NameValuePair("@Remarks", shipment.Remarks),
                        new NameValuePair("@VehicleNumber", shipment.VehicleNumber),
                        new NameValuePair("@HandOverPerson", shipment.HandOverPerson),
                        new NameValuePair("@HandOverPersonMobile", shipment.HandOverPersonMobile),
                        new NameValuePair("@ShipmentDetail", shpDetailTypes.ToDataTable()),
                        new NameValuePair("@RequestId", CommonLogicObj.RequestId),
                        new NameValuePair("@QueryType", "INSERT")
                        },
                        "@OutParam"
                    ).ToString();
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string SetDirect(ShipmentDirect shipmentDirect)
        {
            List<IShipmentDetailType> spmDetailTypes = new List<IShipmentDetailType>();
            List<ISalesOrderDetailType> orderDetailTypes = new List<ISalesOrderDetailType>();
            int count = 1;
            shipmentDirect.ShipmentDirectDetails.ForEach(spmD =>
            {
                if (spmD.Quantity > 0)
                {
                    // If Service Then StockDetailId will be 0
                    // Service doesn't have any Stock and it's StockDetailId
                    if (spmD.StockDetailId > 0)
                    {
                        spmDetailTypes.Add(new ShipmentDetailType()
                        {
                            Id = count++,
                            SalesOrderDetailId = spmD.SalesOrderDetailId,
                            StockDetailId = spmD.StockDetailId,
                            ProductId = spmD.ProductId,
                            ShippedQuantity = spmD.Quantity,
                            Description = spmD.Description,
                            IsPerishable = spmD.IsPerishable,
                            BatchNo = spmD.BatchNo?.Trim(),
                            ExpiryDate = spmD.ExpiryDate?.AddDays(1).AddSeconds(-1)
                        });
                    }

                    orderDetailTypes.Add(new SalesOrderDetailType()
                    {
                        AmendmentNumber = spmD.AmendmentNumber,
                        Amount = spmD.Amount,
                        Description = spmD.Description,
                        DiscountAmount = spmD.DiscountAmount,
                        DiscountPercentage = spmD.DiscountPercentage,
                        HSNSAC = spmD.HSNSAC,
                        Price = spmD.Price,
                        ProductId = spmD.ProductId,
                        Quantity = spmD.Quantity,
                        SubTotal = spmD.SubTotal,
                        TaxAmount = spmD.TaxAmount,
                        TaxPercentage = spmD.TaxPercentage,
                        Total = spmD.Total
                    });
                }
            });

            if (orderDetailTypes.Count > 0)
            {
                
                NameValuePairs nvp = new NameValuePairs()
                {
                    new NameValuePair("@ComapnyIsGSTRegistered", GenericLogic.IsValidGSTNumber(CommonLogicObj.AppSettings.GSTNumber)),                     

                    //SO
                    new NameValuePair("@SalesTypeId", shipmentDirect.SalesTypeId),
                    new NameValuePair("@CustomerId", shipmentDirect.CustomerId),
                    new NameValuePair("@SalesOrderDate", shipmentDirect.ShipmentDate),
                    new NameValuePair("@SalesOrderAmount", shipmentDirect.SalesOrderAmount),
                    new NameValuePair("@SalesOrderTaxAmount", shipmentDirect.SalesOrderTaxAmount),
                    new NameValuePair("@SalesOrderTotalAmount", shipmentDirect.SalesOrderTotalAmount),
                    new NameValuePair("@DeliveryDate", shipmentDirect.ShipmentDate),

                    new NameValuePair("@SalesOrderDetails", orderDetailTypes.ToDataTable()),

                    //Ship
                    new NameValuePair("@WareHouseId", shipmentDirect.WareHouseId),
                    new NameValuePair("@ShipmentTypeId", shipmentDirect.ShipmentTypeId),
                    new NameValuePair("@ShipmentNumber", "DO-"),
                    new NameValuePair("@ShipmentDate", shipmentDirect.ShipmentDate),
                    new NameValuePair("@IsDirect", true),
                    new NameValuePair("@IsFullShipped", true),
                    new NameValuePair("@VehicleNumber", shipmentDirect.VehicleNumber),
                    new NameValuePair("@HandOverPerson", shipmentDirect.HandOverPerson),
                    new NameValuePair("@HandOverPersonMobile", shipmentDirect.HandOverPersonMobile),

                    new NameValuePair("@ShipmentDetails", spmDetailTypes.ToDataTable()),

                    // Common
                    new NameValuePair("@Remarks", shipmentDirect.Remarks),

                    new NameValuePair("@RequestId", CommonLogicObj.RequestId),
                    new NameValuePair("@QueryType", "INSERT")
                };
                return _sqlDBAccess.DataManipulation(CommonLogicObj.SqlSchema + ".[spSetShipmentDirect]", nvp, "@OutParam").ToString();
            }
            return string.Empty;
        }

        public Shipment GetForDetail(long ShipmentId)
        {
            Shipment shipment = new Shipment();
            
            NameValuePairs nvp = new NameValuePairs()
            {


                new NameValuePair("@ShipmentId", ShipmentId),
                new NameValuePair("@QueryType", "DETAIL")
            };
            DataSet ds = _sqlDBAccess.GetDataSet(CommonLogicObj.SqlSchema + ".[spGetShipment]", nvp);
            shipment = ds.Tables[0].FirstOrDefault<Shipment>();
            if (shipment != null)
            {
                shipment.ShipmentDetails = new List<ShipmentDetail>();
                shipment.ShipmentDetails = ds.Tables[1].Rows.Count > 0 ? ds.Tables[1].ToList<ShipmentDetail>() : null;
                if (shipment.ShipmentDetails == null)
                    shipment = null;
            }
            return shipment;
        }
    }
}
