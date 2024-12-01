using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSectionSelection : MonoBehaviour
{
    public Texture2D hoverCursor;
    private void OnMouseEnter()
    {
        Cursor.SetCursor(hoverCursor, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
