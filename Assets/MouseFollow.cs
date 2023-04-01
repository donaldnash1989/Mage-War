using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MouseFollow : MonoBehaviour
{
    private Vector3 lastWorldPosition;
    [SerializeField] private VisualEffect particles;

    public void Update()
    {
        if(Camera.main != null)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = -5.0f;
            if (Vector3.Distance(position, lastWorldPosition) < 0.1f)
            {
                particles.Stop();
            }
            else
            {
                particles.Play();
            }
            transform.position = position;
            lastWorldPosition = position;
        }
    }
}
