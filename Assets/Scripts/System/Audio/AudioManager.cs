using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public void Awake()
    {
        AudioPlayer.OnPlayMusicEvent += OnPlayMusicEventHandler;
    }

    private void OnPlayMusicEventHandler(AudioClip musicClip)
    {
        audioSource.Stop();
        audioSource.clip = musicClip;
        audioSource.Play();
    }

    public void OnDestroy()
    {
        AudioPlayer.OnPlayMusicEvent -= OnPlayMusicEventHandler;
    }
}
