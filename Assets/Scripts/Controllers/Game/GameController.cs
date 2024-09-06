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
    }

    void LateUpdate()
    {
        GlobalConfig.IncreaseSpeed(increaseByPerMinute * Time.deltaTime / 60);
    }
}
