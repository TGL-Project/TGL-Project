using NCMB;
using System.Collections;

public class Notification {

	public void SendPush () {
		NCMBPush push = new NCMBPush();
		push.Message = "杏鈴「おはろーございます．にーさん！5分前ですよーっ！！」";
		push.PushToAndroid = true;
		push.SendPush();
	}
}
