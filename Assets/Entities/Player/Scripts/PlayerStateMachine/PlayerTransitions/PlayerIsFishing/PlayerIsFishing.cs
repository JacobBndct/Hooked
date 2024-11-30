using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Player/Transitions/IsFishing")]
public class PlayerIsFishing : EntityStateTransitions
{
    PlayerCharacter _player;

    // evaluates the condition for grappling
    public override bool EvaluateCondition()
    {
        return _player.IsFishing;
    }

    // evaluates the player transition's condition to see which state to return
    public override EntityState EvaluateState(Entity entity)
    {
        _player = (PlayerCharacter)entity;
        return EvaluateCondition() ? TrueState : FalseState;
    }
}