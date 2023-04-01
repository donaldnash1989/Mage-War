using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected static Transform _target;

    public State( ) { }

    public abstract Type Tick();

    internal abstract void Init();
}
