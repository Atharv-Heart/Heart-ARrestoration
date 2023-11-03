// using UnityEngine;
// using UnityEngine.UI;

// public class DropDown : MonoBehaviour
// {
//     public GameObject[] models; // Add your 3D model prefabs here
//     private GameObject currentModel;

//     public void OnDropdownValueChanged(int val)
//     {
//         if (currentModel != null)
//         {
//             Destroy(currentModel);
//         }

//         int selectedIndex = val;
//         if (selectedIndex >= 0 && selectedIndex < models.Length)
//         {
//             currentModel = Instantiate(models[selectedIndex], Vector3.zero, Quaternion.identity);
//             currentModel.transform.position = Vector3.zero; // Place at the origin
//         }
//     }

//     void buttonPress()
//     {
//         Debug.Log("fire");
//         // OnDropdownValueChanged();
//     }
// }

using UnityEngine;
using UnityEngine.UI;

public class DropDown : MonoBehaviour
{
    public GameObject[] models; // Add your 3D model prefabs here
    private GameObject currentModel;
    private Vector3 fixedPosition = new Vector3(0, 0, 3); // Fixed position for the 3D models
    private float initialDistance;
    private Vector3 initialScale;

    public void OnDropdownValueChanged(int val)
    {
        if (currentModel != null)
        {
            Destroy(currentModel);
        }

        int selectedIndex = val;
        if (selectedIndex >= 0 && selectedIndex < models.Length)
        {
            currentModel = Instantiate(models[selectedIndex], fixedPosition, Quaternion.identity);
            initialScale = currentModel.transform.localScale;
        }
    }

    void Update()
    {
        if (currentModel != null)
        {
            HandleTouches();
        }
    }

    void HandleTouches()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            // Get movement of the finger since last frame
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            // Move object across the screen
            currentModel.transform.Translate(-touchDeltaPosition.x * 0.01f, -touchDeltaPosition.y * 0.01f, 0);
        }

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            ScaleModel(difference * 0.01f);
        }
    }

    void ScaleModel(float scaleFactor)
    {
        currentModel.transform.localScale = initialScale * (1 + scaleFactor);
    }

    void buttonPress()
    {
        Debug.Log("fire");
        // OnDropdownValueChanged();
    }
}
