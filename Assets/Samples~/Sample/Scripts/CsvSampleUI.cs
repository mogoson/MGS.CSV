/*************************************************************************
 *  Copyright © 2025 Mogoson All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CsvSampleUI.cs
 *  Description  :  Default.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  12/18/2025
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;
using UnityEngine.UI;

namespace MGS.CSV.Sample
{
    public class CsvSampleUI : MonoBehaviour
    {
        public InputField iptID;
        public InputField iptDec;
        public Button btnExport;

        [Space]
        public InputField txtID;
        public InputField txtDec;
        public Button btnImport;

        string filePath;

        private void Awake()
        {
            filePath = $"{Application.persistentDataPath}/TestCSV.csv";
            btnExport.onClick.AddListener(OnExport);
            btnImport.onClick.AddListener(OnImport);
        }

        void OnExport()
        {
            var lines = new CsvSampleLine[]
            {
                new CsvSampleLine()
                {
                    id = iptID.text,
                    description=iptDec.text
                }
            };
            var succeed = CsvFile.Export(filePath, lines);
            if (succeed)
            {
                Debug.Log($"Export CSV to file {filePath}");
            }
        }

        void OnImport()
        {
            var lines = CsvFile.Import<CsvSampleLine>(filePath);
            var lineEnum = lines.GetEnumerator();
            if (lineEnum.MoveNext())
            {
                txtID.text = lineEnum.Current.id;
                txtDec.text = lineEnum.Current.description;
                Debug.Log($"Import CSV from file {filePath}");
            }
        }
    }
}