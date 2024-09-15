using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPiecesSpeedController : MonoBehaviour
{

    [SerializeField] Transform topMiddlePoint;
    [SerializeField] Transform bottomMiddlePoint;
    [SerializeField] PlayerController playerController;
    [SerializeField] float topSpeedMultiplier = 1.2f, bottomSpeedMultiplier = 0.7f, secondToLerpToTarget = 0.3f;


    Transform playerTransform;

    void Start()
    {
        playerTransform = playerController.gameObject.transform;
    }

    void Update()
    {
        if (playerTransform.position.z > topMiddlePoint.position.z)
        {
            ChangeSpeedMultiplier(topSpeedMultiplier);
        }
        else if (playerTransform.position.z < bottomMiddlePoint.position.z)
        {
            ChangeSpeedMultiplier(bottomSpeedMultiplier);
        }
        else
        {
            ChangeSpeedMultiplier(1);
        }
    }

    void ChangeSpeedMultiplier(float targetMultiplier)
    {
        float speedMultiplier = GlobalConfig.GetSpeedMultiplier();
        speedMultiplier = Mathf.Lerp(speedMultiplier, targetMultiplier, 1 / secondToLerpToTarget * Time.deltaTime);
        if (Mathf.Abs(targetMultiplier - speedMultiplier) < 0.01f)
        {
            GlobalConfig.SetSpeedMultiplier(targetMultiplier);
        }
        else
        {
            GlobalConfig.SetSpeedMultiplier(speedMultiplier);
        }
    }
}
