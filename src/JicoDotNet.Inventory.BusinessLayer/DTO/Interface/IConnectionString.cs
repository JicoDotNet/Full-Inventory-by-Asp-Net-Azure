using DataAccess.Sql;
using DataAccess.Sql.Entity;
using JicoDotNet.Inventory.BusinessLayer.DTO.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface IConnectionString
    {
        ICommonRequestDto CommonObj { get; }
        ISqlDBAccess _sqlDBAccess { get; }
    }
}