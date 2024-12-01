/**
 * Class which implements the slot machine logic.
 * 
 * @author Marina (Mars) Semenova
 */

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SlotMachineController : MonoBehaviour
{
    // slot textures
    private static Texture2D[][] skins = ResourcesLoader.skinsSlotTxts; 
    private static int[] skinCount = ResourcesLoader.skinCount;
    private static Texture2D[] worms = ResourcesLoader.wormsSlotTxts;
    private static Material[] slots = ResourcesLoader.slotsMats; 
    
    // skin drop rates
    private const float C_RATE = 5f;
    private const float R_RATE = 2f;
    private const float E_RATE = 0.1f;
    private const float SKIN_RATE = C_RATE + R_RATE + E_RATE;
    
    // refs
    private Animator animator;
    private AudioSource[] audSrcs;
    
    private bool spinning = false;
    
    void Awake()
    { 
        skins = ResourcesLoader.skinsSlotTxts; // TODO: remove
        skinCount = ResourcesLoader.skinCount; // TODO: remove
        worms = ResourcesLoader.wormsSlotTxts; // TODO: remove
        slots = ResourcesLoader.slotsMats; // TODO: remove
        // get components
        animator = GetComponent<Animator>();
        audSrcs = GetComponents<AudioSource>();
    }
    
    /**
     * Method which randomly assigns textures to the slot materials for every spin.
     */
    private void AssignTextures()
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
                
                skinPos = Random.Range(0, skinCount[rarity]);
                slots[x].SetTexture("_MainTex", skins[rarity][skinPos]);
            }
            else
            {
                worm = Random.Range(0, 8);
                slots[x].SetTexture("_MainTex", worms[worm]);
            }
        }
    }
    
    /**
     * Method which implements the logic for when the slots wheel spins.
     */
    public void Spin()
    {
        if (!spinning)
        {
            spinning = true;
            bool enoughWorms = true; // TODO: implement check

            if (enoughWorms)
            {
                // TODO: decrement worms
                audSrcs[0].Play();
                animator.enabled = true;
                AssignTextures();
            }
            else
            {
                audSrcs[1].Play();
            }
        }
    }
    
    /**
     * Method which implements the logic for when the slots wheel lands on a prize.
     */
    public void Landed()
    {
        animator.enabled = false;
        animator.Rebind();
        animator.Update(0f);
        
        string prize =  slots[0].GetTexture("_MainTex").name;
        prize = prize.Substring(0, prize.Length - 7);
        
        if (prize.Substring(0, 4) == "worm") // increment worms
        {
            // TODO: increment worm
        }
        else // unlock skin 
        {
            bool dupe = PlayerSkins.UnlockSkin(prize);

            if (dupe) // if won dupe skin get some worms back
            {
                // TODO: increment worms
            }
        }
        
        spinning = false;
    }
}
