using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using SchemaExplorer;
using SkydiverFL.Extensions.CodeSmith.Helpers;

// ReSharper disable once CheckNamespace
namespace SkydiverFL.Extensions.CodeSmith.Languages.JSON
{
    public static class JsonExtensions
    {
        private const string DATE_FORMAT = "yyyyMMddHHmmss";

        //public static string ToJsonNameArray(this ColumnSchemaCollection columns)
        //{

        //    var text = string.Empty;
        //    foreach (var column in columns)
        //    {
        //        if (text.Length > 0) { text += ","; }
        //        text += string.Format("\"{0}\"", column.Name);
        //    }
        //    return text;
        //}
        //public static string ToJsonNameArray(this TableSchemaCollection tables)
        //{
        //    var text = string.Empty;
        //    foreach (var table in tables)
        //    {
        //        if (text.Length > 0) { text += ","; }
        //        text += string.Format("\"{0}\"", table.Name);
        //    }
        //    return text;
        //}
        //public static string ToJsonNameArray(this MemberColumnSchemaCollection columns)
        //{
        //    var text = string.Empty;
        //    foreach (var column in columns)
        //    {
        //        if (text.Length > 0) { text += ","; }
        //        text += string.Format("\"{0}\"", column.Name);
        //    }
        //    return text;
        //}
        private static string[] ToNameArray(this ColumnSchemaCollection columns)
        {
            return columns.Select(column => column.Name).ToArray();
        }
        private static string[] ToNameArray(this TableSchemaCollection tables)
        {
            return tables.Select(table => table.Name).ToArray();
        }
        private static string[] ToNameArray(this MemberColumnSchemaCollection columns)
        {
            return columns.Select(column => column.Name).ToArray();
        }

        public static object ToSchemaObject(this ColumnSchema column)
        {
            return new
            {
                name = column.Name, 
                isNullable = column.AllowDBNull,
                desc = column.Description,
                fullName = column.FullName,
                isFk = column.IsForeignKeyMember,
                isPk = column.IsPrimaryKeyMember,
                isUnique = column.IsUnique,
                parent = (column.Parent == null ? string.Empty : column.Parent.Name),
                precision = column.Precision,
                scale = column.Scale,
                size = column.Size,
                sortName = column.SortName,
                systemType = column.SystemType.Name,
                table = column.Table.Name,
                auditType = column.GetAuditColumnType(),
                defaultValue = column.GetDefaultValue(),
                pkColumn = (column.GetPrimaryColumn() == null ? string.Empty : column.GetPrimaryColumn().Name),
                pkTable = (column.GetPrimaryTable() == null ? string.Empty : column.GetPrimaryTable().Name),
                hasDefault = column.HasDefaultValue(),
                isAudit = column.IsAuditColumn(),
                isDelete = column.IsDeleteFlag(),
                isUid = column.IsIdentifierColumn(),
                isId = column.IsIdentityColumn(),
                isSingularUnique = column.IsSingularUnique(),
                isVersionDate = column.IsVersionDateColumn(),
                isVersionUser = column.IsVersionUserColumn()
            };
        }
        public static object[] ToSchemaObject(this ColumnSchemaCollection columns)
        {
            return columns.Select(column => column.ToSchemaObject()).ToArray();
        }
        public static object ToSchemaObject(this IndexSchema index)
        {
            return new
            {
                name = index.Name,
                desc = index.Description,
                fullName = index.FullName,
                isClustered = index.IsClustered,
                isPk = index.IsPrimaryKey,
                isUnique = index.IsUnique,
                sortName = index.SortName,
                table = index.Table.Name,
                members = index.MemberColumns.ToNameArray()
            };
        }
        public static object[] ToSchemaObject(this IndexSchemaCollection indices)
        {
            return indices.Select(index => index.ToSchemaObject()).ToArray();
        }
        public static object ToSchemaObject(this TableSchema table)
        {
            var columns = table.Columns.ToSchemaObject();
            var indicies = table.Indexes.ToSchemaObject();

            var columnItem = new
            {
                count = columns.Length,
                items = columns
            };

            var indicesItem = new
            {
                count = indicies.Length,
                items = indicies
            };

            return new
            {
                name = table.Name,
                isRoot = table.IsRoot(),
                parents = table.GetParents().ToNameArray(),
                desc = table.Description,
                fullName = table.FullName,
                hasPk = table.HasPrimaryKey,
                owner = table.Owner,
                sortName = table.SortName,
                delFlag = (table.GetDeleteFlagColumn() == null ? string.Empty : table.GetDeleteFlagColumn().Name),
                singularUidColumn = (table.GetSingularIdentifierColumn() == null ? string.Empty : table.GetSingularIdentifierColumn().Name),
                singularPkColumn = (table.GetSingularKeyColumn() == null ? string.Empty : table.GetSingularKeyColumn().Name),
                versionDateColumn = (table.GetVersionDateColumn() == null ? string.Empty : table.GetVersionDateColumn().Name),
                versionUserColumn = (table.GetVersionUserColumn() == null ? string.Empty : table.GetVersionUserColumn().Name),
                hasCompositeKey = table.HasCompositePrimaryKey(),
                hasCrossSchemaRelationships = table.HasCrossSchemaRelationships(),
                hasDefaultOwner = table.HasDefaultOnwer(),
                hasDeleteFlag = table.HasDeleteFlag(),
                hasForeignKeys = table.HasForeignKeys(),
                hasUidColumns = table.HasIdentifierColumns(),
                hasMultipleUidColumns = table.HasMultipleIdentifierColumns(),
                hasSingleUidColumn = table.HasSingleIdentifierColumn(),
                hasSingularKey = table.HasSingularPrimaryKey(),
                hasUniqueIndicies = table.HasUniqueIndices(),
                hasVersionDate = table.HasVersionDateColumn(),
                hasVersionUser = table.HasVersionUserColumn(),
                columns = columnItem,
                indices = indicesItem,
                auditColumns = table.GetAuditColumns().ToNameArray(),
                children = table.GetChildren().ToNameArray(),
                uidColumns = table.GetIdentifierColumns().ToNameArray(),
                fkColumns = table.ForeignKeyColumns.ToNameArray(),
                nonFkColumns = table.NonForeignKeyColumns.ToNameArray(),
                nonKeyColumns = table.NonKeyColumns.ToNameArray(),
                nonPkColumns = table.NonPrimaryKeyColumns.ToNameArray(),
            };

        }
        public static object[] ToSchemaObject(this TableSchemaCollection tables)
        {
            return tables.Select(table => table.ToSchemaObject()).ToArray();
        }
        public static object ToSchemaObject(this DatabaseSchema database)
        {
            var tables = database.Tables.ToSchemaObject();
            var tablesItem = new
            {
                count = tables.Length,
                items = tables
            };

            return new
            {
                name = database.Name,
                roots = database.GetRoots().ToNameArray(),
                tables = tablesItem
            };
        }

        public static string ToJsonSchema(this ColumnSchema column)
        {
            return new JSonPresentationFormatter().Format(column.ToSchemaObject());
        }
        public static string ToJsonSchema(this IndexSchema index)
        {
            return new JSonPresentationFormatter().Format(index.ToSchemaObject());
        }
        public static string ToJsonSchema(this TableSchema table)
        {
            return new JSonPresentationFormatter().Format(table.ToSchemaObject());
        }
        public static string ToJsonSchema(this DatabaseSchema database)
        {
            var json = (new JSonPresentationFormatter().Format(database.ToSchemaObject()));
            var source = "\"name\":\"" + database.Name + "\"";
            var target = "\"gen_date\":\"" + DateTime.UtcNow.ToString(DATE_FORMAT) + "\"," + source;

            return (new JSonPresentationFormatter().Format(json.Replace(source, target)));
        }
    }
}
