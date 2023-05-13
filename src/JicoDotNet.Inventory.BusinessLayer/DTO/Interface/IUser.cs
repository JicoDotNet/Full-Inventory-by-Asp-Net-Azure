using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface IUser
    {
        string UserFullName { get; set; }
        string UserEmail { get; set; }
    }
}