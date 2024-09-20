using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWheelController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] float wheelRotationSpeed;
    void Update()
    {


        if (Time.timeScale > 0)
        {
            if (playerController.IsMovingForward())
            {
                transform.Rotate(0, 0, wheelRotationSpeed * Time.deltaTime * GlobalConfig.GetSpeedMultiplied());
            }
            else
            {
                transform.Rotate(0, 0, -wheelRotationSpeed * Time.deltaTime * GlobalConfig.GetSpeedMultiplied());
            }
        }
        else if (!GlobalConfig.GetOnPause())
        {
            transform.Rotate(0, 0, wheelRotationSpeed * Time.unscaledDeltaTime * GlobalConfig.GetSpeedMultiplied());
        }

    }
}
