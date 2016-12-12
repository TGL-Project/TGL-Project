using System;
using UnityEngine;
using UnityEngine.UI;
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
	/// 次の電車の時刻
	/// </summary>
	private List<DateTime> nextTrainDate = new List<DateTime>();

	/// <summary>
	/// 残り時間(diff)
	/// (次の電車到着予定時刻 - 現在時刻)を配列にして入れる
	/// </summary>
	private List<TimeSpan> diff = new List<TimeSpan>();

	/// <summary>
	/// The first node.
	/// getNodeで動的にfor文のスタートを変更させるために使用
	/// </summary>
	private int firstNode = 0;

	/// <summary>
	/// 初期化用
	/// プレハブの複製及びContent以下への挿入
	/// </summary>
	void Start ()
	{
		csvMgr.ReadCsv();
		Initialize(); //初期化用
	}

	/// <summary>
	///	初期化用メソッド
	/// 最初とすべての電車が行ってしまったときに読み込む
	/// </summary>
	private void Initialize()
	{
		for (int i = 0; i < GetRemainingTrainCount(); i++)
		{
			// プレハブのコピー
			RectTransform item = GameObject.Instantiate(prefab) as RectTransform;
			item.name = "RemainingTimeNode" + i;
			item.SetParent(transform, false);

			// 次の電車時刻から終電までを収納
			if (i == 0)
			{
				nextTrainDate.Add(csvMgr.NextTime(DateTime.Now));
			}
			else
			{
				nextTrainDate.Add(csvMgr.NextTime(nextTrainDate[i-1]));
			}

			// 最後のノード
			if (i == GetRemainingTrainCount() - 1)
			{
				// item.GetComponent<Image>().color = new Color32(200,200,200,255);
			}

			// 差分を残り時間配列に収納
			diff.Insert(i, nextTrainDate[i] - DateTime.Now);
		}
	}

	/// <summary>
	/// Unityのアップデート関数
	/// </summary>
	void Update () {

		// 全てのの電車が行ったときに初期化する
		if(nextTrainDate.Count == 0) {
			Initialize();
		}

		// getNodeの番号は0を消してもそこを取得するためノードが参照できずエラーが起きる
		// →int i = 0 の部分を動的に変えてやれば良い
		for(int i = 0; i < nextTrainDate.Count; i++) {

			// 入れる前に古いデータを削除
			diff.RemoveAt(i);
			// 差分を残り時間配列に収納
			diff.Insert(i, nextTrainDate[i] - DateTime.Now);
			// i番目の時間表示ノードを取得
			GameObject node = GetNode(firstNode + i);
			GameObject nodeChild = node.transform.FindChild("RemainingTimeText").gameObject;
			Text text = nodeChild.GetComponent<Text>();

			// +""で文字列変換をした後UniyUIに代入
				// デフォルトは分表示
				text.text = diff[i].Minutes + "分";
				// 1時間以上の表示
				if (diff[i].TotalSeconds >= 3600) {
					text.text = diff[i].Hours + "時間" + diff[i].Minutes + "分";
				}
				// 1分未満の表示
				if (diff[i].TotalSeconds <= 60) {
					text.text = diff[i].Seconds + "秒";
				}
		}

		// 差が0以下(電車が行ってしまったとき)
		if (diff[0].TotalSeconds < 0) {

			diff.RemoveAt(0); // トップの表示用データを削除
			nextTrainDate.RemoveAt(0); // 過ぎた電車の削除
			Destroy(GetNode(firstNode)); // トップのノードを削除

			firstNode++; //オブジェクトが消えるため次のオブジェクトを指定

		}
	}

	/// <summary>
	/// 動的に生成された"RemainingTimeNode[i]"(iは0..*)ゲームオブジェクト群の取得
	/// </summary>
	/// <returns>取得したNode</returns>
	/// <param name="number">取りたいゲームオブジェクトの番号</param>
	private GameObject GetNode(int number) {
		return GameObject.Find("RemainingTimeNode" + number);
	}

	/// <summary>
	/// ここで残りの電車本数の算出を行う
	/// </summary>
	/// <returns>The remaining train count.</returns>
	private int GetRemainingTrainCount() {
		return csvMgr.GetTimeTableLength() - csvMgr.NextTimeNumber() - 1;
	}
}
