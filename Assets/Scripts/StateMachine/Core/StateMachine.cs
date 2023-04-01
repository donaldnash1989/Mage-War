using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    private State currentState;

    private Dictionary<Type, State> _states;

    public void Initialize(Dictionary<Type, State> states)
    {
        _states = states;
        currentState = _states.First().Value;
    }

    public void Update()
    {
        Type nextState = currentState.Tick();

        if (nextState != null)
        {
            currentState = _states[nextState];
            currentState.Init();
        }
    }
}
