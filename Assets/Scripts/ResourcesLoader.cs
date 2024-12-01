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
    public static Material[] slotsMats;
    
    // skin assets
    public static Texture2D defTxt;
    public static Material fishMat;
    public static Texture2D[] skinsTxts; 

	void Awake()
    {
        // load skins slot textures
        skinCount = new int[3];
        skinsSlotTxts = new Texture2D[3][];
        LoadSkinsSlotTxts("Common", 0);
        LoadSkinsSlotTxts("Rare", 1);
        LoadSkinsSlotTxts("Epic", 2);
        
        // load worms slot textures
        wormsSlotTxts = new Texture2D[8];
        LoadWormsSlotTxts();
        
        // load slots mats
        slotsMats = new Material[8];
        LoadMaterials();
        
        // load fish mat
        fishMat = Resources.Load<Material>("Pet Fish/petfishmat");
        
        // load skin textures
        defTxt = Resources.Load<Texture2D>("Pet Fish/def");
        LoadSkinsTxts();
    }

	public static void KYSAwake() // TODO: remove
    {
        // load skins slot textures
        skinCount = new int[3];
        skinsSlotTxts = new Texture2D[3][];
        LoadSkinsSlotTxts("Common", 0);
        LoadSkinsSlotTxts("Rare", 1);
        LoadSkinsSlotTxts("Epic", 2);
        
        // load worms slot textures
        wormsSlotTxts = new Texture2D[8];
        LoadWormsSlotTxts();
        
        // load slots mats
        slotsMats = new Material[8];
        LoadMaterials();
        
        // load fish mat
        fishMat = Resources.Load<Material>("Pet Fish/petfishmat");
        
        // load skin textures
        defTxt = Resources.Load<Texture2D>("Pet Fish/def");
        LoadSkinsTxts();
    }
    
    /**
     * Helper method to load skin slot textures from /Resources
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
     * Helper method to load worm slot textures from /Resources
     */
    private static void LoadWormsSlotTxts()
    {
        for (int x = 0; x < 8; x++)
        {
            wormsSlotTxts[x] = Resources.Load<Texture2D>("Slot Machine/textures/Worms/worm" + (x + 1) + "slottxt");
        }
    }
    
    /**
     * Helper method to load materials from /Resources
     */
    private static void LoadMaterials()
    {
        for (int x = 0; x < 8; x++)
        {
            slotsMats[x] = Resources.Load<Material>("Slot Machine/materials/gacha" + (x + 1) + "mat");
        }
    }
    
    /**
     * Helper method to load skin textures from /Resources
     */
    private static void LoadSkinsTxts()
    {
        string[] files = Directory.GetFiles("Assets/Resources/Pet Fish/Textures", "*", SearchOption.TopDirectoryOnly);
        int txtCount = files.Length / 2;
        skinsTxts = new Texture2D[txtCount];

        string fileName;
        for (int x = 0; x < txtCount; x++)
        {
            fileName = Path.GetFileName(files[x * 2]);
            skinsTxts[x] = Resources.Load<Texture2D>("Pet Fish/Textures/" + fileName.Substring(0,fileName.Length - 4));
        }
    }
}
