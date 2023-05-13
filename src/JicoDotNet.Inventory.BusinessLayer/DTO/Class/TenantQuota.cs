using Microsoft.WindowsAzure.Storage.Table;
using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    //public class TenantQuota : TableEntity, ITenantQuota, ITenantQuotaUsage,   IHRequest, IStatus
    //{
    //    public string LimitOn { get; set; }
    //    public int? Limit { get; set; }

    //    public int? Used { get; set; }

    //     
    //     
    //    public string RequestId { get; set; }
    //    public bool IsActive { get; set; }
    //}

    ////public class TenantQuotaUsed
    ////{
    ////    // Tenant wase Limit
    ////    public long User { get; set; }
    ////    public long Organization { get; set; }
    ////    public long Retail { get; set; }


    ////    // Tenant's Company wase Limit
    ////    public long Product { get; set; }
    ////    public long MessurementUnit { get; set; }
    ////    public long Warehouse { get; set; }


    ////    // Month wise Limit
    ////    public long Bill { get; set; }
    ////    public long Invoice { get; set; }
    ////}
}
