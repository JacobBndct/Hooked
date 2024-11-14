using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public AreaTransitionManager transitionManager;
    public Cinemachine.CinemachineVirtualCamera targetCamera;
    public Texture2D hoverCursor;
    private Texture2D defaultCursor;
    private Collider areaCollider;

    private void Start()
    {
        defaultCursor = CursorTextureManager.Instance.GetDefaultCursor();
        areaCollider = GetComponent<Collider>();

    }

    private void OnMouseEnter()
    {
        Cursor.SetCursor(hoverCursor, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseDown()
    {
        if (targetCamera != null)
        {
            transitionManager.TransitionToArea(targetCamera, areaCollider);
        }
    }
}

