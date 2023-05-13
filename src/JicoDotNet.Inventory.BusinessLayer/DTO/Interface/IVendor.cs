using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface IVendor : IActivity, IStatus,   IHRequest
    {
        long VendorId { get; set; }
        long VendorTypeId { get; set; }
        string CompanyName { get; set; }
        string CompanyType { get; set; }
        string StateCode { get; set; }
        bool IsGSTRegistered { get; set; }
        string GSTStateCode { get; set; }
        string GSTNumber { get; set; }
        string PANNumber { get; set; }
        string ContactPerson { get; set; }
        string Email { get; set; }
        string Mobile { get; set; }
    }
}
