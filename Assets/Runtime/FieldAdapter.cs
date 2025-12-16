/*************************************************************************
 *  Copyright © 2025 Mogoson All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  FieldAdapter.cs
 *  Description  :  Default.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  12/16/2025
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace MGS.CSV
{
    public sealed class FieldAdapter
    {
        #region
        public static IList<string> GetFields(Type type)
        {
            var names = new List<string>();
            var fields = type.GetFields();
            foreach (var field in fields)
            {
                names.Add(field.Name);
            }
            return names;
        }

        public static IList<object> GetValues(object obj)
        {
            var values = new List<object>();
            var fields = obj.GetType().GetFields();
            foreach (var field in fields)
            {
                values.Add(field.GetValue(obj));
            }
            return values;
        }
        #endregion

        #region
        public static void Fill(object obj, IList<object> values)
        {
            var fields = obj.GetType().GetFields();
            var index = 0;
            foreach (var field in fields)
            {
                if (index >= values.Count)
                {
                    break;
                }
                var fieldValue = values[index];
                try
                {
                    field.SetValue(obj, fieldValue);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
                finally
                {
                    index++;
                }
            }
        }

        public static void Fill(object obj, IList<object> values, IList<string> names)
        {
            var fields = obj.GetType().GetFields();
            foreach (var field in fields)
            {
                var index = names.IndexOf(field.Name);
                if (index < 0 || index >= values.Count)
                {
                    continue;
                }
                var value = values[index];
                try
                {
                    field.SetValue(obj, value);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            }
        }
        #endregion
    }
}