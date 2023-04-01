using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{
    public void Hit(Hit hit);
}

public struct Hit
{
    public int damage;
    public Vector2 knockbackDirection;
    public float knockbackForce;
}
