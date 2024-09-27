namespace JicoDotNet.Inventory.Core.Entities
{
    public interface ICustomProperty
    {
        /// <summary>
        /// Desplay Text
        /// </summary>
        string LabelName { get; set; }

        /// <summary>
        /// Coloumn Name
        /// </summary>
        string ColumnName { get; set; }

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