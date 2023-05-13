using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface ISalesOrderDetail : IDetail
    {
        long SalesOrderDetailId { get; set; }

        long SalesOrderId { get; set; }
        string SalesOrderNumber { get; set; }
        string AmendmentNumber { get; set; }
    }
}