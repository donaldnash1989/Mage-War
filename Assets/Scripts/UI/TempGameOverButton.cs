using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempGameOverButton : MonoBehaviour
{
    public static event Action<string> OnSceneSwitchRequestEvent;
    public void OnMainMenuClicked()
    {
        OnSceneSwitchRequestEvent?.Invoke("main_menu");
    }
}
