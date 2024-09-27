namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IDefaultSettings
    {
        string POTnC { get; set; }
        string PORemarks { get; set; }

        string GRNRemarks { get; set; }
        string BillRemarks { get; set; }

        string QuotationSendingTnC { get; set; }
        string QuotationSendingRemarks { get; set; }

        string SOTnC { get; set; }
        string SORemarks { get; set; }
        string DeliveryRemarks { get; set; }
        string InvoiceTnC { get; set; }
        string InvoiceRemarks { get; set; }
    }
}
