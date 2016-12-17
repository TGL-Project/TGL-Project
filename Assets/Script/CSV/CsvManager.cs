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
	/// dummy→5分ごと
	/// timeTable→平日用
	/// </summary>
	public void ReadCsv ()
	{
		timeTableWeekday = GetCsvValues("dummy"); // 平日用
		timeTableHoliday = GetCsvValues("dummy"); // 休日用
		setTodayTimeTable();
	}

	/// <summary>
	/// Gets the time spans.
	/// 現在~x時間後のタイムテーブルを取得する
	/// </summary>
	public List<TimeSpan> GetTimeSpans(TimeSpan time)
	{
		// 返却するデータ
		List<TimeSpan> tsWant = new List<TimeSpan>();

		// 現在の時刻
		TimeSpan tsNow = DateTime.Now - DateTime.Today;

		// 締切の時刻
		TimeSpan tsLimit = tsNow + time;

		// タイムテーブルを確認
		foreach (String td_s in timeTable)
		{
			TimeSpan td_ts = TimeSpan.Parse(td_s);
			// 範囲内
			if (tsNow < td_ts && td_ts < tsLimit)
			{
				tsWant.Add(td_ts);
			}
		}

		return tsWant;
	}

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
		int actualLength = lines.Length;
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
	/// 引数より一つ先の電車到着予定時刻を取ってくる．
	/// 次の次の時刻を呼び出したい場合，現在の時刻を引数に渡して帰ってきた値をまた引数に入れる．
	/// </summary>
	/// <returns>タイムテーブルにあるtimeより１つ後の時刻</returns>
	/// <param name="time">現在時刻/ない場合は引数の時刻</param>
	public TimeSpan GetNextTime(TimeSpan time)
	{
		TimeSpan nextTimeData = time;
		for (int i = 0; i < timeTable.Count; i++)
		{
			nextTimeData = TimeSpan.Parse(timeTable[i]);

			// timeよりも後の時刻かどうかを判定
			if (TimeSpan.Compare(nextTimeData, time) > 0)
			{
				break;
			}
		}
		return nextTimeData;
	}

	///// <summary>
	///// 現時刻の次の電車到着予定時刻の配列番号を取ってくる．
	///// </summary>
	///// <returns>The time number.</returns>
	//public int NextTimeNumber()
	//{
	//	int n = 0;
	//	for (int i = 0; i < timeTable.Count; i++)
	//	{
	//		DateTime nextTimeTable = DateTime.Parse(timeTable[i]);
	//		if (DateTime.Compare(nextTimeTable, DateTime.Now) > 0)
	//		{
	//			n = i;
	//			break;
	//		}
	//	}
	//	return n;
	//}

	///// <summary>
	///// 電車の本数を返す
	///// </summary>
	///// <returns>The time table length.</returns>
	//public int GetTimeTableLength()
	//{
	//	return timeTable.Count;

	//}
}