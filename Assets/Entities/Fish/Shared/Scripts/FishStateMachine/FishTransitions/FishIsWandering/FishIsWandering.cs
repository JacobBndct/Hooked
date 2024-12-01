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
        float playerDistance = Vector3.Distance(PlayerCharacter.Instance.transform.position, _fish.transform.position);

        bool isInAvoidanceRange = playerDistance < _fish.GetFishData().AvoidanceRadius;
        bool isScared = isInAvoidanceRange && PlayerCharacter.Instance.IsMoving;

        return !isScared && !_fish.IsInterested;
    }

    // evaluates the player transition's condition to see which state to return
    public override EntityState EvaluateState(Entity entity)
    {
        _fish = (FishController)entity;
        return EvaluateCondition() ? TrueState : FalseState;
    }
}
