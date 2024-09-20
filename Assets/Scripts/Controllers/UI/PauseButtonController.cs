using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void PauseGame()
    {
        Time.timeScale = 0;
        GlobalConfig.SetOnPause(true);
        pauseMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
