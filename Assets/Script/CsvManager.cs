using System;
using System.IO;
using System.Text;
using UnityEngine;
using System.Collections;

public class CsvManager {

	private string[] timeTable_s = null;

	// Use this for initialization
	void Start() {
		ReadCsv();
		NextTime(DateTime.Now);
	}

	void ReadCsv() {
		string filePath = "TimeTable.csv";
		StreamReader reader = new StreamReader(filePath, Encoding.GetEncoding("UTF-8"));
		while (reader.Peek() >= 0) {
			timeTable_s = reader.ReadLine().Split(',');
		}
		reader.Close();
	}

	void NextTime(DateTime time) {
		for(int n = 0; n < timeTable_s.Length; n++){
			DateTime timeTable = DateTime.Parse(timeTable_s [n]);
			Debug.Log(timeTable);
		}
	}

}
