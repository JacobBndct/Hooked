using UnityEngine;
using Cinemachine;

public class ShopTransition : MonoBehaviour
{
    public CinemachineVirtualCamera mainCamera;
    public CinemachineVirtualCamera shopCamera;
    public GameObject shopUI;
    public GameObject returnButton;

    private CinemachineVirtualCamera activeCamera;

    private void Start()
    {
        //initial UI state
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
        //show ui
        if (shopCamera != null)
        {
            TransitionToArea(shopCamera);
        }
        
        if (shopUI != null)
        {
            shopUI.SetActive(true);
        }
    }

    public void ReturnToMainView()
    {
        //camera reset
        if (activeCamera != null)
        {
            activeCamera.Priority = 0;
            activeCamera = null;
        }

        if (mainCamera != null)
        {
            mainCamera.Priority = 10;
        }

        //hide ui
        if (shopUI != null)
        {
            shopUI.SetActive(false);
        }
        
        if (returnButton != null)
        {
            returnButton.SetActive(false);
        }
    }
    //camera movement logic
    public void TransitionToArea(CinemachineVirtualCamera targetCamera)
    {
        if (activeCamera != null)
        {
            activeCamera.Priority = 0;
        }

        activeCamera = targetCamera;
        activeCamera.Priority = 10;

        //return button
        if (returnButton != null)
        {
            returnButton.SetActive(true);
        }
    }
}
