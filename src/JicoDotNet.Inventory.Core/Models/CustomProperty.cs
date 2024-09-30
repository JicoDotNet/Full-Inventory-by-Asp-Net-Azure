using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Entities.Inner;
using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace JicoDotNet.Inventory.Core.Models
{
    public class CustomProperty : TableEntity, ICustomProperty,
        IStatus, IHRequest, IActivity
    {
        // TODO - need to change
        /*
         * PartitionKey : TenantCode
         * RowKey : CompanyId + UserId + TimeStamp(X)
         */

        /// <summary>
        /// Display Text
        /// </summary>
        public string LabelName { get; set; }

        /// <summary>
        /// Column Name
        /// </summary>
        public string ColumnName { get; set; }

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
