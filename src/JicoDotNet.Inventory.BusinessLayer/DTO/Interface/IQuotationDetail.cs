using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface IQuotationDetail : IDetail
    {
        long QuotationDetailId { get; set; }
        long QuotationId { get; set; }
        string QuotationNumber { get; set; }
    }
}