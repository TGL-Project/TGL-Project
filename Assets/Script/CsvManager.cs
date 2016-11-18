using UnityEngine;
using System.Collections;
using System.IO;

public class CsvManager {

    static void ReadCsv() {
        TextAsset csv = Resources.Load("CsvData") as TextAsset;
        StringReader reader = new StringReader(csv.text);
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            string[] values = line.Split(',');
            Debug.Log(values);
        }
    }
}
