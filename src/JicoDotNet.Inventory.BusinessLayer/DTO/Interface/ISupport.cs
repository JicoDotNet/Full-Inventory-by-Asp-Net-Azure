using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface ISupport : ITableEntity
    {
        string ScreenshotImageUrl { get; set; }
        string QueryStatement { get; set; }
        string TicketNumber { get; set; }
        long UserId { get; set; }
        string QueriesUrl { get; set; }
    }
}
