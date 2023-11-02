using UnityEngine;

public class ScreenshotCapture : MonoBehaviour
{
    // Set the desired folder path and filename for saving screenshots
    public string folderPath = "Screenshots";
    public string filenamePrefix = "Screenshot";
    public string fileExtension = "png";

    // Capture a screenshot when a specific key is pressed, e.g., the "P" key
    public KeyCode captureKey = KeyCode.P;

    // Update is called once per frame
    void Update()
    {
        // Check if the capture key is pressed
        if (Input.GetKeyDown(captureKey))
        {
            CaptureScreenshot();
        }
    }

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
}
