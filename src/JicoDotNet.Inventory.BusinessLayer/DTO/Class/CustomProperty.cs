using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class CustomProperty : TableEntity, ICustomProperty, 
        IStatus, IHRequest, IActivity
    {
        /*
         * PartitionKey : TenantCode
         * RowKey : CompanyId + UserId + TimeStamp(X)
         */

         
         
         

        /// <summary>
        /// Desplay Text
        /// </summary>
        public string LabelName { get; set; }

        /// <summary>
        /// Coloumn Name
        /// </summary>
        public string ColoumnName { get; set; }

        /// <summary>
        /// Microsoft.WindowsAzure.Storage.Table.EdmType 
        /// Data Type
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// It will set default value
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// IsRequired : Field can be blank or not
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// IsPrintable as PDF
        /// </summary>
        public bool IsPrintable { get; set; }

        /// <summary>
        /// PropertyFor : enum ECustomPropertyFor
        /// </summary>
        public string PropertyFor { get; set; }


        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }
        public string RequestId { get; set; }
    }
}
