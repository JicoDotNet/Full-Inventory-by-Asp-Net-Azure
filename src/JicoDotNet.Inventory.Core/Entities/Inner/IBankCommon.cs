namespace JicoDotNet.Inventory.Core.Entities.Inner
{
    public interface IBankCommon
    {
        string AccountName { get; set; }
        string AccountNumber { get; set; }
        string BankName { get; set; }
        string IFSC { get; set; }
        string MICR { get; set; }
        string BranchName { get; set; }
        string BranchAddress { get; set; }
    }
}
