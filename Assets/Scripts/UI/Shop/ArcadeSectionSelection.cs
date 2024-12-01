using UnityEngine;

public class ArcadeSectionSelection : MonoBehaviour
{
    public ShopTransition transitionManager;
    public Cinemachine.CinemachineVirtualCamera arcadeCamera;
    public GameObject arcadeUI; // UI to toggle for the arcade machine
    public Texture2D hoverCursor;

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
            transitionManager.TransitionToArcade(arcadeCamera, arcadeUI);
        }
    }
}