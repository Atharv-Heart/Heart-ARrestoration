using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.IO;

public class ApiRequest : MonoBehaviour
{
    public string url = "https://heart-backend.onrender.com/api/gallery";
    public string imagePath = "C:/Users/91914/Desktop/Heart-ARrestoration/Assets/Resources/camera.jpeg"; // Set the path to your image here
    public string bearerToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI2NTQxNDgyOWYyNmM3YjEyZTdiNGY3MDgiLCJ1c2VybmFtZSI6InZpbml0aGFuYWJhcjIwQGdtYWlsLmNvbSIsImlhdCI6MTY5ODgzMTE5N30.OymeTZE0Q4wGHRmse5U0LqAwtkPb4Q7H2O0DBRrA1qg";

    IEnumerator SendData()
    {
        // imagePath = "C:/Users/akash/Downloads/aniDurga.jpg";
        WWWForm form = new WWWForm();
        byte[] imgData = File.ReadAllBytes(imagePath);
        form.AddBinaryData("image", imgData, "screenshot.jpg", "image/jpg");
        
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
        Debug.Log("Button Pressed");
        StartCoroutine(SendData());
        Debug.Log("Ayeein");
    }

    public void TestButton()
    {
        Debug.Log("Button workings");
        CallAPI();
        // DisplayImage();
    }
}