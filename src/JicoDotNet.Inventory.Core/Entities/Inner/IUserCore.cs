using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.Core.Entities.Inner
{
    public interface IUserCore
    {
        string UserFullName { get; set; }
        string UserEmail { get; set; }
    }
}
