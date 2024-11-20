using UnityEngine;

public class MapClickHandler : MonoBehaviour
{
    public GameObject mapUI; // Reference to the Map UI Panel

    private void OnMouseDown()
    {
        if (mapUI != null)
        {
            mapUI.SetActive(true); // Show the map UI
        }
        else
        {
            Debug.LogWarning("Map UI is not assigned!");
        }
    }
}