using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public static event Action OnImpact; 

    private float timer = 0.0f;
    private float lifeTime = 0.3f;
    [SerializeField] private AudioClip soundEffect;
    [SerializeField] private AudioSource soundSource;

    public void Start()
    {
        soundSource.clip = soundEffect;
        soundSource.Play();
        OnImpact?.Invoke();
    }

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime) Destroy(gameObject);
    }
}
