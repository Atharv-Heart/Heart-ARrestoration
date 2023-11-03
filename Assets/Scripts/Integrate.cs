using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public void OpenApp() {
		string bundleId = "com.whatsapp"; // your target bundle id
		AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject ca = up.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaObject packageManager = ca.Call<AndroidJavaObject>("getPackageManager");

		//if the app is installed, no errors. Else, doesn't get past next line
		AndroidJavaObject launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage",bundleId);

		ca.Call("startActivity",launchIntent);

		up.Dispose();
		ca.Dispose();
		packageManager.Dispose();
		launchIntent.Dispose();
	}

    public void ButtonPress()
    {
        Debug.Log("Btn pressed");
        OpenApp();
    }
}
