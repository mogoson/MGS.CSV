/*************************************************************************
 *  Copyright © 2025 Mogoson All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CsvAdapter.cs
 *  Description  :  Default.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  12/16/2025
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections.Generic;

namespace MGS.CSV
{
    public sealed class CsvAdapter
    {
        #region
        public static string ToText<T>(IEnumerable<T> objs, bool includeHeader = true)
        {
            var lines = ToLines(objs, includeHeader);
            return CsvParser.ToText(lines);
        }

        public static IList<string> ToLines<T>(IEnumerable<T> objs, bool includeHeader = true)
        {
            var lines = new List<string>();
            if (includeHeader)
            {
                lines.Add(ToLine(typeof(T)));
            }
            foreach (object obj in objs)
            {
                lines.Add(ToLine(obj));
            }
            return lines;
        }

        public static string ToLine(Type type)
        {
            var fields = FieldAdapter.GetFields(type);
            return CsvParser.ToLine(fields);
        }

        public static string ToLine(object obj)
        {
            var values = FieldAdapter.GetValues(obj);
            return CsvParser.ToLine(values);
        }
        #endregion

        #region
        public static IList<T> FromText<T>(string text, bool includeHeader = true)
        {
            var lines = CsvParser.ToLines(text);
            return FromLines<T>(lines, includeHeader);
        }

        public static IList<T> FromLines<T>(IList<string> lines, bool includeHeader = true)
        {
            var objs = new List<T>();
            if (includeHeader)
            {
                var fields = CsvParser.ToFields(lines[0]);
                for (var i = 1; i < lines.Count; i++)
                {
                    objs.Add(FromLine<T>(lines[i], fields));
                }
            }
            else
            {
                foreach (var line in lines)
                {
                    objs.Add(FromLine<T>(line));
                }
            }
            return objs;
        }

        public static T FromLine<T>(string line)
        {
            var obj = Activator.CreateInstance<T>();
            var values = CsvParser.ToFields(line);
            FieldAdapter.Fill(obj, new List<object>(values));
            return obj;
        }

        public static T FromLine<T>(string line, IList<string> fields)
        {
            var obj = Activator.CreateInstance<T>();
            var values = CsvParser.ToFields(line);
            FieldAdapter.Fill(obj, new List<object>(values), fields);
            return obj;
        }
        #endregion
    }
}