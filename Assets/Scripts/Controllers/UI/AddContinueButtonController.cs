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
    [SerializeField] AdsManagerController adsManagerController;

    void Update()
    {
        int adsState = PlayerPrefs.GetInt(Constants.adsRewardVariableKey, 0);
        if (PlayerPrefs.GetInt(Constants.adsRewardVariableKey, 0) == (int)RewardedAdsState.Granted)
        {
            PlayerPrefs.SetInt(Constants.adsRewardVariableKey, (int)RewardedAdsState.NotGranted);
            AddContinue();
        }
        else if (PlayerPrefs.GetInt(Constants.adsRewardVariableKey, 0) == (int)RewardedAdsState.Failed)
        {
            PlayerPrefs.SetInt(Constants.adsRewardVariableKey, (int)RewardedAdsState.NotGranted);
            gameObject.SetActive(false);
        }
    }

    public void PlayAds()
    {
        adsManagerController.ShowRewarded();
    }

    void AddContinue()
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
