using Managers.CustomSceneManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransit : MonoBehaviour
{
    public MapSelectionManager mapSelectionManager;

    private void OnMouseDown()
    {
        string selectedLevel = mapSelectionManager.GetSelectedLevel();

        if (selectedLevel != null)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            if (selectedLevel != null)
            {
                SceneTransitioner.Instance.LoadScene(selectedLevel);
            }
        }
    }
}

