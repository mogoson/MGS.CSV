/*************************************************************************
 *  Copyright © 2025 Mogoson All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CsvIOTests.cs
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
    public class CsvIOTests
    {
        class TestLineInfo
        {
            public string id;
            public string description;
        }

        string filePath = $"{Application.persistentDataPath}/TestCSV.csv";

        [Test]
        public void ExportTest()
        {
            var infos = new TestLineInfo[]
            {
            new TestLineInfo()
            {
                id = "0",
                description=" Test description start with empty, and follow 'single quotes' and \"double quotation\" and \r\n" +
                "new line with , and = and @ and true and null and end with empty "
            }
            };
            var succeed = CsvIO.Export(filePath, infos);
            Assert.IsTrue(succeed);
            Debug.Log($"Export CSV to path {filePath}");
        }

        [Test]
        public void ImportTest()
        {
            var infos = CsvIO.Import<TestLineInfo>(filePath);
            Assert.IsNotNull(infos);
            Assert.AreEqual(infos.Count(), 1);
            Debug.Log($"Import CSV from path {filePath}");
            foreach (var info in infos)
            {
                Debug.Log($"{info.id} {info.description}");
            }
        }
    }
}