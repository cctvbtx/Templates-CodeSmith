using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using CodeSmith.Core.Extensions;
using SchemaExplorer;

namespace SkydiverFL.Extensions.CodeSmith
{
    public static class Relationships
    {
        public const string SQL_SERVER_DEFAULT_OWNER = "dbo";

        public static TableSchema GetPrimaryTable(this ColumnSchema column)
        {
            if ( !column.Table.ForeignKeyColumns.Contains(column) ) { return null; }

            foreach (var tableKeySchema in column.Table.ForeignKeys)
            {
                if (tableKeySchema.ForeignKeyMemberColumns.Contains(column)) { return tableKeySchema.PrimaryKeyTable; }
            }

            return null;
        }

        public static ColumnSchema GetPrimaryColumn(this ColumnSchema column)
        {
            var table = GetPrimaryTable(column);

            return (table != null && table.PrimaryKey != null && table.PrimaryKey.MemberColumns != null &&
                    table.PrimaryKey.MemberColumns.Count == 1)
                ? table.PrimaryKey.MemberColumns[0]
                : null;
        }
        public static ColumnSchema GetPrimaryKeyColumn(this IndexSchema index)
        {
            return index.Table.HasPrimaryKey && index.Table.PrimaryKey.MemberColumns.Count == 1
                ? index.Table.PrimaryKey.MemberColumns[0]
                : null;
        }

        public static bool HasCompositePrimaryKey(this TableSchema table)
        {
            return table != null && table.HasPrimaryKey && table.PrimaryKey.MemberColumns.Count > 1;
        }
        public static bool HasSingularPrimaryKey(this TableSchema table)
        {
            return table != null && table.HasPrimaryKey && table.PrimaryKey.MemberColumns.Count == 1;
        }
        public static bool HasCompositePrimaryKey(this IndexSchema index)
        {
            return index.Table.HasCompositePrimaryKey();
        }
        public static bool HasSingularPrimaryKey(this IndexSchema index)
        {
            return index.Table.HasSingularPrimaryKey();
        }

        public static ColumnSchema GetSingularKeyColumn(this TableSchema table)
        {
            return table.HasSingularPrimaryKey()
                ? table.PrimaryKey.MemberColumns[0]
                : null;
        }

        public static string[] GetRelatedOwners(this TableSchema table, bool includeMyOwner = false)
        {
            var list = new List<string>();

            if (includeMyOwner) { list.Add(table.Owner); }

            foreach (var column in table.Columns.Where(x => x.IsForeignKeyMember))
            {
                var priTable = column.GetPrimaryTable();

                if (string.Equals(table.Owner, priTable.Owner, StringComparison.OrdinalIgnoreCase)) { continue; }

                if (list.FindIndex(x => x.Equals(priTable.Owner, StringComparison.OrdinalIgnoreCase)) >= 0) { continue; }

                list.Add(column.GetPrimaryTable().Owner);
            }

            return list.ToArray();
        }
        public static bool HasCrossSchemaRelationships(this TableSchema table)
        {
            return (table.GetRelatedOwners().Count(x => !x.Equals(table.Owner)) > 0);
        }
        public static bool HasDefaultOnwer(this TableSchema table)
        {
            return table.Owner.Equals(SQL_SERVER_DEFAULT_OWNER, StringComparison.OrdinalIgnoreCase);
        }

        public static bool HasForeignKeys(this TableSchema table, bool includeSelf = false)
        {
            return table.Columns.Where(x => x.IsForeignKeyMember)
                .Any(column => includeSelf || !column.GetPrimaryTable().Equals(table));
        }

        public static TableSchemaCollection GetChildren(this TableSchema table)
        {
            var list = new List<TableSchema>();

            foreach (var possibleChild in table.Database.Tables.Where(x => !x.Equals(table)))
            {
                foreach (var column in possibleChild.Columns)
                {
                    if (column.IsForeignKeyMember && column.GetPrimaryTable().Equals(table) && !list.Contains(possibleChild)) { list.Add(possibleChild); }
                }
            }

            return list.ToTableSchemaCollection();
        }
        public static bool HasChildren(this TableSchema table)
        {
            return (table.GetChildren().Count > 0);
        }

        public static TableSchemaCollection GetParents(this TableSchema table)
        {
            var list = new List<TableSchema>();

            foreach (var column in table.Columns.Where(x => x.IsForeignKeyMember && !x.GetPrimaryTable().Equals(table)))
            {
                if (!list.Contains(column.GetPrimaryTable()))
                {
                    list.Add(column.GetPrimaryTable());
                }
            }
            return list.ToTableSchemaCollection();
        }
        public static bool HasParents(this TableSchema table)
        {
            return (table.GetParents().Count > 0);
        }
        public static bool IsRoot(this TableSchema table)
        {
            return !table.HasParents();
        }

        public static TableSchemaCollection GetRoots(this DatabaseSchema database)
        {
            return database.Tables.GetRoots();
        }
        public static TableSchemaCollection GetRoots(this TableSchemaCollection tables)
        {
            return tables.Where(x => x.IsRoot()).ToList().ToTableSchemaCollection();
        }

        public static string[] GetOwners(this DatabaseSchema database)
        {
            var list = new List<string>();

            foreach (var table in database.Tables.OrderBy(x => x.Owner).Where(x => !x.HasDefaultOnwer()))
            {
                if (list.FindIndex(x => x.Equals(table.Owner, StringComparison.OrdinalIgnoreCase)) >= 0) { continue; }

                list.Add(table.Owner);
            }

            return list.ToArray();
        }
    }
}
