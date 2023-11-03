using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Test : MonoBehaviour
{
    public void testing()
    {
        string imagePath = "Screenshots/Screenshot_2023-11-03-09-28-43.png";
        byte[] imgData = File.ReadAllBytes(imagePath);
        Debug.Log("Nothing Broke");
    }

    public void ButtonPress()
    {
        Debug.Log("Btn pressed");
        testing();
    }
}
