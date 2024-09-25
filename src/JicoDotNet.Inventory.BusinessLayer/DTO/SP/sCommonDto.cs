using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.SP
{
    public class sCommonDto : IHRequest
    {
        public string RequestId { get; set; }
        public string Token { get; set; }


        public object SqlConnectionString { get; set; }
        public object NoSqlConnectionString { get; set; }
    }
}