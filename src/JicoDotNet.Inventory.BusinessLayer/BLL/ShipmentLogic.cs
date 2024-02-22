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
    public class ShipmentLogic : ConnectionString
    {
        public ShipmentLogic(sCommonDto CommonObj) : base(CommonObj) { }

        #region Payment Type
        public string TypeSet(ShipmentType shipmentType)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = string.Empty;
            if (shipmentType.ShipmentTypeId > 0)
                qt = "UPDATE";
            else
                qt = "INSERT";

            nameValuePairs nvp = new nameValuePairs
            {
                 
                new nameValuePair("@ShipmentTypeId", shipmentType.ShipmentTypeId),
                new nameValuePair("@ShipmentTypeName", shipmentType.ShipmentTypeName),
                new nameValuePair("@Description", shipmentType.Description),
                 
                new nameValuePair("@RequestId", CommonObj.RequestId),
                new nameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetShipmentType]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public string TypeDeactive(string ShipmentTypeId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = "INACTIVE";

            nameValuePairs nvp = new nameValuePairs
            {
                new nameValuePair("@ShipmentTypeId", ShipmentTypeId),
                 
                 
                new nameValuePair("@RequestId", CommonObj.RequestId),
                new nameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetShipmentType]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public List<ShipmentType> TypeGet(bool? IsActive = null)
        {
            List<ShipmentType> shipmentTypes = new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetShipmentType]",
                new nameValuePairs
                {
                     
                     
                    new nameValuePair("@QueryType", "ALL")
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
            return new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetShipment]",
                new nameValuePairs
                {
                     
                     
                    new nameValuePair("@QueryType", "LIST")
                }).ToList<Shipment>();
        }

        public List<ShipmentDetail> GetShipmentDetails(long salesOrderId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            nameValuePairs nvp = new nameValuePairs()
                {
                     
                     
                    new nameValuePair("@SalesOrderId", salesOrderId),
                    new nameValuePair("@QueryType", "COMULTATIVE")
                };
            List<ShipmentDetail> shpdtl = _sqlDBAccess.GetData("[dbo].[spGetShipment]", nvp).ToList<ShipmentDetail>();
            return shpdtl;
        }

        public string Set(Shipment shipment)
        {
            try
            {
                List<ShipmentDetailType> shpDetailTypes = new List<ShipmentDetailType>();
                int count = 1;
                if(shipment.ShipmentDetails != null)
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
                    return new SqlDBAccess(CommonObj.SqlConnectionString)
                        .InsertUpdateDeleteReturnObject("[dbo].[spSetShipment]", new nameValuePairs
                        {
                         
                         
                        new nameValuePair("@ShipmentTypeId", shipment.ShipmentTypeId),
                        new nameValuePair("@ShipmentDate", shipment.ShipmentDate),
                        new nameValuePair("@ShipmentNumber", "DO-"),
                        new nameValuePair("@IsDirect", false),
                        new nameValuePair("@IsFullShipped", shipment.IsFullShipped),
                        new nameValuePair("@SalesOrderId", shipment.SalesOrderId),
                        new nameValuePair("@WareHouseId", shipment.WareHouseId),
                        new nameValuePair("@Remarks", shipment.Remarks),
                        new nameValuePair("@VehicleNumber", shipment.VehicleNumber),
                        new nameValuePair("@HandOverPerson", shipment.HandOverPerson),
                        new nameValuePair("@HandOverPersonMobile", shipment.HandOverPersonMobile),
                        new nameValuePair("@ShipmentDetail", shpDetailTypes.ToDataTable()),
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string SetDirect(ShipmentDirect shipmentDirect)
        {
            List<ShipmentDetailType> spmDetailTypes = new List<ShipmentDetailType>();
            List<SalesOrderDetailType> orderDetailTypes = new List<SalesOrderDetailType>();
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
                _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
                nameValuePairs nvp = new nameValuePairs()
                {
                    new nameValuePair("@ComapnyIsGSTRegistered", GenericLogic.IsValidGSTNumber(WebConfigAppSettingsAccess.GSTNumber)),                     

                    //SO
                    new nameValuePair("@SalesTypeId", shipmentDirect.SalesTypeId),
                    new nameValuePair("@CustomerId", shipmentDirect.CustomerId),
                    new nameValuePair("@SalesOrderDate", shipmentDirect.ShipmentDate),
                    new nameValuePair("@SalesOrderAmount", shipmentDirect.SalesOrderAmount),
                    new nameValuePair("@SalesOrderTaxAmount", shipmentDirect.SalesOrderTaxAmount),
                    new nameValuePair("@SalesOrderTotalAmount", shipmentDirect.SalesOrderTotalAmount),
                    new nameValuePair("@DeliveryDate", shipmentDirect.ShipmentDate),

                    new nameValuePair("@SalesOrderDetails", orderDetailTypes.ToDataTable()),

                    //Ship
                    new nameValuePair("@WareHouseId", shipmentDirect.WareHouseId),
                    new nameValuePair("@ShipmentTypeId", shipmentDirect.ShipmentTypeId),
                    new nameValuePair("@ShipmentNumber", "DO-"),
                    new nameValuePair("@ShipmentDate", shipmentDirect.ShipmentDate),
                    new nameValuePair("@IsDirect", true),
                    new nameValuePair("@IsFullShipped", true),
                    new nameValuePair("@VehicleNumber", shipmentDirect.VehicleNumber),
                    new nameValuePair("@HandOverPerson", shipmentDirect.HandOverPerson),
                    new nameValuePair("@HandOverPersonMobile", shipmentDirect.HandOverPersonMobile),
                    
                    new nameValuePair("@ShipmentDetails", spmDetailTypes.ToDataTable()),

                    // Common
                    new nameValuePair("@Remarks", shipmentDirect.Remarks),

                    new nameValuePair("@RequestId", CommonObj.RequestId),
                    new nameValuePair("@QueryType", "INSERT")
                };
                return _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetShipmentDirect]", nvp, "@OutParam").ToString();
            }
            return string.Empty;
        }

        public Shipment GetForDetail(long ShipmentId)
        {
            Shipment shipment = new Shipment();
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            nameValuePairs nvp = new nameValuePairs()
            {
                 
                 
                new nameValuePair("@ShipmentId", ShipmentId),
                new nameValuePair("@QueryType", "DETAIL")
            };
            DataSet ds = _sqlDBAccess.GetDataSet("[dbo].[spGetShipment]", nvp);
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
