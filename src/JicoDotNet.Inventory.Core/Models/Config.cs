using Microsoft.WindowsAzure.Storage.Table;
using System;
using JicoDotNet.Inventory.Core.Entities;

namespace JicoDotNet.Inventory.Core.Models
{
    public class Config : TableEntity, IConfig, IDefaultSettings, IDtoHeader
    {
        public bool HasPerishableProduct { get; set; }
        public bool HasExpirationDate { get; set; }         
        public bool HasBatchNo { get; set; }

        #region Currently Not Using
        //public bool HasManufacturingUnit { get; set; }
        //public bool AllowSalesReturn { get; set; }
        //public bool AllowPurchaseReturn { get; set; }
        //public string SalesMode { get; set; }
        //public string PaymentReceiveMode { get; set; }
        //public string PaymentReleaseMode { get; set; }
        #endregion

        public double TimeZone { get; set; }
        public string CurrencySymbol { get; set; }
        public string CurrencyCode { get; set; }

        public int MaxDetailsCount { get; set; }

        #region Default Settings
        public string POTnC { get; set; }           // TextArea
        public string PORemarks { get; set; }       // TextArea

        public string GRNRemarks { get; set; }      // Input
        public string BillRemarks { get; set; }     // Input

        public string QuotationSendingTnC { get; set; }     // TextArea
        public string QuotationSendingRemarks { get; set; } // TextArea

        public string SOTnC { get; set; }           // TextArea
        public string SORemarks { get; set; }       // TextArea
        public string DeliveryRemarks { get; set; } // Input
        public string InvoiceTnC { get; set; }      // TextArea
        public string InvoiceRemarks { get; set; }  // TextArea
        public string RetailTnC { get; set; }       // TextArea
        public string RetailRemarks { get; set; }   // TextArea 
        #endregion

        public bool IsActive { get; set; }
        public DateTime TransactionDate { get; set; }
        public string RequestId { get; set; }
    }
}