using System;
using System.IO;
using UnityEngine;
using System.Collections;

public class IOSettingFile {
		
	/// <summary>
	/// 歩行時間の取得メソッド
	/// </summary>
	/// <param name="textFile">データ取得をしたいtxt file.</param>
	/// <returns></returns>
	public string GetWalkingTime()
	{
		return PlayerPrefs.GetString("walkTime", "0");
	}

	/// <summary>
	/// 歩行時間の書き換えメソッド
	/// </summary>
	/// <param name="textFile">書き込みを行うファイル</param>
	public void ChangeWalkingTime(int value, bool toggle, int stationValue)
	{
		PlayerPrefs.SetString("walkTime", value + "");
		PlayerPrefs.SetString("notificationBool", toggle.ToString());
		PlayerPrefs.SetInt("stationValue", stationValue);
	}
}
