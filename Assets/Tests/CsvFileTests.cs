/*************************************************************************
 *  Copyright © 2025 Mogoson All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CsvFileTests.cs
 *  Description  :  Default.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  12/16/2025
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Linq;
using NUnit.Framework;
using UnityEngine;

namespace MGS.CSV.Tests
{
    public class CsvFileTests
    {
        class CsvTestLine
        {
            public string id;
            public string description;
        }

        string filePath = $"{Application.persistentDataPath}/TestCSV.csv";

        [Test]
        public void ExportTest()
        {
            var lines = new CsvTestLine[]
            {
                new CsvTestLine()
                {
                    id = "0",
                    description=" Test description start with empty, and follow 'single quotes' and \"double quotation\" and \r\n" +
                    "new line with , and = and @ and true and null and end with empty "
                }
            };
            var succeed = CsvFile.Export(filePath, lines);
            Assert.IsTrue(succeed);
            Debug.Log($"Export CSV to file {filePath}");
        }

        [Test]
        public void ImportTest()
        {
            var lines = CsvFile.Import<CsvTestLine>(filePath);
            Assert.IsNotNull(lines);
            Assert.AreEqual(lines.Count(), 1);
            Debug.Log($"Import CSV from file {filePath}");
            foreach (var line in lines)
            {
                Debug.Log($"{line.id} {line.description}");
            }
        }
    }
}