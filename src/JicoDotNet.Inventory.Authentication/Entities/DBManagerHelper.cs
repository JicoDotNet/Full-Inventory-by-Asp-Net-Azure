using JicoDotNet.Authentication.Interfaces;
using System;
namespace JicoDotNet.Authentication.Entities
{
    public class DBManagerHelper : IDBManagerHelper
    {
        protected DBManagerHelper(ICommonLogicHelper commonDtoObj)
        {
            CommonLogicObj = commonDtoObj ?? throw new ArgumentNullException(nameof(commonDtoObj), "Object can not be null");
        }
        public ICommonLogicHelper CommonLogicObj { get; }
    }
}
