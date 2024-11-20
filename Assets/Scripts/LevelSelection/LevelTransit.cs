using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransit : MonoBehaviour
{
    public MapSelectionManager mapSelectionManager;

    private void OnMouseDown()
    {
        string selectedLevel = mapSelectionManager.GetSelectedLevel();
        SceneManager.LoadScene(selectedLevel);
    }
}

