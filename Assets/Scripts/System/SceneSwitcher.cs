using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void Awake()
    {
        MainMenuButtonHandler.OnSceneSwitchRequestEvent += SceneSwitchEventHandler;
        TempGameOverButton.OnSceneSwitchRequestEvent += SceneSwitchEventHandler;
    }
    
    public void SceneSwitchEventHandler(string sceneName)
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene(), UnloadSceneOptions.None).completed += operation => {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive).completed += operation =>
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
            };
        };
    }

    public void OnDestroy()
    {
        MainMenuButtonHandler.OnSceneSwitchRequestEvent -= SceneSwitchEventHandler;
        TempGameOverButton.OnSceneSwitchRequestEvent -= SceneSwitchEventHandler;
    }
}
