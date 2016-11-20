using System;
using System.IO;
using System.Text;
using UnityEngine;
using System.Collections;

public class CsvManager : MonoBehaviour {

	private string[] timeTable_s = null;

	// Use this for initialization
	void Start() {
		ReadCsv();
		Debug.Log(NextTime(DateTime.Now));
	}

	void ReadCsv() {
		string filePath = "TimeTable.csv";
		StreamReader reader = new StreamReader(filePath, Encoding.GetEncoding("UTF-8"));
		while (reader.Peek() >= 0) {
			timeTable_s = reader.ReadLine().Split(',');
		}
		reader.Close();

		/*//csv読み込み時点のDebug用
		for(int i = 0; i < timeTable_s.Length; i++) {
	 		Debug.Log(timeTable_s[i]);
		}
		*/
	}

	DateTime NextTime(DateTime nowTime) {
		for(int i = 0; i < timeTable_s.Length; i++){
			//問題:Parseで入れると今日の日付になるから24:00以降をどうするか
			DateTime timeTable_d = DateTime.Parse(timeTable_s[i]);
			if (DateTime.Compare(timeTable_d, nowTime) >= 0) {
				return timeTable_d;
			}
		}
		return nowTime; //今日の電車がもうないとき
	}

}
