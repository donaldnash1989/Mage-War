using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    public static event Action OnPlayButtonClickedEvent;
    public static event Action OnSettingsButtonClickedEvent;
    public static event Action OnQuitButtonClickedEvent;

    public void OnPlayButtonClicked()
    {
        OnPlayButtonClickedEvent?.Invoke();
    }

    public void OnSettingsButtonClicked()
    {
        OnSettingsButtonClickedEvent?.Invoke();
    }

    public void OnQuitButtonClicked()
    {
        OnQuitButtonClickedEvent?.Invoke();
    }
}
