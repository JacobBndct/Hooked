/**
 * Class which implements the pet fish logic.
 * 
 * @author Marina (Mars) Semenova
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetFishController : MonoBehaviour
{
    // resources
    private static Material fishMat = ResourcesLoader.fishMat;
    public static string curSkin;
    
	void Awake() {
		// reset skin to def (last equipped skin not saved)
		curSkin = "def";
	}

    void Start()
    {
        SetSkin(curSkin); // equip current skin
    }

    /**
     * Updates fish model to match curSkin.
     *
     * @param skin Name - Name of the skin.
     */
    public void SetSkin(string skinName)
    {
        Skin skinObj = PlayerSkins.GetSkin(skinName);
		Skin prevSkinObj = PlayerSkins.GetSkin(curSkin);
        
        // texture
        Texture2D txt = skinObj.texture;
        if (txt != null)
        {
            fishMat.SetTexture("_MainTex", txt);
        }
        else
        {
            fishMat.SetTexture("_MainTex", ResourcesLoader.defTxt);
        }

        // accessories
		GameObject prevAccessories = prevSkinObj.accessories;
		if (prevAccessories != null) // remove curr accessories if any
        {
            prevAccessories.SetActive(false);
        }

        GameObject accessories = skinObj.accessories;
        if (accessories != null)
        {
            accessories.SetActive(true);
        }

        curSkin = skinName;
    }
}
