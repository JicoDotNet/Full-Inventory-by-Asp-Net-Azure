using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface IDraft : ITableEntity
    {
        long UserId { get; set; }
        string DraftData { get; set; }
        string DraftType { get; set; }
    }
}
