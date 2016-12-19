using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    public DropDownCallBack test = new DropDownCallBack();
   

    /// <summary>
    /// 設定ボタン入力
    /// </summary>
    public void OnClickConfig()
    {
        SceneManager.LoadScene(1);
        SceneManager.UnloadScene(0);
    }

    /// <summary>
    /// 戻る(破棄して戻る)を入力
    /// </summary>
    public void OnClickDestruction()
    {
        SceneManager.LoadScene(0);
        SceneManager.UnloadScene(1);
    }

    /// <summary>
    /// 保存ボタンの入力
    /// </summary>
    public void OnClickSave()
    {
        GetComponent<ChangeTime>().ChangeWalkingTime("ConfigWalkTime");
        SceneManager.LoadScene(0);
        SceneManager.UnloadScene(1);
    }


    public void TestCallBack()
    {
        if (test)
        {
            Debug.Log(test.GetCurrentValue());
            //
            GetComponent<ChangeTime>().ChangeWalkingTime("ConfigWalkTime");
        }
        else
        {
            Debug.Log("Null");
        }
    }
}
