using UnityEngine;

public class ShopSectionSelection : MonoBehaviour
{
    public ShopTransition transitionManager;
    public Cinemachine.CinemachineVirtualCamera shopCamera;
    public Texture2D hoverCursor;
    public GameObject shopUI;
    private void OnMouseEnter()
    {
        Cursor.SetCursor(hoverCursor, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseDown()
    {
        if (transitionManager != null)
        {
            // Transition to the shop camera
            transitionManager.TransitionToShop(shopCamera, shopUI);
        }
    }
}