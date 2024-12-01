using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Fish/Actions/CatchFish")]
public class CatchFish : EntityStateAction
{
    public override void PerformAction(Entity entityReference)
    {
        Debug.Log("Capture");
        FishController fish = ((FishController)entityReference);
        if (fish != null)
        {
            if (PlayerCharacter.Instance.IsPullingFish)
            {
                PlayerManager.Instance.playerData.money += fish.GetFishData().SellingValue;

                Destroy(fish.gameObject);
            }
        }
    }
}
