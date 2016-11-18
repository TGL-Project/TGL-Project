using UnityEngine;
using System.Collections;

public class Camera_Aspect : MonoBehaviour {
    private Camera camera;//カメラコンポーネント

    private float width = 640f;//横
    private float height = 960f;//盾

    private float pixelPerUnit = 100f;
    
	// Use this for initialization ...コンストラクタみたいなもの
	void Awake () {
        

        
        camera = GetComponent<Camera>();
        camera.orthographicSize = height / 2f / pixelPerUnit;
	}
	
	// Update is called once per frame ...Updateメソッド。更新処理
	void Update () {

        float aspect = (float)Screen.height / (float)Screen.width;
        float bgAcpect = height / width;

        if (bgAcpect > aspect)
        {
            // 倍率
            float bgScale = height / Screen.height;
            // viewport rectの幅
            float camWidth = width / (Screen.width * bgScale);
            // viewportRectを設定
            camera.rect = new Rect((1f - camWidth) / 2f, 0f, camWidth, 1f);
        }
        else
        {
            // 倍率
            float bgScale = width / Screen.width;
            // viewport rectの幅
            float camHeight = height / (Screen.height * bgScale);
            // viewportRectを設定
            camera.rect = new Rect(0f, (1f - camHeight) / 2f, 1f, camHeight);
        }
    }
}
