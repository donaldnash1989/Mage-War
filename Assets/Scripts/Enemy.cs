using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : StateMachine
{
    public static event Action OnDeathEvent;

    private Dictionary<Type, State> _states = new Dictionary<Type, State>();
    public void Awake()
    {
        _states = new Dictionary<Type, State> {
            { typeof(WanderState), new WanderState(GetComponent<IWanderBehaviour>(), GetComponent<ITargetSearch>()) },
            { typeof(ChaseState), new ChaseState(GetComponent<IChaseBehaviour>()) },
            { typeof(AttackState), new AttackState(GetComponent<IAttackBehaviour>()) }
        };

        Initialize(_states);
    }

    public void OnDestroy()
    {
        OnDeathEvent?.Invoke();
    }
}
