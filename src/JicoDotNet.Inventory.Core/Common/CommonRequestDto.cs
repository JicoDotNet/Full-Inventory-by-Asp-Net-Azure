using JicoDotNet.Inventory.Core.Entities;

namespace JicoDotNet.Inventory.Core.Common
{
    public class CommonRequestDto : ICommonRequestDto
    {
        public string RequestId { get; set; }

        public string Token { get; set; }
        public object SqlConnectionString { get; set; }
        public object NoSqlConnectionString { get; set; }
    }
}