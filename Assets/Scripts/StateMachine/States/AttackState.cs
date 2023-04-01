using System;
using UnityEngine;

public class AttackState : State
{
    private IAttackBehaviour _attackBehaviour;

    public AttackState(IAttackBehaviour attackBehaviour)
    {
        _attackBehaviour = attackBehaviour;
    }

    public override Type Tick()
    {
        TargetState targetState = _attackBehaviour.Attack(_target);

        if (targetState == TargetState.CANT_SEE) return typeof(WanderState);

        return null;
    }

    internal override void Init()
    {

    }
}
