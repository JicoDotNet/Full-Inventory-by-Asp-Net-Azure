using DataAccess.AzureStorage;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Validator.Interfaces;
using JicoDotNet.Inventory.Core.Models;
using System;
using System.Web;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class SupportLogic : DBManager
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
            support.RequestId = CommonLogicObj.RequestId;
            if (memory != null)
                support.ScreenshotImageUrl = new ExecuteBlobManager("MyCompany", CommonLogicObj.NoSqlConnectionString).UploadFile(memory, new string[] { "Support", "Screenshot", support.UserId.ToString() }, CommonLogicObj.RequestId);
            TableManager = new ExecuteTableManager("Support", CommonLogicObj.NoSqlConnectionString);
            TableManager.InsertEntity(support);
            return support;
        }
    }
}
