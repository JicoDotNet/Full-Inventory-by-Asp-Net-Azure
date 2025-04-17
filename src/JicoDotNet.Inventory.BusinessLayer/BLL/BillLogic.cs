﻿using DataAccess.Sql;
using DataAccess.Sql.Entity;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Custom;
using JicoDotNet.Inventory.Core.Custom.Interface;
using JicoDotNet.Inventory.Core.Enumeration;
using JicoDotNet.Inventory.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class BillLogic : DBManager
    {
        public BillLogic(ICommonLogicHelper commonObj) : base(commonObj) { }

        #region Bill Type
        public string TypeSet(IBillType billType)
        {
            string queryType = billType.BillTypeId > 0 ? "UPDATE" : "INSERT";

            INameValuePairs nvp = new NameValuePairs
            {
                new NameValuePair("@BillTypeId", billType.BillTypeId),
                new NameValuePair("@BillTypeName", billType.BillTypeName),
                new NameValuePair("@Description", billType.Description),

                new NameValuePair("@RequestId", CommonLogicObj.RequestId),
                new NameValuePair("@QueryType", queryType)
            };

            string returnDs = _sqlDBAccess.DataManipulation(CommonLogicObj.SqlSchema + ".[spSetBillType]", nvp, "@OutParam").ToString();
            return returnDs;
        }

        public string TypeDeactivate(string billTypeId)
        {
            string queryType = "INACTIVE";

            INameValuePairs nvp = new NameValuePairs
            {
                new NameValuePair("@BillTypeId", billTypeId),


                new NameValuePair("@RequestId", CommonLogicObj.RequestId),
                new NameValuePair("@QueryType", queryType)
            };

            string returnDs = _sqlDBAccess.DataManipulation(CommonLogicObj.SqlSchema + ".[spSetBillType]", nvp, "@OutParam").ToString();
            return returnDs;
        }

        public IList<BillType> TypeGet(bool? isActive = null)
        {
            IList<BillType> billTypes = new SqlDBAccess(CommonLogicObj.SqlConnectionString).GetData(CommonLogicObj.SqlSchema + ".[spGetBillType]",
                new NameValuePairs
                {
                    new NameValuePair("@QueryType", "ALL")
                }).ToList<BillType>();
            if (isActive != null)
            {
                if (isActive.Value)
                    return billTypes.Where(a => a.IsActive).ToList();
                if (!isActive.Value)
                    return billTypes.Where(a => !a.IsActive).ToList();
            }
            return billTypes;
        }
        #endregion

        #region Bill
        public IList<Bill> GetBills()
        {
            try
            {
                INameValuePairs nvp = new NameValuePairs()
                {
                    new NameValuePair("@QueryType", "LIST")
                };
                IList<Bill> bills = _sqlDBAccess.GetData(CommonLogicObj.SqlSchema + ".[spGetBill]", nvp).ToList<Bill>();
                return bills;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IBill GetBill(long billId)
        {
            try
            {                
                INameValuePairs nvp = new NameValuePairs()
                {
                    new NameValuePair("@BillId", billId),
                    new NameValuePair("@QueryType", "SINGLE")
                };
                IBill bill = _sqlDBAccess.GetData(CommonLogicObj.SqlSchema + ".[spGetBill]", nvp).FirstOrDefault<Bill>();
                return bill;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<PurchaseOrder> GetForEntry()
        {            
            INameValuePairs nvp = new NameValuePairs()
            {
                new NameValuePair("@QueryType", "ENTRY")
            };
            IList<PurchaseOrder> purchaseOrders = _sqlDBAccess.GetData(CommonLogicObj.SqlSchema + ".[spGetBill]", nvp).ToList<PurchaseOrder>();
            return purchaseOrders;
        }

        public IPurchaseOrder GetForEntry(long purchaseOrderId)
        {
            try
            {                
                INameValuePairs nvp = new NameValuePairs
                {
                    new NameValuePair("@PurchaseOrderId", purchaseOrderId),
                    new NameValuePair("@QueryType", "ENTRYSINGLE")
                };
                PurchaseOrder purchaseOrder = _sqlDBAccess.GetData(CommonLogicObj.SqlSchema + ".[spGetBill]", nvp).FirstOrDefault<PurchaseOrder>();
                return purchaseOrder;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<BillDetail> GetBillDetails(long purchaseOrderId)
        {            
            INameValuePairs nvp = new NameValuePairs
            {
                new NameValuePair("@PurchaseOrderId", purchaseOrderId),
                new NameValuePair("@QueryType", "COMMUTATIVE")
            };
            IList<BillDetail> billDetails = _sqlDBAccess.GetData(CommonLogicObj.SqlSchema + ".[spGetBill]", nvp).ToList<BillDetail>();
            return billDetails;
        }

        public IList<Bill> GetBillsForPayment(long vendorId)
        {
            try
            {                
                INameValuePairs nvp = new NameValuePairs
                {
                    new NameValuePair("@VendorId", vendorId),
                    new NameValuePair("@QueryType", "PAYMENT")
                };
                IList<Bill> bills = _sqlDBAccess.GetData(CommonLogicObj.SqlSchema + ".[spGetBill]", nvp).ToList<Bill>();
                return bills;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Set(Bill bill)
        {
            try
            {
                IList<IBillDetailsType> billDetailsTypes = new List<IBillDetailsType>();
                int count = 1;
                bill.BilledAmount = 0;
                bill.TaxAmount = 0;
                bill.TotalAmount = 0;

                bill.BillDetails.ForEach(billItem =>
                {
                    if (billItem.BilledQuantity > 0)
                    {
                        IBillDetailsType billDetailsType = new BillDetailsType
                        {
                            Id = count++,
                            PurchaseOrderDetailId = billItem.PurchaseOrderDetailId,
                            ProductId = billItem.ProductId,
                            HSNSAC = billItem.HSNSAC,
                            Price = billItem.Price,
                            BilledQuantity = billItem.BilledQuantity,
                            TaxPercentage = billItem.TaxPercentage,
                            SubTotal = billItem.Price * billItem.BilledQuantity,
                            Description = billItem.Description,

                            // Set 0 as default
                            CGSTAmount = 0,
                            SGSTAmount = 0,
                            IGSTAmount = 0,
                            Total = 0
                        };

                        if (bill.IsGstApplicable)
                        {
                            if (bill.GSTType == (short)EGSTType.CGSTSGST)
                            {
                                billDetailsType.CGSTAmount = billDetailsType.SubTotal * (billDetailsType.TaxPercentage / 100) / 2;
                                billDetailsType.SGSTAmount = billDetailsType.SubTotal * (billDetailsType.TaxPercentage / 100) / 2;
                                billDetailsType.Total = billDetailsType.SubTotal + billDetailsType.CGSTAmount + billDetailsType.SGSTAmount;
                            }
                            if (bill.GSTType == (short)EGSTType.IGST)
                            {
                                billDetailsType.IGSTAmount = billDetailsType.SubTotal * (billDetailsType.TaxPercentage / 100);
                                billDetailsType.Total = billDetailsType.SubTotal + billDetailsType.IGSTAmount;
                            }
                        }
                        else
                        {
                            billDetailsType.Total = billDetailsType.SubTotal;
                        }

                        // Header value Set
                        bill.BilledAmount += billDetailsType.SubTotal;
                        bill.TaxAmount += billDetailsType.CGSTAmount + billDetailsType.SGSTAmount + billDetailsType.IGSTAmount;
                        bill.TotalAmount += billDetailsType.Total;

                        billDetailsTypes.Add(billDetailsType);
                    }
                });

                if (billDetailsTypes.Count > 0)
                {                    
                    NameValuePairs nvp = new NameValuePairs()
                    {
                        new NameValuePair("@BillTypeId", bill.BillTypeId),
                        new NameValuePair("@BillDate", bill.BillDate),
                        new NameValuePair("@BillDueDate", bill.BillDueDate),
                        new NameValuePair("@BillNumber", "BIL-"),
                        new NameValuePair("@VendorId", bill.VendorId),
                        new NameValuePair("@PurchaseOrderId", bill.PurchaseOrderId),
                        new NameValuePair("@VendorDONumber", bill.VendorDONumber),
                        new NameValuePair("@VendorInvoiceNumber", bill.VendorInvoiceNumber),
                        new NameValuePair("@VendorInvoiceDate", bill.VendorInvoiceDate),
                        new NameValuePair("@Remarks", bill.Remarks),
                        new NameValuePair("@IsGstApplicable", bill.IsGstApplicable),
                        new NameValuePair("@GSTNumber", bill.GSTNumber),
                        new NameValuePair("@GSTStateCode", bill.GSTStateCode),
                        new NameValuePair("@GSTType", bill.GSTType),
                        new NameValuePair("@BilledAmount", bill.BilledAmount),
                        new NameValuePair("@TaxAmount", bill.TaxAmount),
                        new NameValuePair("@TotalAmount", bill.TotalAmount),
                        new NameValuePair("@IsFullBilled", bill.IsFullBilled),
                        new NameValuePair("@BillDetail", billDetailsTypes.ToDataTable()),
                        new NameValuePair("@RequestId", CommonLogicObj.RequestId),
                        new NameValuePair("@QueryType", "INSERT")
                    };
                    return _sqlDBAccess.DataManipulation(CommonLogicObj.SqlSchema + ".[spSetBill]", nvp, "@OutParam").ToString();
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

        public Bill GetForDetail(long BillId)
        {
            Bill bill = new Bill();
            
            NameValuePairs nvp = new NameValuePairs()
            {
                new NameValuePair("@BillId", BillId),
                new NameValuePair("@QueryType", "DETAIL")
            };
            DataSet ds = _sqlDBAccess.GetDataSet(CommonLogicObj.SqlSchema + ".[spGetBill]", nvp);
            bill = ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].FirstOrDefault<Bill>() : null;
            if (bill != null)
            {
                bill.BillDetails = new List<BillDetail>();
                bill.BillDetails = ds.Tables[1].Rows.Count > 0 ? ds.Tables[1].ToList<BillDetail>() : null;
                if (bill.BillDetails == null)
                    bill = null;
            }
            return bill;
        }
        #endregion
    }
}
