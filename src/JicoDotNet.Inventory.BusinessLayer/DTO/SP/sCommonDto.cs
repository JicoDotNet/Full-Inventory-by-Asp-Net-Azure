using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;

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