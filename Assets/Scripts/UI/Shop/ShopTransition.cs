using UnityEngine;
using Cinemachine;

public class ShopTransition : MonoBehaviour
{
    public CinemachineVirtualCamera mainCamera;
    public GameObject returnButton; // UI button to return to the main view

    private CinemachineVirtualCamera activeCamera;
    
    private void Start()
    {
        // Ensure the return button is hidden at the start
        if (returnButton != null)
        {
            returnButton.SetActive(false);
        }
    }
    public void TransitionToArea(CinemachineVirtualCamera targetCamera)
    {
        // Reset priority of the currently active camera if there is one
        if (activeCamera != null)
        {
            activeCamera.Priority = 0;
        }

        // Set the new area camera as the active one and give it higher priority
        activeCamera = targetCamera;
        activeCamera.Priority = 10;

        // Show the return button to allow transitioning back to the main view
        returnButton.SetActive(true);
    }

    public void ReturnToMainView()
    {
        // Reset the priority of the active camera to stop viewing the area
        if (activeCamera != null)
        {
            activeCamera.Priority = 0;
            activeCamera = null;
        }

        // Set the main camera as the active one with higher priority
        mainCamera.Priority = 10;

        // Hide the return button when back in the main view
        returnButton.SetActive(false);
    }
}