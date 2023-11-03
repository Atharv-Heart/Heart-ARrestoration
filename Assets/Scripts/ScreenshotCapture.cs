using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenshotCapture : MonoBehaviour
{
    // Set the desired folder path and filename for saving screenshots
    public string folderPath = "Screenshots";
    public string filenamePrefix = "Screenshot";
    public string fileExtension = "png";
    public float captureFraction = 2f / 3f;

    // Captures a screenshot and saves it to the specified folder
    private void CaptureScreenshot()
    {
        // Create a directory for screenshots if it doesn't exist
        if (!System.IO.Directory.Exists(folderPath))
        {
            System.IO.Directory.CreateDirectory(folderPath);
        }

        // Generate a unique filename with a timestamp
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        string screenshotName = $"{filenamePrefix}_{timestamp}.{fileExtension}";

        // Calculate the dimensions for capturing the top portion of the screen
        int captureWidth = Screen.width;
        int captureHeight = (int)(Screen.height * captureFraction);

        // Create a texture to capture the screenshot
        Texture2D screenshotTexture = new Texture2D(captureWidth, captureHeight, TextureFormat.RGB24, false);

        // Capture the screenshot
        screenshotTexture.ReadPixels(new Rect(0, 0, captureWidth, captureHeight), 0, 0);
        screenshotTexture.Apply();

        // Encode the texture as a PNG and save it
        byte[] bytes = screenshotTexture.EncodeToPNG();
        string screenshotPath = System.IO.Path.Combine(folderPath, screenshotName);
        System.IO.File.WriteAllBytes(screenshotPath, bytes);

        Debug.Log($"Screenshot saved at: {screenshotPath}");
    }

    public void ButtonPress()
    {
        Debug.Log("Btn pressed");
        CaptureScreenshot();
        Debug.Log("FunctionWalked");
    }
}
