/**
 * Class which keeps track of skin data and whether a player has unlocked
 * them or not.
 * 
 * @author Marina (Mars) Semenova
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

// struct for skin data
public struct Skin
{
    public GameObject accessories;
    public Texture2D texture;
    public Image dispSlot;
    public bool unlocked;
    public int rarity;
}

public class PlayerSkins : MonoBehaviour
{
    private static Dictionary<string, Skin> skinDict;
	private const string UNLOCKED_SKINS_FILE = "Assets/Resources/Pet Fish/unlockedskins.txt";
    
    void Awake()
    {
        // init skin dict
        CreateSkinDict();
        
        // get + add textures to entries
        AddSkinTextures();

        // get + add accessories to entries
        AddSkinAccessories();
        
        // get + add skin UI display slots to entries
        if (SceneManager.GetActiveScene().name == "Shop")
        {
            GetSkinDisplaySlots();
        }

        // load unlocked skins from text file
        LoadUnlockedSkins();
    }

    /**
     * Helper method to init the skins dictionary.
     */
    private static void CreateSkinDict()
    {
		skinDict = new Dictionary<string, Skin>();

        // add def skin
        Skin defSkin = new Skin();
        defSkin.unlocked = true;
        defSkin.texture = ResourcesLoader.defTxt;
        defSkin.rarity = -1;
        skinDict.Add("def", defSkin);

        Skin newSkin;
        Texture2D[][] skinsSlotTxts = ResourcesLoader.skinsSlotTxts;
        string newSkinName;
        for (int x = 0; x < skinsSlotTxts.Length; x++)
        {
            for (int y = 0; y < skinsSlotTxts[x].Length; y++)
            {
                newSkin = new Skin();
                newSkinName = skinsSlotTxts[x][y].name;
                newSkinName = newSkinName.Substring(0, newSkinName.Length - 7);
                newSkin.rarity = x;
                skinDict.Add(newSkinName, newSkin);
            }
        }
    }

    /**
     * Helper method to add skin textures to their dictionary entries.
     */
    private static void AddSkinTextures()
    {
        Texture2D[] skinsTxts = ResourcesLoader.skinsTxts;
        
		string curName;
		Skin curSkin;
        for (int x = 0; x < skinsTxts.Length; x++) {
			curName = skinsTxts[x].name;
			curName = curName.Substring(0, curName.Length - 3);
			curSkin = skinDict[curName];
			curSkin.texture = skinsTxts[x];
			skinDict[curName] = curSkin;
		}
    }

    /**
     * Helper method to add skin accessories to their dictionary entries.
     */
    private static void AddSkinAccessories()
    {        
        GameObject accessories = GameObject.FindGameObjectWithTag("accessories spawn");
        Transform[] accessoriesObjs = accessories.GetComponentsInChildren<Transform>(true);

		string skinName;
        Skin curSkin;
		GameObject accessory;
        for (int x = 0; x < accessoriesObjs.Length; x++)
        {
			accessory = accessoriesObjs[x].gameObject;
            skinName = accessory.name;
			if (skinDict.ContainsKey(skinName)) 
			{
            	curSkin = skinDict[skinName];
            	curSkin.accessories = accessory;
				skinDict[skinName] = curSkin;
			}
        }
    }

    /**
     * Helper method to add skin UI display slots to their dictionary entries.
     */
    private static void GetSkinDisplaySlots()
    {
		GameObject canvas = GameObject.FindGameObjectWithTag("canvas");
        Image[] skinSlots = canvas.GetComponentsInChildren<Image>(true);

        string skinName;
        Skin curSkin;
        for (int x = 0; x < skinSlots.Length; x++)
        {
			if (skinSlots[x].sprite != null && skinSlots[x].sprite.texture != null) 
			{
            	skinName = skinSlots[x].sprite.texture.name;
				if (skinDict.ContainsKey(skinName)) 
				{
            		curSkin = skinDict[skinName];
            		curSkin.dispSlot = skinSlots[x];
					skinDict[skinName] = curSkin;
				}
			}
        }
    }
    
    /**
     * Helper method to load unlocked skins from a text file.
     */
    private static void LoadUnlockedSkins()
    {
        StreamReader reader = new StreamReader(UNLOCKED_SKINS_FILE);

        Skin curSkin;
        string inp;
        while(!reader.EndOfStream)
        {
            inp = reader.ReadLine();
            curSkin = skinDict[inp];
            curSkin.unlocked = true;
			skinDict[inp] = curSkin;
        }
    }
    
    /**
     * Method called to unlock a skin.
     *
     * @param skinName - Name of the skin.
	 * @return Whether the skin was already unlocked.
     */
    public static bool UnlockSkin(string skinName)
    {		
        Skin curSkin = skinDict[skinName];

		if (!curSkin.unlocked) {
			// update dict
        	curSkin.unlocked = true;
			skinDict[skinName] = curSkin;
        
        	// write to file
        	StreamWriter writer = new StreamWriter(UNLOCKED_SKINS_FILE, true);
        	writer.WriteLine(skinName);
        	writer.Close();

			return false;
		}
			
		return true;
    }

    /**
     * Getter for a skin's Skin object.
     *
     * @param skinName - Name of the skin.
     * @return Skin object of the skin.
     */
    public static Skin GetSkin(string skinName)
    {
        Skin curSkin = skinDict[skinName];
        return curSkin;
    } 

    /**
     * Method which checks whether a skin is unlocked.
     *
     * @param skinName - Name of the skin.
     * @return Whether the skin is unlocked.
     */
    public static bool IsUnlocked(string skinName)
    {
        Skin curSkin = skinDict[skinName];
        return curSkin.unlocked;
    }

    /**
     * Getter for the skins dictionary.
     *
     * @return skinDict object.
     */
    public static Dictionary<string, Skin> GetSkinDict()
    {
        return skinDict;
    }
}
