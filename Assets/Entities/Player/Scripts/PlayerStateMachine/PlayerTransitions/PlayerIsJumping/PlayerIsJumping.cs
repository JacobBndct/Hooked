using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Player/Transitions/IsJumping")]
public class PlayerIsJumping : EntityStateTransitions
{
    PlayerCharacter _player;

    // evaluates the condition for jumping
    public override bool EvaluateCondition()
    {
        return _player.isJumping;
    }

    // evaluates the player transition's condition to see which state to return
    public override EntityState EvaluateState(Entity entity)
    {
        _player = (PlayerCharacter)entity;
        return EvaluateCondition() ? TrueState : FalseState;
    }
}

