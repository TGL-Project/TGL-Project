using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// プレハブのコピー，プレハブへのテキスト代入
/// </summary>
public class ScrollController : MonoBehaviour {

	/// <summary>
	/// 時刻表のプレハブ
	/// </summary>
	[SerializeField]
	private RectTransform prefab = null;

	/// <summary>
	/// The csv mgr.
	/// </summary>
	[SerializeField]
	private CsvManager csvMgr = null;

	/// <summary>
	/// オブジェクトがないときの待機用画像
	/// </summary>
	[SerializeField]
	private GameObject waitingImage = null;

	/// <summary>
	/// 更新頻度
	/// 146行目で調整
	/// </summary>
	private float timeOut = 0.0f;

	/// <summary>
	/// 経過時間
	/// </summary>
	private float timeElapsed = 0.0f;

	/// <summary>
	/// 次に作成するオブジェクト名の番号
	/// </summary>
	private int nextNodeNumber = 0;

	/// <summary>
	/// 各RemainingTimeNodeの情報を入れる
	/// </summary>
	private List<RemainingTime> remainingTimeList = new List<RemainingTime>();

	/// <summary>
	/// 初期化用
	/// プレハブの複製及びContent以下への挿入
	/// </summary>
	void Start ()
	{
		Initialize(); //初期化用
	}

	/// <summary>
	///	初期化用メソッド
	/// 最初とすべての電車が行ってしまったときに読み込む
	/// </summary>
	private void Initialize()
	{
		// csvファイルの読み込み
		csvMgr.ReadCsv();
		// 一時間後までのタイムテーブルを取得
		List<TimeSpan> displayTimes = csvMgr.GetTimeSpans(new TimeSpan(1, 0, 0));
		// 表示する分のオブジェクトを作成
		foreach (TimeSpan displayTime in displayTimes)
		{
			CreateNode(displayTime);
		}
	}

	/// <summary>
	/// Unityのアップデート関数
	/// </summary>
	void Update () {
		// 経過時刻の測定
		timeElapsed += Time.deltaTime;

		/// 1秒おきに呼び出す
		if (timeElapsed >= timeOut)
		{
			// 初期は 0 ~ 11
			foreach (RemainingTime reTime in remainingTimeList)
			{

				Text text = reTime.GetText();

				// 分表示
				text.text = reTime.GetDiffTime().Minutes + "分";

				// 1分未満の表示
				if (reTime.GetDiffTime().TotalSeconds <= 60)
				{
					text.text = reTime.GetDiffTime().Seconds + "秒";
				}

			}

			/// オブジェクトが存在しているときの処理
			if (remainingTimeList.Count > 0)
			{
				/// 作成
				//Debug.Log(remainingTimeList.Count);
				//Debug.Log(remainingTimeList[remainingTimeList.Count - 1].GetTime());
				// 現在の最後尾の時刻(差分ではない)
				TimeSpan lastDisplayTime = remainingTimeList[remainingTimeList.Count - 1].GetTime();
				// 次に表示されるやつが60分以下になっているかどうか
				if ((csvMgr.GetNextTime(lastDisplayTime) - (DateTime.Now - DateTime.Today)).TotalMinutes <= 60)
				{
					CreateNode(csvMgr.GetNextTime(lastDisplayTime));
				}

				/// 破棄
				if (remainingTimeList[0].GetDiffTime().TotalSeconds <= 0)
				{
					Destroy(remainingTimeList[0].GetGameObj());
					remainingTimeList.RemoveAt(0);
				}

				/// 色変更
				if (remainingTimeList.Count == 1)
				{
					//本数残り1で文字赤
					remainingTimeList[remainingTimeList.Count - 1].GetText().color = new Color(255f, 0, 0);
				}
			}
			/// オブジェクトが無いとき
			else
			{
				/// 何もない
				waitingImage.SetActive(true);

				/// 作成
				if ((csvMgr.GetNextTime(DateTime.Now - DateTime.Today) - (DateTime.Now - DateTime.Today)).TotalMinutes <= 60 &&
				     csvMgr.GetNextTime(DateTime.Now - DateTime.Today) != new TimeSpan(-1, 0, 0, 0))
				{
					CreateNode(csvMgr.GetNextTime(DateTime.Now - DateTime.Today));
					waitingImage.SetActive(false);
				}
			}
			timeOut = 1.0f;
			timeElapsed = 0.0f;
		}

	}

	/// <summary>
	/// remainingTimeListに新たなRemainingTimeNodeを作成する
	/// </summary>
	/// <param name="displayTime">追加するnodeの時刻</param>
	private void CreateNode(TimeSpan displayTime)
	{
		// プレハブのコピー
		RectTransform item = GameObject.Instantiate(prefab) as RectTransform;
		item.name = "RemainingTimeNode" + nextNodeNumber;
		remainingTimeList.Add(new RemainingTime(item.gameObject, displayTime));
		item.SetParent(transform, false);
		nextNodeNumber++;
	}

}
