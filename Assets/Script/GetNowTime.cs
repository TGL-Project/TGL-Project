using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class GetNowTime : MonoBehaviour {

	public Text nowTime; //現在の時刻を取得
	public Text remainingTime1; //残り時間
	public Text remainingTime2; //残り時間
	public Text remainingTime3; //残り時間
	private DateTime nextTrainDate1; //次の時刻表
	private DateTime nextTrainDate2; //次の時刻表
	private DateTime nextTrainDate3; //次の時刻表
	private DateTime nextTrainDate4; //次の時刻表


	// 初回の動作「コンストラクタ」
	void Start () {
		// 現在時刻を取得(直近を取るために必要そう)
		// DateTime dtToday = DateTime.Now;
		// ここの new Date(省略)を直近の時刻表に変える
		nextTrainDate1 = new DateTime(2016,11,18,20,20,0);
		nextTrainDate2 = new DateTime(2016,11,18,20,25,0);
		nextTrainDate3 = new DateTime(2016,11,18,20,30,0);
		nextTrainDate4 = new DateTime(2016,11,18,20,35,0);
		// nextTrainDate = recentTrainTime(dtToday);
	}

	// フレームごとに更新
	void Update () {
		// 現在時刻を取得
		DateTime dtToday = DateTime.Now;
		// UnityUIに代入(時刻表示は後に消す予定)
		nowTime.text = dtToday.ToShortTimeString();

		//差分(TimeSpan)
		TimeSpan difference1 = nextTrainDate1 - dtToday;
		TimeSpan difference2 = nextTrainDate2 - dtToday;
		TimeSpan difference3 = nextTrainDate3 - dtToday;
		TimeSpan difference4 = nextTrainDate4 - dtToday;

		// if (difference = 0)で次の直近時刻表を取る
		if (difference1.TotalSeconds <= 0) {
			// ここの new Date(省略)を直近の時刻表に変える
			nextTrainDate1 = nextTrainDate2;
			nextTrainDate2 = nextTrainDate3;
			nextTrainDate3 = nextTrainDate4;
		}

		//+""で文字列変換とUniyUIに代入
		if (difference1.TotalSeconds <= 60) {
			remainingTime1.text = difference1.Seconds + "秒";
		} else {
			remainingTime1.text = difference1.Minutes + "分";
		}
			remainingTime2.text = difference2.Minutes + "分";
			remainingTime3.text = difference3.Minutes + "分";
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
