using UnityEngine;

public class CursorTextureManager : MonoBehaviour
{
    public static CursorTextureManager Instance;

    [SerializeField] private Texture2D defaultCursor;

    public Texture2D GetDefaultCursor()
    {
        return defaultCursor;
    }

    public void SetDefaultCursor(Texture2D cursor)
    {
        defaultCursor = cursor;
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }
}