using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface IUnitOfMeasure : IActivity, IStatus,   IHRequest
    {
        long UnitOfMeasureId { get; set; }
        string UnitOfMeasureName { get; set; }
        string Description { get; set; }
    }
}
