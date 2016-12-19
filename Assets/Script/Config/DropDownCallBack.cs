using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DropdownCallBack : MonoBehaviour {
    
	/// <summary>
	/// ドロップダウンリスト
	/// </summary>
	[SerializeField]
	private Dropdown dropdown = null;

    public int GetCurrentValue()
    {
        return dropdown.value;
    }


}
