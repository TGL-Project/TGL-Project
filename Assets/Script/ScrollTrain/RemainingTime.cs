using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RemainingTime {

	private GameObject obj;
	private TimeSpan timeData;

	public RemainingTime(GameObject obj,TimeSpan timeData)
	{
		this.obj = obj;
		this.timeData = timeData;
	}

	public GameObject GetGameObj()
	{
		return this.obj;
	}

	public TimeSpan GetDiffTime()
	{
		return timeData - (DateTime.Now - DateTime.Today);
	}

	public TimeSpan GetTime()
	{
		return timeData;
	}

	public Text GetText()
	{
		return GetGameObj().transform.FindChild("RemainingTimeText").gameObject.GetComponent<Text>();
	}
}
