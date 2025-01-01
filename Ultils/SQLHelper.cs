using SEP.CustomClassBuilder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEP.Ultils
{
    internal class SQLHelper
    {
        public static string ConvertTypeToSql(Type type)
        {
            return type switch
            {
                var t when t == typeof(int) => "INT",
                var t when t == typeof(string) => $"NVARCHAR({Constants.stringMaxLength})",
                var t when t == typeof(double) => "FLOAT",
                var t when t == typeof(bool) => "BIT",
                var t when t == typeof(DateTime) => "DATETIME",
                var t when t == typeof(long) => "BIGINT",
                var t when t == typeof(decimal) => "DECIMAL(18,2)",
                _ => throw new NotSupportedException($"Type {type.Name} is not supported."),
            };
        }
        public static Type ConvertSqlToType(string sql)
        {
            var sqlType = sql.ToUpper().Trim();

            return sqlType switch
            {
                var t when t.StartsWith("NVARCHAR") || t.StartsWith("VARCHAR") => typeof(string),
                "INT" => typeof(int),
                "FLOAT" => typeof(double),
                "BIT" => typeof(bool),
                "DATETIME" => typeof(DateTime),
                "BIGINT" => typeof(long),
                "DECIMAL" => typeof(decimal),
                _ => throw new NotSupportedException($"SQL type {sql} is not supported.")
            };
        }
        public static string updateStringBuilder(string tableName, CustomClass updateDocumentObject, string primaryColumn, string _id)
        {
            var updateQueryBuilder = $"UPDATE [{tableName}] SET ";

            var parameters = new List<string>();

            foreach (var property in updateDocumentObject.properties)
            {
                var propertyName = property.PropertyName;
                var type = property.PropertyType;
                var propertyValue = updateDocumentObject.getProp(propertyName);
                if (type == typeof(string))
                {
                    parameters.Add($"[{propertyName}] = \'{propertyValue}\'"); // String must be inside double quotes
                }
                else if (type == typeof(bool))
                {
                    parameters.Add($"[{propertyName}] = {(propertyValue.ToString().ToLower() == "true" ? 1 : 0)}"); // Boolean to 1/0
                }
                else if (type == typeof(DateTime))
                {
                    parameters.Add($"[{propertyName}] = '{((DateTime)propertyValue).ToString("yyyy-MM-dd HH:mm:ss")}'"); // DateTime formatted
                }
                else
                {
                    parameters.Add($"[{propertyName}] = {propertyValue}");
                }
            }
            return updateQueryBuilder +
                $"{string.Join(", ", parameters)}" +
                $" WHERE {primaryColumn} = {_id}";
        }
    }
}
