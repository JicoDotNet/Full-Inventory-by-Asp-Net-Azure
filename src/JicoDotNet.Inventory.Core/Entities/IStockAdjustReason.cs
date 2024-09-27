namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IStockAdjustReason
    {
        long AdjustReasonId { get; set; }
        string AdjustReason { get; set; }
        bool IsDefault { get; set; }

        bool IsStockIncreasing { get; set; }
    }
}
