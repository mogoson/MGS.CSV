/*************************************************************************
 *  Copyright © 2025 Mogoson All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CsvIO.cs
 *  Description  :  Default.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  12/16/2025
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MGS.CSV
{
    public sealed class CsvIO
    {
        public static bool Export<T>(string path, IEnumerable<T> objs, bool includeTittleLine = true)
        {
            try
            {
                var dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                var text = CsvAdapter.ToText(objs, includeTittleLine);
                File.WriteAllText(path, text);
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                return false;
            }
        }

        public static IEnumerable<T> Import<T>(string path, bool includeTittleLine = true)
        {
            try
            {
                var text = File.ReadAllText(path);
                return CsvAdapter.FromText<T>(text);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                return null;
            }
        }
    }
}