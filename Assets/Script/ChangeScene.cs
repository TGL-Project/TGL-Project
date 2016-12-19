using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    /// <summary>
    /// 設定ボタン入力
    /// </summary>
    public void OnClickConfig()
    {
        SceneManager.LoadScene(1);
		//SceneManager.UnloadScene(0);
    }

    /// <summary>
    /// 戻る(破棄して戻る)を入力
    /// </summary>
    public void OnClickDestruction()
    {
        SceneManager.LoadScene(0);
        //SceneManager.UnloadScene(1);
    }
}
