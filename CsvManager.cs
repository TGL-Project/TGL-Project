using System;
using System.IO;
using System.Text;
using UnityEngine;
using System.Collections;

public class CsvManager : MonoBehaviour {
	private string[] timeTable=null;
	// Use this for initialization
	void Start()
	{
		ReadCsv ();

		NextTime (DateTime.Now);
	}

	// Update is called once per frame
	void Update()
	{

	}

	static void ReadCsv()
	{
		string filePath = "TimeTable.csv";
		StreamReader reader = new StreamReader(filePath);
		while (reader.Peek() >= 0) {
			string[] cols = reader.ReadLine().Split(',');
			for (int n = 0; n < cols.Length; n++) {
				

			}



		}
		reader.Close();
	}




	void NextTime(DateTime time)
	{ 
		for(int n = 0; n < timeTable.Length;n++){
			DateTime timeTable = DateTime.Parse(timeTable [n]);

			Debug.Log(timeTable);


		}
	}

}