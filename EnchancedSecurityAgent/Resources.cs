using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Data;

namespace EnchancedSecurityAgent
{
    public static class ResourceHelpers
    {
        public static string sqlStreamToText(string streamPath)
        {
            string commandText;
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream s = assembly.GetManifestResourceStream($"{streamPath}"))
            {
                using(StreamReader sr = new StreamReader(s)) 
                { 
                    commandText = sr.ReadToEnd();
                    return commandText;
                }
            }
        }

        public static DataTable ToDataTable<T>(this List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                tb.Columns.Add(prop.Name, prop.PropertyType);
            }

            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }

        public static DataTable Reporting()
        {
            DataTable dt = new DataTable();
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "UserArtifactId";
            dt.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "FirstName";
            dt.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "LastName";
            dt.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "EmailAddress";
            dt.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "Group(s)";
            dt.Columns.Add(dtColumn);
            return dt;
        }
    }
}
