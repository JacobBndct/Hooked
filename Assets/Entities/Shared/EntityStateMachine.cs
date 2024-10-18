using UnityEngine;
using Patterns.StateMachine;

public class EntityStateMachine : IStateMachine<EntityState>
{
    // base entity state machine variables
    protected Entity _entity;
    public EntityState _currentState { get; private set; }

    // base entity state machine constructor
    public EntityStateMachine(EntityState startState, Entity entity)
    {
        // set the starting state and entity for the state machine
        _entity = entity;
        SetState(startState);
    }

    // a function which sets the state of the state machine and calls the exit and enter functions of the respective states
    public void SetState(EntityState newState)
    {
        _currentState?.ExitState();
        _currentState = newState;
        _currentState.SetEntity(_entity);
        _currentState?.EnterState();
    }

    // call the update function of the current states
    public void Update()
    {
        _currentState?.Update();
    }

    // call the fixed update function of the current states
    public void FixedUpdate()
    {
        _currentState?.FixedUpdate();
    }

    // call the lae update function of the current states
    public void LateUpdate()
    {
        _currentState?.LateUpdate();

        // check to see if any transition conditions are meet
        EntityState transitionState = (EntityState)_currentState?.TryTransitions();
        if (transitionState != null)
        {
            SetState(transitionState);
        }
    }
}
