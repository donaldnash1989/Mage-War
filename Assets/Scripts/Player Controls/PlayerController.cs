using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private ILookAt _lookAt;

    private IMovement _movement;

    private ISpellCast _spellCast;

    public void Start()
    {
        _lookAt = GetComponent<ILookAt>();

        _movement = GetComponent<IMovement>();

        _spellCast = GetComponent<ISpellCast>();
    }

    public void Update()
    {
        _lookAt.Look(transform);

        _movement.Move(transform);

        _spellCast.Cast();
    }
}
