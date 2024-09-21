using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject colliderForPause;

    public void PauseGame()
    {
        Time.timeScale = 0;
        GlobalConfig.SetOnPause(true);
        colliderForPause.SetActive(true);
        pauseMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
