using UnityEngine;

public class CursorTextureManager : MonoBehaviour
{
    public static CursorTextureManager Instance;

    [SerializeField] private Texture2D defaultCursor;
    [SerializeField] private Texture2D hoverCursor;

    private void Awake()
    {
        // If an instance already exists, destroy the duplicate
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;

        
        if (transform.parent != null)
        {
            transform.parent = null;
        }
        
        DontDestroyOnLoad(gameObject);

        // Set the default cursor on startup
        SetDefaultCursor();
    }

    public Texture2D GetDefaultCursor()
    {
        return defaultCursor;
    }

    public void SetDefaultCursor()
    {
        if (defaultCursor != null)
        {
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Debug.LogWarning("Default cursor texture is missing!");
        }
    }

    public void SetHoverCursor()
    {
        if (hoverCursor != null)
        {
            Cursor.SetCursor(hoverCursor, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Debug.LogWarning("Hover cursor texture is missing!");
        }
    }
}