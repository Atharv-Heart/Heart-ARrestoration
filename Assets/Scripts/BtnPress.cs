using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnPress : MonoBehaviour
{
    public string appPackageName = "com.android.chrome"; // Replace with your desired package name

    public void OpenApp()
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject packageManager = currentActivity.Call<AndroidJavaObject>("getPackageManager");
        AndroidJavaObject launchIntent = null;

        try
        {
            launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", appPackageName);
            if (launchIntent != null)
            {
                currentActivity.Call("startActivity", launchIntent);
            }
            else
            {
                Debug.Log("App not found. Make sure the package name is correct.");
            }
        }
        catch (AndroidJavaException e)
        {
            Debug.Log("Error opening app: " + e);
        }
    }

    public void OpenAnotherApp()
    {
        // Replace "package_name" with the package name of the app you want to open
        // For example, for Google Maps: "com.google.android.apps.maps"
        string packageName = "com.android.chrome";

        // Create the URL using the package name
        string storeUrl = "market://details?id=" + packageName;

        // Open the URL
        Application.OpenURL(storeUrl);
    }

    public void OpenWhatsApp()
    {
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_VIEW"));
        intentObject.Call<AndroidJavaObject>("setPackage", "com.whatsapp"); // WhatsApp package name

        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "Your message to share on WhatsApp");

        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        currentActivity.Call("startActivity", intentObject);
    }

    // public void OpenTheWhatsApp()
    // {
    //     // Check if WhatsApp is installed
    //     if (!AndroidPlugin.IsPackageInstalled("com.whatsapp"))
    //     {
    //         // WhatsApp is not installed
    //         Debug.Log("WhatsApp is not installed");
    //         return;
    //     }

    //     // Create a new intent
    //     AndroidIntent intent = new AndroidIntent();

    //     // Set the intent action
    //     intent.SetAction(AndroidIntent.Action.Send);

    //     // Set the intent package name
    //     intent.SetPackage("com.whatsapp");

    //     // Set the intent text
    //     intent.SetExtra(AndroidIntent.Extra.Text, "Your message to share on WhatsApp");

    //     // Start the activity
    //     AndroidPlugin.StartActivity(intent);
    // }

    public void ButtonPress()
    {
        Debug.Log("Btn pressed");
        OpenApp();
    }

    // public void ButtonPress2()
    // {
    //     Debug.Log("Btn pressed");
    //     OpenTheWhatsApp();
    // }
}

