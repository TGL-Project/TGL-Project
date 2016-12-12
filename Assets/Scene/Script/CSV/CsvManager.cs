using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class CsvManager : MonoBehaviour {

	/// <summary>
	/// 使用中のタイムテーブル
	/// </summary>
	private List<string> timeTable = new List<string>();

	/// <summary>
	/// 平日のタイムテーブル
	/// </summary>
	private List<string> timeTableWeekday = new List<string>();

	/// <summary>
	/// 休日のタイムテーブル
	/// </summary>
	private List<string> timeTableHoliday = new List<string>();

	/// <summary>
	/// CSV読み取り用
	/// </summary>
	public void ReadCsv ()
	{
		timeTableWeekday = GetCsvValues("dummy"); // 平日用
		timeTableHoliday = GetCsvValues("timeTable"); // 休日用
		setTodayTimeTable();
	}

	/// <summary>
	/// 引数より一つ先の電車到着予定時刻を取ってくる．
	/// 次の次の時刻を呼び出したい場合，現在の時刻を引数に渡して帰ってきた値をまた引数に入れる．
	/// </summary>
	/// <returns>タイムテーブルにあるtimeより１つ後の時刻</returns>
	/// <param name="time">普通は現在時刻</param>
	public DateTime NextTime (DateTime time)
	{
		DateTime nextTimeTable = time;
		for(int i = 0; i < timeTable.Count; i++)
		{
			nextTimeTable = DateTime.Parse(timeTable[i]);

			// timeよりも後の時刻かどうかを判定
			if (DateTime.Compare(nextTimeTable, time) > 0)
			{
				break;
			}
		}
		return nextTimeTable;
	}

	/// <summary>
	/// 現時刻の次の電車到着予定時刻の配列番号を取ってくる．
	/// </summary>
	/// <returns>The time number.</returns>
	public int NextTimeNumber ()
	{
		int n = 0;
		for(int i = 0; i < timeTable.Count; i++) 
		{
			DateTime nextTimeTable = DateTime.Parse(timeTable[i]);
			if (DateTime.Compare(nextTimeTable, DateTime.Now) > 0)
			{
				n = i;
				break;
			}
		}
		return n;
	}

	/// <summary>
	/// 休日用と平日用の入れ替えメソッド
	/// </summary>
	//private void ChangeCsv()
	//{
	//	if (timeTable == timeTableWeekday)
	//	{
	//		timeTable = timeTableHoliday;
	//	}
	//	else
	//	{
	//		timeTable = timeTableWeekday;
	//	}
	//}

	/// <summary>
	/// 今日の時刻表をセットするメソッド
	/// </summary>
	private void setTodayTimeTable()
	{
		DateTime today = DateTime.Today;
		if (today.ToString("ddd") == "Sat" || today.ToString("ddd") == "Sun")
		{
			timeTable = timeTableHoliday;
		}
		else 
		{
			timeTable = timeTableWeekday;
		}
	}

	/// <summary>
	/// 電車の本数を返す
	/// </summary>
	/// <returns>The time table length.</returns>
	public int GetTimeTableLength ()
	{
		return timeTable.Count;

	}

	/// <summary>
	/// csvファイルをListで返すメソッド．複数行可能．
	/// csvの書き方
	/// 1行目|a,b,c|
	/// 2行目|1,2,3|
	/// </summary>
	/// <returns>The load.</returns>
	/// <param name="csvFile">データを取得したいCsv file.</param>
	private List<string> GetCsvValues(string csvFile)
	{
		List<string> values = new List<string>();
		TextAsset csv = Resources.Load("CSV/" + csvFile) as TextAsset;
		StringReader reader = new StringReader(csv.text);
		// 全体を取得
		string sentence = reader.ReadToEnd();

		// readerの終了
		reader.Close();

		// 行に分割
		string[] lines = sentence.Split('\n');

		// 最後の空行がある場合削除
		int actualLength = lines.Length - 1;
		if (lines[lines.Length - 1] == "")
		{
			actualLength--;
		}

		// 行数分回す
		for (int i = 0; i < actualLength; i++)
		{
			// 行を分解
			string[] word = lines[i].Split(',');

			// 分解した文字をvaluesに入れる
			for (int j = 0; j < word.Length; j++)
			{
				values.Add(word[j]);
			}

		}
		return values;
	}
}