using System;
using UnityEngine;
using UnityEngine.UI;

public class BoxMotion : MonoBehaviour {

	// 初回の動作
	void Start () {

	}

	// フレームごとに更新
	void Update () {

    }

    public void Move () {
        float addX = this.transform.localPosition.x - 1;
        this.transform.localPosition =
        new Vector3(addX, this.transform.localPosition.y);
    }
}
