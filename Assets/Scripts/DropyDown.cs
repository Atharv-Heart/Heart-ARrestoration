using System.Collections;
using System.Collections.Generic;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class DropyDown : MonoBehaviour
{
    public GameObject[] models; // Add your 3D model prefabs here
    private GameObject currentModel;
    private ARRaycastManager raycastManager;
    private Vector2 touchStart = default;
    private Vector3 initialScale;
    private float initialDistance;

    private void Awake()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    public void OnDropdownValueChanged(int val)
    {
        if (currentModel != null)
        {
            Destroy(currentModel);
        }

        int selectedIndex = val;
        if (selectedIndex >= 0 && selectedIndex < models.Length)
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            if (raycastManager.Raycast(touchStart, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                currentModel = Instantiate(models[selectedIndex], hitPose.position, hitPose.rotation);
                initialScale = currentModel.transform.localScale;
            }
        }
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStart = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved && currentModel)
            {
                Vector2 touchDeltaPosition = touch.deltaPosition;
                Vector3 rotation = new Vector3(-touchDeltaPosition.y * 0.1f, touchDeltaPosition.x * 0.1f, 0);
                currentModel.transform.Rotate(rotation, Space.World);
            }
        }
        else if (Input.touchCount == 2)
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
        if (currentModel != null)
        {
            currentModel.transform.localScale = initialScale * (1 + scaleFactor);
        }
    }

    void buttonPress()
    {
        Debug.Log("fire");
        // OnDropdownValueChanged();
    }
}
