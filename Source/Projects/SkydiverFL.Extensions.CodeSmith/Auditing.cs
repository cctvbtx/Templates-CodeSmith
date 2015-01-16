using System;
using System.Collections.Generic;
using System.Linq;
using SchemaExplorer;

namespace SkydiverFL.Extensions.CodeSmith
{
    public enum AuditColumnType
    {
        Unknown,

        DeleteFlag,
        VersionDate,
        VersionMember
    }

    public static class Autiting
    {
        public static readonly string[] AuditColumnDeleteFlagNames = { "AuditDeletedDate", "_k", "DateDeleted", "IsDeleted" };
        public static readonly string[] AuditColumnVersionMemberNames = { "AuditMemberUid", "_u" };
        public static readonly string[] AuditColumnVersionDateNames = { "AuditVersionDate", "_d", "AuditDateCreated" };
        public static readonly string[] AuditColumnNames;

        static Autiting()
        {
            var list = new List<string>();
            list.AddRange(AuditColumnDeleteFlagNames);
            list.AddRange(AuditColumnVersionMemberNames);
            list.AddRange(AuditColumnVersionDateNames);
            AuditColumnNames = list.ToArray();
        }

        public static AuditColumnType GetAuditColumnType(this ColumnSchema column)
        {
            if (column == null) { return AuditColumnType.Unknown; }

            if (AuditColumnDeleteFlagNames.ToList()
                .FindIndex(x => x.Equals(column.Name, StringComparison.OrdinalIgnoreCase)) >= 0)
            {
                return AuditColumnType.DeleteFlag;
            }

            if (AuditColumnVersionMemberNames.ToList()
                .FindIndex(x => x.Equals(column.Name, StringComparison.OrdinalIgnoreCase)) >= 0)
            {
                return AuditColumnType.VersionMember;
            }

            if (AuditColumnVersionDateNames.ToList()
                .FindIndex(x => x.Equals(column.Name, StringComparison.OrdinalIgnoreCase)) >= 0)
            {
                return AuditColumnType.VersionDate;
            }

            return AuditColumnType.Unknown;
        }

        public static bool IsAuditColumn(this ColumnSchema column)
        {
            return column.GetAuditColumnType() != AuditColumnType.Unknown;
        }

        public static ColumnSchemaCollection GetAuditColumns(this TableSchema table)
        {
            return table.Columns.GetAuditColumns();
        }
        public static ColumnSchemaCollection GetAuditColumns(this ColumnSchemaCollection columns)
        {
            return columns.Where(x => x.IsAuditColumn()).ToList().ToColumnSchemaCollection();
        }

        public static ColumnSchema GetAuditColumn(this TableSchema table, AuditColumnType auditColumnType)
        {
            return table.Columns.GetAuditColumn(auditColumnType);
        }
        public static ColumnSchema GetAuditColumn(this ColumnSchemaCollection columns, AuditColumnType auditColumnType)
        {
            return auditColumnType == AuditColumnType.Unknown
                ? null
                : columns.GetAuditColumns().SingleOrDefault(x => x.GetAuditColumnType() == auditColumnType);
        }

        public static bool ContainsAuditColumns(this TableSchema table)
        {
            return table.Columns.ContainsAuditColumns();
        }
        public static bool ContainsAuditColumns(this ColumnSchemaCollection columns)
        {
            return columns.GetAuditColumns().Count > 0;
        }

        public static ColumnSchema GetDeleteFlagColumn(this TableSchema table)
        {
            return table.Columns.SingleOrDefault(x => x.GetAuditColumnType() == AuditColumnType.DeleteFlag);
        }
        public static bool HasDeleteFlag(this TableSchema table)
        {
            return table.GetDeleteFlagColumn() != null;
        }
        public static bool IsDeleteFlag(this ColumnSchema column)
        {
            return
                AuditColumnDeleteFlagNames.ToList()
                    .FindIndex(x => x.Equals(column.Name, StringComparison.OrdinalIgnoreCase)) >= 0;
        }

        public static ColumnSchema GetVersionUserColumn(this TableSchema table)
        {
            return table.Columns.SingleOrDefault(x => x.GetAuditColumnType() == AuditColumnType.VersionMember);
        }
        public static bool HasVersionUserColumn(this TableSchema table)
        {
            return table.GetVersionUserColumn() != null;
        }
        public static bool IsVersionUserColumn(this ColumnSchema column)
        {
            return
                AuditColumnVersionMemberNames.ToList()
                    .FindIndex(x => x.Equals(column.Name, StringComparison.OrdinalIgnoreCase)) >= 0;
        }

        public static ColumnSchema GetVersionDateColumn(this TableSchema table)
        {
            return table.Columns.SingleOrDefault(x => x.GetAuditColumnType() == AuditColumnType.VersionDate);
        }
        public static bool HasVersionDateColumn(this TableSchema table)
        {
            return table.GetVersionDateColumn() != null;
        }
        public static bool IsVersionDateColumn(this ColumnSchema column)
        {
            return
                AuditColumnVersionDateNames.ToList()
                    .FindIndex(x => x.Equals(column.Name, StringComparison.OrdinalIgnoreCase)) >= 0;
        }
    }
}
