using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Fish/Actions/ApproachHook")]
public class ApproachHook : EntityStateAction
{
    public override void PerformAction(Entity entityReference)
    {
        FishController fish = ((FishController)entityReference);
        if (fish != null)
        {
            // set location to be someplace in opposite direction of player
            fish.SetTargetLocation(PlayerCharacter.Instance.HookPosition);
        }
    }
}
