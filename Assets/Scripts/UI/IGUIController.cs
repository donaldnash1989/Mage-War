using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGUIController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverMenu;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) gameOverMenu.SetActive(!gameOverMenu.activeSelf);
    }
}
