using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class GetNowTime : MonoBehaviour {

	public Text nowTime; //現在の時刻を取得
	public Text remainingTime; //残り時間
	private DateTime nextTrainDate; //次の時刻表

	// 初回の動作「コンストラクタ」
	void Start () {
		// 現在時刻を取得(直近を取るために必要そう)
		// DateTime dtToday = DateTime.Now;
		// ここの new Date(省略)を直近の時刻表に変える
		nextTrainDate = new DateTime(2016,11,18,18,4,0);
		// nextTrainDate = recentTrainTime(dtToday);
	}

	// フレームごとに更新
	void Update () {
		// 現在時刻を取得
		DateTime dtToday = DateTime.Now;
		// UnityUIに代入(時刻表示は後に消す予定)
		nowTime.text = dtToday.ToShortTimeString();

		//差分(TimeSpan)
		TimeSpan difference = nextTrainDate - dtToday;

		// if (difference = 0)で次の直近時刻表を取る
		if (difference.TotalSeconds <= 0) {
			// ここの new Date(省略)を直近の時刻表に変える
			nextTrainDate = new DateTime(2016,11,18,18,55,0);
		}

		//+""で文字列変換とUniyUIに代入
		if (difference.TotalSeconds <= 60) {
			remainingTime.text = difference.Seconds + "秒";
		} else {
			remainingTime.text = difference.Minutes + "分";
		}
	}

	// DateTime recentTrainTime (DateTime nowTime) {
	// 	ArrayList time = new ArrayList();
	// 	time.Add(new DateTime(2016,11,18,16,40,0));
	// 	time.Add(new DateTime(2016,11,18,15,45,0));
	// 	time.Add(new DateTime(2016,11,18,15,50,0));
	// 	Console.WriteLine(time[0]);
	//
	// 	int bestTime = 0;
	// 	TimeSpan diff = (DateTime)time[0] - nowTime;
	// 	for(int i = 1; i < time.Count; i++) {
	// 		TimeSpan diff2 = (DateTime)time[i] - nowTime;
	// 		if (diff2 < diff) {
	// 			diff = diff2;
	// 			bestTime = i;
	// 		}
	// 	}
	// 	return (DateTime)time[bestTime];
	// }

}
