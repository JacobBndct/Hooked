using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Fish/Actions/LostInterest")]
public class FishLostInterest : EntityStateAction
{
    public override void PerformAction(Entity entityReference)
    {
        FishController fish = ((FishController)entityReference);
        if (fish != null)
        {
            fish.IsInterested = false;
        }
    }
}
