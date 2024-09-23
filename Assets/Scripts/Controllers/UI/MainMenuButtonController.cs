using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonController : MonoBehaviour
{
    public void GoToMainMenu()
    {
        GlobalConfig.SetOnPause(false);
        GlobalConfig.SetGameOver(false);
        GlobalConfig.UpdateHighScore(ScoreTextController.instance.GetScore());
        GlobalConfig.UpdateTotalCoins(CoinsTextController.instance.GetCoins());
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
