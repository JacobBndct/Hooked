using UnityEngine;

public class MapUIController : MonoBehaviour
{
    public GameObject mapUI; // Reference to the Map UI panel

    public void CloseMap()
    {
        mapUI.SetActive(false); // Hide the map UI
    }
}