using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void Awake()
    {
        Application.targetFrameRate = 60;
    }
    private void Start()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }
}
