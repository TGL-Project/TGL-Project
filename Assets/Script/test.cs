using NCMB;
using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		NCMBPush push = new NCMBPush();
		push.Message = "おはろーございます";
		push.PushToAndroid = true;
		//or
		NCMBPush push2 = new NCMBPush()
		{
			Message = "test message",
			PushToAndroid = true,
		};
		push.SendPush();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//	private void sendPush() throws JSONException
//	{
//		NCMBPush push = new NCMBPush();
//    push.sendInBackground(new DoneCallback()
//	{
//		@Override

//		public void done(NCMBException e)
//	{
//		if (e != null)
//		{
//			// エラー処理
//		}
//		else
//		{
//			// プッシュ通知登録後の操作
//		}
//	}
//});
//}
}
