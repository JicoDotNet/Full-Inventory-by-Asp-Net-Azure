using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface IBranch : IActivity, IStatus,   IHRequest
    {
        long BranchId { get; set; }
        string BranchName { get; set; }
        string BranchCode { get; set; }
        string Address { get; set; }
        string City { get; set; }
        string State { get; set; }
        string PostalCode { get; set; }
        string ContactPerson { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
        string Description { get; set; }
    }
}
