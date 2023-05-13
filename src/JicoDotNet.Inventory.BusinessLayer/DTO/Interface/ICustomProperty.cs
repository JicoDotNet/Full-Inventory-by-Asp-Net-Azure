using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface ICustomProperty : ITableEntity
    {
        /// <summary>
        /// Desplay Text
        /// </summary>
        string LabelName { get; set; }

        /// <summary>
        /// Coloumn Name
        /// </summary>
        string ColoumnName { get; set; }

        /// <summary>
        /// Microsoft.WindowsAzure.Storage.Table.Edm 
        /// Data Type
        /// </summary>
        string DataType { get; set; }

        /// <summary>
        /// It will set default value
        /// </summary>
        string DefaultValue { get; set; }

        /// <summary>
        /// IsRequired : Field can be blank or not
        /// </summary>
        bool IsRequired { get; set; }

        /// <summary>
        /// IsPrintable as PDF
        /// </summary>
        bool IsPrintable { get; set; }

        /// <summary>
        /// PropertyFor : enum ECustomPropertyFor
        /// </summary>
        string PropertyFor { get; set; }
    }
}