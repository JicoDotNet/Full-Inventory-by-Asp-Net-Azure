using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface ICommonRequestDto : IHRequest
    {
        string Token { get; set; }
        object SqlConnectionString { get; set; }
        object NoSqlConnectionString { get; set; }
    }
}
