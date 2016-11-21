﻿using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class GetNowTime : MonoBehaviour {

	public Text nowTime; // 現在の時刻を取得
	public Text remainingTime1; // 残り時間1
	public Text remainingTime2; // 残り時間2
	public Text remainingTime3; // 残り時間3
	private ArrayList nextTrainDate = new ArrayList(); //次の時刻表たち
	private CsvManager csvManager = new CsvManager();
	private ArrayList diff = new ArrayList(); //差分現在時刻から駅の時間を引いて残り時間(diff)をだす

	// 初回の動作
	void Start () {
		// 時刻表データの取り出し
		nextTrainDate.Add(csvManager.NextTime(DateTime.Now));
		for (int i = 1; i < 3; i++) {
			nextTrainDate.Add(
				csvManager.NextTime((DateTime)nextTrainDate[i-1])
			);
		}
	}

	// フレームごとに更新
	void Update () {
		// 現在時刻を取得
		DateTime dtToday = DateTime.Now;
		// UnityUIに代入(時刻表示は後に消す予定)
		nowTime.text = dtToday.ToShortTimeString();

		for (int i = 0; i < 3; i++) {
			diff.Insert(i,(TimeSpan)(((DateTime)nextTrainDate[i]) - dtToday));
		}

		// if (difference = 0)で次の直近時刻表を取る
		if (((TimeSpan)diff[0]).TotalSeconds < 0) {
			// 時刻表データの取り出し
			nextTrainDate.Insert(0,(DateTime)nextTrainDate[1]);
			for (int i = 1; i < 3; i++) {
				nextTrainDate.Insert(i,
					csvManager.NextTime((DateTime)nextTrainDate[i-1])
				);
			}
			// for (int i = 0; i < 4; i++) {
			// 	nextTrainDate.Insert(i,(DateTime)nextTrainDate[i+1]);
			// 	Debug.Log((DateTime)nextTrainDate[i+1]);
			// }
		}

		//+""で文字列変換をした後UniyUIに代入
		if (((TimeSpan)diff[0]).TotalSeconds <= 60) {
			remainingTime1.text = ((TimeSpan)diff[0]).Seconds + "秒";
		} else {
			remainingTime1.text = ((TimeSpan)diff[0]).Minutes + "分";
		}
			remainingTime2.text = ((TimeSpan)diff[1]).Minutes + "分";
			remainingTime3.text = ((TimeSpan)diff[2]).Minutes + "分";
	}
}
