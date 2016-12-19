using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DropDownCallBack : MonoBehaviour {
    //public int currentWalkTime=new int();
    public int nextWalkTime = new int();
    
        
    /// <summary>
    /// ドロップダウンの選択結果を受け取るメソッド
    /// </summary>
    /// <param name="result"></param>
    public void OnValueChanged(Dropdown dropDown)
    {
        Debug.Log(dropDown);
        nextWalkTime = dropDown.value;
        //Debug.Log(currentWalkTime);
        Debug.Log(nextWalkTime);
    }

    public int GetCurrentValue()
    {
        return nextWalkTime;
    }


}
