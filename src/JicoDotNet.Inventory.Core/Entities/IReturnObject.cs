namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IReturnObject
    {
        bool Status { get; set; }
        object ReturnData { get; set; }
        string Message { get; set; }
        int ResponseStatus { get; set; }
    }
}
