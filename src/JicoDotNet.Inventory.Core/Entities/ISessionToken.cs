namespace JicoDotNet.Inventory.Core.Entities
{
    public interface ISessionToken : ISessionCredential
    {
        string Key { get; set; }
    }
}
