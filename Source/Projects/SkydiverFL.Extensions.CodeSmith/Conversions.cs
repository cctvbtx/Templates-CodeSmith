using System.Collections.Generic;
using System.Linq;
using SchemaExplorer;

namespace SkydiverFL.Extensions.CodeSmith
{
    public static class Conversions
    {
        public static ColumnSchemaCollection ToColumnSchemaCollection(this IEnumerable<ColumnSchema> columns)
        {
            return new ColumnSchemaCollection(columns.ToArray());
        }
        public static ColumnSchemaCollection ToColumnSchemaCollection(this IndexSchema index)
        {
            return index.MemberColumns.Select(column => column.Column).ToList().ToColumnSchemaCollection();
        }

        public static IndexSchemaCollection ToIndexSchemaCollection(this IEnumerable<IndexSchema> indices)
        {
            return new IndexSchemaCollection(indices.ToArray());
        }
        public static TableSchemaCollection ToTableSchemaCollection(this IEnumerable<TableSchema> tables)
        {
            return new TableSchemaCollection(tables.ToArray());
        }
    }
}
