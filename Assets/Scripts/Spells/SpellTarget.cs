using UnityEngine;

public class SpellTarget : MonoBehaviour, ISpellTarget
{
    [SerializeField] private LayerMask _hitMask;
    [SerializeField] private LayerMask _ignore;

    public LayerMask HitMask()
    {
        return _hitMask;
    }

    public LayerMask IgnoreMask()
    {
        return ~_ignore;
    }
}
