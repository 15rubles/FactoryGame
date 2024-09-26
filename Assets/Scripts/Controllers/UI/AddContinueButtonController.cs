using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddContinueButtonController : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;

    [SerializeField] GameObject colliderForPause;
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerSpawnPoint;
    [SerializeField] GameObject tutorialText;

    public void AddContinue()
    {
        GlobalConfig.SetGameOver(false);
        SpawnerController.instance.ResetSpawner();
        player.transform.position = playerSpawnPoint.transform.position;
        player.SetActive(true);
        gameObject.SetActive(false);
        gameOverScreen.SetActive(false);
        colliderForPause.SetActive(false);
        tutorialText.SetActive(true);
    }
}
