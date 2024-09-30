namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IBranch
    {
        long BranchId { get; set; }
        string BranchName { get; set; }
        string BranchCode { get; set; }
        string Address { get; set; }
        string City { get; set; }
        string State { get; set; }
        string PostalCode { get; set; }
        string ContactPerson { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
        string Description { get; set; }
    }
}
