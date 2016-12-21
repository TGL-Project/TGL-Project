using NCMB;
using System.Collections;

public class Notification {

	public void SendPush () {
		NCMBPush push = new NCMBPush();
		push.Message = "杏鈴「おはろーございます！！にーさん！！\n5分前なんですから起きてください！」";
		push.PushToAndroid = true;
		push.SendPush();
	}
}
