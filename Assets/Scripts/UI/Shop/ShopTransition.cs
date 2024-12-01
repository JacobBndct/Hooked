using UnityEngine;
using Cinemachine;

public class ShopTransition : MonoBehaviour
{
    public CinemachineVirtualCamera mainCamera;
    public GameObject returnButton;
    public GameObject shopUI;
    public GameObject arcadeUI;
    public GameObject fishUI;
    private CinemachineVirtualCamera activeCamera;

    private void Start()
    {
        if (returnButton != null)
        {
            returnButton.SetActive(false);
        }
    }

    public void TransitionToShop(CinemachineVirtualCamera targetCamera, GameObject shopUI)
    {
        TransitionToArea(targetCamera);

        if (shopUI != null)
        {
            shopUI.SetActive(true);
        }
    }

    
    public void TransitionToArcade(CinemachineVirtualCamera targetCamera, GameObject arcadeUIObj)
    {
        TransitionToArea(targetCamera);
        arcadeUI = arcadeUIObj;

        if (arcadeUIObj != null)
        {
            arcadeUIObj.SetActive(true);
        }
    }
    
    public void TransitionToFish(CinemachineVirtualCamera targetCamera, GameObject fishUIObj)
    {
        TransitionToArea(targetCamera);
        fishUI = fishUIObj;

        if (fishUIObj != null)
        {
            fishUIObj.SetActive(true);
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

        if (activeCamera != null)
        {
            activeCamera.Priority = 10;
        }

        //return button
        if (returnButton != null)
        {
            returnButton.SetActive(true);
        }
    }
    public void ReturnToMainView()
    {
        //camera reset
        if (activeCamera != null)
        {
            activeCamera.Priority = 0;
        }
        activeCamera = null;

        if (mainCamera != null)
        {
            mainCamera.Priority = 10;
        }
        
        if (returnButton != null)
        {
            returnButton.SetActive(false);
        }

        if (shopUI != null)
        {
            shopUI.SetActive(false);
        }
        
        if (arcadeUI != null)
        {
            arcadeUI.SetActive(false);
        }
        
        if (fishUI != null)
        {
            fishUI.SetActive(false);
        }
    }
}
