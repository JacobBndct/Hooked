using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SlotMachineController : MonoBehaviour
{
    private static Texture2D[] skins; // blue
    private static int[] curSkinInds; // blue
    private static int skinCount;
    private static Material[] wheelSlots; // blue by def
    
    void Awake()
    {
        // load textures
        skinCount = Directory.GetFiles("Assets/Resources/Slot Machine/textures", "*", SearchOption.TopDirectoryOnly).Length / 2;
        skins = new Texture2D[skinCount];
        LoadTextures();
        
        // load + assign textures to mats
        wheelSlots = new Material[8];
        curSkinInds = new int[8];
        LoadMaterials();
        AssignTextures();
    }

    /**
     * Helper method to load textures from /Resources
     */
    private void LoadTextures()
    {
        for (int x = 0; x < skinCount; x++)
        {
            skins[x] = Resources.Load<Texture2D>("Slot Machine/textures/gacha" + (x + 1) + "txt");
        }
    }
    
    /**
     * Helper method to load materials from /Resources
     */
    private void LoadMaterials()
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
        
        for (int x = 0; x < 8; x++)
        {
            rng = x; // TODO: rand
            wheelSlots[x].SetTexture("_MainTex", skins[rng]);
            curSkinInds[x] = rng;
        }
    }
}
