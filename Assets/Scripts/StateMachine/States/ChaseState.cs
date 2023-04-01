using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    private IChaseBehaviour _chaseBehaviour;

    public ChaseState(IChaseBehaviour chaseBehaviour) : base( )
    {
        _chaseBehaviour = chaseBehaviour;
    }

    public override Type Tick()
    {
        TargetState targetState = _chaseBehaviour.Chase(_target);

        if(targetState == TargetState.CANT_SEE)
        {
            return typeof(WanderState);
        }

        if(targetState == TargetState.CAN_ATTACK)
        {
            return typeof(AttackState);
        }

        return null;
    }

    internal override void Init()
    {

    }
}

public enum TargetState
{
    CANT_SEE,
    CAN_SEE,
    CAN_ATTACK
}
