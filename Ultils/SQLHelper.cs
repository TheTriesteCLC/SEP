using System;
using System.Collections.Generic;
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

    }
}
