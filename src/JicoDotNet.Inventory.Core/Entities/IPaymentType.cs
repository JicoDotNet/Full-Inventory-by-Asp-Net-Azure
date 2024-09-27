namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IPaymentType
    {
        long PaymentTypeId { get; set; }
        string PaymentTypeName { get; set; }
        string Description { get; set; }
    }
}
