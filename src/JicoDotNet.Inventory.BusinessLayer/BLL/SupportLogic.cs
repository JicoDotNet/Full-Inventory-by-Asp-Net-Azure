using DataAccess.AzureStorage;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Models;
using System;
using System.Web;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class SupportLogic : ConnectionString
    {
        public SupportLogic(ICommonLogicHelper CommonObj) : base(CommonObj) { }

        public Support Set(Support support, MemoryFile memory)
        {
            DateTime dt = GenericLogic.IstNow;
            support.PartitionKey = "MyCompany";
            support.RowKey = GenericLogic.IstNow.TimeStamp().ToString();
            support.TicketNumber = "T" +
                dt.DayOfYear.ToString("X") +
                Convert.ToInt32(dt.TimeOfDay.TotalSeconds).ToString("X");
            support.TransactionDate = dt;
            support.IsActive = true;
            support.RequestId = CommonObj.RequestId;
            if (memory != null)
                support.ScreenshotImageUrl = new ExecuteBlobManager("MyCompany", CommonObj.NoSqlConnectionString).UploadFile(memory, new string[] { "Support", "Screenshot", support.UserId.ToString() }, CommonObj.RequestId);
            TableManager = new ExecuteTableManager("Support", CommonObj.NoSqlConnectionString);
            TableManager.InsertEntity(support);
            return support;
        }
    }
}
