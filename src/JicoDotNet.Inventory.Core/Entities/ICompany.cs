namespace JicoDotNet.Inventory.Core.Entities
{
    public interface ICompany : ICompanyBasic
    {
        string Email { get; set; }
        string Mobile { get; set; }
        string Address { get; set; }
        string PINCode { get; set; }
        string City { get; set; }
        string WebsiteUrl { get; set; }
        string LogoUrl { get; set; }
    }
}
