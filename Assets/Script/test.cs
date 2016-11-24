using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	private Vector2 sd,sdt;

	// Use this for initialization
	void Start () {
		sd = GameObject.Find("RemainingTimeBackGround").GetComponent<RectTransform>().sizeDelta;
		sdt = GameObject.Find("RemainingTime1").GetComponent<RectTransform>().localScale;
	}

	// Update is called once per frame
	void Update () {
		// float addX = this.transform.localPosition.x + 1;
		// this.transform.localPosition =
		// new Vector2(addX, this.transform.localPosition.y);

		if (sd.x > 0 && sd.y > -1) {
			sd.y -= 4;
			sd.x -= 4;
			GameObject.Find("RemainingTimeBackGround").GetComponent<RectTransform>().sizeDelta = sd;
		}

		if ( sdt.x > 0 && sdt.y > 0) {
			sdt.x -= 0.03f;
			sdt.y -= 0.03f;
			GameObject.Find("RemainingTime1").GetComponent<RectTransform>().localScale = sdt;
		}

		if (sd.x <= 0 && sd.y <= 0 || sdt.x <= 0 && sdt.y <= 0) {
			if (GameObject.Find("RemainingTimeData") != null) {
				Destroy(GameObject.Find("RemainingTimeData"));
			}
		}
		// Vector2 pos = GetComponent<RectTransform>().anchoredPosition;
		// pos.x *= 2;
		// GetComponent<RectTransform>().anchoredPosition = pos;
	}
}
