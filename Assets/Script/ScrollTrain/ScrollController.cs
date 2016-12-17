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
	/// タイムデータを表示している要素の親要素
	/// </summary>
	[SerializeField]
	private Transform contentTransform = null;

	/// <summary>
	/// The csv mgr.
	/// </summary>
	[SerializeField]
	private CsvManager csvMgr = null;

	/// <summary>
	/// 表示するタイムテーブル
	/// </summary>
	private List<TimeSpan> displayTimeData = new List<TimeSpan>();

	/// <summary>
	/// 更新頻度
	/// </summary>
	private float timeOut = 0.0f;

	/// <summary>
	/// 経過時間
	/// </summary>
	private float timeElapsed;

	/// <summary>
	/// 次に作成するオブジェクト名の番号
	/// </summary>
	private int nextNodeNumber = 0;

	/// <summary>
	/// 初期化用
	/// プレハブの複製及びContent以下への挿入
	/// </summary>
	void Start ()
	{
		csvMgr.ReadCsv();
		Initialize(); //初期化用
		Test(); // デバッグ用
	}

	/// <summary>
	///	初期化用メソッド
	/// 最初とすべての電車が行ってしまったときに読み込む
	/// </summary>
	private void Initialize()
	{
		// 一時間後までのタイムテーブルを取得
		displayTimeData = csvMgr.GetTimeSpans(new TimeSpan(1, 0, 0));

		// 表示する分のオブジェクトを作成
		while (nextNodeNumber < displayTimeData.Count)
		{
			CreateNode();
			WillDeleteNode(nextNodeNumber);
			nextNodeNumber++;
		}
	}

	/// <summary>
	/// Unityのアップデート関数
	/// </summary>
	void Update () {
		timeElapsed += Time.deltaTime;

		if (timeElapsed >= timeOut)
		{
			int forCount = 0;
			foreach (Transform child in contentTransform)
			{

				GameObject childGObj = child.transform.FindChild("RemainingTimeText").gameObject;
				Text text = childGObj.GetComponent<Text>();

				TimeSpan diff = displayTimeData[forCount] - (DateTime.Now - DateTime.Today);

				//if (diff.TotalSeconds <= 0)
				//{
				//	displayTimeData.RemoveAt(0);
				//	diff = displayTimeData[forCount] - (DateTime.Now - DateTime.Today);
				//}

				// 分表示
				text.text = diff.Minutes + "分";

				// 1分未満の表示
				if (diff.TotalSeconds <= 60)
				{
					text.text = diff.Seconds + "秒";
				}

<<<<<<< HEAD
				forCount++;

			}
			forCount = 0;

			if ((csvMgr.GetNextTime(displayTimeData[displayTimeData.Count - 1]) - (DateTime.Now - DateTime.Today)).TotalMinutes <= 60)
			{
				displayTimeData.Add(csvMgr.GetNextTime(displayTimeData[displayTimeData.Count - 1]));
				CreateNode();
				WillDeleteNode(displayTimeData.Count - 1);
				nextNodeNumber++;
			}

			timeOut = 1.0f;
			timeElapsed = 0.0f;
=======

				if (GetRemainingTrainCount() == 1)
                {
                    //本数残り1で文字赤
                    text.color = new Color(255f, 0, 0);
                }
>>>>>>> daa566a87f656a311b8ac5f3e0aa7db6c839b811
		}
	}

	///// <summary>
	///// ここで残りの電車本数の算出を行う
	///// </summary>
	///// <returns>The remaining train count.</returns>
	//private int GetRemainingTrainCount() {
	//	return csvMgr.GetTimeTableLength() - csvMgr.NextTimeNumber() - 1;
	//}

	private void CreateNode()
	{
		// プレハブのコピー
		RectTransform item = GameObject.Instantiate(prefab) as RectTransform;
		item.name = "RemainingTimeNode" + nextNodeNumber;
		item.SetParent(transform, false);
	}

	bool Testa = true;

	private void WillDeleteNode(int deleteNumber)
	{
		float tmp = (float)((displayTimeData[deleteNumber] - (DateTime.Now - DateTime.Today)).TotalSeconds);
		float deleteTime = tmp + 0.0f;
		Destroy(GetNode(nextNodeNumber), deleteTime);

		if (Testa)
		{
			Testa = false;
			StartCoroutine(DisplayTimeDataDestroy(deleteTime, deleteNumber));
		}
	}

	//「コルーチン」で呼び出すメソッド
	private IEnumerator DisplayTimeDataDestroy(float deleteTime, int deleteNumber)
	{
		Debug.Log(deleteTime);
		yield return new WaitForSeconds(deleteTime);
		displayTimeData.RemoveAt(deleteNumber);
		Debug.Log(deleteNumber + "を消しました");
		Debug.Log(Time.deltaTime);
	}

	/// <summary>
	/// 動的に生成された"RemainingTimeNode[i]"(iは0..*)ゲームオブジェクト群の取得
	/// </summary>
	/// <returns>取得したNode</returns>
	/// <param name="number">取りたいゲームオブジェクトの番号</param>
	private GameObject GetNode(int number)
	{
		return GameObject.Find("RemainingTimeNode" + number);
	}

	/// <summary>
	/// デバッグ用
	/// </summary>
	private void Test()
	{
		//List<DateTime> dt = csvMgr.MultipleTime(DateTime.Now.AddHours(19), DateTime.Now.AddHours(20));
		//Debug.Log(dt.Count);
		//for (int i = 0; i < dt.Count;i++) 
		//{
		//	Debug.Log(dt[i]);
		//}
	}
}