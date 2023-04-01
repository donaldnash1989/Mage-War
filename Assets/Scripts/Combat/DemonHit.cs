using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonHit : MonoBehaviour, IHittable
{
    public void Hit(Hit hit)
    {
        Destroy(gameObject);
    }
}
