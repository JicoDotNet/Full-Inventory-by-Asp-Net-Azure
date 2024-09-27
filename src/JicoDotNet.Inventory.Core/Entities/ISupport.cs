namespace JicoDotNet.Inventory.Core.Entities
{
    public interface ISupport
    {
        string ScreenshotImageUrl { get; set; }
        string QueryStatement { get; set; }
        string TicketNumber { get; set; }
        long UserId { get; set; }
        string QueriesUrl { get; set; }
    }
}
