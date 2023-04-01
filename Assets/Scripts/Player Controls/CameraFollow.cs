using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform _transform;

    public void Start()
    {
        _transform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void LateUpdate()
    {
        if(_transform != null)
            transform.position = new Vector3(_transform.position.x, _transform.position.y, transform.position.z);
    }
}
