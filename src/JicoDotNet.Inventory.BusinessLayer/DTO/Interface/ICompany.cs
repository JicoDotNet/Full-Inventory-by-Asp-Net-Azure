using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface ICompany
    {
        string Email { get; set; }
        string Mobile { get; set; }
        string Address { get; set; }
        string PINCode { get; set; }
        string StateCode { get; set; }
        string City { get; set; }
        string WebsiteUrl { get; set; }
    }
}
