using System;
using System.IO;
using UnityEngine;
using System.Collections;

public class IOSettingFile {

	/// <summary>
	/// 設定ファイル名
	/// </summary>
	private static readonly string SETTINGFILENAME = "configWalkTime";
		
	/// <summary>
	/// 歩行時間の取得メソッド
	/// </summary>
	/// <param name="textFile">データ取得をしたいtxt file.</param>
	/// <returns></returns>
	public string GetWalkingTime()
	{
		TextAsset txt = Resources.Load("TXT/" + SETTINGFILENAME) as TextAsset;
		StringReader reader = new StringReader(txt.text);
		string sentence = reader.ReadToEnd();
		reader.close();
		Debug.Log(sentence);
		return (sentence == "") ? "0" : sentence;
	}


	/// <summary>
	/// 歩行時間の書き換えメソッド
	/// </summary>
	/// <param name="textFile">書き込みを行うファイル</param>
	public void ChangeWalkingTime(int value)
	{
		// パスのロード
		StreamWriter streamWriter = new StreamWriter(Application.dataPath + "/Resources/TXT/" + SETTINGFILENAME + ".txt", false);

		// 値の記述
		streamWriter.Write(value.ToString());

		streamWriter.Flush();
		streamWriter.Close();

	}
}
