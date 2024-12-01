/**
 * Class which implements the pet fish skins menu logic.
 * 
 * @author Marina (Mars) Semenova
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishSkinsMenuController : MonoBehaviour
{
	// refs
    private static AudioSource audSrc;
    private static PetFishController petFishController;
    
    void Awake()
    { 
        // get pet fish controller
        GameObject petFish = GameObject.FindGameObjectsWithTag("pet fish")[0];
        petFishController = petFish.GetComponent<PetFishController>();
        
        // get audio src
        audSrc = GetComponent<AudioSource>();
    }
    
    void OnEnable()
    {
        // update whether a skin is greyed out based on whether it is unlocked
        Dictionary<string, Skin> skinDict = PlayerSkins.GetSkinDict();
        
        foreach(KeyValuePair<string, Skin> skin in skinDict)
        {
            Skin curSkin = skin.Value;
            if (curSkin.unlocked)
            {
                curSkin.dispSlot.color = Color.white;
            }
        }
    }

    /**
     * Method which sets the pet fish skin when a skin slot is clicked.
     *
     * @param skinSlotObj - GameObject of the skin slot which called this method.
     */
    public static void SetSkin(GameObject skinSlotObj)
    {
        // extract name
        Image skinSlotImg = skinSlotObj.GetComponentInChildren<Image>();
        string skinName = skinSlotImg.sprite.name;
        
        if (PlayerSkins.IsUnlocked(skinName))
        {
            petFishController.SetSkin(skinName);
        }
        else
        {
            audSrc.Play();
        }
    } 
}
