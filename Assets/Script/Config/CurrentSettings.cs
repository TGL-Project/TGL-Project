using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CurrentSettings : MonoBehaviour {

	IOSettingFile ioSetting;

	// 開始時に現在の設定の分表示を変更
	void Start () 
	{
		ioSetting = new IOSettingFile();
	}

	void Update()
	{
		// データを読み出してxx分に代入する
		GetComponent<Text>().text = ioSetting.GetWalkingTime();
	}
}
