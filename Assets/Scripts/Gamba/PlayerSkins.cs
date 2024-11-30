using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkins : MonoBehaviour
{
    public static void UnlockSkin(string skinName)
    {
        Debug.Log("unlocked!");
        Debug.Log(skinName);
    }

    // TODO: temp
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) 
        {
            // TODO: menu sim
        }
    }
}
