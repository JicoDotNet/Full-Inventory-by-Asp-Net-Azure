namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IDraft
    {
        long UserId { get; set; }
        string DraftData { get; set; }
        string DraftType { get; set; }
    }
}
