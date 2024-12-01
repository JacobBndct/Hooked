using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetFishController : MonoBehaviour
{
    // resources
    private static Material fishMat = ResourcesLoader.fishMat;
    public static string curSkin;
    
	void Awake() {
		// reset skin to def (not saving)
		curSkin = "def";
	}

    void Start()
    {
        ResourcesLoader.KYSAwake(); // TODO: remove
        fishMat = ResourcesLoader.fishMat; // TODO: remove
        // get curr skin from material
        SetSkin(curSkin); // call jic model doesn't match
    }

    /**
     * Updates fish model to match curSkin.
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
		if (prevAccessories != null)
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
