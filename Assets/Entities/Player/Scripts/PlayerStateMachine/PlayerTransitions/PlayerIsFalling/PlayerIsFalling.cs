using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Player/Transitions/IsFalling")]
public class PlayerIsFalling : EntityStateTransitions
{
    PlayerCharacter _player;

    // evaluates the condition for falling
    public override bool EvaluateCondition()
    {
        return !_player.isJumping && !_player.isGrounded && !_player.isGrappling;
    }

    // evaluates the player transition's condition to see which state to return
    public override EntityState EvaluateState(Entity entity)
    {
        _player = (PlayerCharacter)entity;
        return EvaluateCondition() ? TrueState : FalseState;
    }
}