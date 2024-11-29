using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SlotMachineController : MonoBehaviour
{
    private static Texture2D[][] skins; // blue
    private static KeyValuePair<int, int>[] curSkinInds; // blue
    private static int[] skinCount;
    private static Material[] slots; // blue by def
    private static Texture2D[] worms; 
    
    // rates
    private const float C_RATE = 1.5f;
    private const float R_RATE = 25f;
    private const float E_RATE = 1.5f;
    private const float SKIN_RATE = C_RATE + R_RATE + E_RATE;
    
    void Awake()
    {
        // load skins
        skinCount = new int[3];
        skins = new Texture2D[3][];
        LoadSkins("Common", 0);
        LoadSkins("Rare", 1);
        
        
        // load worms
        
        worms = new Texture2D[8];
        LoadWorms();
        
        // load + assign textures to mats
        slots = new Material[8];
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
    private static void LoadSkins(string rarityStr, int rarity)
    {
        string[] files = Directory.GetFiles("Assets/Resources/Slot Machine/textures/Skins/" + rarityStr, "*", SearchOption.TopDirectoryOnly);
        skinCount[rarity] = files.Length / 2;
        skins[rarity] = new Texture2D[skinCount[rarity]];

        string fileName;
        for (int x = 0; x < skinCount[rarity]; x++)
        {
            fileName = Path.GetFileName(files[x * 2]);
            skins[rarity][x] = Resources.Load<Texture2D>("Slot Machine/textures/Skins/" + rarityStr + "/" + fileName.Substring(0,fileName.Length - 4));
        }
    }

    /**
     * 
     */
    private static void LoadWorms()
    {
        for (int x = 0; x < 8; x++)
        {
            worms[x] = Resources.Load<Texture2D>("Slot Machine/textures/Worms/worm" + (x + 1) + "slottxt");
        }
    }
    
    /**
     * Helper method to load materials from /Resources
     */
    private static void LoadMaterials()
    {
        for (int x = 0; x < 8; x++)
        {
            slots[x] = Resources.Load<Material>("Slot Machine/materials/gacha" + (x + 1) + "mat");
        }
    }

    /**
     * Randomly assign textures to the 8 wheel slot materials
     */
    private static void AssignTextures()
    {
        float rng;
        int rarity, skinPos, worm;
        
        for (int x = 0; x < 8; x++)
        {
            rng = Random.Range(0f, 100.0f);
            if (rng <= SKIN_RATE)
            {
                if (rng <= E_RATE)
                {
                    rarity = 2;
                }
                else if (rng <= R_RATE) 
                {
                    rarity = 1;
                }
                else 
                {
                    rarity = 0;
                }
                rarity = 1; // TODO: temp

                bool dupe = true;
                while (dupe)
                {
                    skinPos = Random.Range(0, skinCount[rarity]);
                    KeyValuePair<int, int> curSkinInd = new KeyValuePair<int, int>(rarity, skinPos);
                    if (!curSkinInds.Contains(curSkinInd))
                    {
                        slots[x].SetTexture("_MainTex", skins[rarity][skinPos]);
                        curSkinInds[x] = curSkinInd;
                        dupe = false;
                    }
                }
            }
            else
            {
                worm = Random.Range(0, 8);
                slots[x].SetTexture("_MainTex", worms[worm]);
            }
        }
    }
}
