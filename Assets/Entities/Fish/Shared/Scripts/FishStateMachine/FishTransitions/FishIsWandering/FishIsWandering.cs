using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Fish/Transitions/IsWandering")]
public class FishIsWandering : EntityStateTransitions
{
    FishController _fish;

    // evaluates the condition for grappling
    public override bool EvaluateCondition()
    {
        float distance = Vector3.Distance(PlayerCharacter.Instance.transform.position, _fish.transform.position);
        bool isInRange = distance < _fish.GetFishData().AvoidanceRadius;

        bool isScared = isInRange && PlayerCharacter.Instance.IsMoving;

        return !isScared && !_fish.IsInterested;
    }

    // evaluates the player transition's condition to see which state to return
    public override EntityState EvaluateState(Entity entity)
    {
        _fish = (FishController)entity;
        return EvaluateCondition() ? TrueState : FalseState;
    }
}
