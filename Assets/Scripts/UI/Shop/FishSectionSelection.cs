using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSectionSelection : MonoBehaviour
{
    public ShopTransition transitionManager;
    public Cinemachine.CinemachineVirtualCamera fishCamera;
    public GameObject fishUI; // UI to toggle for the arcade machine
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
            transitionManager.TransitionToFish(fishCamera, fishUI);
        }
    }
}
