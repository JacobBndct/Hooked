using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Fish/Transitions/IsNibbling")]
public class FishIsNibbling : EntityStateTransitions
{
    FishController _fish;

    // evaluates the condition for grappling
    public override bool EvaluateCondition()
    {
        float dist = Vector3.Distance(_fish.transform.position, PlayerCharacter.Instance.HookPosition);
        return dist < _fish.GetFishData().CaptureDistance;
    }

    // evaluates the player transition's condition to see which state to return
    public override EntityState EvaluateState(Entity entity)
    {
        _fish = (FishController)entity;
        return EvaluateCondition() ? TrueState : FalseState;
    }
}