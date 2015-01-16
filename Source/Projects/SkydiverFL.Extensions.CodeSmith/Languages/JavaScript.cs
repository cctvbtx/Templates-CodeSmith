using System.Linq;
using CodeSmith.Core.Extensions;
using SchemaExplorer;

// ReSharper disable once CheckNamespace
namespace SkydiverFL.Extensions.CodeSmith.Languages.JavaScript
{
    public static class JavaScriptExtensions
    {
        public static string ToParamsArray(this IndexSchema index, bool includeDeleteFlag = false)
        {
            var result = string.Empty;

            foreach (var memberColumn in index.MemberColumns.Where(x => !x.Column.IsAuditColumn()))
            {
                if ( result.Length > 0 ) { result += ", "; }

                result += memberColumn.Column.Name.ToCamelCase();
            }

            if ( includeDeleteFlag && index.Table.HasDeleteFlag() ) { result += ", includeDeleted"; }

            return result;
        }

        public static string ToParamsArray(this ColumnSchemaCollection columns, bool includeDeleteFlag = false)
        {
            var result = string.Empty;
            var hasDeleteFlag = false;

            foreach (var column in columns)
            {
                hasDeleteFlag = column.Table.HasDeleteFlag();

                if (result.Length > 0) { result += ", "; }

                result += column.Name.ToCamelCase();
            }

            if (includeDeleteFlag && hasDeleteFlag) { result += ", includeDeleted"; }

            return result;
        }
    }
}
