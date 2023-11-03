using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Linq;

public class Integrate : MonoBehaviour
{
    public string folderPath = "Screenshots";
    public string filenamePrefix = "Screenshot";
    public string fileExtension = "png";
    public string url = "https://heart-backend.onrender.com/api/gallery";
    // public string imagePath = "C:/Users/91914/Desktop/Heart-ARrestoration/Assets/Resources/camera.jpeg"; // Set the path to your image here
    public string bearerToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI2NTQxNDgyOWYyNmM3YjEyZTdiNGY3MDgiLCJ1c2VybmFtZSI6InZpbml0aGFuYWJhcjIwQGdtYWlsLmNvbSIsImlhdCI6MTY5ODgzMTE5N30.OymeTZE0Q4wGHRmse5U0LqAwtkPb4Q7H2O0DBRrA1qg";
    public string fileName;
    
    // Captures a screenshot and saves it to the specified folder
    void CaptureScreenshot()
    {
        // Create a directory for screenshots if it doesn't exist
        Debug.Log("Capture screenshot called");

        if (!System.IO.Directory.Exists(folderPath))
        {
            System.IO.Directory.CreateDirectory(folderPath);
        }

        // Generate a unique filename with a timestamp
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        string screenshotName = $"{filenamePrefix}_{timestamp}.{fileExtension}";
        fileName = screenshotName;

        // Capture the screenshot
        string screenshotPath = System.IO.Path.Combine(folderPath, screenshotName);
        ScreenCapture.CaptureScreenshot(screenshotPath);

        Debug.Log($"Screenshot saved at: {screenshotPath}");

        CallAPI();
    }

    string FilePath()
    {
        Debug.Log("FilePath called");
        // Check if the specified folder exists
        if (Directory.Exists(folderPath))
        {
            // Get a list of all files in the folder
            string[] files = Directory.GetFiles(folderPath);

            // Check if there are any files in the folder
            if (files.Length > 0)
            {
                // Sort the files by creation time in descending order
                var sortedFiles = files.Select(f => new FileInfo(f))
                                       .OrderByDescending(f => f.CreationTime)
                                       .ToList();

                // Retrieve the name of the last (most recent) file
                string lastFileName = sortedFiles[0].Name;

                Debug.Log("Last file name: " + lastFileName);
                return lastFileName;
            }
            else
            {
                Debug.Log("No files in the folder.");
                return null;
            }
        }
        else
        {
            Debug.Log("Folder not found: " + folderPath);
            return null;
        }
    }

    IEnumerator SendData()
    {
        Debug.Log("SendData called");
    // "C:\Users\91914\Desktop\Heart-ARrestoration\Assets\Screenshots\Screenshot_2023-11-03-09-19-56.png"
        string folderPath = "Screenshots";
        string image_name = FilePath();
        // imagePath = imagePath + "/" + fileName;
        string imagePath = Path.Combine(folderPath, fileName);
        
        // imagePath = imagePath.Replace("\/", "\\");  
        // Debug.log("SDD+  "+ imagePath);

        WWWForm form = new WWWForm();
        byte[] imgData = File.ReadAllBytes(imagePath);
        form.AddBinaryData("image", imgData, "screenshot.png", "image/png");
        
        UnityWebRequest request = UnityWebRequest.Post(url, form);

        request.SetRequestHeader("Authorization", "Bearer " + bearerToken);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("Image upload complete!");
         }

    
    
    }

    public void CallAPI()
    {
        Debug.Log("CallAPI called");
        StartCoroutine(SendData());
        Debug.Log("Ayeein");
    }

    
    public void ButtonPress()
    {
        Debug.Log("Btn pressed");
        CaptureScreenshot();
    }
}
