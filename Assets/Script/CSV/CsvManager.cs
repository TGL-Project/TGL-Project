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
	/// ↓時刻表↓
	/// /(uehonmachi|kawachikokubu)(Weekday|Holiday)/ : 各時刻表
	/// dummy : 5分毎の時刻が記述されている
	/// empty : 空ファイル
	/// </summary>
	public void ReadCsv ()
	{
		timeTableWeekday = GetCsvValues("uehonmachiWeekday"); // 平日用
		timeTableHoliday = GetCsvValues("uehonmachiHoliday"); // 休日用
		//timeTableWeekday = GetCsvValues("dummy"); // 平日用
		//timeTableHoliday = GetCsvValues("dummy"); // 休日用

		SetTodayTimeTable();
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
		TimeSpan tsNow = (DateTime.Now - DateTime.Today);

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
	private void SetTodayTimeTable()
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
	public void SwapOfHolidayAndWeekday()
	{
		if (timeTable == timeTableWeekday)
		{
			timeTable = timeTableHoliday;
		}
		else
		{
			timeTable = timeTableWeekday;
		}
	}


	/// <summary>
	/// 引数より一つ先の電車到着予定時刻を取ってくる．
	/// 次の次の時刻を呼び出したい場合，現在の時刻を引数に渡して帰ってきた値をまた引数に入れる．
	/// </summary>
	/// <returns>タイムテーブルにあるtimeより１つ後の時刻|なかった場合は-24h</returns>
	/// <param name="time">取りたい時刻より一つ前の時刻</param>
	public TimeSpan GetNextTime(TimeSpan time)
	{
		// timeは仮置き
		TimeSpan nextTimeData = new TimeSpan(-1, 0, 0, 0);

		for (int i = 0; i < timeTable.Count; i++)
		{
			TimeSpan tmp = TimeSpan.Parse(timeTable[i]);

			// timeよりも後の時刻かどうかを判定
			if (TimeSpan.Compare(tmp, time) > 0)
			{
				nextTimeData = TimeSpan.Parse(timeTable[i]);
				break;
			}
		}
		return nextTimeData;
	}

	public bool IsHolidayOrWeekDay()
	{
		if (timeTable == timeTableWeekday)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
