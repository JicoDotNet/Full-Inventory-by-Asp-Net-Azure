namespace JicoDotNet.Authentication.Interfaces
{
    public interface ICommonLogicHelper : IHttpRequest
    {
        string Token { get; set; }
        string SqlSchema { get; }
        string SqlConnectionString { get; set; }
        object NoSqlConnectionString { get; set; }
    }
}