using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class GetNowTime : MonoBehaviour {

	[SerializeField] // 現在の時刻を取得
	private Text nowTime;

	[SerializeField] // 残り時間
	private Text remainingTime1, remainingTime2, remainingTime3;

	private List<DateTime> nextTrainDate = new List<DateTime>();
	private CsvManager csvManager = new CsvManager();
	private List<TimeSpan> diff = new List<TimeSpan>(); // 差分現在時刻から駅の時間を引いて残り時間(diff)をだす

	// 初回の動作
	void Start () {
		// 時刻表データの取り出し
		nextTrainDate.Add(csvManager.NextTime(DateTime.Now));
		for (int i = 1; i < 3; i++) {
			nextTrainDate.Add(csvManager.NextTime(nextTrainDate[i-1]));
		}
	}

	// フレームごとに更新
	void Update () {
		// 現在時刻を取得
		DateTime dtToday = DateTime.Now;
		// UnityUIに代入(時刻表示は後に消す予定)
		nowTime.text = dtToday.ToShortTimeString();

		for (int i = 0; i < 3; i++) {
			diff.Insert(i,nextTrainDate[i] - dtToday);
		}

		// 差が0以下(電車が行ってしまったとき)
		if (diff[0].TotalSeconds < 0) {
			// 時刻表の入れ替え
			nextTrainDate.Insert(0,nextTrainDate[1]);
			for (int i = 1; i < 3; i++) {
				nextTrainDate.Insert(i,csvManager.NextTime(nextTrainDate[i-1]));
			}
		}

		// +""で文字列変換をした後UniyUIに代入
		if (diff[0].TotalSeconds <= 60) {
			remainingTime1.text = diff[0].Seconds + "秒";
		} else {
			remainingTime1.text = diff[0].Minutes + "分";
		}
			remainingTime2.text = diff[1].Minutes + "分";
			remainingTime3.text = diff[2].Minutes + "分";
	}
}
