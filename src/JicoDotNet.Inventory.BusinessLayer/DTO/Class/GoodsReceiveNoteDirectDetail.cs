using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    /// <summary>
    /// This class Only for Model POST from HTML to Controller and Logic.
    /// </summary>
    public class GoodsReceiveNoteDirectDetail : PurchaseOrderDetail, IProductAttribute
    {
        public bool IsPerishable { get; set; }
        public string BatchNo { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
