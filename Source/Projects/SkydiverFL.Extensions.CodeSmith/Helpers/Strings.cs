using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using CodeSmith.Core.Extensions;
using CodeSmith.Engine;

namespace SkydiverFL.Extensions.CodeSmith.Helpers
{
    public static class CommonStrings
    {
        public static readonly string Alphanumeric = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    }

    public enum SentenceCase
    {
        Upper,
        Lower,
        Title,
        Sentence
    }

    public static class Strings
    {
        public static string ToSentence(this string value, char delimeter = ' ', SentenceCase sentenceCase = SentenceCase.Sentence)
        {
            if (string.IsNullOrWhiteSpace(value)) { return value; }

            var words = value.ToWords();
            if (words == null || words.Length < 1) { return string.Empty; }

            var result = string.Empty;
            for (var i = 0; i < words.Length; i++)
            {
                if (result.Length > 0) { result += delimeter; }
                switch (sentenceCase)
                {
                    case SentenceCase.Sentence:
                        result += (i == 0 ? words[i].ToTitleCase() : words[i].ToLower());
                        break;
                    case SentenceCase.Lower:
                        result += words[i].ToLower();
                        break;
                    case SentenceCase.Title:
                        result += words[i].ToPascalCase();
                        break;
                    case SentenceCase.Upper:
                        result += words[i].ToUpper();
                        break;
                    default:
                        result += "ERROR";
                        break;
                }
            }

            return result;
        }
        public static string ToHypehatedLowercase(this string value)
        {
            return value.ToSentence('-', SentenceCase.Lower);
        }
        public static string ToTitle(this string value)
        {
            return value.ToSentence(' ', SentenceCase.Title);
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

        public static string FormatAsJson(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) { return value; }

            var result = string.Empty;
            try
            {
                result = new JSonPresentationFormatter().Format(value);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        public static string ToPlural(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) { return value; }

            return StringUtil.ToPlural(value);
        }
    }
}
