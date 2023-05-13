using DataAccess.Sql;
using Newtonsoft.Json;
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
    public class RetailLogic : ConnectionString
    {
        public RetailLogic(sCommonDto CommonObj) : base(CommonObj) { }

        public long Set(RetailSales retailSales, CompanyBasic currentCompany, 
            Dictionary<string, object> dynamicFormValue, out short ReturnType)
        {
            long SalesOrderId = 0;
            List<ShipmentDetailType> spmDetailTypes = new List<ShipmentDetailType>();
            List<SalesOrderDetailType> orderDetailTypes = new List<SalesOrderDetailType>();
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
            catch(Exception ex) { throw ex; }
            

            if (orderDetailTypes.Count > 0)
            {
                // SO & Shipment
                try
                {
                    _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
                    nameValuePairs nvp = new nameValuePairs()
                    {
                         
                         

                        // Customer
                        new nameValuePair("@ContactPerson", retailSales.ContactPerson),
                        new nameValuePair("@Email", retailSales.Email),
                        new nameValuePair("@Mobile", retailSales.Mobile),
                        new nameValuePair("@CompanyName", retailSales.CompanyName),
                        new nameValuePair("@CompanyType", retailSales.CompanyType),
                        new nameValuePair("@StateCode", retailSales.IsGSTRegistered? GenericLogic.GstStateCode(retailSales.GSTNumber) : retailSales.StateCode),
                        new nameValuePair("@IsGSTRegistered", retailSales.IsGSTRegistered),
                        new nameValuePair("@GSTStateCode", retailSales.IsGSTRegistered? (object)GenericLogic.GstStateCode(retailSales.GSTNumber) : DBNull.Value),
                        new nameValuePair("@GSTNumber", retailSales.IsGSTRegistered?(object)retailSales.GSTNumber:DBNull.Value),
                        new nameValuePair("@PANNumber", retailSales.PANNumber),

                        //SO
                        new nameValuePair("@CustomerId", retailSales.CustomerId),
                        new nameValuePair("@SalesOrderDate", retailSales.ShipmentDate),
                        new nameValuePair("@SalesOrderAmount", retailSales.SalesOrderAmount),
                        new nameValuePair("@SalesOrderTaxAmount", retailSales.SalesOrderTaxAmount),
                        new nameValuePair("@SalesOrderTotalAmount", retailSales.SalesOrderTotalAmount),
                        new nameValuePair("@DeliveryDate", retailSales.ShipmentDate),
                        new nameValuePair("@SalesOrderDetails", orderDetailTypes.ToDataTable()),

                        // Ship
                        new nameValuePair("@WareHouseId", retailSales.WareHouseId),
                        new nameValuePair("@ShipmentNumber", "DO-"),
                        new nameValuePair("@ShipmentDate", retailSales.ShipmentDate),
                        new nameValuePair("@IsDirect", true),
                        new nameValuePair("@IsFullShipped", true),
                        new nameValuePair("@VehicleNumber", retailSales.VehicleNumber),
                        new nameValuePair("@HandOverPerson", retailSales.HandOverPerson),
                        new nameValuePair("@HandOverPersonMobile", retailSales.HandOverPersonMobile),
                        new nameValuePair("@ShipmentDetails", spmDetailTypes.ToDataTable()),

                        // Common
                        new nameValuePair("@Remarks", retailSales.Remarks),

                        new nameValuePair("@RequestId", CommonObj.RequestId),
                        new nameValuePair("@QueryType", "INSERT")
                    };
                    SalesOrderId = Convert.ToInt64(_sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetRetailSales]", nvp, "@OutParam"));
                }
                catch (Exception ex) { throw ex; }

                try
                {
                    SalesOrder salesOrder = new SalesOrderLogic(CommonObj).GetForDetail(SalesOrderId);
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

                        InvoiceLogic invoiceLogic = new InvoiceLogic(CommonObj);
                        invoice = invoiceLogic.Set(invoice);
                        ReturnType = 2;

                        // Custom Property Set
                        CustomPropertyLogic propertyLogic = new CustomPropertyLogic(CommonObj);
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
