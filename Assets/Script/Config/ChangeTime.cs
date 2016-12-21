using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeTime : MonoBehaviour {

	/// <summary>
	/// 保存ボタンの入力
	/// </summary>
	public void OnClickSave()
	{
		IOSettingFile ioSetting = new IOSettingFile();

		int dropdownValue = GetComponent<DropdownCallBack>().GetCurrentValue();
		bool isToggle = GetComponent<PushActionTimeBoolCallBack>().IsGetToggle();
		int stationValue = GetComponent<StationCallBack>().GetStationValue();

		ioSetting.ChangeWalkingTime(dropdownValue, isToggle, stationValue);
		SceneManager.LoadScene(0);
		//SceneManager.UnloadScene(1);
	}

    
}
