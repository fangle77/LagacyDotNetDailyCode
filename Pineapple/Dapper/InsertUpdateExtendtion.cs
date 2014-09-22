using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Dapper
{
    public static class InsertUpdateExtendtion
    {
        public static string GetInsertSql(this object model, params string[] ignoreFields)
        {
            HashSet<string> ignores = new HashSet<string>(ignoreFields ?? new string[0], StringComparer.OrdinalIgnoreCase);
            var properties = GetProperties(model);

            StringBuilder fileds = new StringBuilder(properties.Count() * 16);
            StringBuilder values = new StringBuilder();

            foreach (var property in properties)
            {
                if (!property.CanRead) continue;
                if (ignores.Contains(property.Name)) continue;

                fileds.AppendFormat("{0},", property.Name);
                values.AppendFormat("@{0},", property.Name);
            }
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("INSERT INTO {0}", model.GetType().Name);
            sql.AppendFormat("({0}) ", fileds.Remove(fileds.Length - 1, 1));
            sql.AppendFormat(" Values({0}); ", values.Remove(values.Length - 1, 1));
            return sql.ToString();
        }

        public static string GetUpdateSql(this object model, string keyField, params string[] ignoreFields)
        {
            HashSet<string> ignores = new HashSet<string>(ignoreFields ?? new string[0], StringComparer.OrdinalIgnoreCase);

            var properties = GetProperties(model);
            //update xx set a=@a,b=@b where c=@c
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("UPDATE {0} SET ", model.GetType().Name);
            foreach (var propertyInfo in properties)
            {
                if (!propertyInfo.CanRead || !propertyInfo.CanWrite) continue;
                if (ignores.Contains(propertyInfo.Name)) continue;
                
                if (propertyInfo.GetValue(model, BindingFlags.Default, null, null, null) == null) continue;

                sql.AppendFormat("{0} = @{0}", propertyInfo.Name);
            }

            sql.AppendFormat("where {0} = @{0}; ", keyField);
            return sql.ToString();
        }

        private static PropertyInfo[] GetProperties(object model)
        {
            Type type = model.GetType();
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }
    }
}
