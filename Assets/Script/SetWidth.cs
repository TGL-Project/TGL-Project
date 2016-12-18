using UnityEngine;
using System.Collections;

public class SetWidth : MonoBehaviour {

	/// <summary>
	/// 自分のwidth
	/// </summary>
	[SerializeField]
	private RectTransform myWidth;

	// Use this for initialization
	void Start () {
		myWidth.sizeDelta = new Vector2(Screen.width, myWidth.sizeDelta.y);
	}
}
