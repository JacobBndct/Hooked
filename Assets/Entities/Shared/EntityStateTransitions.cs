using UnityEngine;

public abstract class EntityStateTransitions : ScriptableObject, ITransitionCondition
{
    // conditional transition states
    public EntityState TrueState { get { return (trueState != null) ? Instantiate(trueState) : null; } }
    [SerializeField] private EntityState trueState = null;

    public EntityState FalseState { get { return (falseState != null) ? Instantiate(falseState) : null; } }
    [SerializeField] private EntityState falseState = null;

    // abstract entity  transition functions
    public abstract bool EvaluateCondition();
    public abstract EntityState EvaluateState(Entity entityReference);
}
