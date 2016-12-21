using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PushActionTimeBoolCallBack : MonoBehaviour
{

	/// <summary>
	/// ドロップダウンリスト
	/// </summary>
	[SerializeField]
	private Toggle toggle = null;

	public bool IsGetToggle()
	{
		return toggle.isOn;
	}


}
