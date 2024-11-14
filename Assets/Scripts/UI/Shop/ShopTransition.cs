using UnityEngine;
using Cinemachine;

public class AreaTransitionManager : MonoBehaviour
{
    public CinemachineVirtualCamera mainCamera;
    public Collider mainCollider;

    private CinemachineVirtualCamera activeCamera;
    private Collider activeAreaCollider;

    public void TransitionToArea(CinemachineVirtualCamera targetCamera, Collider areaCollider)
    {
        if (activeCamera != null)
        {
            activeCamera.Priority = 0;
        }

        activeCamera = targetCamera;
        activeCamera.Priority = 10;

        if (activeAreaCollider != null)
        {
            activeAreaCollider.enabled = true;
        }

        activeAreaCollider = areaCollider;
        activeAreaCollider.enabled = false;
    }

    public void ReturnToMainView()
    {
        if (activeCamera != null)
        {
            activeCamera.Priority = 0;
        }

        mainCamera.Priority = 10;
        activeCamera = null;

        if (activeAreaCollider != null)
        {
            activeAreaCollider.enabled = true;
            activeAreaCollider = null;
        }
    }
}