using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContinueCountDownController : MonoBehaviour
{

    [SerializeField] TMP_Text countdownText;
    [SerializeField] TMP_Text countdownShadowText;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject colliderForPause;
    [SerializeField] int countdownFrom = 3;

    public void ActivateCountDown()
    {
        countdownShadowText.gameObject.SetActive(true);
        countdownText.text = countdownFrom.ToString();
        countdownShadowText.text = countdownFrom.ToString();
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        int counter = countdownFrom;
        while (counter > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            counter--;
            countdownText.text = counter.ToString();
            countdownShadowText.text = counter.ToString();
        }
        Time.timeScale = 1;
        GlobalConfig.SetOnPause(false);
        pauseButton.SetActive(true);
        countdownShadowText.gameObject.SetActive(false);
        gameObject.SetActive(false);
        colliderForPause.SetActive(false);
    }
}
