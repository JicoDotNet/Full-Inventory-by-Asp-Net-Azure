using DataAccess.AzureStorage;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.BusinessLayer.DTO.Core;
using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class SupportLogic : ConnectionString
    {
        public SupportLogic(ICommonRequestDto CommonObj) : base(CommonObj) { }

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
