using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera targetCamera;
    public Texture2D hoverCursor;
    private Texture2D defaultCursor;

    private void Start()
    {
        defaultCursor = CursorTextureManager.Instance.GetDefaultCursor();
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
            targetCamera.Priority = 10; // Prioritize this camera view
        }
    }
}

