using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/*
 * プレハブのコピー，プレハブへのテキスト代入
 */
public class ScrollController : MonoBehaviour {

	[SerializeField]
	private RectTransform prefab = null;

	private CsvManager csvMgr = new CsvManager();
	private List<DateTime> nextTrainDate = new List<DateTime>();
	private List<TimeSpan> diff = new List<TimeSpan>(); // 次の電車到着予定時刻 - 現在時刻 = 残り時間(diff)
	private int firstNode = 0; //getNodeで動的にfor文のスタートを変更させるため

	/*
	 * プレハブの複製及びContent以下への挿入
	 */
	void Start () {
		Initialize(); //初期化用
	}

	/*
	 * 初期化用メソッド
	 * 最初とすべての電車が行ってしまったときに読み込む
	 */
	private void Initialize() {
		for (int i = 0; i < GetRemainingTrainCount(); i++) {
			//プレハブのコピー
			RectTransform item = GameObject.Instantiate(prefab) as RectTransform;
			item.name = "RemainingTimeNode" + i;
			item.SetParent(transform, false);

			// 次の電車時刻から終電までを収納
			if (i == 0) {
				nextTrainDate.Add(csvMgr.NextTime(DateTime.Now));
			} else {
				nextTrainDate.Add(csvMgr.NextTime(nextTrainDate[i-1]));
			}

			// 最後のノード
			if (i == GetRemainingTrainCount() - 1) {
				// item.GetComponent<Image>().color = new Color32(200,200,200,255);
			}

			// 差分を残り時間配列に収納
			diff.Insert(i, nextTrainDate[i] - DateTime.Now);
		}
	}

	// Update is called once per frame
	void Update () {

		// 全てのの電車が行ったときに初期化する
		if(nextTrainDate.Count == 0) {
			Initialize();
		}

		// getNodeの番号は0を消してもそこを取得するためノードが参照できずエラーが起きる
		// →int i = 0　の部分を動的に変えてやれば良い
		for(int i = 0; i < nextTrainDate.Count; i++) {
			Debug.Log(diff.Count);
			Debug.Log(nextTrainDate.Count);

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

			Debug.Log(diff.Count);
			Debug.Log(nextTrainDate.Count);
		}
	}

	private GameObject GetNode(int number) {
		return GameObject.Find("RemainingTimeNode" + number);
	}

	/*
	 * ここで残りの電車本数の算出を行う
	 * ScrollControllerが呼び出す
	 */
	private int GetRemainingTrainCount() {
		return csvMgr.GetTimeTableLength() - csvMgr.NextTimeNumber() - 1;
	}
}
