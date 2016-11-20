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
		//リターンの確認用でDebug使ってます
		Debug.Log(NextTime(DateTime.Now));
	}

	/*
	 * CSV読み取り用
	 */
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

	/*
	 * 引数より一つ先の電車到着予定時刻を取ってくる．
	 * →次の次の時刻を呼び出したい場合現在の時刻を引数に渡して帰ってきた値を
	 *  また引数に入れてあげればいい？
	 */
	DateTime NextTime(DateTime nowTime) {
		for(int i = 0; i < timeTable_s.Length; i++){
			//問題:Parseで入れると今日の日付になるから24:00以降をどうするか
			DateTime timeTable_d = DateTime.Parse(timeTable_s[i]);
			if (DateTime.Compare(timeTable_d, nowTime) >= 0) {
				return timeTable_d;
			}
		}
		return nowTime; //今日の電車がもうないとき　とりあえず仮置き
	}

}
