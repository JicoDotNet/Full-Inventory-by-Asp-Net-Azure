namespace System.Data
{
    using Collections.Generic;
    using Linq;
    using Reflection;
    public static class DataExtension
    {
        public static DataTable ToDataTable<T>(this IList<T> items)
        {
            return ToDataTable(items.ToList());
        }

        public static DataTable ToDataTable<T>(this List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties by using reflection   
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            for (int i = 0; i < props.Length; i++)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(props[i].Name);

                //Setting column type as Property Type
                if (props[i].PropertyType.IsGenericType &&
                    props[i].PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    // Nullable Property
                    dataTable.Columns[i].DataType = props[i].PropertyType.GetGenericArguments()[0];
                }
                else
                {
                    dataTable.Columns[i].DataType = props[i].PropertyType;
                }
            }
            foreach (T item in items)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        public static List<T> ToList<T>(this DataTable dt) where T : new()
        {
            List<T> data = new List<T>();
            if (dt == null)
                return null;
            if (dt.Rows.Count == 0)
                return data;

            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public static T FirstOrDefault<T>(this DataTable dt) where T : new()
        {
            if (dt == null)
                return default;
            if (dt.Rows.Count == 0)
                return default;

            return GetItem<T>(dt.Rows[0]);
        }
        public static T FirstOrDefault<T>(this DataRow dr) where T : new()
        {
            if (dr == null)
                return default;
            return GetItem<T>(dr);
        }
        private static T GetItem<T>(DataRow dr) where T : new()
        {
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                PropertyInfo pro = typeof(T).GetProperties().FirstOrDefault(a => a.Name == column.ColumnName);
                if (pro != null && pro.Name == column.ColumnName)
                {
                    if (dr[column.ColumnName] is DBNull)
                    {
                        pro.SetValue(obj, null, null);
                    }
                    else
                    {
                        pro.SetValue(obj,
                            (dr[column.ColumnName] == null) ?
                                null :
                                Convert.ChangeType(dr[column.ColumnName],
                                    Nullable.GetUnderlyingType(pro.PropertyType) ?? pro.PropertyType),
                            null);
                    }
                }
            }
            return obj;
        }
    }
}

//namespace System.Collections.Generic
//{
//    using Reflection;
//    using System.Data;
//    using System.Linq;
//    public static class Extension
//    {
//        public static DataTable ToDataTable<T>(this List<T> items)
//        {
//            DataTable dataTable = new DataTable(typeof(T).Name);
//            //Get all the properties by using reflection   
//            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
//            for (int i = 0; i < Props.Count(); i++)
//            {
//                dataTable.Columns.Add(Props[i].Name);
//                dataTable.Columns[i].DataType = Props[i].PropertyType;
//            }
//            foreach (PropertyInfo prop in Props)
//            {
//                //Setting column names as Property names  

//            }
//            foreach (T item in items)
//            {
//                var values = new object[Props.Length];
//                for (int i = 0; i < Props.Length; i++)
//                {
//                    values[i] = Props[i].GetValue(item, null);
//                }
//                dataTable.Rows.Add(values);
//            }

//            return dataTable;
//        }
//    }
//}