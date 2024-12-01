using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishSkinsMenuController : MonoBehaviour
{
    private static AudioSource audSrc;
    private static PetFishController petFishController;
    
    void Awake()
    { 
        ResourcesLoader.KYSAwake(); // TODO: remove
        // get pet fish controller
        GameObject petFish = GameObject.FindGameObjectsWithTag("pet fish")[0];
        petFishController = petFish.GetComponent<PetFishController>();
        
        // get audio src
        audSrc = GetComponent<AudioSource>();
    }
    
    void OnEnable()
    {
        // update skin visibility
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
     * Method called by clicking on a skin.
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
