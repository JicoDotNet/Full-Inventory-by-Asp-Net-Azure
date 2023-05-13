using Microsoft.WindowsAzure.Storage.Table;
using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class DataTracking : TableEntity, IDataTracking
    {
        public string Data { get; set; }

        public string RequestId { get; set; }

         
         

         

        public long UserId { get; set; }
        public string UserName { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}
