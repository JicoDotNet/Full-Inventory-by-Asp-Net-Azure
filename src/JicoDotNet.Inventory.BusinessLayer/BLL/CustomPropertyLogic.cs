using DataAccess.AzureStorage.Table;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Enumeration;
using JicoDotNet.Inventory.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class CustomPropertyLogic : DBManager
    {
        public CustomPropertyLogic(ICommonLogicHelper CommonObj) : base(CommonObj) { }

        public string SetMaster(CustomProperty customProperty, ECustomPropertyFor propertyFor)
        {
            try
            {
                if (customProperty != null
                && !string.IsNullOrEmpty(customProperty.LabelName)
                && propertyFor != ECustomPropertyFor.None)
                {
                    TableManager = new AzureTableAccess("PropertyMaster", CommonLogicObj.NoSqlConnectionString);

                    customProperty.PartitionKey = "MyCompany";

                    customProperty.PropertyFor = propertyFor.ToString();
                    customProperty.TransactionDate = GenericLogic.IstNow;

                    // Insert
                    if (string.IsNullOrEmpty(customProperty.RowKey))
                    {
                        customProperty.RowKey = GenericLogic.IstNow.TimeStamp().ToString();
                        customProperty.ColumnName = Regex.Replace(customProperty.LabelName, @"[^a-zA-Z0-9]", "") + "_" + customProperty.RowKey;
                        customProperty.IsActive = true;
                        TableManager.InsertEntity(customProperty);
                    }
                    // Update
                    else
                    {
                        CustomProperty customPropertyOld = GetMaster(propertyFor, customProperty.RowKey);
                        if (customPropertyOld != null)
                        {
                            customProperty.RowKey = customPropertyOld.RowKey;
                            customProperty.ColumnName = customPropertyOld.ColumnName;
                            customProperty.IsActive = customPropertyOld.IsActive;
                            TableManager.UpdateEntity(customProperty);
                        }
                    }
                    return customProperty.RowKey;
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CustomProperty> GetMaster(ECustomPropertyFor propertyFor, bool IsPrintable = false)
        {
            try
            {
                TableManager = new AzureTableAccess("PropertyMaster", CommonLogicObj.NoSqlConnectionString);
                string qry = "PropertyFor eq '" + propertyFor.ToString() + "'" +
                    " and IsActive eq true ";
                if (IsPrintable)
                {
                    qry += " and IsPrintable eq true ";
                }
                return TableManager.RetrieveEntities<CustomProperty>(qry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CustomProperty> GetMasters()
        {
            try
            {
                TableManager = new AzureTableAccess("PropertyMaster", CommonLogicObj.NoSqlConnectionString);
                string qry = "IsActive eq true ";
                return TableManager.RetrieveEntities<CustomProperty>(qry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CustomProperty GetMaster(ECustomPropertyFor propertyFor, string RowKey)
        {
            try
            {
                TableManager = new AzureTableAccess("PropertyMaster", CommonLogicObj.NoSqlConnectionString);
                string q = "RowKey eq '" + RowKey + "' " +
                    " and PropertyFor eq '" + propertyFor.ToString() + "'" +
                    " and IsActive eq true";
                return TableManager.RetrieveEntity<CustomProperty>(q);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeactiveMaster(ECustomPropertyFor propertyFor, string RowKey)
        {
            try
            {
                CustomProperty customProperty = GetMaster(propertyFor, RowKey);
                if (customProperty != null)
                {
                    customProperty.IsActive = false;
                    TableManager = new AzureTableAccess("PropertyMaster", CommonLogicObj.NoSqlConnectionString);
                    TableManager.UpdateEntity(customProperty);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetValue(ECustomPropertyFor propertyFor,
            Dictionary<string, object> formDictionary,
            long? Identity = null,
            string IdentityValue = null)
        {
            try
            {
                if (formDictionary == null || formDictionary.Count == 0)
                    return;

                List<CustomProperty> customProperties = GetMaster(propertyFor);

                IDictionary<string, object> customPropertiesValue = new Dictionary<string, object>();

                if (customProperties == null || customProperties.Count == 0)
                    return;                

                /* Main Custom Property */
                foreach (CustomProperty dm in customProperties)
                {
                    if (dm.DataType == EdmType.String.ToString())
                    {
                        try
                        {
                            if (formDictionary[dm.ColumnName] != null
                                && !string.IsNullOrEmpty(formDictionary[dm.ColumnName]?.ToString()))
                            {
                                customPropertiesValue.Add(formDictionary[dm.ColumnName]?.ToString(),
                                    formDictionary[dm.ColumnName].ToString());
                            }
                        }
                        catch { }
                    }
                    if (dm.DataType == EdmType.Double.ToString())
                    {
                        try
                        {
                            customPropertiesValue.Add(formDictionary[dm.ColumnName]?.ToString(),
                                    Convert.ToDouble(formDictionary[dm.ColumnName]));
                        }
                        catch { }
                    }
                    if (dm.DataType == EdmType.Boolean.ToString())
                    {
                        try
                        {
                            customPropertiesValue.Add(formDictionary[dm.ColumnName]?.ToString(),
                                    Convert.ToBoolean(formDictionary[dm.ColumnName]));
                        }
                        catch { }
                    }
                    if (dm.DataType == EdmType.DateTime.ToString())
                    {
                        try
                        {
                            if (formDictionary[dm.ColumnName] != null
                                && !string.IsNullOrEmpty(formDictionary[dm.ColumnName]?.ToString()))
                            {
                                DateTime.TryParseExact(formDictionary[dm.ColumnName]?.ToString(),
                                                        GenericLogic.DateMaskFormat,
                                                        System.Globalization.CultureInfo.InvariantCulture,
                                                        System.Globalization.DateTimeStyles.None,
                                                        out DateTime PropDateValue);

                                customPropertiesValue.Add(formDictionary[dm.ColumnName]?.ToString(), PropDateValue);
                            }
                        }
                        catch { }
                    }
                }                

                /* Other Fields */
                customPropertiesValue.Add("RequestId", CommonLogicObj.RequestId);
                customPropertiesValue.Add("TransactionDate", GenericLogic.IstNow);
                customPropertiesValue.Add("IsActive", true);
                customPropertiesValue.Add("PropertyFor", propertyFor.ToString());

                DynamicTableEntity dynamicProperty = new DynamicTableEntity();

                /* PartitionKey & RowKey */
                dynamicProperty.PartitionKey = "MyCompany";
                dynamicProperty.RowKey = GenericLogic.IstNow.TimeStamp().ToString();
                dynamicProperty.Set(customPropertiesValue);

                TableManager = new AzureTableAccess("PropertyData", CommonLogicObj.NoSqlConnectionString);
                TableManager.InsertEntity(dynamicProperty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Dictionary<string, string> GetValue(ECustomPropertyFor propertyFor,
            long? Identity = null,
            string IdentityValue = null)
        {
            try
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();

                List<CustomProperty> customProperties = GetMaster(propertyFor, true);
                if (customProperties != null && customProperties.Count > 0)
                {
                    TableManager = new AzureTableAccess("PropertyData", CommonLogicObj.NoSqlConnectionString);
                    string qry = " PropertyFor eq '" + propertyFor.ToString() + "' " +
                        " and IsActive eq true ";
                    if (Identity.HasValue)
                    {
                        qry += " and Identity eq " + Identity.Value + " ";
                    }
                    if (!string.IsNullOrEmpty(IdentityValue))
                    {
                        qry += " and IdentityValue eq '" + IdentityValue + "' ";
                    }

                    DynamicTableEntity dynamicProperty = TableManager.RetrieveEntity(qry);

                    foreach (CustomProperty customProperty in customProperties)
                    {
                        if (dynamicProperty != null && dynamicProperty.Properties.Count > 0)
                        {
                            foreach (KeyValuePair<string, object> property in dynamicProperty.Properties)
                            {
                                if (customProperty.ColumnName == property.Key)
                                {
                                    if (property.Value?.GetType() == typeof(string))
                                    {
                                        dictionary.Add(customProperty.LabelName, property.Value.ToString());
                                    }
                                    if (property.Value?.GetType() == typeof(double))
                                    {
                                        dictionary.Add(customProperty.LabelName, ((double)property.Value).ToDisplayDouble());
                                    }
                                    if (property.Value?.GetType() == typeof(bool))
                                    {
                                        if ((bool)property.Value)
                                            dictionary.Add(customProperty.LabelName, "Yes");
                                        else
                                            dictionary.Add(customProperty.LabelName, "No");
                                    }
                                    if (property.Value?.GetType() == typeof(DateTime))
                                    {
                                        dictionary.Add(customProperty.LabelName, Convert.ToDateTime(property.Value).ToDisplayShortDateString());
                                    }
                                }
                            }
                        }
                    }
                }
                return dictionary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
