using UnityEngine;
using UnityEngine.UI;

public class DropDown : MonoBehaviour
{
    public GameObject[] models; // Add your 3D model prefabs here
    private GameObject currentModel;

    public void OnDropdownValueChanged(int val)
    {
        if (currentModel != null)
        {
            Destroy(currentModel);
        }

        int selectedIndex = val;
        if (selectedIndex >= 0 && selectedIndex < models.Length)
        {
            currentModel = Instantiate(models[selectedIndex], Vector3.zero, Quaternion.identity);
            currentModel.transform.position = Vector3.zero; // Place at the origin
        }
    }

    void buttonPress()
    {
        Debug.Log("fire");
        // OnDropdownValueChanged();
    }
}
