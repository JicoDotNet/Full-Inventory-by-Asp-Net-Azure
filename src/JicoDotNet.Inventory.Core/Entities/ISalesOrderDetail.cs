namespace JicoDotNet.Inventory.Core.Entities
{
    public interface ISalesOrderDetail : IDetail
    {
        long SalesOrderDetailId { get; set; }

        long SalesOrderId { get; set; }
        string SalesOrderNumber { get; set; }
        string AmendmentNumber { get; set; }
    }
}