using System;

namespace Patterns.StateMachine
{
    public interface IState
    {
        void EnterState();
        void ExitState();
        IState TryTransitions();
    }
}
