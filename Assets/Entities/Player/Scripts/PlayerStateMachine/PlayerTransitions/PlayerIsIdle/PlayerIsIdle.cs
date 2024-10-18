using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Player/Transitions/IsIdle")]
public class PlayerIsIdle : EntityStateTransitions
{
    PlayerCharacter _player;

    // evaluates the condition for being idle
    public override bool EvaluateCondition()
    {
        return _player.isGrounded && !_player.isMoving && !_player.isGrappling && !_player.isJumping;
    }

    // evaluates the player transition's condition to see which state to return
    public override EntityState EvaluateState(Entity entity)
    {
        _player = (PlayerCharacter)entity;
        return EvaluateCondition() ? TrueState : FalseState;
    }
}
