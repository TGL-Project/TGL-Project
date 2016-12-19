using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UI;

public class ChangeTime : MonoBehaviour {

    /// <summary>
    /// dropdownのインスタンス
    /// </summary>
    public DropDownCallBack test = new DropDownCallBack();
    public Text currentText;

    /// <summary>
    /// 現時点の値
    /// </summary>
    public int currentTime = new int();

	// Use this for initialization
	void Start () {        
        currentTime = GetWalkingTime("ConfigWalkTime");
        Debug.Log(currentTime);
        //ChangeWalkingTime("ConfigWalkTime");
        currentText.text = currentTime.ToString()+"分";
    }
	// Update is called once per frame
	void Update () {
        
	}
    
    /// <summary>
    /// 歩行時間の取得メソッド
    /// </summary>
    /// <param name="textFile">データ取得をしたいtxt file.</param>
    /// <returns></returns>
    private int GetWalkingTime(string textFile)
    {
        int walkTime = new int();
        TextAsset txt = Resources.Load("CSV/" + textFile) as TextAsset;
        StringReader reader = new StringReader(txt.text);
        string sentence = reader.ReadToEnd();
        reader.Close();

        walkTime=Int32.Parse(sentence);

        return walkTime;
    }
    
    
    /// <summary>
    /// 歩行時間の書き換えメソッド
    /// </summary>
    /// <param name="textFile">書き込みを行うファイル</param>
    public void ChangeWalkingTime(string textFile)
    {
        int changeTime = new int();
        TextAsset txt = Resources.Load("CSV/" + textFile) as TextAsset;
        StringReader reader = new StringReader(txt.text);
        string sentence = reader.ReadToEnd();
        reader.Close();

        sentence = sentence.Remove(0);
        Debug.Log(sentence);
        //問題点
        changeTime = test.GetCurrentValue();

        //sentence = changeTime.ToString();
        //Debug.Log(sentence);
        FileInfo fi = new FileInfo(Application.persistentDataPath + "/Resources/CSV/ConfigWalkTime.txt");

        StreamWriter streamWriter = fi.CreateText();
        streamWriter.Write(changeTime.ToString());
        Debug.Log(changeTime.ToString());
        streamWriter.Flush();
        streamWriter.Close();
        
    }
    
}
