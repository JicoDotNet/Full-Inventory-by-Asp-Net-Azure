namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IWebConfigAppSettings
    {
        string UserFullName { get; }
        string UserEmail { get; }
        string Password { get; }
        string AllowedStartIP { get; }
        string AllowedEndIP { get; }

        string CompanyName { get; }
        string GSTNumber { get; }

        string CompanyEmail { get; }
        string CompanyMobile { get; }
        string CompanyAddress { get; }
        string CompanyPINCode { get; }
        string CompanyCity { get; }
        string CompanyWebsite { get; }
    }
}
