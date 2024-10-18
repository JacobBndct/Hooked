using System.Collections.Generic;
using UnityEngine;
using Patterns.StateMachine;

[CreateAssetMenu(menuName = "StateMachine/Entity/State")]
public class EntityState : ScriptableObject, IState
{
    // store a copy of specific entity
    protected Entity _entityReference;

    // public list of actions to perform when in this state
    public List<EntityStateAction> EnterStateActions;
    public List<EntityStateAction> ExitStateActions;
    public List<EntityStateAction> UpdateActions;
    public List<EntityStateAction> FixedUpdateActions;
    public List<EntityStateAction> LateUpdateActions;

    // list of transitions between states for an entity
    public List<EntityStateTransitions> EntityStateTransition;

    // set the entity reference of this scriptable object
    public void SetEntity(Entity entity)
    {
        _entityReference = entity;
    }

    // trigger an action for all registered actions in the EnterStateActions list
    public virtual void EnterState()
    {
        foreach (EntityStateAction action in EnterStateActions)
        {
            action.PerformAction(_entityReference);
        }
    }

    // trigger an action for all registered actions in the ExitStateActions list
    public virtual void ExitState()
    {
        foreach (EntityStateAction action in ExitStateActions)
        {
            action.PerformAction(_entityReference);
        }
    }

    // trigger an action for all registered actions in the UpdateActions list
    public virtual void Update() 
    {
        foreach (EntityStateAction action in UpdateActions)
        {
            action.PerformAction(_entityReference);
        }
    }

    // trigger an action for all registered actions in the FixedUpdateActions list
    public virtual void FixedUpdate() 
    {
        foreach (EntityStateAction action in FixedUpdateActions)
        {
            action.PerformAction(_entityReference);
        }
    }

    // trigger an action for all registered actions in the LateUpdateActions list
    public virtual void LateUpdate() 
    {
        foreach (EntityStateAction action in LateUpdateActions)
        {
            action.PerformAction(_entityReference);
        }
    }

    // check to see if any transition conditions have been meet and return a new state if so
    public IState TryTransitions()
    {
        foreach (EntityStateTransitions transition in EntityStateTransition)
        {
            IState transitionState = transition.EvaluateState(_entityReference);
            if (transitionState != null)
            {
                return transitionState;
            }
        }

        return null;
    }
}
