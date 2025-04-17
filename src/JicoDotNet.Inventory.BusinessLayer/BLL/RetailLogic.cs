using DataAccess.Sql;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Custom;
using JicoDotNet.Inventory.Core.Custom.Interface;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Enumeration;
using JicoDotNet.Inventory.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class RetailLogic : DBManager
    {
        public RetailLogic(ICommonLogicHelper CommonObj) : base(CommonObj) { }

        public long Set(RetailSales retailSales, ICompanyBasic currentCompany,
            Dictionary<string, object> dynamicFormValue, out short ReturnType)
        {
            long SalesOrderId = 0;
            List<IShipmentDetailType> spmDetailTypes = new List<IShipmentDetailType>();
            List<ISalesOrderDetailType> orderDetailTypes = new List<ISalesOrderDetailType>();
            int count = 1;
            try
            {
                retailSales.ShipmentDirectDetails.ForEach(spmD =>
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
            }
            catch (Exception ex) { throw ex; }


            if (orderDetailTypes.Count > 0)
            {
                // SO & Shipment
                try
                {                    
                    NameValuePairs nvp = new NameValuePairs()
                    {
                        // Customer
                        new NameValuePair("@ContactPerson", retailSales.ContactPerson),
                        new NameValuePair("@Email", retailSales.Email),
                        new NameValuePair("@Mobile", retailSales.Mobile),
                        new NameValuePair("@CompanyName", retailSales.CompanyName),
                        new NameValuePair("@CompanyType", retailSales.CompanyType),
                        new NameValuePair("@StateCode", retailSales.IsGSTRegistered? GenericLogic.GstStateCode(retailSales.GSTNumber) : retailSales.StateCode),
                        new NameValuePair("@IsGSTRegistered", retailSales.IsGSTRegistered),
                        new NameValuePair("@GSTStateCode", retailSales.IsGSTRegistered? (object)GenericLogic.GstStateCode(retailSales.GSTNumber) : DBNull.Value),
                        new NameValuePair("@GSTNumber", retailSales.IsGSTRegistered?(object)retailSales.GSTNumber:DBNull.Value),
                        new NameValuePair("@PANNumber", retailSales.PANNumber),

                        //SO
                        new NameValuePair("@IsGstAllowed", retailSales.IsGstAllowed),
                        new NameValuePair("@CustomerId", retailSales.CustomerId),
                        new NameValuePair("@SalesOrderDate", retailSales.ShipmentDate),
                        new NameValuePair("@SalesOrderAmount", retailSales.SalesOrderAmount),
                        new NameValuePair("@SalesOrderTaxAmount", retailSales.SalesOrderTaxAmount),
                        new NameValuePair("@SalesOrderTotalAmount", retailSales.SalesOrderTotalAmount),
                        new NameValuePair("@DeliveryDate", retailSales.ShipmentDate),
                        new NameValuePair("@SalesOrderDetails", orderDetailTypes.ToDataTable()),

                        // Ship
                        new NameValuePair("@WareHouseId", retailSales.WareHouseId),
                        new NameValuePair("@ShipmentNumber", "DO-"),
                        new NameValuePair("@ShipmentDate", retailSales.ShipmentDate),
                        new NameValuePair("@IsDirect", true),
                        new NameValuePair("@IsFullShipped", true),
                        new NameValuePair("@VehicleNumber", retailSales.VehicleNumber),
                        new NameValuePair("@HandOverPerson", retailSales.HandOverPerson),
                        new NameValuePair("@HandOverPersonMobile", retailSales.HandOverPersonMobile),
                        new NameValuePair("@ShipmentDetails", spmDetailTypes.ToDataTable()),

                        // Common
                        new NameValuePair("@Remarks", retailSales.Remarks),

                        new NameValuePair("@RequestId", CommonLogicObj.RequestId),
                        new NameValuePair("@QueryType", "INSERT")
                    };
                    SalesOrderId = Convert.ToInt64(_sqlDBAccess.DataManipulation(CommonLogicObj.SqlSchema + ".[spSetRetailSales]", nvp, "@OutParam"));
                }
                catch (Exception ex) { throw ex; }

                try
                {
                    SalesOrder salesOrder = new SalesOrderLogic(CommonLogicObj).GetForDetail(SalesOrderId);
                    if (salesOrder != null && salesOrder.SalesOrderId > 0)
                    {
                        Invoice invoice = new Invoice
                        {
                            SalesOrderId = salesOrder.SalesOrderId,
                            IsCustomizedInvoiceNumber = retailSales.IsCustomizedInvoiceNumber,
                            InvoiceNumber = retailSales.InvoiceNumber,
                            InvoiceDate = retailSales.ShipmentDate,
                            Remarks = retailSales.Remarks,
                            HandOverPerson = retailSales.HandOverPerson,
                            HandOverPersonMobile = retailSales.HandOverPersonMobile,
                            CustomerId = salesOrder.CustomerId,
                            IsGstApplicable = salesOrder.IsGstAllowed,
                            GSTNumber = salesOrder.GSTNumber,
                            IsFullInvoiced = true,
                            GSTStateCode = salesOrder.GSTStateCode,
                            GSTType = salesOrder.IsGstAllowed ?
                                (short)GSTLogic.GetType(salesOrder.IsGSTRegistered ?
                                        salesOrder.GSTStateCode :
                                        salesOrder.StateCode,
                                    currentCompany.IsGSTRegistered ?
                                        currentCompany.GSTStateCode :
                                        currentCompany.StateCode) :
                                (short)EGSTType.None,
                            InvoiceDetails = new List<InvoiceDetail>()
                        };
                        foreach (SalesOrderDetail salesOrderDetail in salesOrder.SalesOrderDetails)
                        {
                            invoice.InvoiceDetails.Add(
                                new InvoiceDetail()
                                {
                                    ProductId = salesOrderDetail.ProductId,
                                    SalesOrderDetailId = salesOrderDetail.SalesOrderDetailId,
                                    HSNSAC = salesOrderDetail.HSNSAC,
                                    Price = salesOrderDetail.Price,
                                    TaxPercentage = salesOrderDetail.TaxPercentage,
                                    InvoicedQuantity = salesOrderDetail.Quantity,
                                    Description = salesOrderDetail.Description
                                }
                            );
                        }

                        InvoiceLogic invoiceLogic = new InvoiceLogic(CommonLogicObj);
                        invoice = invoiceLogic.Set(invoice);
                        ReturnType = 2;

                        // Custom Property Set
                        CustomPropertyLogic propertyLogic = new CustomPropertyLogic(CommonLogicObj);
                        propertyLogic.SetValue(ECustomPropertyFor.RetailSalesInvoice, dynamicFormValue, invoice.InvoiceId, invoice.InvoiceNumber);
                        //

                        return invoice.InvoiceId;
                    }
                    else
                    {
                        ReturnType = 1;
                        return SalesOrderId;
                    }
                }
                catch (Exception ex) { throw ex; }
            }
            ReturnType = -1;
            return 0;
        }
    }
}
