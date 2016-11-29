using System;
using System.IO;
using System.Text;
using System.Collections;

public class CsvManager {

	private string[] timeTable_s = null;

	/*
	 * コンストラクタ
	 */
	public CsvManager () {
		ReadCsv(); // 駅データ読み込み
	}

	/*
	 * CSV読み取り用
	 */
	private void ReadCsv () {
		string filePath = "dummy.csv"; //Debugはdummy.csv 駅ファイルはtimeTable.csv
		StreamReader reader = new StreamReader(filePath, Encoding.GetEncoding("UTF-8"));
		while (reader.Peek() >= 0) {
			timeTable_s = reader.ReadLine().Split(',');
		}
		reader.Close();
	}

	/*
	 * 引数より一つ先の電車到着予定時刻を取ってくる．
	 * 次の次の時刻を呼び出したい場合現在の時刻を引数に渡して帰ってきた値を
	 * また引数に入れる．
	 */
	public DateTime NextTime (DateTime time) {
		for(int i = 0; i < timeTable_s.Length; i++){
			DateTime timeTable_d = DateTime.Parse(timeTable_s[i]);
			if (DateTime.Compare(timeTable_d, time) > 0) {
				return timeTable_d;
			}
		}
		return time; //今日の電車がもうないときは現在時刻を返品
	}

	/*
	 * 現時刻の次の電車到着予定時刻の配列番号を取ってくる．
	 */
	public int NextTimeNumber () {
		int n = 0;
		for(int i = 0; i < timeTable_s.Length; i++) {
			DateTime timeTable_d = DateTime.Parse(timeTable_s[i]);
			if (DateTime.Compare(timeTable_d, DateTime.Now) > 0) {
				n = i;
				break;
			}
		}
		return n;
	}

	/*
	 * 電車の本数を返す
	 */
	public int GetTimeTableLength () {
		return timeTable_s.Length;
	}
}
