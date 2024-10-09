using JicoDotNet.Authentication.Interfaces;
using JicoDotNet.Inventory.Core.Entities;

namespace JicoDotNet.Inventory.Core.Common
{
    public class CommonRequestDto : ICommonLogicHelper
    {
        public string RequestId { get; set; }

        public string Token { get; set; }
        public string SqlConnectionString { get; set; }
        public object NoSqlConnectionString { get; set; }
    }
}