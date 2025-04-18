﻿using JicoDotNet.Inventory.Core.Entities.Inner;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface ICommonLogicHelper : IHttpRequest
    {
        string Token { get; set; }
        string SqlSchema { get; }
        string SqlConnectionString { get; set; }
        string NoSqlConnectionString { get; set; }
        IWebConfigAppSettings AppSettings { get; set; }
    }
}