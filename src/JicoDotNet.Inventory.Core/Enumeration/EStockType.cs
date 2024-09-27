namespace JicoDotNet.Inventory.Core.Enumeration
{
    public enum EStockType
    {
        Transfer = 0,
        GRN = 1,
        Shipment = -1,

        SalesReturn = 2,
        PurchaseReturn = -2,

        OpeningStock = 10,
        ClosingStock = -10,

        AdjustIncrease = 20,
        AdjustDecrease = -20
    }
}