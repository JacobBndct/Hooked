using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ResourcesLoader : MonoBehaviour
{
    // slot textures
    public static Texture2D[][] skinsSlotTxts; 
    public static int[] skinCount;
    public static Texture2D[] wormsSlotTxts; 

	void Awake()
    {
        // load skins
        skinCount = new int[3];
        skinsSlotTxts = new Texture2D[3][];
        LoadSkinsSlotTxts("Common", 0);
        LoadSkinsSlotTxts("Rare", 1);
        LoadSkinsSlotTxts("Epic", 2);
        
        // load worms
        wormsSlotTxts = new Texture2D[8];
        LoadWormsSlotTxts();
    }
    
    /**
     * Helper method to load skin textures from /Resources
     *
     * @param rarityStr - Name of the rarity, used to access the correct folder.
     * @param rarity - Index of rarity in arrays.
     */
    private static void LoadSkinsSlotTxts(string rarityStr, int rarity)
    {
        string[] files = Directory.GetFiles("Assets/Resources/Slot Machine/textures/Skins/" + rarityStr, "*", SearchOption.TopDirectoryOnly);
        skinCount[rarity] = files.Length / 2;
        skinsSlotTxts[rarity] = new Texture2D[skinCount[rarity]];

        string fileName;
        for (int x = 0; x < skinCount[rarity]; x++)
        {
            fileName = Path.GetFileName(files[x * 2]);
            skinsSlotTxts[rarity][x] = Resources.Load<Texture2D>("Slot Machine/textures/Skins/" + rarityStr + "/" + fileName.Substring(0,fileName.Length - 4));
        }
    }

    /**
     * Helper method to load worm textures from /Resources
     */
    private static void LoadWormsSlotTxts()
    {
        for (int x = 0; x < 8; x++)
        {
            wormsSlotTxts[x] = Resources.Load<Texture2D>("Slot Machine/textures/Worms/worm" + (x + 1) + "slottxt");
        }
    }
}
