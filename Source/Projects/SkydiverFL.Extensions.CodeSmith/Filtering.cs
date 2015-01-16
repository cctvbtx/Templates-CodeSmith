using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SchemaExplorer;
using SkydiverFL.Extensions.CodeSmith.Enums;

namespace SkydiverFL.Extensions.CodeSmith
{
    [Flags]
    public enum FilterRules
    {
        IsPrimaryKey,
        IsIdentity,
        IsIdentifier,

        IsDeleteFlag,
        IsVersionDate,
        IsVersionUser,

        HasDefaultValue,
    }

    public enum FilterDirection
    {
        Include,
        Exclude,
    }

    public class FilterRuleSet
    {
        public FilterRules Rules { get; set; }
        public FilterDirection Direction { get; set; }
    }

    public enum FilterMode
    {
        Create,
        Select,
        Update,
        ToDto,
    }
        
    public static class Filtering
    {
        public static ColumnSchemaCollection Filter(this ColumnSchemaCollection columns, FilterRules rules, FilterDirection direction)
        {
            var matches = (from column in columns
                            where !column.IsPrimaryKeyMember || !rules.HasFlag(FilterRules.IsPrimaryKey)
                            where !column.IsIdentityColumn() || !rules.HasFlag(FilterRules.IsIdentity)
                            where !column.IsIdentifierColumn() || !rules.HasFlag(FilterRules.IsIdentifier)
                            where !column.IsDeleteFlag() || !rules.HasFlag(FilterRules.IsDeleteFlag)
                            where !column.IsVersionDateColumn() || !rules.HasFlag(FilterRules.IsDeleteFlag)
                            where !column.IsVersionUserColumn() || !rules.HasFlag(FilterRules.IsVersionUser)
                            where !column.HasDefaultValue() || !rules.HasFlag(FilterRules.HasDefaultValue)
                            select column).ToList();

            return (direction == FilterDirection.Include)
                ? matches.ToColumnSchemaCollection()
                : columns.Where(column => !matches.Contains(column)).ToList().ToColumnSchemaCollection();
        }
        public static ColumnSchemaCollection Filter(this ColumnSchemaCollection columns, FilterRuleSet ruleSet)
        {
            if (ruleSet == null) { throw new Exception("Rules not set"); }

            return columns.Filter(ruleSet.Rules, ruleSet.Direction);
        }

        public static ColumnSchemaCollection Filter(this ColumnSchemaCollection columns, FilterRuleSet pkRules, FilterRuleSet generalRules, FilterRuleSet auditRules)
        {
            if (pkRules == null) { throw new Exception("Primary key rules not set"); }
            if (generalRules == null) { throw new Exception("General column rules not set"); }
            if (auditRules == null) { throw new Exception("Audit column rules not set"); }

            var list = new List<ColumnSchema>();

            // IsPrimaryKeyMember & IsAuditColumn always checked to decrease the chance of naming conflicts

            foreach (var column in columns.Where(x => x.IsPrimaryKeyMember && !x.IsAuditColumn()).ToList().ToColumnSchemaCollection().Filter(pkRules)) 
            {
                if (list.Contains(column)) { throw new Exception("Duplicate column detected while adding primary key columns."); }

                list.Add(column);
            }

            foreach (var column in columns.Where(x => !x.IsPrimaryKeyMember && !x.IsAuditColumn()).ToList().ToColumnSchemaCollection().Filter(generalRules))
            {
                if (list.Contains(column)) { throw new Exception("Duplicate column detected while adding general columns."); }

                list.Add(column);
            }

            foreach (var column in columns.Where(x => !x.IsPrimaryKeyMember && x.IsAuditColumn()).ToList().ToColumnSchemaCollection().Filter(generalRules))
            {
                if (list.Contains(column)) { throw new Exception("Duplicate column detected while adding audit columns."); }

                list.Add(column);
            }

            return list.ToColumnSchemaCollection();
        }

        public static ColumnSchemaCollection Filter(this ColumnSchemaCollection columns, FilterMode mode)
        {
            switch (mode)
            {
                case FilterMode.Create:
                    return columns.Where(x =>
                        !x.IsDeleteFlag() && // Handled by repo ... cannot be supplied
                        !x.IsVersionDateColumn() && // Handled by repo ... cannot be supplied
                        !x.IsIdentityColumn() && // Handled by database ... cannot be supplied
                        !((x.IsPrimaryKeyMember || x.IsIdentifierColumn()) &&
                            x.NativeType.Equals(NativeTypes.UNIQUEIDENTIFIER.ToString(), StringComparison.OrdinalIgnoreCase)) // Handled by repo ... cannot be supplied
                        ).ToList().ToColumnSchemaCollection();
                case FilterMode.Select:
                case FilterMode.Update:
                    return columns.Where(x =>
                        !x.IsDeleteFlag() && // Handled by repo ... cannot be supplied
                        !x.IsVersionDateColumn() // Handled by repo ... cannot be supplied
                        ).ToList().ToColumnSchemaCollection();
                case FilterMode.ToDto:
                    return columns.Where(x =>   // Remove ID columns if the table has a safer UID column
                        !x.IsPrimaryKeyMember ||
                        !x.Table.HasIdentifierColumns()
                        ).ToList().ToColumnSchemaCollection();
                default:
                    return new ColumnSchemaCollection();
            }
        }
        public static ColumnSchemaCollection Filter(this TableSchema table, FilterMode mode)
        {
            return table.Columns.Filter(mode);
        }
        public static ColumnSchemaCollection Filter(this IndexSchema index, FilterMode mode)
        {
            return index.ToColumnSchemaCollection().Filter(mode);
        }
    }
}
