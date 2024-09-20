using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButtonController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] ContinueCountDownController continueCountDown;

    public void ContinueGameGame()
    {
        continueCountDown.gameObject.SetActive(true);
        continueCountDown.ActivateCountDown();
        pauseMenu.SetActive(false);
    }
}
