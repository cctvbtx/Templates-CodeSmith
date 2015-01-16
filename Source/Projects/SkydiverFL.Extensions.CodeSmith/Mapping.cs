using System.Collections.Generic;
using SchemaExplorer;

namespace SkydiverFL.Extensions.CodeSmith
{
    public enum MapType
    {
        SqlToCSharp,
        SqlToCSharpNullable,
        SqlToCSharpNullableSymbol,
    }

    public static class Mapping
    {
        public static string Map(this string key, MapType mapType)
        {
            switch (mapType)
            {
                case MapType.SqlToCSharp:
                    return SqlToCSharpMap.Values.ContainsKey(key) ? SqlToCSharpMap.Values[key] : string.Empty;
                case MapType.SqlToCSharpNullable:
                    return SqlToCSharpNullableMap.Values.ContainsKey(key) ? SqlToCSharpNullableMap.Values[key] : string.Empty;
                case MapType.SqlToCSharpNullableSymbol:
                    return SqlToCSharpNullableSymbolMap.Values.ContainsKey(key) ? SqlToCSharpNullableSymbolMap.Values[key] : string.Empty;
                default:
                    return key;
            }
        }

        public static string SqlToCSharp(this ColumnSchema column)
        {
            return column.NativeType.Map(MapType.SqlToCSharp);
        }
        public static string SqlToCSharpNullable(this ColumnSchema column)
        {
            return column.NativeType.Map(MapType.SqlToCSharp);
        }
        public static string SqlToCSharpNullableSymbol(this ColumnSchema column)
        {
            return column.NativeType.Map(MapType.SqlToCSharpNullableSymbol);
        }
    }

    internal class SqlToCSharpMap
    {
        internal static Dictionary<string, string> Values = new Dictionary<string, string>();

        static SqlToCSharpMap()
        {
            Values.Add("bigint", "long");
            Values.Add("binary", "object");
            Values.Add("bit", "bool");
            Values.Add("char", "string");
            Values.Add("datetime", "DateTime");
            Values.Add("decimal", "decimal");
            Values.Add("float", "double");
            Values.Add("image", "byte[]");
            Values.Add("int", "int");
            Values.Add("money", "decimal");
            Values.Add("nchar", "string");
            Values.Add("ntext", "string");
            Values.Add("numeric", "decimal");
            Values.Add("nvarchar", "string");
            Values.Add("real", "float");
            Values.Add("smalldatetime", "DateTime");
            Values.Add("smallint", "short");
            Values.Add("smallmoney", "decimal");
            Values.Add("text", "string");
            Values.Add("timestamp", "byte[]");
            Values.Add("tinyint", "byte");
            Values.Add("uniqueidentifier", "Guid");
            Values.Add("varbinary", "byte[]");
            Values.Add("varchar", "string");
            Values.Add("xml", "string");
            Values.Add("sql_variant", "object");
            Values.Add("datetime2", "DateTime");
            Values.Add("datetimeoffset", "DateTimeOffset");
            Values.Add("time", "TimeSpan");
            Values.Add("date", "DateTime");
            Values.Add("rowversion", "byte[]");
        }
    }

    internal class SqlToCSharpNullableMap
    {
        internal static Dictionary<string, string> Values = new Dictionary<string, string>();

        static SqlToCSharpNullableMap()
        {
            Values.Add("bigint", "long?");
            Values.Add("binary", "object");
            Values.Add("bit", "bool?");
            Values.Add("char", "string");
            Values.Add("datetime", "DateTime?");
            Values.Add("decimal", "decimal?");
            Values.Add("float", "double?");
            Values.Add("image", "byte[]");
            Values.Add("int", "int?");
            Values.Add("money", "decimal?");
            Values.Add("nchar", "string");
            Values.Add("ntext", "string");
            Values.Add("numeric", "decimal?");
            Values.Add("nvarchar", "string");
            Values.Add("real", "float?");
            Values.Add("smalldatetime", "DateTime?");
            Values.Add("smallint", "short?");
            Values.Add("smallmoney", "decimal?");
            Values.Add("text", "string");
            Values.Add("timestamp", "byte[]");
            Values.Add("tinyint", "byte");
            Values.Add("uniqueidentifier", "Guid?");
            Values.Add("varbinary", "byte[]");
            Values.Add("varchar", "string");
            Values.Add("xml", "string");
            Values.Add("sql_variant", "object");
            Values.Add("datetime2", "DateTime?");
            Values.Add("datetimeoffset", "DateTimeOffset?");
            Values.Add("time", "TimeSpan?");
            Values.Add("date", "DateTime?");
            Values.Add("rowversion", "byte[]");
        }
    }
    internal class SqlToCSharpNullableSymbolMap
    {
        internal static Dictionary<string, string> Values = new Dictionary<string, string>();

        static SqlToCSharpNullableSymbolMap()
        {
            Values.Add("bigint", "?");
            Values.Add("binary", "");
            Values.Add("bit", "?");
            Values.Add("char", "");
            Values.Add("datetime", "?");
            Values.Add("decimal", "?");
            Values.Add("float", "?");
            Values.Add("image", "");
            Values.Add("int", "?");
            Values.Add("money", "?");
            Values.Add("nchar", "");
            Values.Add("ntext", "");
            Values.Add("numeric", "?");
            Values.Add("nvarchar", "");
            Values.Add("real", "?");
            Values.Add("smalldatetime", "?");
            Values.Add("smallint", "?");
            Values.Add("smallmoney", "?");
            Values.Add("text", "");
            Values.Add("timestamp", "");
            Values.Add("tinyint", "");
            Values.Add("uniqueidentifier", "?");
            Values.Add("varbinary", "");
            Values.Add("varchar", "");
            Values.Add("xml", "");
            Values.Add("sql_variant", "");
            Values.Add("datetime2", "?");
            Values.Add("datetimeoffset", "?");
            Values.Add("time", "?");
            Values.Add("date", "?");
            Values.Add("rowversion", "");
        }
    }
}
