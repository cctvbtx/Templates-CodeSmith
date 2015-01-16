using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using CodeSmith.Core.Extensions;

namespace SkydiverFL.Extensions.CodeSmith.Helpers
{
    public static class CommonStrings
    {
        public static readonly string Alphanumeric = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    }

    public static class Strings
    {
        public static string ToHypehatedLowercase(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) { return value; }

            var words = value.ToWords();
            if (words == null || words.Length < 1) { return string.Empty; }

            var result = string.Empty;
            foreach (var word in words)
            {
                if (result.Length > 0) { result += "-"; }
                result += word.ToLower();
            }

            return result;
        }

        public static IEnumerable ToPascalCase(this IEnumerable<string> values)
        {
            var list = new List<string>();

            foreach (var value in values) 
            {
                if ( list.FindIndex(x => x.Equals(value.ToPascalCase(), StringComparison.Ordinal)) < 0 )
                {
                    list.Add(value.ToPascalCase() );
                }
            }

            return list;
        }
        public static IEnumerable ToCamelCase(this IEnumerable<string> values)
        {
            var list = new List<string>();

            foreach (var value in values)
            {
                if (list.FindIndex(x => x.Equals(value.ToCamelCase(), StringComparison.Ordinal)) < 0)
                {
                    list.Add(value.ToCamelCase());
                }
            }

            return list;
        }

        public static string Clean(this string value, string validChars, bool isCaseSensitive = true)
        {
            if ( string.IsNullOrEmpty(value) || 
                string.IsNullOrEmpty(validChars) ) { return string.Empty; }

            var result = string.Empty;

            for (var i = 0; i < value.Length; i++)
            {
                if (validChars.IndexOf(value.Substring(i, 1),
                        (isCaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase)) >= 0 ) {
                            result += value.Substring(i, 1);
                        }
            }

            return result;
        }
    }
}
