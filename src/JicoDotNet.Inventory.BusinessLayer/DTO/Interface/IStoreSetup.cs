using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfRnd.MiddleLayer.DTO.Interface
{
    public interface IStoreSetup
    {
        string StoreDomain { get; set; }
        bool IsGoodsStore { get; set; }
        bool ServiceBookingEligible { get; set; }
    }
}
