using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonHandler : MonoBehaviour
{
    public static event Action<string> OnSceneSwitchRequestEvent;

    public void Awake()
    {
        MainMenuButtons.OnPlayButtonClickedEvent += OnPlayButtonClickedEventHandler;
        MainMenuButtons.OnSettingsButtonClickedEvent += OnSettingsButtonClickedEventHandler;
        MainMenuButtons.OnQuitButtonClickedEvent += OnQuitButtonClickedEventHandler;
    }

    public void OnPlayButtonClickedEventHandler()
    {
        OnSceneSwitchRequestEvent?.Invoke("dev_scene");
    }

    public void OnSettingsButtonClickedEventHandler()
    {

    }

    public void OnQuitButtonClickedEventHandler()
    {
        Application.Quit();
    }

    public void OnDestroy()
    {

    }
}
