using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Player/Transitions/IsMoving")]
public class PlayerIsMoving : EntityStateTransitions
{
    Entity _entity;

    // returns true if the given entity is on the ground and is moving
    public override bool EvaluateCondition()
    {
        return _entity.isGrounded && _entity.isMoving;
    }

    // return a given state if the condition is true and a different state if not
    public override EntityState EvaluateState(Entity entity)
    {
        _entity = entity;
        return EvaluateCondition() ? TrueState : FalseState;
    }
}

