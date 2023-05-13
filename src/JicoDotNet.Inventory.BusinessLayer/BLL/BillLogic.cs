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

            nameValuePairs nvp = new nameValuePairs
            {
                 
                new nameValuePair("@BillTypeId", billType.BillTypeId),
                new nameValuePair("@BillTypeName", billType.BillTypeName),
                new nameValuePair("@Description", billType.Description),
                 
                new nameValuePair("@RequestId", CommonObj.RequestId),
                new nameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetBillType]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public string TypeDeactive(string BillTypeId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = "INACTIVE";

            nameValuePairs nvp = new nameValuePairs
            {
                new nameValuePair("@BillTypeId", BillTypeId),
                 
                 
                new nameValuePair("@RequestId", CommonObj.RequestId),
                new nameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetBillType]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public List<BillType> TypeGet(bool? IsActive = null)
        {
            List<BillType> billTypes = new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetBillType]",
                new nameValuePairs
                {
                     
                     
                    new nameValuePair("@QueryType", "ALL")
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
                nameValuePairs nvp = new nameValuePairs()
                {
                     
                     
                    new nameValuePair("@QueryType", "LIST")
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
                nameValuePairs nvp = new nameValuePairs()
                {
                     
                     
                    new nameValuePair("@BillId", BillId),
                    new nameValuePair("@QueryType", "SINGLE")
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
            nameValuePairs nvp = new nameValuePairs()
                {
                     
                     
                    new nameValuePair("@QueryType", "ENTRY")
                };
            List<PurchaseOrder> purchaseOrders = _sqlDBAccess.GetData("[dbo].[spGetBill]", nvp).ToList<PurchaseOrder>();
            return purchaseOrders;
        }

        public PurchaseOrder GetForEntry(long purchaseOrderId)
        {
            try
            {
                _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
                nameValuePairs nvp = new nameValuePairs()
                {
                     
                     
                    new nameValuePair("@PurchaseOrderId", purchaseOrderId),
                    new nameValuePair("@QueryType", "ENTRYSINGLE")
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
            nameValuePairs nvp = new nameValuePairs()
                {
                     
                     
                    new nameValuePair("@PurchaseOrderId", purchaseOrderId),
                    new nameValuePair("@QueryType", "COMULTATIVE")
                };
            List<BillDetail> bildtl = _sqlDBAccess.GetData("[dbo].[spGetBill]", nvp).ToList<BillDetail>();
            return bildtl;
        }

        public List<Bill> GetBillsForPayment(long vendorId)
        {
            try
            {
                _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
                nameValuePairs nvp = new nameValuePairs()
                {
                     
                     
                    new nameValuePair("@VendorId", vendorId),
                    new nameValuePair("@QueryType", "PAYMENT")
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
                    nameValuePairs nvp = new nameValuePairs()
                    {
                         
                         
                        new nameValuePair("@BillTypeId", bill.BillTypeId),
                        new nameValuePair("@BillDate", bill.BillDate),
                        new nameValuePair("@BillDueDate", bill.BillDueDate),
                        new nameValuePair("@BillNumber", "BIL-"),
                        new nameValuePair("@VendorId", bill.VendorId),
                        new nameValuePair("@PurchaseOrderId", bill.PurchaseOrderId),
                        new nameValuePair("@VendorDONumber", bill.VendorDONumber),
                        new nameValuePair("@VendorInvoiceNumber", bill.VendorInvoiceNumber),
                        new nameValuePair("@VendorInvoiceDate", bill.VendorInvoiceDate),
                        new nameValuePair("@Remarks", bill.Remarks),
                        new nameValuePair("@IsGstApplicable", bill.IsGstApplicable),
                        new nameValuePair("@GSTNumber", bill.GSTNumber),
                        new nameValuePair("@GSTStateCode", bill.GSTStateCode),
                        new nameValuePair("@GSTType", bill.GSTType),
                        new nameValuePair("@BilledAmount", bill.BilledAmount),
                        new nameValuePair("@TaxAmount", bill.TaxAmount),
                        new nameValuePair("@TotalAmount", bill.TotalAmount),
                        new nameValuePair("@IsFullBilled", bill.IsFullBilled),
                        new nameValuePair("@BillDetail", billDetailsTypes.ToDataTable()),
                        new nameValuePair("@RequestId", CommonObj.RequestId),
                        new nameValuePair("@QueryType", "INSERT")
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
            nameValuePairs nvp = new nameValuePairs()
            {
                 
                 
                new nameValuePair("@BillId", BillId),
                new nameValuePair("@QueryType", "DETAIL")
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
