using UnityEngine;
using Cinemachine;

public class ShopTransition : MonoBehaviour
{
    public CinemachineVirtualCamera mainCamera;
    public CinemachineVirtualCamera shopCamera;
    public GameObject shopUI; // Reference to the shop UI
    public GameObject returnButton; // UI button to return to the main view

    private CinemachineVirtualCamera activeCamera;

    private void Start()
    {
        // Ensure UI elements are hidden at the start
        if (shopUI != null)
        {
            shopUI.SetActive(false);
        }

        if (returnButton != null)
        {
            returnButton.SetActive(false);
        }
    }

    public void TransitionToShop()
    {
        // Switch to the shop camera
        if (shopCamera != null)
        {
            TransitionToArea(shopCamera);
        }

        // Show the shop UI
        if (shopUI != null)
        {
            shopUI.SetActive(true);
        }
    }

    public void ReturnToMainView()
    {
        // Reset the active camera and switch back to the main camera
        if (activeCamera != null)
        {
            activeCamera.Priority = 0;
            activeCamera = null;
        }

        if (mainCamera != null)
        {
            mainCamera.Priority = 10;
        }

        // Hide the shop UI
        if (shopUI != null)
        {
            shopUI.SetActive(false);
        }

        // Hide the return button
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
        if (returnButton != null)
        {
            returnButton.SetActive(true);
        }
    }
}
