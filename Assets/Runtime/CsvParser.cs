/*************************************************************************
 *  Copyright © 2025 Mogoson All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CsvParser.cs
 *  Description  :  Default.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  12/16/2025
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MGS.CSV
{
    public sealed class CsvParser
    {
        #region
        const string DELIMITER_FIELD = ",";

        public static string ToLine(IEnumerable<object> fields)
        {
            var escapeFields = new List<string>();
            foreach (var field in fields)
            {
                escapeFields.Add(EscapeField(field.ToString()));
            }
            return string.Join(DELIMITER_FIELD, escapeFields);
        }

        static string EscapeField(string field)
        {
            if (!NeedEscape(field))
            {
                return field;
            }
            field = field.Replace("\"", "\"\"");
            return $"\"{field}\"";
        }

        static bool NeedEscape(string field)
        {
            if (string.IsNullOrEmpty(field))
            {
                return false;
            }

            if (field.Contains(DELIMITER_FIELD))
            {
                return true;
            }

            if (field.Contains('"'))
            {
                return true;
            }

            if (field.Contains('\r') || field.Contains('\n'))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region
        const string DELIMITER_LINE = "\r\n";

        public static string ToText(IList<string> lines)
        {
            return string.Join(DELIMITER_LINE, lines);
        }
        #endregion

        #region
        const string PATTERN_LINE = @"(?:(?:""[^""]*"")|[^""\r\n])*";

        public static IList<string> ToLines(string text)
        {
            var lines = new List<string>();
            var matches = Regex.Matches(text, PATTERN_LINE);
            foreach (Match match in matches)
            {
                if (!string.IsNullOrEmpty(match.Value))
                {
                    lines.Add(match.Value);
                }
            }
            return lines;
        }
        #endregion

        #region
        const string PATTERN_FIELD = @"(?:^|,)(?:\""([^\""]*(?:\""\""[^\""]*)*)\""|([^,""]*))";

        public static IList<string> ToFields(string line)
        {
            var fields = new List<string>();
            var matches = Regex.Matches(line, PATTERN_FIELD);
            foreach (Match match in matches)
            {
                var field = match.Groups[1].Success ? match.Groups[1].Value : match.Groups[2].Value;
                fields.Add(UnescapeField(field));
            }
            return fields;
        }

        static string UnescapeField(string field)
        {
            if (string.IsNullOrEmpty(field))
            {
                return field;
            }
            return field.Replace("\"\"", "\"");
        }
        #endregion
    }
}