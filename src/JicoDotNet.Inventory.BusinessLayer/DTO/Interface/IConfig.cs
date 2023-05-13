using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface IConfig: ITableEntity
    {
        bool HasPerishableProduct { get; set; }
        bool HasExpirationDate { get; set; }
        bool HasBatchNo { get; set; }

        #region Currently Not Using
        //bool HasManufacturingUnit { get; set; }
        //bool AllowSalesReturn { get; set; }
        //bool AllowPurchaseReturn { get; set; }
        //string SalesMode { get; set; }
        //string PaymentReceiveMode { get; set; }
        //string PaymentReleaseMode { get; set; } 
        #endregion

        double TimeZone { get; set; }
        string CurrencySymbol { get; set; }
        string CurrencyCode { get; set; }

        int MaxDetailsCount { get; set; }
    }
}
