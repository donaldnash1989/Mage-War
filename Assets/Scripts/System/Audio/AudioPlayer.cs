using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioPlayer
{
    public static event Action<AudioClip> OnPlayMusicEvent;
    public static void PlayMusic(AudioClip musicClip)
    {
        OnPlayMusicEvent?.Invoke(musicClip);
    }
}
