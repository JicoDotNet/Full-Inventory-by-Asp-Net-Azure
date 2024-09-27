namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IUser
    {
        string UserFullName { get; set; }
        string UserEmail { get; set; }
    }
}