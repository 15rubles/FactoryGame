using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButtonController : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject playScreen;
    [SerializeField] SpawnerController spawnerController;

    public void StartGame()
    {
        Time.timeScale = 1;
        mainMenu.SetActive(false);
        playScreen.SetActive(true);
        spawnerController.gameObject.SetActive(true);
    }
}
