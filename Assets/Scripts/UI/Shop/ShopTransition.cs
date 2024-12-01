using UnityEngine;
using Cinemachine;

public class ShopTransition : MonoBehaviour
{
    public CinemachineVirtualCamera mainCamera;
    public GameObject returnButton;

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

    
    public void TransitionToArcade(CinemachineVirtualCamera targetCamera, GameObject arcadeUI)
    {
        TransitionToArea(targetCamera);

        if (arcadeUI != null)
        {
            arcadeUI.SetActive(true);
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
    }
}
