namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IQuotationDetail : IDetail
    {
        long QuotationDetailId { get; set; }
        long QuotationId { get; set; }
        string QuotationNumber { get; set; }
    }
}