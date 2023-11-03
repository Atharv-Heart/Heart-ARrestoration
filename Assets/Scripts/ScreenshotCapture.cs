using UnityEngine;
using System.IO;
using System.Linq;

public class ScreenshotCapture : MonoBehaviour
{
    // Set the desired folder path and filename for saving screenshots
    public string folderPath = "Screenshots";
    public string filenamePrefix = "Screenshot";
    public string fileExtension = "png";
    
    // Captures a screenshot and saves it to the specified folder
    void CaptureScreenshot()
    {
        // Create a directory for screenshots if it doesn't exist
        if (!System.IO.Directory.Exists(folderPath))
        {
            System.IO.Directory.CreateDirectory(folderPath);
        }

        // Generate a unique filename with a timestamp
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        string screenshotName = $"{filenamePrefix}_{timestamp}.{fileExtension}";

        // Capture the screenshot
        string screenshotPath = System.IO.Path.Combine(folderPath, screenshotName);
        ScreenCapture.CaptureScreenshot(screenshotPath);

        Debug.Log($"Screenshot saved at: {screenshotPath}");
    }


    public void ButtonPress()
    {
        Debug.Log("Btn pressed");
        CaptureScreenshot();
    }
}
