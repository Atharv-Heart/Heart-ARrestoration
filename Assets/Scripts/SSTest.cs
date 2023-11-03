using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SSTest : MonoBehaviour
{
    // Capture and send a screenshot to an API
    private bool capturingScreenshot = false;

    // Capture and send a screenshot to an API
    public void CaptureAndSendScreenshot()
    {
        if (!capturingScreenshot)
        {
            StartCoroutine(UploadScreenshot());
        }
    }

    private IEnumerator UploadScreenshot()
    {
        capturingScreenshot = true;

        // Yield a frame to make sure you are inside a rendering frame
        yield return new WaitForEndOfFrame();

        // Capture a screenshot
        Texture2D screenshot = new Texture2D(Screen.width, Screen.height);
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        // Encode the screenshot to PNG format
        byte[] screenshotBytes = screenshot.EncodeToPNG();

        // Create a UnityWebRequest to send the screenshot
        string apiUrl = "https://heart-backend-tfru.onrender.com/api/gallery";
        // UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");

        // UploadHandlerRaw uploadHandler = new UploadHandlerRaw(screenshotBytes);
        // uploadHandler.contentType = "image/png";
        // request.uploadHandler = uploadHandler;

        WWWForm form = new WWWForm();
        form.AddBinaryData("image", screenshotBytes, "screenshot.png", "image/png");
        UnityWebRequest request = UnityWebRequest.Post(apiUrl, form);
        // Set headers and other request parameters if needed
        // request.SetRequestHeader("Authorization", "Bearer YOUR_TOKEN");

        // Send the POST request
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            Debug.Log("Screenshot sent successfully.");
        }

        capturingScreenshot = false;
    }

    public void BtnPress()
    {
        Debug.Log("Button Pressed");
        CaptureAndSendScreenshot();
    }
}
