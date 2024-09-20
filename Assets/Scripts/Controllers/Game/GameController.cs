using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] float increaseByPerMinute = 0.5f;
    [SerializeField] float initialSpeed = 3;

    void Awake() {
        GlobalConfig.SetSpeed(initialSpeed);
        GlobalConfig.SetInitialSpeed(initialSpeed);
        Time.timeScale = 0;
    }

    void LateUpdate()
    {
        if (Time.timeScale > 0 && !GlobalConfig.GetGameOver())
        {
            GlobalConfig.IncreaseSpeed(increaseByPerMinute * Time.deltaTime / 60);
        }
    }
}
