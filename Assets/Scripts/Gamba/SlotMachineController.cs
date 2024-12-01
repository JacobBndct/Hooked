/**
 * Class which implements the slot machine logic.
 * 
 * @author Marina (Mars) Semenova
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

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
            bool enoughWorms = PlayerManager.Instance.playerData.worms >= 160; 

            if (enoughWorms)
            {
                spinning = true;
                PlayerManager.Instance.playerData.worms -= 160; // decrement worms 
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
            int wormNum = Int32.Parse(prize.Substring(4, 1));

            if (wormNum == 1 || wormNum == 4) // regular worm
            {
                PlayerManager.Instance.playerData.worms += 1;
            } else if (wormNum == 2) // golden worm
            {
                PlayerManager.Instance.playerData.worms += 160;
            }
            else // special worm
            {
                PlayerManager.Instance.playerData.worms += 20;
            }
        }
        else // unlock skin 
        {
            bool dupe = PlayerSkins.UnlockSkin(prize);

            if (dupe) // if won dupe skin get some worms back
            {
                int rarity = PlayerSkins.GetSkin(prize).rarity;

                if (rarity == 2) // epic dupe
                {
                    PlayerManager.Instance.playerData.worms += 160;
                } else if (rarity == 1) // rare dupe
                {
                    PlayerManager.Instance.playerData.worms += 80;
                }
                else // common dupe
                {
                    PlayerManager.Instance.playerData.worms += 20;
                }
            }
        }
        
        spinning = false;
    }
}
