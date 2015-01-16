using System.Collections.Generic;
using System.Linq;
using CodeSmith.Core.Extensions;
using SchemaExplorer;

namespace SkydiverFL.Extensions.CodeSmith
{
    public static class Contraints
    {
        public static IndexSchemaCollection GetIndices(this ColumnSchema column)
        {
            var list = column.Table.Indexes.Where(index => index.MemberColumns.Contains(column)).ToList();

            return list.ToIndexSchemaCollection();
        }

        public static bool IsSingularUnique(this ColumnSchema column)
        {
            foreach (var index in column.GetIndices())
            {
                if (index.IsPrimaryKey) { continue; }
                if (!index.IsUnique) { continue; }
                if (index.MemberColumns.Count != 1) { continue; }

                return true;
            }

            return false;
        }

        public static IndexSchemaCollection GetUniqueIndicies(this TableSchema table, bool includePrimaryKey = false, bool includeIdentifiers = false)
        {
            var list = new List<IndexSchema>();

            foreach (var index in table.Indexes)
            {
                if (!index.IsUnique) { continue; }
                if (!includePrimaryKey && index.IsPrimaryKey) { continue; }
                if (!includeIdentifiers && index.MemberColumns.Count == 1 && index.MemberColumns[0].Column.IsIdentifierColumn()) { continue; }

                list.Add(index);
            }

            return list.ToIndexSchemaCollection();
        }
        public static bool HasUniqueIndices(this TableSchema table, bool includePrimaryKey = false, bool includeIdentifiers = false)
        {
            return (table.GetUniqueIndicies(includePrimaryKey, includeIdentifiers).Count > 0);
        }

        public static string CreateUniqueName(this IndexSchema index, bool includeAuditColumns = false)
        {
            var result = string.Empty;

            var cols = includeAuditColumns
                ? index.MemberColumns.ToList()
                : index.MemberColumns.Where(x => !x.Column.IsAuditColumn()).ToList();

            foreach (var col in cols) { result += col.Name.ToPascalCase(); }

            return result;
        }
    }
}
