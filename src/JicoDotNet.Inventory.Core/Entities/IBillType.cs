namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IBillType : IDtoHeader
    {
        long BillTypeId { get; set; }
        string BillTypeName { get; set; }
        string Description { get; set; }
    }
}
