using UnityEngine;

public interface ISpellTarget
{
    public LayerMask IgnoreMask();
    public LayerMask HitMask();
}
