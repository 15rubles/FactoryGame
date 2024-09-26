using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverColliderController : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject colliderForPause;
    [SerializeField] GameObject tutorialText;

    void OnTriggerEnter(Collider collider) {
        if (collider.tag == Constants.playerTag) {
            GlobalConfig.SetGameOver(true);
            gameOverScreen.SetActive(true);
            collider.gameObject.SetActive(false);
            colliderForPause.SetActive(true);
            tutorialText.SetActive(false);
        }
    }
}
