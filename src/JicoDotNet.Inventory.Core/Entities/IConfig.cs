namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IConfig
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
