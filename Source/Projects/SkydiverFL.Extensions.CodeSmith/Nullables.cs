using System.Linq;
using SchemaExplorer;

namespace SkydiverFL.Extensions.CodeSmith
{
    public static class Nullables
    {
        public static ColumnSchemaCollection GetNullableColumns(this IndexSchema index, bool includeAuditColumns = false)
        {
            return index.ToColumnSchemaCollection().GetNullableColumns(includeAuditColumns);
        }

        public static ColumnSchemaCollection GetNullableColumns(this ColumnSchemaCollection columns,
            bool includeAuditColumns = false)
        {
            var list = includeAuditColumns
                ? columns.Where(x => x.AllowDBNull).ToList()
                : columns.Where(x => x.AllowDBNull && !x.IsAuditColumn()).ToList();

            return list.ToColumnSchemaCollection();
        }

        public static ColumnSchemaCollection GetNonNullableColumns(this IndexSchema index,
            bool includeAuditColumns = false)
        {
            return index.ToColumnSchemaCollection().GetNonNullableColumns(includeAuditColumns);
        }

        public static ColumnSchemaCollection GetNonNullableColumns(this ColumnSchemaCollection columns,
            bool includeAuditColumns = false)
        {
            var list = includeAuditColumns
                ? columns.Where(x => !x.AllowDBNull).ToList()
                : columns.Where(x => !x.AllowDBNull && !x.IsAuditColumn()).ToList();

            return list.ToColumnSchemaCollection();
        }

        public static bool HasNullableColumns(this IndexSchema index, bool includeAuditColumns = false)
        {
            return index.ToColumnSchemaCollection().HasNullableColumns(includeAuditColumns);
        }

        public static bool HasNullableColumns(this ColumnSchemaCollection columns, bool includeAuditColumns = false)
        {
            return columns.GetNullableColumns(includeAuditColumns).Count > 0;
        }

        public static bool HasNonNullableColumns(this IndexSchema index, bool includeAuditColumns = false)
        {
            return index.ToColumnSchemaCollection().HasNonNullableColumns(includeAuditColumns);
        }

        public static bool HasNonNullableColumns(this ColumnSchemaCollection columns, bool includeAuditColumns = false)
        {
            return columns.GetNonNullableColumns(includeAuditColumns).Count > 0;
        }
    }
}
