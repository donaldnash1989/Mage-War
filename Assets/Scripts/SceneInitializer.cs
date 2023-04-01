using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInitializer : MonoBehaviour
{
    public string sceneName;

    [SerializeField] private AudioClip musicClip;
    
    public void Awake()
    {
    }

    public void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        AudioPlayer.PlayMusic(musicClip);
    }


    public void Update()
    {
        
    }
}
