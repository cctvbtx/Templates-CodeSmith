using System;
using System.Linq;
using SchemaExplorer;

namespace SkydiverFL.Extensions.CodeSmith
{
    public static class Identification
    {
        public const string SQL_SERVER_EXTENDED_PROPERTY_IDENTITY = "CS_IsIdentity";

        public static readonly string[] IdentifierColumnNames = { "Identifier", "PublicIdentifier", "PublicUid", "uid" };

        public static string SqlServerIdentityValue(this ColumnSchema column)
        {
            return column.ExtendedProperties.Contains(SQL_SERVER_EXTENDED_PROPERTY_IDENTITY) && !string.IsNullOrWhiteSpace(column.ExtendedProperties[SQL_SERVER_EXTENDED_PROPERTY_IDENTITY].Value.ToString())
                ? column.ExtendedProperties[SQL_SERVER_EXTENDED_PROPERTY_IDENTITY].Value.ToString()
                : null;
        }

        public static bool IsIdentityColumn(this ColumnSchema column)
        {
            return (column.SqlServerIdentityValue() != null &&
                    column.SqlServerIdentityValue().Equals("true", StringComparison.OrdinalIgnoreCase));
        }
        public static bool IsIdentifierColumn(this ColumnSchema column)
        {
            return
                IdentifierColumnNames.ToList().FindIndex(x =>
                    x.Equals(column.Name, StringComparison.OrdinalIgnoreCase)) >= 0;
        }

        public static ColumnSchemaCollection GetIdentityColumns(this TableSchema table)
        {
            return table.Columns.GetIdentityColumns();
        }
        public static ColumnSchemaCollection GetIdentityColumns(this ColumnSchemaCollection columns)
        {
            return columns.Where(x => x.IsIdentityColumn()).ToList().ToColumnSchemaCollection();
        }

        public static ColumnSchemaCollection GetIdentifierColumns(this TableSchema table)
        {
            return table.Columns.GetIdentifierColumns();
        }
        public static ColumnSchemaCollection GetIdentifierColumns(this ColumnSchemaCollection columns)
        {
            var list = columns.Where(x => x.IsIdentifierColumn()).ToList();

            return list.ToColumnSchemaCollection();
        }

        public static ColumnSchema GetSingularIdentifierColumn(this TableSchema table)
        {
            return table.HasSingleIdentifierColumn()
                ? table.GetIdentifierColumns()[0]
                : null;
        }
        public static ColumnSchema GetSingularIdentifierColumn(this ColumnSchemaCollection columns)
        {
            return columns.HasSingleIdentifierColumn()
                ? columns.GetIdentifierColumns()[0]
                : null;
        }

        public static bool HasIdentifierColumns(this TableSchema table)
        {
            return table.GetIdentifierColumns().Count > 0;
        }
        public static bool HasMultipleIdentifierColumns(this TableSchema table)
        {
            return table.GetIdentifierColumns().Count > 1;
        }
        public static bool HasSingleIdentifierColumn(this TableSchema table)
        {
            return table.GetIdentifierColumns().Count == 1;
        }

        public static bool HasIdentifierColumns(this ColumnSchemaCollection table)
        {
            return table.GetIdentifierColumns().Count > 0;
        }
        public static bool HasMultipleIdentifierColumns(this ColumnSchemaCollection table)
        {
            return table.GetIdentifierColumns().Count > 1;
        }
        public static bool HasSingleIdentifierColumn(this ColumnSchemaCollection table)
        {
            return table.GetIdentifierColumns().Count == 1;
        }
    }
}
