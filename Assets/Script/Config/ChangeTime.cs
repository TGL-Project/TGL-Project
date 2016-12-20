using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeTime : MonoBehaviour {

	/// <summary>
	/// 保存ボタンの入力
	/// </summary>
	public void OnClickSave()
	{
		int dropdownValue = GetComponent<DropdownCallBack>().GetCurrentValue();
		IOSettingFile ioSetting = new IOSettingFile();
		ioSetting.ChangeWalkingTime(dropdownValue);


		WalkTimeData.walkTime = PlayerPrefs.GetString("walkTime", "0");
		SceneManager.LoadScene(0);
		//SceneManager.UnloadScene(1);
	}

    
}
