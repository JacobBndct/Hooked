using System;
using System.Collections.Generic;

namespace Patterns.StateMachine
{
    public interface IStateMachine<T> where T : IState
    {
        void SetState(T newState);
    }
}
