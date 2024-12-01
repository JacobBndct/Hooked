using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Fish/Transitions/OnTimerEnd")]
public class FishOnTimerEnd : EntityStateTransitions
{
    FishController _fish;

    // evaluates the condition for grappling
    public override bool EvaluateCondition()
    {
        return _fish.Timer.Ended;
    }

    // evaluates the player transition's condition to see which state to return
    public override EntityState EvaluateState(Entity entity)
    {
        _fish = (FishController)entity;
        return EvaluateCondition() ? TrueState : FalseState;
    }
}
