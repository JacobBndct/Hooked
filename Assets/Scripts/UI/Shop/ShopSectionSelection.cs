using UnityEngine;

public class ShopSectionSelection : MonoBehaviour
{
    public ShopTransition transitionManager;
    public Cinemachine.CinemachineVirtualCamera targetCamera;
    public Texture2D hoverCursor;
    private Texture2D defaultCursor;

    private void Start()
    {
        defaultCursor = CursorTextureManager.Instance.GetDefaultCursor();
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
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
        if (targetCamera != null && transitionManager != null)
        {
            // Transition to the shop camera
            transitionManager.TransitionToShop();
        }
    }
}