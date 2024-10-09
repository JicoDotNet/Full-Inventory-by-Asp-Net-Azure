using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Authentication.Interfaces
{
    public interface IUser
    {
        string UserFullName { get; set; }
        string UserEmail { get; set; }
    }
}
