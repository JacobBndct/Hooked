using UnityEngine;

public abstract class EntityStateTransitions : ScriptableObject, ITransitionCondition
{
    // conditional transition states
    public EntityState TrueState = null;
    public EntityState FalseState = null;

    // abstract entity  transition functions
    public abstract bool EvaluateCondition();
    public abstract EntityState EvaluateState(Entity entityReference);
}
