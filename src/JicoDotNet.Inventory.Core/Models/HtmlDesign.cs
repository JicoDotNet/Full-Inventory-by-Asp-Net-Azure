using Microsoft.WindowsAzure.Storage.Table;

namespace JicoDotNet.Inventory.Core.Models
{
    public class HtmlDesign : TableEntity
    {
        public string InvoiceHtml { get; set; }
    }
}
