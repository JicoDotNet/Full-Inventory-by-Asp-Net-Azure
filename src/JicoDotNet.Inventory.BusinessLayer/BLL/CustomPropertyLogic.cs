using DataAccess.AzureStorage;
using DataAccess.Sql;
using Microsoft.WindowsAzure.Storage.Table;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Enumeration;
using JicoDotNet.Inventory.Core.Models;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class CustomPropertyLogic : ConnectionString
    {
        public CustomPropertyLogic(ICommonRequestDto CommonObj) : base(CommonObj) { }

        public string SetMaster(CustomProperty customProperty, ECustomPropertyFor propertyFor)
        {
            try
            {
                if (customProperty != null
                && !string.IsNullOrEmpty(customProperty.LabelName)
                && propertyFor != ECustomPropertyFor.None)
                {
                    TableManager = new ExecuteTableManager("PropertyMaster", CommonObj.NoSqlConnectionString);

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
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<CustomProperty> GetMaster(ECustomPropertyFor propertyFor, bool IsPrintable = false)
        {
            try
            {
                TableManager = new ExecuteTableManager("PropertyMaster", CommonObj.NoSqlConnectionString);
                string qry = "PropertyFor eq '" + propertyFor.ToString() + "'" +
                    " and IsActive eq true ";
                if (IsPrintable)
                {
                    qry += " and IsPrintable eq true ";
                }
                return TableManager.RetrieveEntity<CustomProperty>(qry);
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
                TableManager = new ExecuteTableManager("PropertyMaster", CommonObj.NoSqlConnectionString);
                string qry = "IsActive eq true ";                
                return TableManager.RetrieveEntity<CustomProperty>(qry);
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
                TableManager = new ExecuteTableManager("PropertyMaster", CommonObj.NoSqlConnectionString);
                string q = "RowKey eq '" + RowKey + "' " +
                    " and PropertyFor eq '" + propertyFor.ToString() + "'" +
                    " and IsActive eq true";
                return TableManager.RetrieveEntity<CustomProperty>(q).FirstOrDefault();
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
                    TableManager = new ExecuteTableManager("PropertyMaster", CommonObj.NoSqlConnectionString);
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

                DynamicTableEntity dynamicProperty = new DynamicTableEntity();
                List<CustomProperty> customProperties = GetMaster(propertyFor);
                EntityProperty Prop;

                if (customProperties == null || customProperties.Count == 0)
                    return;
                
                /* PartitionKey & RowKey */
                dynamicProperty.PartitionKey = "MyCompany";
                dynamicProperty.RowKey = GenericLogic.IstNow.TimeStamp().ToString();

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
                                Prop = new EntityProperty(formDictionary[dm.ColumnName]?.ToString());
                                dynamicProperty.Properties[dm.ColumnName] = Prop;
                            }                            
                        }
                        catch { }
                    }
                    if (dm.DataType == EdmType.Double.ToString())
                    {
                        try
                        {
                            Prop = new EntityProperty(Convert.ToDouble(formDictionary[dm.ColumnName]));
                            dynamicProperty.Properties[dm.ColumnName] = Prop;
                        }
                        catch { }
                    }
                    if (dm.DataType == EdmType.Boolean.ToString())
                    {
                        try
                        {
                            Prop = new EntityProperty(Convert.ToBoolean(formDictionary[dm.ColumnName]));
                            dynamicProperty.Properties[dm.ColumnName] = Prop;
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
                                Prop = new EntityProperty(PropDateValue);
                                dynamicProperty.Properties[dm.ColumnName] = Prop;
                            }                                
                        }
                        catch { }
                    }
                }

                /* Other Fields */
                Prop = new EntityProperty(CommonObj.RequestId);
                dynamicProperty.Properties["RequestId"] = Prop;                

                Prop = new EntityProperty(GenericLogic.IstNow);
                dynamicProperty.Properties["TransactionDate"] = Prop;

                Prop = new EntityProperty(true);
                dynamicProperty.Properties["IsActive"] = Prop;

                /* Scope Wise Fields */
                Prop = new EntityProperty(propertyFor.ToString());
                dynamicProperty.Properties["PropertyFor"] = Prop;
                if (Identity.HasValue)
                {
                    Prop = new EntityProperty(Identity.Value);
                    dynamicProperty.Properties["Identity"] = Prop;
                }
                if (!string.IsNullOrEmpty(IdentityValue))
                {
                    Prop = new EntityProperty(IdentityValue);
                    dynamicProperty.Properties["IdentityValue"] = Prop;
                }

                TableManager = new ExecuteTableManager("PropertyData", CommonObj.NoSqlConnectionString);
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
                if(customProperties != null && customProperties.Count > 0)
                {
                    TableManager = new ExecuteTableManager("PropertyData", CommonObj.NoSqlConnectionString);
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

                    DynamicTableEntity dynamicProperty = TableManager.RetrieveEntity(qry).FirstOrDefault();

                    foreach(CustomProperty customProperty in customProperties)
                    {
                        if (dynamicProperty != null && dynamicProperty.Properties.Count > 0)
                        {
                            foreach (KeyValuePair<string, EntityProperty> property in dynamicProperty.Properties)
                            {
                                if(customProperty.ColumnName == property.Key)
                                {
                                    if (property.Value.PropertyType == EdmType.String)
                                    {
                                        dictionary.Add(customProperty.LabelName, property.Value.PropertyAsObject.ToString());
                                    }
                                    if (property.Value.PropertyType == EdmType.Double)
                                    {
                                        dictionary.Add(customProperty.LabelName, ((double)property.Value.PropertyAsObject).ToDisplayDouble());
                                    }
                                    if (property.Value.PropertyType == EdmType.Boolean)
                                    {
                                        if ((bool)property.Value.PropertyAsObject)
                                            dictionary.Add(customProperty.LabelName, "Yes");
                                        else
                                            dictionary.Add(customProperty.LabelName, "No");
                                    }
                                    if (property.Value.PropertyType == EdmType.DateTime)
                                    {
                                        dictionary.Add(customProperty.LabelName, Convert.ToDateTime(property.Value.PropertyAsObject).ToDisplayShortDateString());
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
