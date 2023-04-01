using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : State
{
    private IWanderBehaviour _wanderBehaviour;
    private ITargetSearch _targetSearch;

    public WanderState(IWanderBehaviour wanderBehaviour, ITargetSearch targetSearch) : base( ) {
        _wanderBehaviour = wanderBehaviour;
        _targetSearch = targetSearch;
    }

    public override Type Tick()
    {
        _wanderBehaviour.Wander();

        if (_targetSearch != null)
        {
            Transform target = _targetSearch.FindTarget();

            if (target != null)
            {
                _target = target;
                return typeof(ChaseState);
            }
        }
        
        return null;
    }

    internal override void Init()
    {
        _wanderBehaviour.Init(_target);
    }
}
