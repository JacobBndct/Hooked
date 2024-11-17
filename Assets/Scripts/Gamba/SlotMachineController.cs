using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SlotMachineController : MonoBehaviour
{
    private static Texture2D[][] skins; // blue
    private static Texture2D winSkin;
    private static KeyValuePair<int, int>[] curSkinInds; // blue
    private static int[] skinCount;
    private static Material[] wheelSlots; // blue by def
    
    // rates
    private const float C_RATE = 1.5f;
    private const float R_RATE = 1.5f;
    private const float E_RATE = 1.5f;
    private const float W_RATE = 1.5f;
    
    void Awake()
    {
        // load textures
        winSkin = Resources.Load<Texture2D>("Slot Machine/textures/winfishtxt");
        skinCount = new int[3];
        skins = new Texture2D[3][];
        LoadTextures("Common", 0);
        LoadTextures("Rare", 1);
        LoadTextures("Epic", 2);
        
        // load + assign textures to mats
        wheelSlots = new Material[8];
        curSkinInds = new KeyValuePair<int, int>[8];
        LoadMaterials();
        AssignTextures();
    }

    void Start()
    {
        // check rotation of wheel +/- diff mod 360 == 0
        // if T assign new textures
        // add a check for overflow where if rot large reset to init amt
    }
    
    /**
     * Helper method to load textures from /Resources
     *
     * @param rarityStr - Name of the rarity, used to access the correct folder.
     * @param rarity - Index of rarity in arrays.
     */
    private static void LoadTextures(string rarityStr, int rarity)
    {
        skinCount[rarity] = Directory.GetFiles("Assets/Resources/Slot Machine/textures/" + rarityStr, "*", SearchOption.TopDirectoryOnly).Length / 2;
        skins[rarity] = new Texture2D[skinCount[rarity]];
        
        for (int x = 0; x < skinCount[rarity]; x++)
        {
            skins[rarity][x] = Resources.Load<Texture2D>("Slot Machine/textures/" + rarityStr + "/" + rarityStr.Substring(0,1).ToLower() + "gacha" + (x + 1) + "txt");
        }
    }
    
    /**
     * Helper method to load materials from /Resources
     */
    private static void LoadMaterials()
    {
        for (int x = 0; x < 8; x++)
        {
            wheelSlots[x] = Resources.Load<Material>("Slot Machine/materials/gacha" + (x + 1) + "mat");
        }
    }

    /**
     * Randomly assign textures to the 8 wheel slot materials
     */
    private void AssignTextures()
    {
        int rng;
        int rarity;
        
        for (int x = 0; x < 8; x++)
        {
            rarity = 0; // TODO: rand 0, 1, 2, or 3 (win skin) based on rates
            rng = x; // TODO: rand between 0 n skinCount[rarity] - 1
            wheelSlots[x].SetTexture("_MainTex", skins[rarity][rng]);
            curSkinInds[x] = new KeyValuePair <int, int>(rarity, rng);
        }
    }
}
