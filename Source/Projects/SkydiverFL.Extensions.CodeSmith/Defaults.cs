using SchemaExplorer;

namespace SkydiverFL.Extensions.CodeSmith
{
    public static class Defaults
    {
        public const string SQL_SERVER_EXTENDED_PROPERTY_DEFAULT = "CS_Default";

        public static string GetDefaultValue(this ColumnSchema column)
        {
            return (column.ExtendedProperties.Contains(SQL_SERVER_EXTENDED_PROPERTY_DEFAULT) && !string.IsNullOrWhiteSpace(column.ExtendedProperties[SQL_SERVER_EXTENDED_PROPERTY_DEFAULT].Value.ToString()))
                ? column.ExtendedProperties[SQL_SERVER_EXTENDED_PROPERTY_DEFAULT].Value.ToString()
                : null;
        }

        public static bool HasDefaultValue(this ColumnSchema column)
        {
            return column.GetDefaultValue() != null;
        }
    }
}
