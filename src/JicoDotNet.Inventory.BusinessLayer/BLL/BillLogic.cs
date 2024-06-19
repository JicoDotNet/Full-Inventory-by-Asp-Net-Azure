using DataAccess.Sql;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class.Custom;
using JicoDotNet.Inventory.BusinessLayer.DTO.SP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class BillLogic : ConnectionString
    {
        public BillLogic(sCommonDto CommonObj) : base(CommonObj) { }

        #region Bill Type
        public string TypeSet(BillType billType)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt;
            if (billType.BillTypeId > 0)
                qt = "UPDATE";
            else
                qt = "INSERT";

            NameValuePairs nvp = new NameValuePairs
            {
                 
                new NameValuePair("@BillTypeId", billType.BillTypeId),
                new NameValuePair("@BillTypeName", billType.BillTypeName),
                new NameValuePair("@Description", billType.Description),
                 
                new NameValuePair("@RequestId", CommonObj.RequestId),
                new NameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetBillType]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public string TypeDeactive(string BillTypeId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = "INACTIVE";

            NameValuePairs nvp = new NameValuePairs
            {
                new NameValuePair("@BillTypeId", BillTypeId),
                 
                 
                new NameValuePair("@RequestId", CommonObj.RequestId),
                new NameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetBillType]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public List<BillType> TypeGet(bool? IsActive = null)
        {
            List<BillType> billTypes = new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetBillType]",
                new NameValuePairs
                {
                     
                     
                    new NameValuePair("@QueryType", "ALL")
                }).ToList<BillType>();
            if (IsActive != null)
            {
                if (IsActive.Value)
                    return billTypes.Where(a => a.IsActive).ToList();
                if (!IsActive.Value)
                    return billTypes.Where(a => !a.IsActive).ToList();
            }
            return billTypes;
        }
        #endregion

        #region Bill
        public List<Bill> GetBills()
        {
            try
            {
                _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
                NameValuePairs nvp = new NameValuePairs()
                {
                     
                     
                    new NameValuePair("@QueryType", "LIST")
                };
                List<Bill> bills = _sqlDBAccess.GetData("[dbo].[spGetBill]", nvp).ToList<Bill>();
                return bills;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bill GetBill(long BillId)
        {
            try
            {
                _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
                NameValuePairs nvp = new NameValuePairs()
                {
                     
                     
                    new NameValuePair("@BillId", BillId),
                    new NameValuePair("@QueryType", "SINGLE")
                };
                Bill bill = _sqlDBAccess.GetData("[dbo].[spGetBill]", nvp).FirstOrDefault<Bill>();
                return bill;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PurchaseOrder> GetForEntry()
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            NameValuePairs nvp = new NameValuePairs()
                {
                     
                     
                    new NameValuePair("@QueryType", "ENTRY")
                };
            List<PurchaseOrder> purchaseOrders = _sqlDBAccess.GetData("[dbo].[spGetBill]", nvp).ToList<PurchaseOrder>();
            return purchaseOrders;
        }

        public PurchaseOrder GetForEntry(long purchaseOrderId)
        {
            try
            {
                _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
                NameValuePairs nvp = new NameValuePairs()
                {
                     
                     
                    new NameValuePair("@PurchaseOrderId", purchaseOrderId),
                    new NameValuePair("@QueryType", "ENTRYSINGLE")
                };
                PurchaseOrder purchaseOrder = _sqlDBAccess.GetData("[dbo].[spGetBill]", nvp).FirstOrDefault<PurchaseOrder>();
                return purchaseOrder;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BillDetail> GetBillDetails(long purchaseOrderId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            NameValuePairs nvp = new NameValuePairs()
                {
                     
                     
                    new NameValuePair("@PurchaseOrderId", purchaseOrderId),
                    new NameValuePair("@QueryType", "COMULTATIVE")
                };
            List<BillDetail> bildtl = _sqlDBAccess.GetData("[dbo].[spGetBill]", nvp).ToList<BillDetail>();
            return bildtl;
        }

        public List<Bill> GetBillsForPayment(long vendorId)
        {
            try
            {
                _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
                NameValuePairs nvp = new NameValuePairs()
                {
                     
                     
                    new NameValuePair("@VendorId", vendorId),
                    new NameValuePair("@QueryType", "PAYMENT")
                };
                List<Bill> bills = _sqlDBAccess.GetData("[dbo].[spGetBill]", nvp).ToList<Bill>();                
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
                List<BillDetailsType> billDetailsTypes = new List<BillDetailsType>();
                int count = 1;
                bill.BilledAmount = 0;
                bill.TaxAmount = 0;
                bill.TotalAmount = 0;

                bill.BillDetails.ForEach(billItem =>
                {
                    if(billItem.BilledQuantity > 0)
                    {
                        BillDetailsType billDetailsType = new BillDetailsType
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
                    _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
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
                        new NameValuePair("@RequestId", CommonObj.RequestId),
                        new NameValuePair("@QueryType", "INSERT")
                    };
                    return _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetBill]", nvp, "@OutParam").ToString();
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
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            NameValuePairs nvp = new NameValuePairs()
            {
                 
                 
                new NameValuePair("@BillId", BillId),
                new NameValuePair("@QueryType", "DETAIL")
            };
            DataSet ds = _sqlDBAccess.GetDataSet("[dbo].[spGetBill]", nvp);
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
