using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StationCallBack : MonoBehaviour
{

	/// <summary>
	/// ドロップダウンリスト
	/// </summary>
	[SerializeField]
	private Dropdown dropdown = null;

	public int GetStationValue()
	{
		return dropdown.value;
	}


}
