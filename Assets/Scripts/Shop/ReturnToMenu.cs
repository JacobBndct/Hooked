using System.Collections;
using System.Collections.Generic;
using Managers.CustomSceneManager;
using UnityEngine;

public class ReturnToMenu : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            GameObject.Find("SceneManager").GetComponent<SceneTransitioner>().LoadScene("startscreen"); 
        }
    }
}
