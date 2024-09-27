namespace JicoDotNet.Inventory.Core.Entities
{
    public interface ICustomer : ICustomerType
    {
        long CustomerTypeId { get; set; }
        string CompanyName { get; set; }
        string CompanyType { get; set; }
        string StateCode { get; set; }
        bool IsGSTRegistered { get; set; }
        string GSTStateCode { get; set; }
        string GSTNumber { get; set; }
        string PANNumber { get; set; }
        string ContactPerson { get; set; }
        string Email { get; set; }
        string Mobile { get; set; }
    }
}
