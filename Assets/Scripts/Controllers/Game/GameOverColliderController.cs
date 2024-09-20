using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverColliderController : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;

    void OnTriggerEnter(Collider collider) {
        if (collider.tag == Constants.playerTag) {
            GlobalConfig.SetGameOver(true);
            gameOverScreen.SetActive(true);
            collider.gameObject.SetActive(false);
            // GlobalConfig.UpdateHighScore(ScoreTextController.instance.GetScore());
            // GlobalConfig.UpdateTotalCoins(GlobalConfig.GetTotalCoins() + CoinsTextController.instance.GetCoins());
            // Scene currentScene = SceneManager.GetActiveScene();
            // SceneManager.LoadScene(currentScene.name);
        }
    }
}
