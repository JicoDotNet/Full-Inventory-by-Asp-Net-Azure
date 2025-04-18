using JicoDotNet.Inventory.Core.Entities.Inner;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IPaymentType : IGenericDescription
    {
        long PaymentTypeId { get; set; }
        string PaymentTypeName { get; set; }
    }
}
