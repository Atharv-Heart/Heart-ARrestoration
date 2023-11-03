using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections;
using System.Collections.Generic;

public class ARObjectPlacement : MonoBehaviour
{
    public GameObject[] models; // Add your 3D model prefabs here
    private GameObject currentModel;
    private ARRaycastManager raycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    public void PlaceObject(int index)
    {
        if (currentModel != null)
        {
            Destroy(currentModel);
        }

        if (index >= 0 && index < models.Length)
        {
            if (raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                var hitPose = hits[0].pose;
                currentModel = Instantiate(models[index], hitPose.position, hitPose.rotation);
            }
        }
    }
}
