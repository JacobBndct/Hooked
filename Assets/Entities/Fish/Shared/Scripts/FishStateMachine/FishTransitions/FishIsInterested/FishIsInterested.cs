using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Fish/Transitions/IsInterested")]
public class FishIsInterested : EntityStateTransitions
{
    FishController _fish;

    // evaluates the condition for grappling
    public override bool EvaluateCondition()
    {
        return _fish.IsInterested;
    }

    // evaluates the player transition's condition to see which state to return
    public override EntityState EvaluateState(Entity entity)
    {
        _fish = (FishController)entity;
        return EvaluateCondition() ? TrueState : FalseState;
    }
}
